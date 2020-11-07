Feature: Resource

@mytag
Scenario: Get a single resource
	Given resource number
		| Number |
		| 2      |
	Then Resource provide company info

Scenario: Get a list of resources
	Then Resources is received

Scenario:  Get a single resource Not Found
	Given resource number
		| Number |
		| 255555 |
	Then Resource is Not Found