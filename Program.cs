using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFGetStarted
{
	class Program
    {
        static void Main(string[] args)
        {

            using var connection = new SqlConnection("Data Source=.;Initial Catalog=Test;Integrated Security=True; MultipleActiveResultSets=True");
            using var db = new BloggingContext(connection);

            using var transaction = db.Database.BeginTransaction();


            // Create
            Console.WriteLine("Inserting a new blog");
            var newBlog = new Blog { Url = "http://blogs.msdn.com/adonet" + $"{Guid.NewGuid()}" };
            db.Add(newBlog);
            db.SaveChanges();

            using var db2 = new BloggingContext(connection);
            db2.Database.UseTransaction(transaction.GetDbTransaction());
            // Read
            Console.WriteLine("Querying for a blog");
            var blog = db2.Blogs.Where(x => x.BlogId == newBlog.BlogId)
                .OrderBy(b => b.BlogId)
                .First();

            // Update
            Console.WriteLine("Updating the blog and adding a post");
            blog.Url = "https://devblogs.microsoft.com/dotnet";
            blog.Posts.Add(
                new Post { Title = "Hello World", Content = $"{Guid.NewGuid()}" });
            db2.SaveChanges();

            transaction.Commit();

        }
    }

}
