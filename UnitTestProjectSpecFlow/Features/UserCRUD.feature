Feature: UserCRUD
	 Create, Read, Update, Remove

@mytag
Scenario: Create a user with name and job
	Given name and job
	When I send request with method Post
	Then the result should contains name, job, id, createdAt
	And the status code should be Created

Scenario: Delete the user
	Given existed user id
	When I send request with method Delete
	Then the status code should be No Content
	
Scenario: Put a user
	Given user
	When I send request with method Put
	Then the result should contains name, job, createdAt
	And the status code should be Ok