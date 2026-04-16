
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace backend_lab11.Models;

[Table("publishers")]
public class Publisher
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [JsonIgnore]
    public ICollection<Book> Books { get; set; } = new List<Book>();
}