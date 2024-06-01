using Moq;
using Bank.Domain.Models;
using Bank.Domain.Repositories;
using Bank.Domain.Services;
using Bank.Domain.Exceptions;
using System.Linq.Expressions;


namespace Bank.Domain.Tests.Services;

public class AccountCustomerServiceTests
{
    [Fact]
    public async Task CloseAccountAsync_Valid_CallsRepositoryAndReturnsAccount()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var accountClose = new AccountClose(1, 1, 0);
        var account = new Account(1, 1, 0, AccountType.Checking, AccountStatus.Open);

        mockAccountRepo.Setup(repo => repo.GetByIdAsync(accountClose.AccountId)).ReturnsAsync(account);
        mockAccountRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Account>())).ReturnsAsync((Account a) => a);

        // Act
        var result = await service.CloseAccountAsync(accountClose);

        // Assert
        mockAccountRepo.Verify(repo => repo.GetByIdAsync(accountClose.AccountId), Times.Once());
        mockAccountRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Account>()), Times.Once());
        Assert.Equal(AccountStatus.Closed, result.Status);
    }

    [Fact]
    public async Task CloseAccountAsync_InValid_No_Account_Found()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var accountClose = new AccountClose(1, 1, 0);

        mockAccountRepo.Setup(repo => repo.GetByIdAsync(accountClose.AccountId)).ReturnsAsync(() => null!);

        // Act & Assert
        await Assert.ThrowsAsync<AccountCustomerValidationException>(async () => await service.CloseAccountAsync(accountClose));
    }

    [Fact]
    public async Task CreateAccountAsync_Valid_CallsRepositoryAndReturnsAccount()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var customer = new Customer(1, "John", "Doe", "12345");
        var account = new Account(1, 1, 0, AccountType.Checking, AccountStatus.Open);

        mockCustomerRepo.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(customer);
        mockAccountRepo.Setup(repo => repo.AddAsync(It.IsAny<Account>())).ReturnsAsync((Account a) => a);
        mockAccountRepo.Setup(repo => repo.Find(It.IsAny<AccountSearch>())).ReturnsAsync([account]);

        // Act
        var result = await service.CreateAccountAsync(customer.CustomerId, 100, AccountType.Checking);

        // Assert
        mockCustomerRepo.Verify(repo => repo.GetByIdAsync(customer.CustomerId), Times.Once());
        mockAccountRepo.Verify(repo => repo.AddAsync(It.IsAny<Account>()), Times.Once());
        Assert.Equal(AccountStatus.Open, result.Status);
        Assert.Equal(AccountType.Checking, result.AccountType);
        Assert.Equal(100, result.Balance);
        Assert.Equal(customer.CustomerId, result.CustomerId);
    }

    [Fact]
    public async Task CreateAccountAsync_InValid_Initial_Balance_Too_Low()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var customer = new Customer(1, "John", "Doe", "12345");
        var account = new Account(1, 1, 0, AccountType.Checking, AccountStatus.Open);

        mockCustomerRepo.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(customer);
        mockAccountRepo.Setup(repo => repo.AddAsync(It.IsAny<Account>())).ReturnsAsync((Account a) => a);
        mockAccountRepo.Setup(repo => repo.Find(It.IsAny<AccountSearch>())).ReturnsAsync([account]);

        // Act & Assert
        await Assert.ThrowsAsync<AccountCustomerValidationException>(async () => await service.CreateAccountAsync(customer.CustomerId, 50, AccountType.Checking));
    }

    [Fact]
    public async Task CreateAccountAsync_InValid_Wrong_Account_Type()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var customer = new Customer(1, "John", "Doe", "12345");
        var account = new Account(1, 1, 0, AccountType.Checking, AccountStatus.Open);

        mockCustomerRepo.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(customer);
        mockAccountRepo.Setup(repo => repo.AddAsync(It.IsAny<Account>())).ReturnsAsync((Account a) => a);
        mockAccountRepo.Setup(repo => repo.Find(It.IsAny<AccountSearch>())).ReturnsAsync([account]);

        // Act & Assert
        await Assert.ThrowsAsync<AccountCustomerValidationException>(async () => await service.CreateAccountAsync(customer.CustomerId, 100, AccountType.Unknown));
    }

    [Fact]
    public async Task CreateAccountAsync_InValid_First_Account_Must_Be_Savings()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var customer = new Customer(1, "John", "Doe", "12345");

        mockCustomerRepo.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(customer);
        mockAccountRepo.Setup(repo => repo.AddAsync(It.IsAny<Account>())).ReturnsAsync((Account a) => a);
        mockAccountRepo.Setup(repo => repo.Find(It.IsAny<AccountSearch>())).ReturnsAsync([]);

        // Act & Assert
        await Assert.ThrowsAsync<AccountCustomerValidationException>(async () => await service.CreateAccountAsync(customer.CustomerId, 100, AccountType.Checking));
    }

    [Fact]
    public async Task CreateAccountAsync_InValid_No_Customer()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var customer = new Customer(1, "John", "Doe", "12345");

        mockCustomerRepo.Setup(repo => repo.GetByIdAsync(customer.CustomerId)).ReturnsAsync(() => null!);

        // Act & Assert
        await Assert.ThrowsAsync<AccountCustomerValidationException>(async () => await service.CreateAccountAsync(customer.CustomerId, 100, AccountType.Unknown));
    }

    [Fact]
    public async Task DepositAsync_Valid_CallsRepositoryAndReturnsAccount()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var deposit = new Deposit(1, 1, 50);
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);

        mockAccountRepo.Setup(repo => repo.GetByIdAsync(deposit.AccountId)).ReturnsAsync(account);
        mockAccountRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Account>())).ReturnsAsync((Account a) => a);

        // Act
        var result = await service.DepositAsync(deposit);

        // Assert
        mockAccountRepo.Verify(repo => repo.GetByIdAsync(deposit.AccountId), Times.Once());
        mockAccountRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Account>()), Times.Once());
        Assert.Equal(150, result.Balance);
    }

    [Fact]
    public async Task DepositAsync_InValid_No_Account()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var deposit = new Deposit(1, 1, 50);
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);

        mockAccountRepo.Setup(repo => repo.GetByIdAsync(deposit.AccountId)).ReturnsAsync(() => null!);

        // Act & Assert
        await Assert.ThrowsAsync<AccountCustomerValidationException>(async () => await service.DepositAsync(deposit));
    }

    [Fact]
    public async Task WithdrawAsync_ValidWithdrawal()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var withdrawal = new Withdrawal(1, 1, 50);
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);

        mockAccountRepo.Setup(repo => repo.GetByIdAsync(withdrawal.AccountId)).ReturnsAsync(account);
        mockAccountRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Account>())).ReturnsAsync((Account a) => a);

        // Act
        var result = await service.WithdrawAsync(withdrawal);

        // Assert
        mockAccountRepo.Verify(repo => repo.GetByIdAsync(withdrawal.AccountId), Times.Once());
        mockAccountRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Account>()), Times.Once());
        Assert.Equal(50, result.Balance);
    }

    [Fact]
    public async Task WithdrawAsync_InValid_No_Account()
    {
        // Arrange
        var mockAccountRepo = new Mock<IRepository<Bank.Domain.Models.Account, Bank.Domain.Models.AccountSearch>>();
        var mockCustomerRepo = new Mock<IRepository<Bank.Domain.Models.Customer, Bank.Domain.Models.CustomerSearch>>();
        var service = new AccountCustomerService(mockAccountRepo.Object, mockCustomerRepo.Object);
        var withdrawal = new Withdrawal(1, 1, 50);
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);

        mockAccountRepo.Setup(repo => repo.GetByIdAsync(withdrawal.AccountId)).ReturnsAsync(() => null!);
        mockAccountRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Account>())).ReturnsAsync((Account a) => a);

        // Act & Assert
        await Assert.ThrowsAsync<AccountCustomerValidationException>(async () => await service.WithdrawAsync(withdrawal));
    }
}
