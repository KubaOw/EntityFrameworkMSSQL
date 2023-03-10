using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    }
);
}

app.Run();

