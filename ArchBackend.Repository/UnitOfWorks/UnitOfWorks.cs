using System;
using System.Threading.Tasks;
using ArchBackend.Core.Models;
using ArchBackend.Core.UnitOfWorks;

namespace ArchBackend.Repository.UnitOfWorks
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly AppDbContext _context;

        public UnitOfWorks(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        async Task IUnitOfWorks.CommitAsync()
        {
            await CommitAsync();
        }
    }
}
