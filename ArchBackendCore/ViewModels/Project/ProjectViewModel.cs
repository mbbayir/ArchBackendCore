using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBackend.Core.ViewModels.Project
{
    public class ProjectViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Location { get; set; }

        public string Tag { get; set; }

        public string ImagePath { get; set; }
    }
}
