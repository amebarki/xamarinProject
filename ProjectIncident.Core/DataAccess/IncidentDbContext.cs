using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectIncident.Core.DataAccess
{
    public class IncidentDbContext : DbContext
    {
        #region Singleton

        private static IncidentDbContext _context = null;
        public  async static Task<IncidentDbContext> getCurrent()
        {
            if(_context == null)
            {
                _context = new IncidentDbContext(
                    Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    "incidents.db")
                );
                await _context.Database.MigrateAsync();
            }
            return _context;
        }

        #endregion

        public string DatabasePath { get; }
        public DbSet<Model.User> Users { get; set; }
        public DbSet<Model.Incident> Incidents { get; set; }
        public DbSet<Model.Category> Categories { get; set; }
        public DbSet<Model.Photo> Photos { get; set; }

		private IncidentDbContext(String databasePath)
        {
            DatabasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Filename={DatabasePath}");
        }
    }
}
