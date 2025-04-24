using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;
using ArchBackend.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArchBackend.Repository.Repository
{
    public class ContactMessageRepository : IContactMessageRepository
    {
        private readonly AppDbContext _context;

        public ContactMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ContactMessage message)
        {
            await _context.ContactMessages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ContactMessage>> GetAllAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(c => c.SentAt)
                .ToListAsync();
        }
    }


}
