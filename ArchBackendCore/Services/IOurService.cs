using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;

namespace ArchBackend.Core.Services
{
    public interface IOurService
    {
        Task<IEnumerable<OurService>> GetOurServiceAsync();
        Task<OurService> GetOurServiceIdAsync(int id);
        Task<OurService> AddOurServiceAsync(OurService ourService);
        Task<OurService> UpdateOurServiceAsync(OurService ourService);
        Task<bool> DeleteOurServiceAsync(int id);
    }
}
