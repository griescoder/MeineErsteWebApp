using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeineErsteWebApp.Models;
using MeineErsteWebApp.Services;
namespace MeineErsteWebApp.Pages;
public class DeleteModel(TodoService service):PageModel
{
 public Todo? Todo {get;set;}
 public IActionResult OnGet(int id){Todo=service.GetById(id);return Todo is null?NotFound():Page();}
 public IActionResult OnPost(int id){service.Delete(id);return RedirectToPage("/Index");}
}
