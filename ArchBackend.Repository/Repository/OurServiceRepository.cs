    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ArchBackend.Core.Models;
    using ArchBackend.Core.Repositories;
    using ArchBackend.Core.Services;
    using ArchBackend.Repository.Repository.Generic;

    namespace ArchBackend.Repository.Repository
    {
        public class OurServiceRepository : GenericRepository<OurService> , IOurServiceRepository
        {
            private readonly AppDbContext _context;
            public OurServiceRepository(AppDbContext context) : base(context)
            {
                _context = context;
            }

        public async Task UpdateAsync(OurService ourService)
        {
            _context.OurServices.Update(ourService); 
            await _context.SaveChangesAsync(); 
        }

    }
}
