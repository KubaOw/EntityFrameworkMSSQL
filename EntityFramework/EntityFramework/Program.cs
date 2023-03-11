using EntityFramework.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<Context>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("EFConnectionString"))
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<Context>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

var users = dbContext.Users.ToList();
if (!users.Any())
{
    var user1 = new User()
    {
        Email = "user1@.com",
        FullName = "User One",
        Adress = new Adress()
        {
            City = "Warszawa",
            Street = "Szeroka"
        }
    };

    var user2 = new User()
    {
        Email = "user2@.com",
        FullName = "User Two",
        Adress = new Adress()
        {
            City = "Kraków",
            Street = "Dluga"
        }
    };

    dbContext.Users.AddRange(user1, user2);
    dbContext.SaveChanges();
}

app.MapGet("data", async (Context db) =>
{
    var authorsCommentCounts = await db.Comments
    .GroupBy(c => c.AuthorId)
    .Select(c => new { c.Key, Count = c.Count() }).
    ToListAsync();

    var topAuthor = authorsCommentCounts.
    First(a => a.Count == authorsCommentCounts.Max(acc => acc.Count));

    var userDetails = db.Users.First(u => u.Id == topAuthor.Key);
    return new { userDetails, commentCount = topAuthor.Count };
});

app.MapGet("GettingRelatedData", async (Context db) =>
{
    var user = await db.Users
    .Include(u => u.Comments).ThenInclude(c => c.WorkItem)//database will use JOIN because of Include
    .Include(u => u.Adress)
    .FirstAsync(u => u.Id == Guid.Parse("68366DBE-0809-490F-CC1D-08DA10AB0E61"));
    return user;
});

app.MapPost("update", async (Context db) =>
{
    Epic epic = await db.Epics.FirstAsync(epic => epic.Id == 1);
    //three different ways of update 

    //epic.StateId = 1;//first way
    /*
    //second way
    var onHoldState = await db.WorkItemStates.FirstAsync(wis => wis.Value == "On Hold");
    epic.StateId = onHoldState.Id;
    var rejectedState = await db.WorkItemStates.FirstAsync(wis => wis.Value == "Rejected");
    epic.State = rejectedState;
    */

    /* 
    //third way
    epic.Area = "Updated area";
    epic.Priority = 1;
    epic.StartDate= DateTime.Now;*/
    await db.SaveChangesAsync();
    return epic;
});

app.MapPost("create", async (Context db) =>
{
    Tag tag = new Tag()
    {
        Value = "EF"
    };

    Tag mcv = new Tag()
    {
        Value = "MCV"
    };

    Tag asp = new Tag()
    {
        Value = "ASP"
    };

    var tags = new List<Tag> { mcv, asp };
    //how to add one tag to table
    //await db.AddAsync(tag);
    //await db.Tags.AddAsync(tag);
    await db.Tags.AddRangeAsync(tags);
    await db.SaveChangesAsync();
    return tags;
});

app.MapPost("AddRelatedData", async (Context db) =>
{
    var adress = new Adress()
    {
        Id = Guid.Parse("12ab34cd-56ef-11ef-22aa-abc1122abc21"),
        City = "Kraków",
        Country = "Poland",
        Street = "D³uga"
    };

    var user = new User()
    {
        Email = "user@add.com",
        FullName = "ADD USER",
        Adress = adress,
    };
    db.Users.Add(user);
    await db.SaveChangesAsync();
    return user;
});

app.Run();

