Feature: SingleUser
	Receive data from existed user:
	id, email, first_name, last_name, avatar
	Receive StatusCode Ok 200
	Otherwise Not Found 400

@mytag
Scenario: User has full of data
	Given user Id
		| Id |
		| 2  |
	And I have sent user Id
	Then the result full of data
	Then the status code should be OK

Scenario: User is not found
	Given user Id
		| Id    |
		| 25555 |
	And I have sent user Id
	Then the status code should be Not Found

Scenario: Get list of users
	And I have sent request with page number
	Then the result user list with full of data
	Then the status code should be OK