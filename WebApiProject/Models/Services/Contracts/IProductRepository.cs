using ResponseFramework;
using WebApiProject.Models.DomainModels.ProductAggregates;

namespace WebApiProject.Models.Services.Contracts
{
    public interface IProductRepository
    {
        Task<IResponse<bool>> Insert(Product product);

        Task<IResponse<Product>> SelectById(Guid id);
        Task<IResponse<List<Product>>> SelectAll();

        Task<IResponse<bool>> Update(Product product);
        Task<IResponse<bool>> Delete(Product product);

    }
}
