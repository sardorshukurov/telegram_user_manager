﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TUM.Infrastructure.Data;

#nullable disable

namespace TUM.Infrastructure.Data.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TUM.Domain.Entities.Bot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("bots", (string)null);
                });

            modelBuilder.Entity("TUM.Domain.Entities.BotAdmin", b =>
                {
                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BotId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("AdminId", "BotId");

                    b.HasIndex("BotId");

                    b.ToTable("bots_admins", (string)null);
                });

            modelBuilder.Entity("TUM.Domain.Entities.BotUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BotId")
                        .HasColumnType("uuid");

                    b.Property<bool>("HasPremium")
                        .HasColumnType("boolean");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("boolean");

                    b.HasKey("UserId", "BotId");

                    b.HasIndex("BotId");

                    b.ToTable("bots_users", (string)null);
                });

            modelBuilder.Entity("TUM.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("AddedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastTimeActive")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("TUM.Domain.Entities.BotAdmin", b =>
                {
                    b.HasOne("TUM.Domain.Entities.User", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TUM.Domain.Entities.Bot", "Bot")
                        .WithMany("BotAdmins")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Bot");
                });

            modelBuilder.Entity("TUM.Domain.Entities.BotUser", b =>
                {
                    b.HasOne("TUM.Domain.Entities.Bot", "Bot")
                        .WithMany("BotUsers")
                        .HasForeignKey("BotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TUM.Domain.Entities.User", "User")
                        .WithMany("BotUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bot");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TUM.Domain.Entities.Bot", b =>
                {
                    b.Navigation("BotAdmins");

                    b.Navigation("BotUsers");
                });

            modelBuilder.Entity("TUM.Domain.Entities.User", b =>
                {
                    b.Navigation("BotUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
