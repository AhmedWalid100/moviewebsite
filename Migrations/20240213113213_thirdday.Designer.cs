﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoviesProject.Infrastructure.DBContext;

#nullable disable

namespace MoviesProject.Migrations
{
    [DbContext(typeof(MovieDBContext))]
    [Migration("20240213113213_thirdday")]
    partial class thirdday
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MoviesProject.DomainLayer.Aggregates.Actor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("MoviesProject.DomainLayer.Aggregates.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Length")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("releaseDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ComplexProperty<Dictionary<string, object>>("Genre", "MoviesProject.DomainLayer.Aggregates.Movie.Genre#Genre", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("primaryGenre")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("subGenres")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Language", "MoviesProject.DomainLayer.Aggregates.Movie.Language#Language", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("originalLanguage")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("spokenLanguages")
                                .HasColumnType("nvarchar(max)");
                        });

                    b.HasKey("ID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MoviesProject.DomainLayer.Aggregates.MovieActor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ActorID")
                        .HasColumnType("int");

                    b.Property<int>("MovieID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("ActorID");

                    b.HasIndex("MovieID");

                    b.ToTable("MoviesActors");
                });

            modelBuilder.Entity("MoviesProject.DomainLayer.Entity.Cinema", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MovieID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.ToTable("Cinema");
                });

            modelBuilder.Entity("MoviesProject.DomainLayer.Aggregates.MovieActor", b =>
                {
                    b.HasOne("MoviesProject.DomainLayer.Aggregates.Actor", "Actor")
                        .WithMany("MovieActors")
                        .HasForeignKey("ActorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoviesProject.DomainLayer.Aggregates.Movie", "Movie")
                        .WithMany("MovieActors")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MoviesProject.DomainLayer.Entity.Cinema", b =>
                {
                    b.HasOne("MoviesProject.DomainLayer.Aggregates.Movie", null)
                        .WithMany("Cinemas")
                        .HasForeignKey("MovieID");
                });

            modelBuilder.Entity("MoviesProject.DomainLayer.Aggregates.Actor", b =>
                {
                    b.Navigation("MovieActors");
                });

            modelBuilder.Entity("MoviesProject.DomainLayer.Aggregates.Movie", b =>
                {
                    b.Navigation("Cinemas");

                    b.Navigation("MovieActors");
                });
#pragma warning restore 612, 618
        }
    }
}
