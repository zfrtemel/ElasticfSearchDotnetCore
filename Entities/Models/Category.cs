using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticfSearchDotnetCore.Entities.Models;
public class Category
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("description")]
    public string Description { get; set; }

    public virtual List<Product> Products { get; set; }
}
