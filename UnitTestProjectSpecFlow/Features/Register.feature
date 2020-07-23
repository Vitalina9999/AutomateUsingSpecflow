Feature: Register is succesful and unsuccesful

@mytag
Scenario: Register is succesful
	Given I have entered credentials
	When I sent request
	Then the responce should provide id and token
	And the register responce status code should be OK

Scenario: Register is unsuccesful
	Given I have entered only email
	When I sent request
	Then the responce has an error
	And the register responce status code should be BadRequest