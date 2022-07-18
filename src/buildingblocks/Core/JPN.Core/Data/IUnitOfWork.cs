using System.Threading.Tasks;

namespace JPN.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
