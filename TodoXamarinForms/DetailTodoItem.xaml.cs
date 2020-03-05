using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoXamarinForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TodoXamarinForms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailTodoItem : ContentPage
    {
        public DetailTodoItem()
        {
            InitializeComponent();
        }
        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.TodoRepository.EditItem(todoItem);
            await Navigation.PopAsync();
        }
        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}