using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class SharefileContext:DbContext
    {
        public DbSet<File> Files { get; set; }
		public DbSet<Folder> Folders { get; set; }
		
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"jasdkgfksdgfsdagfkjshdafkjhsadf");
        }
    }
}
