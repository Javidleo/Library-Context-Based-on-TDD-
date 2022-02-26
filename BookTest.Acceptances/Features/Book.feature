Feature: Creating Book
	As a Admin
	I want to Add a new Book To Online Library

Scenario: Creating a new Book
	When I Create a New Book with Following Values
	| Name         | AuthorName    | DateOfAdding |
	| 'raze bagha' | 'reza ahmadi' | '11/12/1399  |
	Then I Can See above Book in My BookList
