Feature: Admin

As a Manager 
I want to create a new Admin
to Manage some Works in Library

Scenario: Creating Admin
	When I Create a new Admin with Following Values
	| Name | Family | DateofBirth | NationalCode | Email                 | UserName | Password  |
	| reza | ahmadi | 11/11/1344  | 0613575024   | javidleo.ef@gmail.com | javid43  | Javid43@@ |
	Then I Should Can See above Admin in AdminList

Scenario: Admin Forget Password
	Given an Admin Fill the password textbox more than two times 
	And cannot sign in 
	When click on Forget Password and pass him/her email 
	| emil | javidleo.ef@gmail.com |
	Then he/she should have recive an email with a link to change him/her password
	And can sign in with new password

Scenario: Admin Update Informations
	Given an Admin with following information want to update informations
	| Name | Family | DateofBirth | NationalCode | Email                 | UserName | Password  |
	| reza | ahmadi | 11/11/1380  | 0613575024   | javidleo.ef@gmail.com | javid43  | Javid43@@ |
	When he go to update form and pass new information and click the save button
	Then the imformation about that admin should be changed
