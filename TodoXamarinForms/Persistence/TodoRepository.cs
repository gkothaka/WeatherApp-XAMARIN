using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace TodoXamarinForms.Persistence
{
    public class TodoRepository
    {
        private readonly SQLiteAsyncConnection _database;
        public string _todoTitle;
        public TodoRepository()
        {
             _todoTitle = "Title";
            _database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite1.db3"));
            _database.CreateTableAsync<TodoItem>().Wait();
        }
        private List<TodoItem> _seedTodoList = new List<TodoItem>
        {
            new TodoItem { Title = "Create First Todo", IsCompleted = true},
            new TodoItem { Title = "Run a Marathon", Description="Hello" },
            new TodoItem { Title = "Create TodoXamarinForms blog post"},
        };
        public async Task<List<TodoItem>> GetList()
        {
            //TODO: remove once Add is implemented
            if ((await _database.Table<TodoItem>().CountAsync() == 0))
            {
                await _database.InsertAllAsync(_seedTodoList);
            }
            return await _database.Table<TodoItem>().ToListAsync();
        }
        public Task DeleteItem(TodoItem itemToDelete)
        {
            return _database.DeleteAsync(itemToDelete);
        }
         public Task SetTodo(TodoItem itemToEdit, string str)
         {
         itemToEdit.Title = str;  
        return _database.UpdateAsync(itemToEdit);
         }
         public Task EditItem(TodoItem itemToEdit)
         {
            DateTime localDate = DateTime.Now;
            itemToEdit.TheDate = localDate;
            return _database.UpdateAsync(itemToEdit);
         }
        public Task ChangeItemIsCompleted(TodoItem itemToChange)
        {
            DateTime localDate = DateTime.Now;
            itemToChange.TheDate = localDate;
            itemToChange.IsCompleted = !itemToChange.IsCompleted;
            return _database.UpdateAsync(itemToChange);
        }
        public Task AddItem(TodoItem itemToAdd)
        {
            return _database.InsertAsync(itemToAdd);
        }
    }
}