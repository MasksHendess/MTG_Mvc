﻿// <auto-generated />
using MTG_Mvc.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MTG_Mvc.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    [Migration("20200829204220_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MTG_Mvc.Models.card", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("decklistid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<string>("set")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("decklistid");

                    b.ToTable("card");
                });

            modelBuilder.Entity("MTG_Mvc.Models.decklist", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.ToTable("decklists");
                });

            modelBuilder.Entity("MTG_Mvc.Models.card", b =>
                {
                    b.HasOne("MTG_Mvc.Models.decklist", null)
                        .WithMany("cards")
                        .HasForeignKey("decklistid");
                });
#pragma warning restore 612, 618
        }
    }
}
