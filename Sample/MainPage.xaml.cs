using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample
{
    public partial class MainPage : ContentPage
    {
        public MainViewModel MainModel { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = MainModel = new MainViewModel(this.Navigation);
        }
    }
}
