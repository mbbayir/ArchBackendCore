using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;

namespace ArchBackend.Core.Repositories
{
    public interface IOurServiceRepository:IGenericRepository<OurService>
    {
        Task<IEnumerable<OurService>> GetAllAsync();

        Task UpdateAsync(OurService ourService);

        Task DeleteAsync(OurService OurService);
    }
}
