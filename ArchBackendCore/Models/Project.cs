using System.ComponentModel.DataAnnotations.Schema;
using ArchBackend.Core.Models.Bridges;

namespace ArchBackend.Core.Models
{
    public class Project :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public string Location { get; set; }

        public string Tag { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        public string DetailPageUrl => $"/project/detail/{Id}";
        public List<ProjectCategory> ProjectCategories { get; set; }
        public List<OurServiceProject> OurServiceProjects { get; set; }

        public List<Picture> Picture { get; set; }
    }
}
