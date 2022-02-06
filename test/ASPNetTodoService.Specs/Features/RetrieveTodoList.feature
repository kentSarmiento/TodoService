Feature: Retrieve Todo List
  As a User,
  I want to retrieve my todo list
  So that I can see information about my tasks

Background:
  Given there are no entries in the todo list

Scenario: Retrieve todo list when there are no entries
  When user retrieves the todo list
  Then todo list retrieved is empty

Scenario: Retrieve todo list when there are existing entries
  Given user added a todo item in the todo list
  When user retrieves the todo list
  Then todo list retrieved contains the todo item