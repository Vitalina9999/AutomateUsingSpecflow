Feature: LoginUser
	Login is successful with correct credentials

@mytag
Scenario: Successful login
	Given I entered the following data into the login form:
		| Email              | Password   |
		| eve.holt@reqres.in | cityslicka |
	When I send request
	Then status code is Ok

Scenario: UnSuccessful login (incorrect email and password)
	Given I entered the incorrect data into the login form
		| Email          | Password |
		| ewewew@rees.in | retete   |
	When I send request
	Then status code is BadRequest

Scenario: UnSuccessful login (Missing password)
	Given I entered only email into the login form
		| Email          |
		| ewewew@rees.in |
	When I send request without password
	Then status code is BadRequest