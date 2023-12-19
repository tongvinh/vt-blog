using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace VTBlog.Data
{
    public class VTBlogContextFactory : IDesignTimeDbContextFactory<VTBlogContext>
    {
        public VTBlogContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<VTBlogContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new VTBlogContext(builder.Options);

        }
    }
}
