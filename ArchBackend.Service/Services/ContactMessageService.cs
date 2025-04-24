using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchBackend.Core.Models;
using ArchBackend.Core.Repositories;
using ArchBackend.Core.Services;
using ArchBackend.Repository.Repository;

namespace ArchBackend.Service.Services
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IContactMessageRepository _contactMessageRepository;

        public ContactMessageService(IContactMessageRepository contactMessageRepository)
        {
            _contactMessageRepository = contactMessageRepository;
        }

        public async Task CreateMessage(ContactMessage message)
        {
            await _contactMessageRepository.AddAsync(message);
        }
        public async Task<List<ContactMessage>> GetAllMessages()
        {
            return await _contactMessageRepository.GetAllAsync();
        }
    }
}
