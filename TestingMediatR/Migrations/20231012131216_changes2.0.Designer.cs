﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestingMediatR.Data;

#nullable disable

namespace TestingMediatR.Migrations
{
    [DbContext(typeof(DbContextClass))]
    [Migration("20231012131216_changes2.0")]
    partial class changes20
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestingMediatR.Models.StudentDetails", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("GradeId")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentAge")
                        .HasColumnType("int");

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("TestingMediatR.Models.StudentGrade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.HasKey("GradeId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("TestingMediatR.Models.StudentDetails", b =>
                {
                    b.HasOne("TestingMediatR.Models.StudentGrade", "Grade")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");
                });
#pragma warning restore 612, 618
        }
    }
}
