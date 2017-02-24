using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample
{
    public class MainViewModel
    {

        private INavigation navigation
        {
            get;
            set;
        }
        public ObservableCollection<CustomData> LstCustomData { get; set; }

        public Command ClickCommand { get; set; }

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            LstCustomData = new ObservableCollection<CustomData>();
            CreateData();
            ClickCommand = new Command(async (obj) => { await GetData(obj); });
        }

        private async Task GetData(object obj)
        {
        }

        private void CreateData()
        {

            for (int i = 0; i < 10; i++)
            {
                LstCustomData.Add(new CustomData() { Title = $"Temporary Title {i.ToString()}", ImageUrl = "http://farm8.staticflickr.com/7107/7441697680_3ef53f81e7_b.jpg" });
            }

        }
    }
}
