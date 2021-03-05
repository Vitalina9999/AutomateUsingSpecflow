Feature: UserCrud
	 Create, Read, Update, Remove

@mytag
Scenario: Create a user with name and job
	Given user with name and job
		| FirstName |  | Job        |
		| Vitalina  |  | QAautomate |
	Then user is created

Scenario: Delete the user
	Given user id
		| Id  |
		| 897 |
	Then user is deleted

Scenario: Put a user
	Given user id
		| Id  |
		| 897 |
	Then user is updated

Scenario: Update a user's job
	Given user with name and job
		| Id  |  | FirstName |  | Job         |
		| 897 |  | Vitalina  |  | Programmist |
	Then user's job is updated