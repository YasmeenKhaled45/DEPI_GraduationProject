using GraduationProject.Models;
using GraduationProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
    {
        // إضافة DbSet لكل كلاس
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasMany(u => u.Addresses)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Post>()
            .HasMany(u => u.Comments)
            .WithOne(a => a.Post)
            .HasForeignKey(a => a.PostId);
        }
    }
}
