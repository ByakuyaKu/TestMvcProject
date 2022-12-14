// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestMvcProject.Data;

#nullable disable

namespace TestMvcProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220922111532_MangaTest")]
    partial class MangaTest
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.6.22329.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AnimeAuthor", b =>
                {
                    b.Property<Guid>("AnimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AnimeId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AnimeAuthor");
                });

            modelBuilder.Entity("AnimeGenre", b =>
                {
                    b.Property<Guid>("AnimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AnimeId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("AnimeGenre");
                });

            modelBuilder.Entity("AnimeManga", b =>
                {
                    b.Property<Guid>("AnimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MangaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AnimeId", "MangaId");

                    b.HasIndex("MangaId");

                    b.ToTable("AnimeManga");
                });

            modelBuilder.Entity("AuthorManga", b =>
                {
                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MangaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AuthorId", "MangaId");

                    b.HasIndex("MangaId");

                    b.ToTable("AuthorManga");
                });

            modelBuilder.Entity("AuthorPosition", b =>
                {
                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AuthorId", "PositionId");

                    b.HasIndex("PositionId");

                    b.ToTable("AuthorPosition");
                });

            modelBuilder.Entity("GenreManga", b =>
                {
                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MangaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GenreId", "MangaId");

                    b.HasIndex("MangaId");

                    b.ToTable("GenreManga");
                });

            modelBuilder.Entity("TestMvcProject.Models.Anime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AnimeEnds")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("AnimeStarts")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Duration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Favorites")
                        .HasColumnType("int");

                    b.Property<DateTime>("ItemCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("LinkCanonical")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Popularity")
                        .HasColumnType("int");

                    b.Property<string>("Premiered")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Rank")
                        .HasColumnType("int");

                    b.Property<string>("Rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Score")
                        .HasColumnType("float");

                    b.Property<int?>("ScoredBy")
                        .HasColumnType("int");

                    b.Property<int?>("SeriesCount")
                        .HasColumnType("int");

                    b.Property<int?>("SeriesRealesed")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleJapanese")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Volumes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Anime");
                });

            modelBuilder.Entity("TestMvcProject.Models.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdditionalName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ItemCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MalId")
                        .HasColumnType("bigint");

                    b.Property<int?>("MemberFavorites")
                        .HasColumnType("int");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("TestMvcProject.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ItemCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("TestMvcProject.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AnimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ItemCreation")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("MangaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ImageId");

                    b.HasIndex("MangaId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("TestMvcProject.Models.Manga", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AnimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ChaptersCount")
                        .HasColumnType("int");

                    b.Property<int?>("ChaptersRealesed")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Favorites")
                        .HasColumnType("int");

                    b.Property<Guid?>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ItemCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MangaEnds")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MangaStarts")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Popularity")
                        .HasColumnType("int");

                    b.Property<int?>("Rank")
                        .HasColumnType("int");

                    b.Property<decimal?>("Score")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ScoredBy")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TitleJapanese")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Volumes")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Manga");
                });

            modelBuilder.Entity("TestMvcProject.Models.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ItemCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("AnimeAuthor", b =>
                {
                    b.HasOne("TestMvcProject.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestMvcProject.Models.Anime", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeGenre", b =>
                {
                    b.HasOne("TestMvcProject.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestMvcProject.Models.Anime", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimeManga", b =>
                {
                    b.HasOne("TestMvcProject.Models.Manga", null)
                        .WithMany()
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestMvcProject.Models.Anime", null)
                        .WithMany()
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuthorManga", b =>
                {
                    b.HasOne("TestMvcProject.Models.Manga", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestMvcProject.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuthorPosition", b =>
                {
                    b.HasOne("TestMvcProject.Models.Position", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestMvcProject.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreManga", b =>
                {
                    b.HasOne("TestMvcProject.Models.Manga", null)
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestMvcProject.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("MangaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestMvcProject.Models.Image", b =>
                {
                    b.HasOne("TestMvcProject.Models.Anime", "Anime")
                        .WithMany()
                        .HasForeignKey("AnimeId");

                    b.HasOne("TestMvcProject.Models.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("TestMvcProject.Models.Anime", null)
                        .WithMany("Images")
                        .HasForeignKey("ImageId");

                    b.HasOne("TestMvcProject.Models.Author", null)
                        .WithMany("Images")
                        .HasForeignKey("ImageId");

                    b.HasOne("TestMvcProject.Models.Manga", null)
                        .WithMany("Images")
                        .HasForeignKey("ImageId");

                    b.HasOne("TestMvcProject.Models.Manga", "Manga")
                        .WithMany()
                        .HasForeignKey("MangaId");

                    b.Navigation("Anime");

                    b.Navigation("Author");

                    b.Navigation("Manga");
                });

            modelBuilder.Entity("TestMvcProject.Models.Anime", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("TestMvcProject.Models.Author", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("TestMvcProject.Models.Manga", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
