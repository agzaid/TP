using Application.Common.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public IEmployeeRepository Employees { get; private set; }
        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new EmployeeRepository(_context);
            Users = new UserRepository(_context);
            Roles = new RoleRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
