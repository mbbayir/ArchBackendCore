using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models.Bridges;

namespace ArchBackend.Core.Models
{
    public class OurService : BaseEntity
    {
        public string Name { get; set; }

        public string Tag { get; set; }

        public string Description { get; set; }
        
        public  List<OurServiceCategory> OurServiceCategories { get; set; }
    }
}
