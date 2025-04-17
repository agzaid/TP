using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        void Update(Role user);
    }
}
