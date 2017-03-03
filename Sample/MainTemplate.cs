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
				Aspect = Aspect.AspectFill,
				Source = UriImageSource.FromUri(new System.Uri(cdata.ImageUrl))
			};

			var myLabel = new Label() {
				Text = cdata.Title,
				FontSize = 12,
				FontAttributes = FontAttributes.Bold,
				TextColor = Color.White,
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.Black,
				Opacity = 0.4
			};
			
			//Stack Layout
			//StackLayout layout = new StackLayout();
			//layout.InputTransparent = true;
			//layout.Orientation = StackOrientation.Vertical;
			//layout.Children.Add(image);
			//layout.Children.Add(myLabel);
			
			//Relative Layout
			RelativeLayout layout = new RelativeLayout();

			layout.Children.Add(image,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent((parent) => { return parent.Width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height; }));

			layout.Children.Add(myLabel,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent((parent) => { return parent.Width; }),
				Constraint.RelativeToParent((parent) => { return parent.Height; }));

			
			
			this.Children.Add(layout);
        }
    }
}
