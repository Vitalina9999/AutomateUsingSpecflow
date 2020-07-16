Feature: LoginUser
	Login is successful with correct credentials

@mytag
Scenario: Successful login
	Given A correct email
	And a correct password
	When I send request 
	Then the user should be returned in the responce
	Then the response status code is 200
	Then the token is NotNull
		Then data is valid

	Scenario: UnSuccessful login
	Given An incorrect email
	And an incorrect password
	When I send request 
	Then the response status code is 400