Feature: LoginUser
	Login is successful with correct credentials

@mytag
Scenario: Successful login
	Given A correct email
	And a correct password
	When I send request 
	Then User has response 200 and token
		Then data is valid

	Scenario: UnSuccessful login
	Given An incorrect email
	And an incorrect password
	When I send request 
	Then User has response 400 and token

