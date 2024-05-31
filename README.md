# bank-api
API that simulates banking operations

## How to run
* Clone the repository.
* Open the solution from the root directory in Visual Studio.
* Set `BankAPI` as the startup project.
* Run the `http` profile by selecting it from the top bar and clicking the green play button.
* This should run the API and host it at:
  * [https://localhost:7200](https://localhost:7200)
  * [http://localhost:5248](http://localhost:5248)
* It uses in-memory database, so while the app is running, the data will be persisted.
* Use Postman or any other API testing tool to test the API.

## How to test
### Unit Tests
* Run the command `dotnet test` from the `root\Tests\Bank.Domain.Tests` directory.
* Alternatively, in the solution explorer, tests are located in the Tests folder.

### Postman Tests
* A Postman collection is provided in the `root\Tests` directory.

## Further Improvements
* Security:
  * Evaluate the need for authentication and implement token-based authentication if necessary.
  * Review the current CORS policy to ensure it is sufficient.

## API Endpoints
### Create Account
```
curl --location 'https://localhost:7200/v1/account' \
--header 'Content-Type: application/json' \
--data '{
    "customerId": 1,
    "initialDeposit": 100,
    "accountTypeId": 2
}'
```

### Deposit
```
curl --location 'https://localhost:7200/v1/account/1/deposit' \
--header 'Content-Type: application/json' \
--data '{
    "customerId": 1,
    "accountId": 1,
    "amount": 10
}'
```

### Withdraw
```
curl --location 'https://localhost:7200/v1/account/1/withdrawal' \
--header 'Content-Type: application/json' \
--data '{
    "customerId": 1,
    "accountId": 1,
    "amount": 10
}'
```

### Close Account
```
curl --location --request PUT 'https://localhost:7200/v1/account/1/close' \
--header 'Content-Type: application/json' \
--data '{
    "customerId": 1,
    "accountId": 1
}'
```