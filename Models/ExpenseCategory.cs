using System.ComponentModel.DataAnnotations;

namespace MeineErsteWebApp;

public class ExpenseCategory
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Bitte einen Kategorienamen eingeben.")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [RegularExpression(
        "^#[0-9A-Fa-f]{6}$",
        ErrorMessage = "Bitte einen gültigen Hex-Farbcode eingeben, z.B. #3498db.")]
    public string Color { get; set; } = "#3498db";
}