using ContentPlatform.Data.Configurations;
using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ContentPlatform.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Film> Filmes { get; set;}
        public DbSet<Game> Games { get; set; }
        public DbSet<Music> Musices { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RateReview> RateReviews { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchevement> UserAchevements { get; set; }
        public DbSet<UserStats> UserStats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContentConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new TVShowConfiguration());
            modelBuilder.ApplyConfiguration(new EpisodeConfiguration());
            modelBuilder.ApplyConfiguration(new RateReviewConfiguration());
            modelBuilder.ApplyConfiguration(new AchievementConfiguration());
            modelBuilder.ApplyConfiguration(new UserAchievementConfiguration());
            modelBuilder.ApplyConfiguration(new UserStataConfiguration());
            modelBuilder.Entity<Film>().ToTable("Films");
            modelBuilder.Entity<TVShow>().ToTable("TVShows");
            modelBuilder.Entity<Episode>().ToTable("Episodes");
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Music>().ToTable("Musics");
            modelBuilder.Entity<Book>().ToTable("Books");
            base.OnModelCreating(modelBuilder);
        }
    }
}
