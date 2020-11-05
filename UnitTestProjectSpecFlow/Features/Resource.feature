Feature: Resource

@mytag
Scenario: Get a single resource
	Given resource number
		| Number |
		| 2      |
	When I sent resource request
	Then the response should provide data resource
	And status code is Ok

Scenario: Get a list of resources
	When GetResources request is sent
	Then Resources is received	

Scenario:  Get a single resource Not Found
	Given resource number
		| Number |
		| 255555 |
	When I sent resource request with unknown url
	Then status code is Not Found