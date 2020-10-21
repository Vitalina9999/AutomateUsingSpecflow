Feature: UserCRUD
	 Create, Read, Update, Remove

@mytag
Scenario: Create a user with name and job
	Given user with name and job
		| FirstName |  | Job        |
		| Vitalina  |  | QAautomate |
	When I send request with method Post
	Then the result should contains name, job, id, createdAt
	And the status code should be Created

Scenario: Delete the user
	Given user id
		| Id  |
		| 897 |
	When I send request with method Delete
	Then the status code should be No Content

Scenario: Put a user
	Given user id
		| Id  |
		| 897 |
	When I send request with method Put
	Then the result should contains name, job, updatedAt
	And the status code should be Ok

Scenario: Update a user
	Given user with name and job
		| Id  |  | FirstName |  | Job         |
		| 897 |  | Vitalina  |  | Programmist |
	When I send request with changed parameters(name, job) method Patch
	Then the result should contains name, job, updatedAt
	And the status code should be Ok