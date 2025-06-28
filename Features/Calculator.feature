Feature: Calculator

  Simple calculator for adding two numbers

  @mytag
  Scenario: Add two numbers
    Given the first number is 50
    And the second number is 70
    When the two numbers are added
    Then the result should be 120

  Scenario: Valid login 
    Given I navigate to the google page
    When I enter search results
    Then I should see the search results

Scenario Outline: Create Package and verify from UI
  Given I login to the application
  When I select the gym "<GymName>"
  And I create a package via API
  Then I should see the package created successfully in the application

Examples:
  | GymName                                 |
  | aniketnewzealand1.dev.au.membr.com      |

    