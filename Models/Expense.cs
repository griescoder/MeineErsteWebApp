using System.ComponentModel.DataAnnotations;

namespace MeineErsteWebApp.Models;

public class Expense
{
    public int Id { get; set; }

    [Range(0.01, 999999999,
    ErrorMessage = "Der Betrag muss größer als 0 sein.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Bitte eine Beschreibung eingeben.")]
    public string Description { get; set; } = string.Empty;

     [Required(ErrorMessage = "Bitte eine Kategorie auswählen.")]
    public int? CategoryId { get; set; }

     [Required(ErrorMessage = "Bitte ein Datum auswählen.")]
    public DateTime Date { get; set; } = DateTime.Today;

    [Required]
    public string PaymentMethod { get; set; } = "Karte";


    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}