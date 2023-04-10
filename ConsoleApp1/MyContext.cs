using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1;

internal class MyContext : DbContext
{
    public DbSet<MyRow> MyRows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
        builder.DataSource = "test.db";

        optionsBuilder.UseSqlite(builder.ConnectionString);

        //optionsBuilder.LogTo(Filter, Logger);

        base.OnConfiguring(optionsBuilder);
    }

    bool Filter(EventId e, LogLevel level)
    {
        if (e.Name == "Microsoft.EntityFrameworkCore.Database.Command.CommandExecuting")
            return true;
        return false;
    }

    void Logger(EventData e)
    {
        Console.WriteLine();
        Console.WriteLine(e);
        Console.WriteLine();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        //configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeToTicksConverter>();
        configurationBuilder.Properties<DateTime>().HaveConversion<long>();
        base.ConfigureConventions(configurationBuilder);
    }

}
