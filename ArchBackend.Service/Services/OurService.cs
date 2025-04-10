using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;
using ArchBackend.Core.Repositories;
using ArchBackend.Core.Services;
using ArchBackend.Core.UnitOfWorks;
using ArchBackend.Repository.Repository;
using ArchBackend.Repository.UnitOfWorks;

namespace ArchBackend.Service.Services
{
    public class OurService : IOurService
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOurServiceRepository _ourServiceRepository;

        public OurService(IUnitOfWork unitOfWork,IOurServiceRepository ourServiceRepository)
        {
            _unitOfWork = unitOfWork;
            _ourServiceRepository = ourServiceRepository;
        }
        public Task<IEnumerable<Core.Models.OurService>> GetOurServiceAsync()
        {
            return _ourServiceRepository.GetAllAsync();
        }

        public Task<Core.Models.OurService> GetOurServiceIdAsync(int id)
        {
            return _ourServiceRepository.GetByIdAsync(id);
        }

        public async Task<Core.Models.OurService> AddOurServiceAsync(Core.Models.OurService ourService)
        {

           await _ourServiceRepository.AddAsync(ourService);
            await _unitOfWork.CommitAsync();
            return ourService;

        }
        public async Task<bool> DeleteOurServiceAsync(int id)
        {
            var ourservice = await _ourServiceRepository.GetByIdAsync(id);
            if (ourservice == null)
                return false;

            await _ourServiceRepository.DeleteAsync(ourservice);
            await _unitOfWork.CommitAsync();
            return true;
        }

       
        public Task<Core.Models.OurService> UpdateOurServiceAsync(Core.Models.OurService ourService)
        {
            var ourservice = _ourServiceRepository.GetByIdAsync(ourService.Id);
            if (ourservice == null)
                return null;
            _ourServiceRepository.UpdateAsync(ourService);
            _unitOfWork.CommitAsync();
            return ourservice;

        }
    }
}
