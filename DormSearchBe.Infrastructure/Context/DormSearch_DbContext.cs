using DormSearchBe.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security;

namespace DormSearchBe.Infrastructure.Context
{
    public class DormSearch_DbContext:DbContext
    {
        public DormSearch_DbContext(DbContextOptions<DormSearch_DbContext> options) : base(options) { }
        public virtual DbSet<Approval> Approvals { get; set; }
        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Favorites> Favorites { get; set; }
        public virtual DbSet<Houses> Houses { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Refresh_Token> Refresh_Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Approval>(e =>
            {
                e.ToTable("Approvals");
                e.HasKey(e => e.ApprovalId);
            });
            modelBuilder.Entity<Areas>(e =>
            {
                e.ToTable("Areass");
                e.HasKey(e => e.AreasId);
            });
            modelBuilder.Entity<City>(e =>
            {
                e.ToTable("Citys");
                e.HasKey(e => e.CityId);
            });
            modelBuilder.Entity<Favorites>(e =>
            {
                e.ToTable("Favorites");
                e.HasKey(e => e.FavoritesId);
                e.HasOne(e => e.User).WithMany(e => e.Favorites).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Houses>(e =>
            {
                e.ToTable("Houses");
                e.HasKey(e => e.HousesId);
                e.HasOne(e => e.Areas).WithMany(e => e.Houses).HasForeignKey(e => e.AreasId).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e => e.City).WithMany(e => e.Houses).HasForeignKey(e => e.CityId).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e => e.Favorites).WithMany(e => e.Houses).HasForeignKey(e => e.FavoritesId).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e => e.Prices).WithMany(e => e.Houses).HasForeignKey(e => e.PriceId).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e => e.Ratings).WithMany(e => e.Houses).HasForeignKey(e => e.RatingsId).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e => e.Users).WithMany(e => e.Houses).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e => e.Roomstyles).WithMany(e => e.Houses).HasForeignKey(e => e.RoomstyleId).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Message>(e =>
            {
                e.ToTable("Messages");
                e.HasKey(e => e.MessagesId);
                e.HasOne(e => e.User).WithMany(e => e.Messages).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Prices>(e =>
            {
                e.ToTable("Price");
                e.HasKey(e => e.PriceId);
            });
            modelBuilder.Entity<Ratings>(e =>
            {
                e.ToTable("Ratings");
                e.HasKey(e => e.RatingsId);
                e.HasOne(e => e.Users).WithMany(e => e.Ratings).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Roles");
                e.HasKey(e => e.RoleId);
            });
            modelBuilder.Entity<Roomstyle>(e =>
            {
                e.ToTable("Roomstyles");
                e.HasKey(e => e.RoomstyleId);
            });
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(e => e.UserId);
                e.HasOne(e => e.Roles).WithMany(e => e.Users).HasForeignKey(e => e.RoleId).OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<Refresh_Token>(e =>
            {
                e.ToTable("RefreshTokens");
                e.HasKey(e => e.UserId);
                e.HasOne(e => e.User).WithMany(e => e.Refresh_Tokens).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
            });
           
            
        }
    }
}
