
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MeineErsteWebApp.Models;
using MeineErsteWebApp.Repositories;

namespace MeineErsteWebApp.Pages;

public class ExpensesModel : PageModel
{
    private readonly ExpenseRepository _expenseRepository;

    public ExpensesModel(ExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public List<ExpenseCategory> Categories { get; set; } = new();

    public List<Expense> Expenses { get; set; } = new();

    [BindProperty]
    public Expense NewExpense { get; set; } = new();

    [BindProperty]
    public Expense EditExpense { get; set; } = new();

    public bool IsEditing { get; set; } = false;

    public void OnGet()
    {
        LoadPageData();
    }

    
public IActionResult OnPost()
{
    LoadPageData();

    ModelState.Clear();

    if (!TryValidateModel(NewExpense, nameof(NewExpense)))
    {
        return Page();
    }

    _expenseRepository.AddExpense(NewExpense);

    return RedirectToPage();
}


    public IActionResult OnPostDelete(int id)
    {
        _expenseRepository.DeleteExpense(id);

        return RedirectToPage();
    }

    public IActionResult OnGetEdit(int id)
    {
        LoadPageData();

        var expense = _expenseRepository.GetExpenseById(id);

        if (expense == null)
        {
            return NotFound();
        }

        EditExpense = expense;
        IsEditing = true;

        return Page();
    }

    public IActionResult OnPostUpdate()
    {
        LoadPageData();
        IsEditing = true;

        // Nur die Daten des Bearbeitungsformulars validieren.
        ModelState.Clear();

        if (!TryValidateModel(EditExpense, nameof(EditExpense)))
        {
            return Page();
        }

        if (EditExpense.CategoryId == null)
        {
            ModelState.AddModelError(
                "EditExpense.CategoryId",
                "Bitte eine Kategorie auswählen.");

            return Page();
        }

        _expenseRepository.UpdateExpense(EditExpense);

        return RedirectToPage();
    }

    private void LoadPageData()
    {
        Categories = _expenseRepository.GetCategories();
        Expenses = _expenseRepository.GetExpenses();
    }
}
