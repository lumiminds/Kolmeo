using System.Threading.Tasks;

namespace Kalmeo.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        Task SaveChangesAsync();
    }
}
