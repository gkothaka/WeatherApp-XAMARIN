using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace TodoXamarinForms
{
    class TodoListViewModel : BaseFodyObservable
    {
        private INavigation navigation;
        public string TodoTitle { get; set; }
        public Command AddItem { get; set; }
        public Command Save { get; set; }
        public TodoListViewModel(INavigation navigation)
        {
            _navigation = navigation;

            GetGroupedTodoList().ContinueWith(t =>
            {
                GroupedTodoList = t.Result;
            });
            Delete = new Command<TodoItem>(HandleDelete);
            // Edit = new Command<TodoItem>(HandleEdit);
            ChangeIsCompleted = new Command<TodoItem>(HandleChangeIsCompleted);
            AddItem = new Command(HandleAddItem);
        }
        private INavigation _navigation;
        public ILookup<string, TodoItem> GroupedTodoList { get; set; }
        public string Title => "Gouthami TODO TASKS";
        private async Task<ILookup<string, TodoItem>> GetGroupedTodoList()
        {
           return (await App.TodoRepository.GetList())
                             .OrderBy(t => t.IsCompleted)
                            .ToLookup(t => t.IsCompleted ? "LIST PARTIALLY INTRESTED" : "LIST MOSTLY INTRESTED IN");
        }
        public Command<TodoItem> Delete { get; set; }
        public async void HandleDelete(TodoItem itemToDelete)
        {
            await App.TodoRepository.DeleteItem(itemToDelete);
            // Update displayed list
            GroupedTodoList = await GetGroupedTodoList();
        }
        public async void HandleEdit(TodoItem itemToDelete)
        {
            await App.TodoRepository.DeleteItem(itemToDelete);
            // Update displayed list
            GroupedTodoList = await GetGroupedTodoList();
        }
        public Command<TodoItem> ChangeIsCompleted { get; set; }
        public async void HandleChangeIsCompleted(TodoItem itemToUpdate)
        {
            await App.TodoRepository.ChangeItemIsCompleted(itemToUpdate);
            // Update displayed list
            GroupedTodoList = await GetGroupedTodoList();
        }

        public async void HandleAddItem()
        {
            await _navigation.PushModalAsync(new AddTodoItem());
        }
         
       
        public async Task RefreshTaskList()
        {
            GroupedTodoList = await GetGroupedTodoList();
        }
    }
}