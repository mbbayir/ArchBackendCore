using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Repository.Models;

namespace ArchBackend.Core.UnitOfWorks
{
    public interface IUnitOfWorks
    {
        void Commit();
        Task CommitAsync();
    }
}