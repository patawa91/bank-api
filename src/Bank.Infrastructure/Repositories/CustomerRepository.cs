using AutoMapper;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bank.Infrastructure.Repositories;

public sealed class CustomerRepository(BankDbContext context, IMapper mapper) : ICustomerRepository
{
    private readonly BankDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<Customer> GetByIdAsync(int customerId)
    {
        return _mapper.Map<Models.Customer?, Domain.Models.Customer>( await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId));
    }
}
