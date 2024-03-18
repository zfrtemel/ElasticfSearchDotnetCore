using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticfSearchDotnetCore.Entities.Models;
public class Product
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("price")]
    public decimal Price { get; set; }
    [Column("image_url")]
    public string ImageUrl { get; set; }
    [Column("category_id")]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}
