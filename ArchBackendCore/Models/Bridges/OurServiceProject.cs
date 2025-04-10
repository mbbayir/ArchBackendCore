using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBackend.Core.Models.Bridges
{
    public class OurServiceProject
    {
        public int OurServiceId { get; set; }
        public OurService OurService { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}