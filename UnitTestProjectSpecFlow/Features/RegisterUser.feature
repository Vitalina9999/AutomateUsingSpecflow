﻿Feature: Register is succesful and unsuccesful

@mytag
Scenario: Register is succesful
	Given I have entered credentials
	When I sent request
	Then the response should provide id and token
	And the register response status code should be OK

Scenario: Register is unsuccesful
	Given I have entered only email
	When I sent request
	Then the response has an error
	And the register response status code should be BadRequest