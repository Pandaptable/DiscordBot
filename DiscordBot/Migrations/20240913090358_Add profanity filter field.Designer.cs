﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OpenShock.DiscordBot.OpenShockDiscordDb;

#nullable disable

namespace OpenShock.DiscordBot.Migrations
{
    [DbContext(typeof(OpenShockDiscordContext))]
    [Migration("20240913090358_Add profanity filter field")]
    partial class Addprofanityfilterfield
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OpenShock.DiscordBot.OpenShockDiscordDb.User", b =>
                {
                    b.Property<decimal>("DiscordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("discord_id");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("api_key");

                    b.Property<string>("ApiServer")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("api_server");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid>("OpenshockId")
                        .HasColumnType("uuid")
                        .HasColumnName("openshock_id");

                    b.Property<bool>("ProfanityShocking")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("profanity_shocking")
                        .HasDefaultValueSql("false");

                    b.HasKey("DiscordId")
                        .HasName("users_pkey");

                    b.HasIndex(new[] { "OpenshockId" }, "users_openshock_id")
                        .IsUnique()
                        .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("OpenShock.DiscordBot.OpenShockDiscordDb.UsersFriendwhitelist", b =>
                {
                    b.Property<decimal>("User")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("user");

                    b.Property<decimal>("WhitelistedFriend")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("whitelisted_friend");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("User", "WhitelistedFriend")
                        .HasName("users_friendwhitelist_pkey");

                    b.HasIndex(new[] { "WhitelistedFriend" }, "friend_id")
                        .HasAnnotation("Npgsql:StorageParameter:deduplicate_items", "true");

                    b.ToTable("users_friendwhitelist", (string)null);
                });

            modelBuilder.Entity("OpenShock.DiscordBot.OpenShockDiscordDb.UsersShocker", b =>
                {
                    b.Property<decimal>("User")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("user");

                    b.Property<Guid>("ShockerId")
                        .HasColumnType("uuid")
                        .HasColumnName("shocker_id");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("User", "ShockerId")
                        .HasName("users_shockers_pkey");

                    b.ToTable("users_shockers", (string)null);
                });

            modelBuilder.Entity("OpenShock.DiscordBot.OpenShockDiscordDb.UsersFriendwhitelist", b =>
                {
                    b.HasOne("OpenShock.DiscordBot.OpenShockDiscordDb.User", "UserNavigation")
                        .WithMany("UsersFriendwhitelists")
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("OpenShock.DiscordBot.OpenShockDiscordDb.UsersShocker", b =>
                {
                    b.HasOne("OpenShock.DiscordBot.OpenShockDiscordDb.User", "UserNavigation")
                        .WithMany("UsersShockers")
                        .HasForeignKey("User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user");

                    b.Navigation("UserNavigation");
                });

            modelBuilder.Entity("OpenShock.DiscordBot.OpenShockDiscordDb.User", b =>
                {
                    b.Navigation("UsersFriendwhitelists");

                    b.Navigation("UsersShockers");
                });
#pragma warning restore 612, 618
        }
    }
}