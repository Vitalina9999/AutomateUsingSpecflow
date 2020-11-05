Feature: Register is succesful and unsuccesful

@mytag
Scenario: Register is succesful
	Given I have entered credentials
		| Email              | Password |
		| eve.holt@reqres.in | pistol   |
	Then the response should provide id and token

Scenario: Register is unsuccesful
	Given I have entered only email
		| Email              |
		| eve.holt@reqres.in |
	Then the response has an error