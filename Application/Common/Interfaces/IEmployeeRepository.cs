using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void Update(Employee employee);
    }
}
