using GameReccomender.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameReccomender.Api.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserLibrary> UserLibraries { get; set; }
        public DbSet<GameTag> GameTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key and relationships for UserLibrary
            modelBuilder.Entity<UserLibrary>()
                .HasKey(ul => new { ul.SteamId, ul.AppId });

            modelBuilder.Entity<UserLibrary>()
                .HasOne(ul => ul.User)
                .WithMany()
                .HasForeignKey(ul => ul.SteamId);

            modelBuilder.Entity<UserLibrary>()
                .HasOne(ul => ul.Game)
                .WithMany()
                .HasForeignKey(ul => ul.AppId);

            // Configure composite primary key and relationships for GameTag
            modelBuilder.Entity<GameTag>()
                .HasKey(gt => new { gt.AppId, gt.TagId });

            modelBuilder.Entity<GameTag>()
                .HasOne(gt => gt.Game)
                .WithMany()
                .HasForeignKey(gt => gt.AppId);

            modelBuilder.Entity<GameTag>()
                .HasOne(gt => gt.Tag)
                .WithMany()
                .HasForeignKey(gt => gt.TagId);
        }
    }
}
