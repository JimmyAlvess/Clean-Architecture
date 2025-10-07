using AutoMapper;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvcApplication.Interfaces;
using CleanArchMvcApplication.Dtos;

namespace CleanArchMvcApplication.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryDTO>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<ICollection<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var category = await _categoryRepository.GetById(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task Add(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task Updade(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task Remove(int? id)
        {
            var category = await _categoryRepository.GetById(id);
            await _categoryRepository.DeleteCategoryAsync(category);
        }
    }
}
