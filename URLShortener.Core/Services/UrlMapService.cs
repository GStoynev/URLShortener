using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using URLShortener.Models;

namespace URLShortener.Services
{
    public class UrlMapService : DbContext, IUrlMapService
    {
        public UrlMapService(string connectionStringName)
            : base(connectionStringName) 
        { }

        #region EF Setup
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<UrlMap>()
                .MapToStoredProcedures(e => e.Insert(i => i.HasName("UrlShortenerViewModel_Insert")));
        }
        #endregion

        #region Repository
        public DbSet<UrlMap> UrlMap { get; set; }
        #endregion

        #region IUrlMapService
        public UrlMap Find(int id)
        {
            return UrlMap.Find(id);
        }

        public UrlMap Find(string shortURL)
        {
            try
            {
                return UrlMap.FirstOrDefault(m => m.ShortURL == shortURL);
            }
            catch (Exception)
            {
                // todo: log, then rethrow (caller must expect and handle errors from repo)
                return null;
            }
        }

        public UrlMap Add(UrlMap urlMap)
        {
            try
            {
                var newUrlMap = UrlMap.Add(urlMap);
            
                this.SaveChanges();

                return newUrlMap;
            }
            catch(Exception)
            {
                // todo: log, then rethrow (caller must expect and handle errors from repo)
                return null;
            }
        }

        public UrlMap UpdateShortUrl(int id, string newShortUrl)
        {
            try
            {
                var newUrlMap = UrlMap.Find(id);
                newUrlMap.ShortURL = newShortUrl;
                this.SaveChanges();
                return newUrlMap;
            }
            catch(Exception)
            {
                // todo: log, then rethrow (caller must expect and handle errors from repo)
                return null;
            }
        }
        #endregion
    }
}