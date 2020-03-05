using System;
using Xamarin.Forms;
namespace TodoXamarinForms
{
    class AddTodoItemViewModel : BaseFodyObservable
    {
        private INavigation _navigation;
        public string TodoTitle { get; set; }
        public string TodoDescription { get; set; }
        public string TodoLanguage { get; set; }
        public Command Save { get; set; }
        public AddTodoItemViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Save = new Command(HandleSave);
            Cancel = new Command(HandleCancel);
        }
   
        public async void HandleSave()
        {
            await App.TodoRepository.AddItem(new TodoItem { Title = TodoTitle, Description = TodoDescription, Language = TodoLanguage, TheDate = DateTime.Now });
            await _navigation.PopModalAsync();
        }
        public Command Cancel { get; set; }
        public async void HandleCancel()
        {
            await _navigation.PopModalAsync();
        }
    }
}

