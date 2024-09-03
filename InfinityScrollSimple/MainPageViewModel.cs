using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InfinityScrollSimple.Services;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace InfinityScrollSimple
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy;


        private HttpClient httpClient;

        List<Todo> todoList = new();
        int PageSize = 10;
        public ObservableCollection<Todo> Todos { get; set; } = new ObservableCollection<Todo>();
        public MainPageViewModel()
        {
            LoadTodos();
        }

        public async Task<List<Todo>> GetDotos()
        {
            httpClient = new HttpClient();
            var result = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            var response = await result.Content.ReadFromJsonAsync<List<Todo>>();
            return response;
        }

        [RelayCommand]
        private async Task LoadTodos()
        {
            try
            {
                IsBusy = true;
                todoList = await GetDotos();
                var initialItems = todoList.Take(PageSize).ToList();
                foreach (var item in initialItems)
                {
                    Todos.Add(item);
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }


        [RelayCommand]
        public async Task GetNextData()
        {
            try
            {
                if (Todos.Count > 0)
                {
                    var nextItems = todoList.Skip(Todos.Count).Take(PageSize).ToList();
                    foreach (var item in nextItems)
                    {
                        Todos.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }
    }
}
