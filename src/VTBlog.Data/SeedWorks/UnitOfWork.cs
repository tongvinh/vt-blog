using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBlog.Core.SeedWorks;

namespace VTBlog.Data.SeedWorks
{
    public class UnitOfWork(VTBlogContext context) : IUnitOfWork
    {
        private readonly VTBlogContext _context = context;
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
