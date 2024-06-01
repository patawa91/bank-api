using AutoMapper;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

public sealed class CustomerRepository(BankDbContext context, IMapper mapper) : IRepository<Domain.Models.Customer, Domain.Models.CustomerSearch>
{
    private readonly BankDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public Task<Customer> AddAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> Find(Domain.Models.CustomerSearch searchCriteria)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return _mapper.Map<Models.Customer?, Domain.Models.Customer>( await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id));
    }

    public Task<Customer> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> UpdateAsync(Customer entity)
    {
        throw new NotImplementedException();
    }
}
