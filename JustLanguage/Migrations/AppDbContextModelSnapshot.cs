﻿// <auto-generated />
using System;
using JustLanguage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JustLanguage.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("JustLanguage.Entities.ArticleInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CoverImageBase64")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SrcUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ArticleInfo");
                });

            modelBuilder.Entity("JustLanguage.Entities.ParseRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rule")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ParseRule");
                });

            modelBuilder.Entity("JustLanguage.Entities.SupportDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Domain")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ParseRuleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParseRuleId");

                    b.ToTable("SupportDomain");
                });

            modelBuilder.Entity("JustLanguage.Entities.SupportDomain", b =>
                {
                    b.HasOne("JustLanguage.Entities.ParseRule", "ParseRule")
                        .WithMany("SupportDomains")
                        .HasForeignKey("ParseRuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParseRule");
                });

            modelBuilder.Entity("JustLanguage.Entities.ParseRule", b =>
                {
                    b.Navigation("SupportDomains");
                });
#pragma warning restore 612, 618
        }
    }
}
