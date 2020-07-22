﻿Feature: SingleUser
	Receive data from existed user:
	id, email, first_name, last_name, avatar
	Receive StatusCode 200
	Otherwise 400

@mytag
Scenario: User has full of data
	Given user Id
	And I have sent user id
	Then the result full of data
	Then the status code should be OK

Scenario: User is not found
	Given unexisted user Id
	And I have sent user id
	Then the status code should be Not Found