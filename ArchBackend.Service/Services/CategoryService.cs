using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchBackend.Core.Models;
using ArchBackend.Core.Repositories;
using ArchBackend.Core.Services;
using ArchBackend.Core.UnitOfWorks;
using ArchBackend.Repository.UnitOfWorks;

namespace ArchBackend.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWorks;
        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWorks = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        
        public async Task<Category> GetCategoryIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task<Category> AddCategoryAsync(Category category)
        {

            await _categoryRepository.AddAsync(category);
            await _unitOfWorks.CommitAsync();
            return category;

        }

      
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return false;

            await _categoryRepository.DeleteAsync(category);
            await _unitOfWorks.CommitAsync();
            return true;
        }



        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(category.Id);
            if (existingCategory == null)
                return null;

            existingCategory.Name = category.Name; // diğer alanları da güncelle

            await _categoryRepository.UpdateAsync(existingCategory);
            await _unitOfWorks.CommitAsync();

            return existingCategory;
        }


    }
}
