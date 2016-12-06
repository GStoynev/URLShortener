using System;
using URLShortener.UI.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace URLShortener.UI.Controllers
{
    internal class UrlMapContext : DbContext
    {
        public UrlMapContext() : base("DefaultConnection") { }

        public DbSet<UrlShortenerViewModel> UrlMappings { get; set; }

        protected override void OnModelCreating (DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<UrlShortenerViewModel>()
                .HasEntitySetName("UrlMappings")
                .MapToStoredProcedures(e => e.Insert(i => i.HasName("UrlShortenerViewModel_Insert")));
        }
    }
}