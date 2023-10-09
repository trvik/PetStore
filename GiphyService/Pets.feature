Feature: Pets

Scenario: 01 - Create pet
	Given I create a 'Dog' pet with 'Rex' name
	Then I get response with 'OK' status
	And pet is created with correct values
		| Object   | Field | Value |
		| Pet      | Name  | Rex   |
		| Category | Name  | Dog   |
		| Tags     | Name  | PXYF  |

Scenario: 02 - Update pet
	Given I create a 'Dog' pet with 'Rex' name
	And I get response with 'OK' status
	When I update pet fields to value
		| Object   | Field | CurrentValue | ValueToUpdate |
		| Pet      | Name  | Rex          | FunnyBoy      |
		| Category | Name  | Dog          | SmallDog      |
		| Tags     | Name  | PXYF         | VBYW          |
	Then pet has correct updated value
		| Object   | Field | Value    |
		| Pet      | Name  | FunnyBoy |
		| Category | Name  | SmallDog |
		| Tags     | Name  | VBYW     |

Scenario Outline: 03 - Get pets by Status
	Given I find the pets by '<Status>' status
	Then I get response with 'OK' status
	And All pets have '<Status>' status

	Examples:
		| Status    |
		| available |
		| sold      |