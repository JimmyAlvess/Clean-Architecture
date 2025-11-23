using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvcApplication.Products.Command;
using MediatR;

namespace CleanArchMvcApplication.Handler
{
    public class ProductRemoveCommandHandler : IRequestHandler<ProductRemoveCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductRemoveCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
        {
            if( request is null)
            {
                throw new ApplicationException($"Error, object invalid");
            }

            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
                throw new ApplicationException($"Error could not be found");

            var result = await _productRepository.DeleteProductAsync(product);
            return result;
        }
    }
}
