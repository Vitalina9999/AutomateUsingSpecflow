Feature: Register is succesful and unsuccesful

@mytag
Scenario: Register is succesful
	Given I have entered credentials
		| Email              | Password |
		| eve.holt@reqres.in | pistol   |
	When I sent request
	Then the response should provide id and token
	And status code is Ok

Scenario: Register is unsuccesful
	Given I have entered only email
		| Email              | 
		| eve.holt@reqres.in | 
	When I sent request
	Then the response has an error
	And status code is BadRequest