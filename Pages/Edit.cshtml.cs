using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeineErsteWebApp.Models;
using MeineErsteWebApp.Services;
namespace MeineErsteWebApp.Pages;
public class EditModel(TodoService service):PageModel
{
 [BindProperty] public Todo Todo {get;set;}=new(); public List<Category> Categories {get;set;}=[];
 public IActionResult OnGet(int id){var t=service.GetById(id);if(t is null)return NotFound();Todo=t;Categories=service.GetCategories();return Page();}
 public IActionResult OnPost(){if(!ModelState.IsValid){Categories=service.GetCategories();return Page();}service.Update(Todo);return RedirectToPage("/Index");}
}
