Feature: Resource

@mytag
Scenario: Get a single resource
	Given resource number
		| Number |
		| 2      |
	When I sent resource request
	Then the response should provide data resource
	And the resource response status code should be OK

Scenario: Get a list of resources
	When I sent resource request with url
	Then the response should provide list of data resource
	And the resource response status code should be OK

Scenario:  Get a single resource Not Found
	Given resource number
		| Number |
		| 255555 |
	When I sent resource request with unknown url
	Then the resource response status code should be Not found