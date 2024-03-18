using Bogus;
using ElasticfSearchDotnetCore.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ElasticfSearchDotnetCore.Entities.Context;
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ImageUrl).HasColumnName("image_url");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.HasOne(d => d.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_products_categories");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Elektronik", Description = "Elektronik Açıklama" },
            new Category { Id = 2, Name = "Giyim", Description = "Giyim Açıklama" },
            new Category { Id = 3, Name = "Aksesuar", Description = "Akseasuar açıklama" });


        // has data by array faker random number
        var userFaker = new Faker<Product>("tr")
            .RuleFor(u => u.Id, f => f.IndexFaker + 1)
         .RuleFor(u => u.Name, f => f.Commerce.Product())
         .RuleFor(u => u.CategoryId, f => f.Random.Number(1, 3))
         .RuleFor(u => u.Description, f => f.Lorem.Sentence(10))
         .RuleFor(u => u.ImageUrl, f => f.Image.PicsumUrl())
         .RuleFor(u => u.Price, f => f.Random.Decimal(10, 1000));

        modelBuilder.Entity<Product>().HasData(userFaker.Generate(10000).ToArray());

        //modelBuilder.Entity<Product>()
        //    .ToSqlQuery("CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT; CREATE FULLTEXT INDEX ON products(Name) KEY INDEX PK_Products WITH STOPLIST = SYSTEM;");

    }

}
