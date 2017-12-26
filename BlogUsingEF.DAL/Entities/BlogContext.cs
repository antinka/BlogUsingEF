using BlogUsingEF.DAL.Entities;
using System.Data.Entity;

namespace BlogUsingEF.DAL.Entities
{
    public class BlogContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        public BlogContext()
            : base("DefaultConnection")
        {
        }
    }
        public class BlogDbInitializer : DropCreateDatabaseIfModelChanges<BlogContext>
        {
        }
}
