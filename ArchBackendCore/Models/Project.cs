using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Repository.Models.Bridges;

namespace ArchBackend.Repository.Models
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        
        public string Tag { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public string ImagePath { get; set; }

        public List<ProjectCategory> ProjectCategories { get; set; }
    }
}
