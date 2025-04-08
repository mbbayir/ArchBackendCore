    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace ArchBackend.Core.Models.Bridges
    {
        public class OurServiceCategory
        {
            public int OurServiceId { get; set; }
            public OurService OurService { get; set; }
            public int CategoryId { get; set; }
            public Category Category { get; set; }
        }
    }
