using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBackend.Core.Models
{
    public class ContactMessage 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Bunu ekle
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Phone { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]

        public string Message { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
