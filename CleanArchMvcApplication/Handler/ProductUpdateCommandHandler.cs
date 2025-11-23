using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvcApplication.Products.Command;
using MediatR;

namespace CleanArchMvcApplication.Handler
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ApplicationException($"Error object invalid");
            }
            
            var product = await _productRepository.GetByIdAsync(request.Id);

            if(product is null)
            {
                throw new ApplicationException("Error could not be found");
            }

            product.Update(request.Nome, request.Description, request.Price, request.Stock,
                           request.Image, request.CategoryId);

            return await _productRepository.UpdateProductAsync(product);
        }
    }
}
