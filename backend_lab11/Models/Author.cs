
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_lab11.Models;

[Table("authors")]
public class Author
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = new List<Book>();
}