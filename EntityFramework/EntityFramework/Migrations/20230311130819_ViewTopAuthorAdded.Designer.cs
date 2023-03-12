﻿// <auto-generated />
using System;
using EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EntityFramework.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230311130819_ViewTopAuthorAdded")]
    partial class ViewTopAuthorAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityFramework.Entities.Adress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("EntityFramework.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("WorkItemId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EntityFramework.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("EntityFramework.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EntityFramework.Entities.WorkItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IterationPath")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Iteration_Path");

                    b.Property<int>("Priority")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("StateID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StateID");

                    b.ToTable("WorkItems");

                    b.HasDiscriminator<string>("Discriminator").HasValue("WorkItem");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EntityFramework.Entities.WorkItemState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("WorkItemStates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Value = "To Do"
                        },
                        new
                        {
                            Id = 2,
                            Value = "Doing"
                        },
                        new
                        {
                            Id = 3,
                            Value = "Done"
                        });
                });

            modelBuilder.Entity("EntityFramework.Entities.WorkItemTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("WorkItemID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("TagId", "WorkItemID");

                    b.HasIndex("WorkItemID");

                    b.ToTable("WorkItemTags");
                });

            modelBuilder.Entity("EntityFramework.ViewModels.TopAuthors", b =>
                {
                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkItemsCreated")
                        .HasColumnType("int");

                    b.ToTable((string)null);

                    b.ToView("View_TopAuthors", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Entities.Epic", b =>
                {
                    b.HasBaseType("EntityFramework.Entities.WorkItem");

                    b.Property<DateTime?>("EndDate")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<DateTime?>("StarDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Epic");
                });

            modelBuilder.Entity("EntityFramework.Entities.Issue", b =>
                {
                    b.HasBaseType("EntityFramework.Entities.WorkItem");

                    b.Property<decimal>("Efford")
                        .HasColumnType("decimal(5,1)");

                    b.HasDiscriminator().HasValue("Issue");
                });

            modelBuilder.Entity("EntityFramework.Entities.Task", b =>
                {
                    b.HasBaseType("EntityFramework.Entities.WorkItem");

                    b.Property<string>("Activity")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("RemainingWork")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasDiscriminator().HasValue("Task");
                });

            modelBuilder.Entity("EntityFramework.Entities.Adress", b =>
                {
                    b.HasOne("EntityFramework.Entities.User", "User")
                        .WithOne("Adress")
                        .HasForeignKey("EntityFramework.Entities.Adress", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EntityFramework.Entities.Comment", b =>
                {
                    b.HasOne("EntityFramework.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Entities.WorkItem", "WorkItem")
                        .WithMany("Comments")
                        .HasForeignKey("WorkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("EntityFramework.Entities.WorkItem", b =>
                {
                    b.HasOne("EntityFramework.Entities.User", "Author")
                        .WithMany("WorkItems")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Entities.WorkItemState", "State")
                        .WithMany()
                        .HasForeignKey("StateID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("State");
                });

            modelBuilder.Entity("EntityFramework.Entities.WorkItemTag", b =>
                {
                    b.HasOne("EntityFramework.Entities.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Entities.WorkItem", "WorkItem")
                        .WithMany()
                        .HasForeignKey("WorkItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("WorkItem");
                });

            modelBuilder.Entity("EntityFramework.Entities.User", b =>
                {
                    b.Navigation("Adress");

                    b.Navigation("Comments");

                    b.Navigation("WorkItems");
                });

            modelBuilder.Entity("EntityFramework.Entities.WorkItem", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}