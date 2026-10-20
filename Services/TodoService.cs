using MeineErsteWebApp.Models;
using MeineErsteWebApp.Repositories;
namespace MeineErsteWebApp.Services;
public class TodoService(TodoRepository repository)
{
    public List<Todo> GetAll(string status,int? priority,int? category,string sort)=>repository.GetAll(status,priority,category,sort);
    public List<Category> GetCategories()=>repository.GetCategories();
    public Todo? GetById(int id)=>repository.GetById(id);
    public void Add(Todo todo) 
    { 
        Validate(todo); 
        todo.Title=todo.Title.Trim(); 
        todo.CreatedAt=DateTime.Now; 
        repository.Add(todo); 
    }
    public void Update(Todo todo) { Validate(todo); todo.Title=todo.Title.Trim(); repository.Update(todo); }
    public void Toggle(int id)=>repository.Toggle(id);
    public void Delete(int id)=>repository.Delete(id);
    private static void Validate(Todo t) { if(string.IsNullOrWhiteSpace(t.Title)) throw new ArgumentException("Bitte einen Titel eingeben."); if(t.Priority is <1 or >3) throw new ArgumentException("Priorität muss zwischen 1 und 3 liegen."); }
}
