using Xunit;
using Moq;
using Bank.Domain.Models;
using Bank.Domain.Exceptions;

namespace Bank.Domain.Tests.Models;

public class AccountTests
{
    [Fact]
    public void Deposit_ValidDeposit_IncreasesBalance()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var deposit = new Deposit(1, 1, 50);

        // Act
        account.Deposit(deposit);

        // Assert
        Assert.Equal(150, account.Balance);
        Assert.Single(account.CompletedCustomerActions);
    }

    [Fact]
    public void Deposit_InvalidDeposit_Mismatch_Customer_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var deposit = new Deposit(2, 1, 50); // Invalid CustomerId

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Deposit(deposit));
    }

    [Fact]
    public void Deposit_InvalidDeposit_Mismatch_Account_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var deposit = new Deposit(1, 2, 50); // Invalid AccountId

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Deposit(deposit));
    }

    [Fact]
    public void Deposit_InvalidDeposit_Amount_Too_Low_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var deposit = new Deposit(1, 1, 0); // Invalid Amount

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Deposit(deposit));
    }

    [Fact]
    public void Withdraw_ValidWithdrawal_DecreasesBalance()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var withdrawal = new Withdrawal(1, 1, 50);

        // Act
        account.Withdraw(withdrawal);

        // Assert
        Assert.Equal(50, account.Balance);
        Assert.Single(account.CompletedCustomerActions);
    }

    [Fact]
    public void Withdraw_ValidWithdrawal_Mismatch_Customer_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var withdrawal = new Withdrawal(2, 1, 50); // Invalid CustomerId

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Withdraw(withdrawal));
    }

    [Fact]
    public void Withdraw_ValidWithdrawal_Mismatch_Account_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var withdrawal = new Withdrawal(1, 2, 50); // Invalid AccountId

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Withdraw(withdrawal));
    }

    [Fact]
    public void Withdraw_InvalidDeposit_Amount_Too_Low_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var withdrawal = new Withdrawal(1, 1, 0); // Invalid Amount

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Withdraw(withdrawal));
    }

    [Fact]
    public void Withdraw_InvalidWithdrawal_Amount_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var withdrawal = new Withdrawal(1, 1, 150); // Invalid Amount

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Withdraw(withdrawal));
    }

    [Fact]
    public void Close_ValidClose_ChangesStatus()
    {
        // Arrange
        var account = new Account(1, 1, 0, AccountType.Checking, AccountStatus.Open);
        var closeAccount = new AccountClose(1, 1, 0);

        // Act
        account.Close(closeAccount);

        // Assert
        Assert.Equal(AccountStatus.Closed, account.Status);
        Assert.Single(account.CompletedCustomerActions);
    }

    [Fact]
    public void Close_InValidClose__Mismatch_Customer_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var closeAccount = new AccountClose(2, 1, 0); // Invalid CustomerId

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Close(closeAccount));
    }

    [Fact]
    public void Close_InvalidClose_ThrowsException()
    {
        // Arrange
        var account = new Account(1, 1, 100, AccountType.Checking, AccountStatus.Open);
        var closeAccount = new AccountClose(1, 1, 0); // Invalid Amount

        // Act & Assert
        Assert.Throws<AccountCustomerValidationException>(() => account.Close(closeAccount));
    }
}
