Feature: Resource
	

@mytag
Scenario: Get single resource
	Given resource Id
	When I sent resource request
	Then the responce should provide data resource
	And the resource responce status code should be OK
