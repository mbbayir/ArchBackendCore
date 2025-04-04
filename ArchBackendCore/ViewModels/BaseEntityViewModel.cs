    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ArchBackend.Core.ViewModels
    {
        public class BaseEntityViewModel
        {
            public int Id { get; set; }
            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
            public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        }
    }
