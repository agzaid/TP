namespace Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }

        void Save();

    }
}
