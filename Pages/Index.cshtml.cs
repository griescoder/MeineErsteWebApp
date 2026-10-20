using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeineErsteWebApp.Models;
using MeineErsteWebApp.Services;
namespace MeineErsteWebApp.Pages;
public class IndexModel(TodoService service) : PageModel
{
    public List<Todo> Todos { get; private set; } = [];
    public List<Category> Categories { get; private set; } = [];
    [BindProperty] public Todo NewTodo { get; set; } = new();
    [BindProperty(SupportsGet = true)]
    public string? Status {get;set;}="offen";
    [BindProperty(SupportsGet=true)] public int? PriorityFilter {get;set;}
    [BindProperty(SupportsGet=true)] public int? CategoryFilter {get;set;}
    [BindProperty(SupportsGet = true)] 
    public string? Sort {get;set;}="faelligkeit";
    public void OnGet() => Load();
    
public IActionResult OnPostAdd()
{
    if (!ModelState.IsValid)
    {
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"VALIDIERUNGSFEHLER: {error.ErrorMessage}");
        }

        Load();
        return Page();
    }

    service.Add(NewTodo);
    return RedirectToPage(new { Status, PriorityFilter, CategoryFilter, Sort });
}

    public IActionResult OnPostToggle(int id) { service.Toggle(id); return RedirectToPage(new {Status,PriorityFilter,CategoryFilter,Sort}); }
    public IActionResult OnPostDelete(int id) { service.Delete(id); return RedirectToPage(new {Status,PriorityFilter,CategoryFilter,Sort}); }
    private void Load() 
    {
    Todos = service.GetAll(
        string.IsNullOrWhiteSpace(Status) ? "offen" : Status,
        PriorityFilter,
        CategoryFilter,
        string.IsNullOrWhiteSpace(Sort) ? "faelligkeit" : Sort
    );

    Categories = service.GetCategories();
}
}
