using AutoMapper;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvcApplication.Dtos;
using CleanArchMvcApplication.Interfaces;

namespace CleanArchMvcApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Add(ProcutDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateProductAsync(productEntity);
        }

        public async Task<ProcutDTO> GetById(int? id)
        {
            var product = await _productRepository.GetById(id);
            return _mapper.Map<ProcutDTO>(product);
        }

        public async Task<ProcutDTO> GetProductCategory(int? id)
        {
            var product = await _productRepository.GetById(id);
            return _mapper.Map<ProcutDTO>(product);
        }

        public async Task<IEnumerable<ProcutDTO>> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProcutDTO>>(products);
        }

        public async Task Remove(ProcutDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.DeleteProductAsync(productEntity);
        }

        public async Task Update(ProcutDTO product)
        {
            var productEntity = _mapper.Map<Product>(product);
            await _productRepository.UpdateProductAsync(productEntity);
        }
    }
}
