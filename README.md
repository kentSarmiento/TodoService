# Todo Web API [![Build status](https://badge.buildkite.com/2c96634ba24ba3900507e530b0eabbb6b7894162645a743719.svg)](https://buildkite.com/kent-sarmiento/aspnet-todo-service)

## Introduction  

This is a simple implementation for a Todo API using ASP.Net Core 6.0.

## What's contained in this project

*  [Source Implementation](src)  
Source Implementation is divided into three Domains based on DDD-oriented Microservice architecture:  
   * [API](src/ASPNetTodoService.API)
   * [Domain](src/ASPNetTodoService.Domain)
   * [Infrastructure](src/ASPNetTodoService.Infrastructure)
*  [Test Implementation](test)  
Test Implementation contains two levels of test:  
   * [Unit Test](test/ASPNetTodoService.UnitTest)
   * [Acceptance Test](test/ASPNetTodoService.Specs)

## Project Setup

The following is the recommended setup for Development:

* Visual Studio / Visual Studio Code
* dotnet SDK
   ```
   > dotnet --version
   6.0.301
   ```
* dotnet Tools
   * Entity Framework CLI tool
     ```
     dotnet tool install --global dotnet-ef
     ```
   * ReportGenerator tool for Coverage report
     ```
     dotnet tool install -g dotnet-reportgenerator-globaltool
     ```
   * Generator for shareable HTML Gherkin feature execution report
     ```
     dotnet tool install -g SpecFlow.Plus.LivingDoc.CLI
     ```
* Trust the HTTPS development certificate by running the following command:
   ```
   dotnet dev-certs https --trust
   ```

## Build the Project

* If using Visual Studio, the project can be built using "Build Solution" option.
* If using Visual Studio Code, the project can be built from terminal using the following command:
  ```
  dotnet build
  ```

## Test the Project

* If using Visual Studio, the project can be tested using "Test Explorer" option.
* If using Visual Studio Code, the project can be tested from terminal using the following commands.
   * Static Testing
     ```
     dotnet build -warnaserror
     ```
   * Unit Testing
     ```
     dotnet test .\test\ASPNetTodoService.UnitTest\
     ```
   * Unit Testing with Coverage
     ```
     dotnet test .\test\ASPNetTodoService.UnitTest\ --collect:"XPlat Code Coverage"
     reportgenerator -reports:<location of coverage.cobertura.xml> -targetdir:"coveragereport" -reporttypes:Html
     ```
   * Acceptance Testing
     ```
     dotnet test .\test\ASPNetTodoService.Specs\
     ```
   * Acceptance Testing with LivingDoc output
     ```
     dotnet test .\test\ASPNetTodoService.Specs\
     livingdoc test-assembly <location of test assmebly file> -t <location of TestExecution.json file>
     ```

## Run the Project

* If using Visual Studio, the project can be run using "Ctrl+F5" option (without debugger) or "F5" option (with debugger).  
Visual Studio launches the default browser and navigates to the swagger documentation for the Web API.
* If using Visual Studio Code, the project can be run using the following command:
   ```
   dotnet run --project .\src\ASPNetTodoService.API\ASPNetTodoService.API.csproj
   ```

## CI/CD

The project currently uses Jenkins Pipeline for automation of build and test in Jenkins server.  
* [Jenkinsfile](Jenkinsfile) for automation
