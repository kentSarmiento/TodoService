using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;
using ASPNetTodoService.Specs.Drivers;

namespace ASPNetTodoService.Specs.Steps
{
    [Binding]
    public class RetrieveTodoListSteps
    {
        private readonly TodoServiceRequestDriver _todoServiceAPI;
        private List<TodoItem>? _todoList;

        public RetrieveTodoListSteps(TodoServiceRequestDriver todoServiceAPI)
        {
            _todoServiceAPI = todoServiceAPI;
        }

        [Given(@"there are no entries in the todo list")]
        public async Task GivenThereAreNoEntriesInTheTodoList()
        {
            await _todoServiceAPI.ClearTodoItems();
        }

        [Given(@"user added a todo item in the todo list")]
        public async Task GivenUserAddedATodoItemInTheTodoList(Table table)
        {
            TodoItem todoItem = table.CreateInstance<TodoItem>();
            await _todoServiceAPI.RegisterTodoItem(todoItem);
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

        [Then(@"todo list retrieved contains the added todo item")]
        public void ThenTodoListRetrievedContainsTheAddedTodoItem(Table table)
        {
            TodoItem todoItem = table.CreateInstance<TodoItem>();

            _todoList.Should().NotBeEmpty()
                .And.OnlyContain(item => item.Equals(todoItem));
        }

    }
}
