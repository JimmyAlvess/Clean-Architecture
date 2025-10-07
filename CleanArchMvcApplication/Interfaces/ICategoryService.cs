using CleanArchMvcApplication.Dtos;

namespace CleanArchMvcApplication.Interfaces
{
    public interface ICategoryService 
    {
        Task <ICollection<CategoryDTO>> GetCategories ();
        Task <CategoryDTO> GetById(int? id);
        Task Add(CategoryDTO category);
        Task Updade(CategoryDTO category);
        Task Remove(int? id);
    }
}
