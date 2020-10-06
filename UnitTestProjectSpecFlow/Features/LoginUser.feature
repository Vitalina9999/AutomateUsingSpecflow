Feature: LoginUser
	Login is successful with correct credentials

@mytag
Scenario: Successful login
	Given I entered the following data into the login form:
		| Email              | Password   |
		| eve.holt@reqres.in | cityslicka |
	When I send request
	Then the result status code should be Ok

Scenario: UnSuccessful login (incorrect email and password)
	Given I entered the incorrect data into the login form
		| Email          | Password |
		| ewewew@rees.in | retete   |
	When I send request
	Then the response is BadRequest

Scenario: UnSuccessful login (Missing password)
	Given a correct email
	And missing password
	When I send request without password
	Then the response status code is 400
#Then the user should be returned in the responce