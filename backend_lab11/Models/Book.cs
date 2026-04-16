
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace backend_lab11.Models;

[Table("books")]
public class Book
{
    [Column("id")]
    public int Id { get; set; }

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("year")]
    public int Year { get; set; }

    [Column("author_id")]
    public int AuthorId { get; set; }

    [Column("publisher_id")]
    public int PublisherId { get; set; }
    [JsonIgnore]
    public Author? Author { get; set; }
    public Publisher? Publisher { get; set; }
}