using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBackend.Core.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
