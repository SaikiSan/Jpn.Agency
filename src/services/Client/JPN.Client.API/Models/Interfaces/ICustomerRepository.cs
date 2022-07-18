using System.Collections.Generic;
using System.Threading.Tasks;
using JPN.Core.Data;

namespace JPN.Customer.API.Models.Interfaces
{
    public interface ICustomerRepository : IRepository<CustomerModel>
    {
        void Add(CustomerModel customer);
        void Update(CustomerModel cliente);
        void Delete(CustomerModel cliente);
        Task<IEnumerable<CustomerModel>> GetAll();
        Task<CustomerModel> GetByCpf(string cpf);
        Task<CustomerModel> GetByEmail(string email);
    }
}
