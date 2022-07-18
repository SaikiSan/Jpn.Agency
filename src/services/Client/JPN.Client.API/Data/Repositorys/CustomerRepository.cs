using System.Collections.Generic;
using System.Threading.Tasks;
using JPN.Core.Data;
using JPN.Customer.API.Models;
using JPN.Customer.API.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JPN.Customer.API.Data.Repositorys
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Models.CustomerModel customer)
        {
            _context.Add(customer);
        }

        public void Update(Models.CustomerModel customer)
        {
            _context.Update(customer);
        }

        public void Delete(Models.CustomerModel customer)
        {
            _context.Remove(customer);
        }

        public async Task<IEnumerable<Models.CustomerModel>> GetAll()
        {
            return await _context.Customer.AsNoTracking().ToListAsync();
        }

        public Task<Models.CustomerModel> GetByCpf(string cpf)
        {
            return _context.Customer.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public Task<Models.CustomerModel> GetByEmail(string email)
        {
            return _context.Customer.FirstOrDefaultAsync(c => c.Email.Adress == email);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
