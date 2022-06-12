
***
WIP Samples and other junk below here
***

Issue task template

``` text
### Tasks

**Client Side**

- [ ] UX Design
- [ ] Blazor UI Prototype
- [ ] WPF UI Prototype
- [ ] Acceptance Test
- [ ] Blazor UI implementation
- [ ] WPF UI implementation
- [ ] UI View Model & Unit Test
- [ ] UI Service & Unit Test
- [ ] REST API client, TypeMapping & UnitTest

**Server Side**
- [ ] REST API Integration Test
- [x] REST API Controler, TypeMapping & Unit Test inc
- [ ] Domain MediatR Query & Unit Test
- [ ] Domain Validator & Unit Test
- [ ] DAL Repository, Specification & Unit Test
- [ ] DAL EF Schema Definition, TypeMapping
```

TODO: Turn into issues

- [ ] Security Auth by roles

User:

- [x] Get a list of currencies
  - [ ] Optionally excluding obsolete
- [ ] Get currency by ID

Admin:

- [ ] Create a new currency
- [ ] Update an existing currency, inc marking as obsolete
- [ ]  Delete a currency (assuming it is not in use)
- [ ] Security Auth by roles

User:

- [x] Get a list of banks
  - [ ] Optionally excluding obsolete
- [x] Get bank by ID

Admin:

- [ ] Create a new bank
- [ ] Update an existing bank, inc marking as obsolete
- [ ] Delete a bank (assuming it is not in use)

Users:

- [ ] Create a new transaction in a bank account
- [ ] Edit an existing transaction in a bank account
- [ ] Split a transaction on a given date between various credits and debits

Bank Account Balance Calculation

- Since the last accounting period
- Since the last reconciliation
- Since the last statement

Transaction date Vs book date

- [ ] Security Auth by roles

User:

- [ ] Create a new bank account

Owner:

- [ ] Get a list of owned bank accounts
  - [ ] Optionally excluding closed
- [ ] Get owned bank account by ID
- [ ] Update an existing owned bank account
- [ ] Close an existing owned bank account
- [ ] Delete an owned bank account (assuming it is not in use)

Admin:

- None.

User:

- [ ] Create a new bank account

Owner:

- [ ] Get a list of owned bank accounts
  - [ ] Optionally excluding closed
- [ ] Get owned bank account by ID
- [ ] Update an existing owned bank account
- [ ] Close an existing owned bank account
- [ ] Delete an owned bank account (assuming it is not in use)

Admin:

- None.
