Feature: DelayRequest
	Waiting 3 secs

@mytag
Scenario: Get users info
	When send response with delay 3 secs
	Then status code is Ok