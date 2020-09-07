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
    [Migration("20200907175449_MorePropertiesOnCard_Decimal")]
    partial class MorePropertiesOnCard_Decimal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MTG_Mvc.Domain.Entities.card", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("artist")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("cmc")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("decklistid")
                        .HasColumnType("int");

                    b.Property<string>("flavourText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("imageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isMainBoard")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<string>("rarity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("set")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("decklistid");

                    b.ToTable("cards");
                });

            modelBuilder.Entity("MTG_Mvc.Domain.Entities.decklist", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("deckName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("decklists");
                });

            modelBuilder.Entity("MTG_Mvc.Domain.Entities.card", b =>
                {
                    b.HasOne("MTG_Mvc.Domain.Entities.decklist", null)
                        .WithMany("cards")
                        .HasForeignKey("decklistid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}