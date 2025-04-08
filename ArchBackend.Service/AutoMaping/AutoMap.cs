using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;
using ArchBackend.Core.ViewModels.Category;
using ArchBackend.Core.ViewModels.OurService;
using ArchBackend.Core.ViewModels.Project;
using AutoMapper;

namespace ArchBackend.Service.AutoMap
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<ProjectViewModel, Project>();
            CreateMap < ProjectUpdatesViewModel, Project>();

            CreateMap<CategoriesViewModel, Category>();

            CreateMap<OurServiceViewModel, OurService>();



        }
    }
}
