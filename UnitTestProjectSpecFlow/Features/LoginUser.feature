Feature: LoginUser
	Login is successful with correct credentials

@mytag
Scenario: Successful login
	Given Correct credentials
		| Email              | Password   |
		| eve.holt@reqres.in | cityslicka |
	Then Login request is successful

Scenario: UnSuccessful login (incorrect email and password)
	Given I entered the incorrect data into the login form
		| Email          | Password |
		| ewewew@rees.in | retete   |
	When Login request is sent
	Then status code is BadRequest

Scenario: UnSuccessful login (Missing password)
	Given I entered only email into the login form
		| Email          |
		| ewewew@rees.in |
	When I send request without password
	Then Login response contains 404 BadRequest