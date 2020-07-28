Feature: DelayRequest
	Waiting 3 secs

@mytag
Scenario: Get user info
	Given list of users
	When send response with delay 
	Then the result status code is Ok