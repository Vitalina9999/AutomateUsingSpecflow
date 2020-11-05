Feature: DelayRequest
	Waiting 3 secs

@mytag
Scenario: Get user info
	When Get user info request is sent with delay 5 sec
	Then Response is delayed for a 5 secs