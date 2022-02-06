using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using FluentAssertions;
using ASPNetTodoService.API.DTOs;
using ASPNetTodoService.Specs.Drivers;

namespace ASPNetTodoService.Specs.Steps
{
    [Binding]
    public class RetrieveTodoListSteps
    {
        private readonly TodoServiceRequestDriver _todoServiceAPI;
        private readonly ScenarioContext _scenarioContext;

        private List<TodoItemDTO>? _todoList;

        public RetrieveTodoListSteps(ScenarioContext injectedContext, TodoServiceRequestDriver todoServiceAPI)
        {
            _scenarioContext = injectedContext;
            _todoServiceAPI = todoServiceAPI;
        }

        [Given(@"there are no entries in the todo list")]
        public async Task GivenThereAreNoEntriesInTheTodoList()
        {
            await _todoServiceAPI.ClearTodoItems();
        }
        
        [Given(@"user added a todo item in the todo list")]
        public async Task GivenUserAddedATodoItemInTheTodoList()
        {
            await _todoServiceAPI.RegisterTodoItem();
        }
        
        [When(@"user retrieves the todo list")]
        public async Task WhenUserRetrievesTheTodoList()
        {
            _todoList = await _todoServiceAPI.RetrieveTodoItems();
        }
        
        [Then(@"todo list retrieved is empty")]
        public void ThenTodoListRetrievedIsEmpty()
        {
            _todoList.Should().BeEmpty();
        }
        
        [Then(@"todo list retrieved contains the todo item")]
        public void ThenTodoListRetrievedContainsTheTodoItem()
        {

            _todoList.Should().NotBeEmpty()
                .And.HaveCount(1);
        }
    }
}
