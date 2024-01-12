using Northwind_API.Repositories;
using Nortwind_API.Entities;

namespace Nortwind_API.Repository
{
    public interface IUnitOfWorkNorthwind
    {
        IRepository<Employee> EmployeeRepository { get; }
        IRepository<Order> OrderRepository { get; }
    }
}
