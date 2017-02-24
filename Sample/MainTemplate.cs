using DotnetMobile.CustomGrid;
using Xamarin.Forms;

namespace Sample
{
    public class MainTemplate : Grid
    {
        public MainTemplate()
        {

        }

        public MainTemplate(object item)
        {
			CustomData cdata = (CustomData)item;
			var image = new Image {
				Aspect = Aspect.AspectFit,
				Source = UriImageSource.FromUri(new System.Uri(cdata.ImageUrl))
			};
			
			this.Children.Add(image);
        }
    }
}
