Feature: DelayRequest
	Waiting 3 secs

@mytag
Scenario: Get users info
	When send response with delay
	Then the result status code is Ok