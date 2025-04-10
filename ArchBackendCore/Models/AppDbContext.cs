using ArchBackend.Core.Models.Bridges;
using ArchBackend.Core.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArchBackend.Core.Models
{
    public class AppDbContext : IdentityDbContext<Users, Roles, int>
    {   
        public DbSet<Project> Projects { get; set; }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<OurService> OurServices { get; set; }

        public DbSet<Users> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectCategory>().HasKey(i => new { i.ProjectId, i.CategoryId });
            modelBuilder.Entity<ProjectCategory>()
                .HasOne<Project>(i=> i.Project)
                .WithMany(i => i.ProjectCategories)
                .HasForeignKey(i => i.ProjectId);


            modelBuilder.Entity<ProjectCategory>()
                .HasOne<Category>(i => i.Category)
                .WithMany(i => i.ProjectCategories)
                .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<OurServiceProject>().HasKey(x => new { x.OurServiceId, x.ProjectId });
            modelBuilder.Entity<OurServiceProject>()
                .HasOne<OurService>(x => x.OurService)
                .WithMany(x => x.OurServiceProjects)
                .HasForeignKey(x => x.OurServiceId);

            modelBuilder.Entity<OurServiceProject>()
                 .HasOne(x => x.Project)
                 .WithMany(x => x.OurServiceProjects)
                 .HasForeignKey(x => x.OurServiceId);


            base.OnModelCreating(modelBuilder);
            
        }

    }
}
