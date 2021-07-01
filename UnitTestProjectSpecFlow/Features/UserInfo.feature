Feature: User
	Receive data from existed user:
	id, email, first_name, last_name, avatar
	Receive StatusCode Ok 200
	Otherwise Not Found 400

@mytag
Scenario: User has full of data
	Given user Id
		| Id |
		| 2  |
	Then User info is received

Scenario: User is not found
	Given user Id
		| Id    |
		| 25555 |
	Then User info is Not Found

Scenario: Get list of users
	Given page number
		| Page |
		| 2    |
	Then Users info is received