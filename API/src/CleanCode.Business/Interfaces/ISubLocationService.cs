using CleanCode.Business.Models;
using CleanCode.Core.Models;

namespace CleanCode.Business.Interfaces
{
    public interface ISubLocationService
    {
        Task<PagedList<SubLocationModel>> Get(PaginationQuery paginationQuery);
        Task<SubLocationModel> GetById(int id);
        Task<SubLocationModel> Create(SubLocationModel sublocationModel);
        Task Update(SubLocationModel sublocationModel);
        Task Delete(SubLocationModel sublocationModel);
    }
}