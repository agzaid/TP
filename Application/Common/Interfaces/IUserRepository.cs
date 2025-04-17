using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);
    }
}
