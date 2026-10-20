
using Microsoft.Data.Sqlite;
using MeineErsteWebApp.Models;

namespace MeineErsteWebApp.Repositories;

public class ExpenseRepository
{
    private readonly string _connectionString;

    public ExpenseRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("Todos")
            ?? "Data Source=todos.db";
    }

    public void Initialize()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            CREATE TABLE IF NOT EXISTS ExpenseCategories (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL UNIQUE,
                Color TEXT NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Expenses (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Amount TEXT NOT NULL,
                Description TEXT NOT NULL,
                CategoryId INTEGER NOT NULL,
                Date TEXT NOT NULL,
                PaymentMethod TEXT NOT NULL,
                Note TEXT,
                CreatedAt TEXT NOT NULL,
                FOREIGN KEY (CategoryId)
                    REFERENCES ExpenseCategories(Id)
            );
            """;

        command.ExecuteNonQuery();

        command.CommandText = """
            INSERT OR IGNORE INTO ExpenseCategories (Name, Color)
            VALUES
                ('Lebensmittel', '#27ae60'),
                ('Wohnen', '#3498db'),
                ('Transport', '#f39c12'),
                ('Freizeit', '#9b59b6'),
                ('Gesundheit', '#e74c3c'),
                ('Sonstiges', '#95a5a6');
            """;

        command.ExecuteNonQuery();
    }

    public List<ExpenseCategory> GetCategories()
    {
        var categories = new List<ExpenseCategory>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            SELECT Id, Name, Color
            FROM ExpenseCategories
            ORDER BY Name;
            """;

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            categories.Add(new ExpenseCategory
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Color = reader.GetString(2)
            });
        }

        return categories;
    }


    public void AddExpense(Expense expense)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            INSERT INTO Expenses
                (Amount, Description, CategoryId, Date,
                 PaymentMethod, Note, CreatedAt)
            VALUES
                ($amount, $description, $categoryId, $date,
                 $paymentMethod, $note, $createdAt);
            """;

        command.Parameters.AddWithValue(
            "$amount",
            expense.Amount.ToString(
                System.Globalization.CultureInfo.InvariantCulture));

        command.Parameters.AddWithValue("$description", expense.Description);
        command.Parameters.AddWithValue("$categoryId", expense.CategoryId!.Value);
        command.Parameters.AddWithValue("$date", expense.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("$paymentMethod", expense.PaymentMethod);
        command.Parameters.AddWithValue("$note", (object?)expense.Note ?? DBNull.Value);
        command.Parameters.AddWithValue("$createdAt", expense.CreatedAt.ToString("O"));

        command.ExecuteNonQuery();
    }


    public List<Expense> GetExpenses()
    {
        var expenses = new List<Expense>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();

        command.CommandText = """
            SELECT Id, Amount, Description, CategoryId, Date,
                   PaymentMethod, Note, CreatedAt
            FROM Expenses
            ORDER BY Date DESC, Id DESC;
            """;

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            expenses.Add(new Expense
            {
                Id = reader.GetInt32(0),
                Amount = decimal.Parse(
                    reader.GetString(1),
                    System.Globalization.CultureInfo.InvariantCulture),
                Description = reader.GetString(2),
                CategoryId = reader.GetInt32(3),
                Date = DateTime.Parse(reader.GetString(4)),
                PaymentMethod = reader.GetString(5),
                Note = reader.IsDBNull(6) ? null : reader.GetString(6),
                CreatedAt = DateTime.Parse(reader.GetString(7))
            });
        }

        return expenses;
    }


public Expense? GetExpenseById(int id)
{
    using var connection = new SqliteConnection(_connectionString);
    connection.Open();

    var command = connection.CreateCommand();

    command.CommandText = """
        SELECT Id, Amount, Description, CategoryId, Date,
               PaymentMethod, Note, CreatedAt
        FROM Expenses
        WHERE Id = $id;
        """;

    command.Parameters.AddWithValue("$id", id);

    using var reader = command.ExecuteReader();

    if (!reader.Read())
    {
        return null;
    }

    return new Expense
    {
        Id = reader.GetInt32(0),
        Amount = decimal.Parse(
            reader.GetString(1),
            System.Globalization.CultureInfo.InvariantCulture),
        Description = reader.GetString(2),
        CategoryId = reader.GetInt32(3),
        Date = DateTime.Parse(reader.GetString(4)),
        PaymentMethod = reader.GetString(5),
        Note = reader.IsDBNull(6) ? null : reader.GetString(6),
        CreatedAt = DateTime.Parse(reader.GetString(7))
    };
}



public void DeleteExpense(int id)
{
    using var connection = new SqliteConnection(_connectionString);
    connection.Open();

    var command = connection.CreateCommand();

    command.CommandText = """
        DELETE FROM Expenses
        WHERE Id = $id;
        """;

    command.Parameters.AddWithValue("$id", id);

    command.ExecuteNonQuery();
}


public void UpdateExpense(Expense expense)
{
    using var connection = new SqliteConnection(_connectionString);
    connection.Open();

    var command = connection.CreateCommand();

    command.CommandText = """
        UPDATE Expenses
        SET Amount = $amount,
            Description = $description,
            CategoryId = $categoryId,
            Date = $date,
            PaymentMethod = $paymentMethod,
            Note = $note
        WHERE Id = $id;
        """;

    command.Parameters.AddWithValue(
        "$amount",
        expense.Amount.ToString(
            System.Globalization.CultureInfo.InvariantCulture));

    command.Parameters.AddWithValue("$description", expense.Description);
    command.Parameters.AddWithValue("$categoryId", expense.CategoryId!.Value);
    command.Parameters.AddWithValue("$date", expense.Date.ToString("yyyy-MM-dd"));
    command.Parameters.AddWithValue("$paymentMethod", expense.PaymentMethod);
    command.Parameters.AddWithValue("$note", (object?)expense.Note ?? DBNull.Value);
    command.Parameters.AddWithValue("$id", expense.Id);

    command.ExecuteNonQuery();
}




}
