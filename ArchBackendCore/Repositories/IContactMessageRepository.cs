using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;

namespace ArchBackend.Core.Repositories
{
    public interface IContactMessageRepository
    {
        Task AddAsync(ContactMessage message);
       Task <List<ContactMessage>> GetAllAsync();
    }
}
