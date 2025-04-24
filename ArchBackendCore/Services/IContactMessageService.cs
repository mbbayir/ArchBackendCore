using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;

namespace ArchBackend.Core.Services
{
    public interface IContactMessageService
    {
        Task CreateMessage(ContactMessage message);

        Task<List<ContactMessage>> GetAllMessages();
    }

}
