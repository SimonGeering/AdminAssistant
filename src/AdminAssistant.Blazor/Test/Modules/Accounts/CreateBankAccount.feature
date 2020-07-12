Feature: CreateBankAccount
	In order to manage my accounts
	As a user
	I want to be be able to create a new bank account

Background:
  Given The application is loaded

@Functional
Scenario: Create a new bank account
	Given I am on the 'Accounts' screen
  And I click 'Add Account'
  And The Bank Account dialog Title reads 'New bank account'
	#And I have entered 70 into the calculator
	#When I press add
	#Then the result should be 120 on the screen
