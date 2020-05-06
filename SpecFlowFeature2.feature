Feature: SpecFlowFeature2
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: User Creation
	Given I Create a New User
	| Name  | Email         | Password |
	| Rohit | rohit@gvg.com | vgvgvgg  |
	And ModelState is Correct
	Then The Api should return ok 
	
		