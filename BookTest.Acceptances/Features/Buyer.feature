Feature: Buyer

As a Buyer I want to Create Admins

Scenario: Creating A Buyer 
	When I Create a Buyer with Following Values
	| Name | Family | NationlCode | PhoneNumber |
	| ali  | rezaie | 0613575024  | 09177034678 |
	Then ICan Login the Game and Do Jobs