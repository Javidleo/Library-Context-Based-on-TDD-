Feature: User

As an Admin 
Iwant to Create Users 
to Give them Book or Take from them 

Scenario: Creating Book
	When I Create a new User with Following Values
	| Name | Family | Age | NationalCode | Email                 |
	| ali  | ahmadi | 16  | 0613575024   | javidleo.ef@gmail.com |
	Then I Should can See this User in UserList
