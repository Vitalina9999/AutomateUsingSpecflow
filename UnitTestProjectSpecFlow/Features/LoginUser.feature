Feature: LoginUser
	Login is successful with correct credentials

@mytag
Scenario: Successful login
	Given a correct email
	And a correct password
	When I send request
	Then the response status code is 200
	Then the token is NotNull
	Then data is valid

Scenario: UnSuccessful login (incorrect email and password)
	Given an incorrect email
	And an incorrect password
	When I send request
	Then the response status code is 400

Scenario: UnSuccessful login (Missing password)
	Given a correct email
	And missing password
	When I send request without password
	Then the response status code is 400
#Then the user should be returned in the responce