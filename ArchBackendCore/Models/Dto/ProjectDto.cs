using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBackend.Core.Models.Dto
{
    public record ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Tag { get; set; }
        public string ImagePath { get; set; }
        public List<string> Categories { get; set; }

        //List OurServices
        public List<string> OurServices { get; set; }
    }
}
