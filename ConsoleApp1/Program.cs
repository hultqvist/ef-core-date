// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");


using (var db = new MyContext())
{
    db.Database.OpenConnection();

    //var cmd = new SqliteCommand("CREATE TABLE MyRows ( id integer primary key, Date integer not null )");
    //cmd.Connection = (SqliteConnection)db.Database.GetDbConnection();
    //cmd.ExecuteNonQuery();

    db.MyRows.ExecuteDelete();

    {
        db.MyRows.Add(new MyRow { Id = 1, Date = DateTime.Now });
        db.MyRows.Add(new MyRow { Id = 2, Date = DateTime.UtcNow });
        db.SaveChanges();
    }
    Thread.Sleep(2000);

    Console.WriteLine("Test All rows");
    foreach (var row in db.MyRows)
        Console.WriteLine(row.Id + " " + row.Date);

    Console.WriteLine("Test < DateTime.UtcNow");
    foreach (var row in db.MyRows.Where(r => r.Date < DateTime.UtcNow))
        Console.WriteLine(row.Id + " " + row.Date);

    Console.WriteLine("Test < UtcNow.AddMinutes(-60)");
    foreach (var row in db.MyRows.Where(r => r.Date < DateTime.UtcNow.AddMinutes(-60)))
        Console.WriteLine(row.Id + " " + row.Date);

    Console.WriteLine("Test < now");
    var now = DateTime.UtcNow.AddMinutes(-60);
    foreach (var row in db.MyRows.Where(r => r.Date < now))
        Console.WriteLine(row.Id + " " + row.Date);

}