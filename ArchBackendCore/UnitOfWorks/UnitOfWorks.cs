using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchBackend.Core.UnitOfWorks
{
    public interface IUnitOfWorks
    {
        void Commit();
        Task CommitAsync();
    }
}