Feature: Resource

@mytag
Scenario: Get a single resource
	Given resource Id
	When I sent resourse request
	Then the response should provide data resource
	And the resource response status code should be OK

Scenario: Get a list of resources
	When I sent resource request with url
	Then the response should provide list of data resource
	And the resource response status code should be OK

Scenario:  Get a single resource Not Found
	When I sent resource request with unknown url
	Then the resource response status code should be Not found