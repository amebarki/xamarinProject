﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ProjectIncident.Core.DataAccess;
using ProjectIncident.Core.Model;
using System;

namespace ProjectIncident.Core.Migrations
{
    [DbContext(typeof(IncidentDbContext))]
    partial class IncidentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("ProjectIncident.Core.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.Property<int?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ProjectIncident.Core.Model.Incident", b =>
                {
                    b.Property<DateTime>("SubmissionDate");

                    b.Property<double>("Altitude");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("Status");

                    b.Property<DateTime>("StatusChangedDate");

                    b.Property<int>("UserId");

                    b.HasKey("SubmissionDate");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("ProjectIncident.Core.Model.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("IncidentId");

                    b.Property<string>("PhotoBase64");

                    b.HasKey("Id");

                    b.HasIndex("IncidentId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("ProjectIncident.Core.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EMail");

                    b.Property<string>("EncryptedPassword");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectIncident.Core.Model.Category", b =>
                {
                    b.HasOne("ProjectIncident.Core.Model.Category", "Parent")
                        .WithMany("Childs")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectIncident.Core.Model.Incident", b =>
                {
                    b.HasOne("ProjectIncident.Core.Model.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProjectIncident.Core.Model.User", "User")
                        .WithMany("Inidents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectIncident.Core.Model.Photo", b =>
                {
                    b.HasOne("ProjectIncident.Core.Model.Incident", "Incident")
                        .WithMany("Photos")
                        .HasForeignKey("IncidentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
