using Fiorella.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductsImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet< Experts>Experts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<FooterNames> FooterNames { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
        .HasData(
            new Blog
            {
                Id = 1,
                Title = "Blog title1",
                Description = "Description1",
                Date = DateTime.Now,
                Image = "blog-feature-img-1.jpg"
            },
             new Blog
             {
                 Id = 2,
                 Title = "Blog title2",
                 Description = "Description2",
                 Date = DateTime.Now,
                 Image = "blog-feature-img-3.jpg"
             },
                  new Blog
                  {
                      Id = 3,
                      Title = "Blog title3",
                      Description = "Description3",
                      Date = DateTime.Now,
                      Image = "blog-feature-img-4.jpg"
                  }
        );
        }
    }
}
