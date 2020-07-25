Feature: UserCRUD
	 Create, Read, Update, Remove

@mytag
Scenario: Create a user with name and job
	Given name and job
	When I send request with method Post
	Then the result should contains name, job, id, createdAt
	And the status code should be Created