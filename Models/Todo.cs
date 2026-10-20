using System.ComponentModel.DataAnnotations;
namespace MeineErsteWebApp.Models;
public class Todo
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;

// ? bedeutet, dass die Beschreibung null sein darf
    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public int Priority { get; set; } = 1;

    public bool Done { get; set;} = false;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int? CategoryId { get; set; }

    public string? CategoryName { get; set; }
}

