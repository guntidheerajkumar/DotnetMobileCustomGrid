using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using PropertyChanged;

namespace Sample
{
	[ImplementPropertyChanged]
    public class MainViewModel
    {
        private INavigation navigation
        {
            get;
            set;
        }
        public ObservableCollection<CustomData> LstCustomData { get; set; }

		public string MainTitle { get; set; }
		
        public Command ClickCommand { get; set; }

		public Type TemplateMain { get; set; } 

		public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
			TemplateMain = typeof(MainTemplate);
            LstCustomData = new ObservableCollection<CustomData>();
            CreateData();
            ClickCommand = new Command(async (obj) => { await GetData(obj); });
        }

        private async Task GetData(object obj)
        {
			MainTitle = ((CustomData)obj).Title;
        }

        private void CreateData()
        {

            for (int i = 0; i < 100; i++)
            {
                LstCustomData.Add(new CustomData() { Title = $"Temporary Title {i.ToString()}", ImageUrl = "http://farm8.staticflickr.com/7107/7441697680_3ef53f81e7_b.jpg" });
            }

        }
    }
}
