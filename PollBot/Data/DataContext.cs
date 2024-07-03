using Microsoft.EntityFrameworkCore;
using PollBot.Data.Models;
using System.Reflection;

namespace PollBot.Data
{
    public class DataContext : DbContext
    {
        private string DbName = "data.db";

        public DataContext() : base() { }

        public DbSet<ChatPoll> ChatPolls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite($"Data Source={GetDbPath()}");
        }

        public static DataContext Create()
        {
            return new DataContext();
        }

        public async Task ApplyMigrations()
        {
            await Database.MigrateAsync();
        }

        private string GetDbPath()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(directory, DbName);
        }
    }
}
