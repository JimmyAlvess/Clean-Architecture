using CleanArchMvcApplication.Dtos;

namespace CleanArchMvcApplication.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProcutDTO>>GetProducts();
        Task<ProcutDTO> GetById(int? id);
        Task<ProcutDTO> GetProductCategory(int? id);
        Task Add(ProcutDTO product);
        Task Update(ProcutDTO product);
        Task Remove (ProcutDTO product);
    }
}
