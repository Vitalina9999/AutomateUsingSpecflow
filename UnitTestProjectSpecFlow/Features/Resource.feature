Feature: Resource

@mytag
Scenario: Get a single resource
	Given resource Id
	When I sent resource request
	Then the responce should provide data resource
	And the resource responce status code should be OK

Scenario: Get a list of resources
	When I sent resource request with url
	Then the responce should provide list of data resource
	And the resource responce status code should be OK