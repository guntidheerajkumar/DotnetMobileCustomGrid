# Custom Grid for Xamarin Forms

#### Simple Solution for creating Grid for Xamarin Forms.

Create a Xamarin Forms Project. Run the nuget package to get the CustomGrid dll. 

###Grid Creation Snippet

Add the following namespace on top of the content element

```
xmlns:custom="clr-namespace:DotnetMobile.CustomGrid;assembly=DotnetMobile.CustomGrid"
```

```
<ScrollView>
   <custom:GridView x:Name="MyDynamicGrid" 
							HorizontalOptions="FillAndExpand" 
							Grid.Row="1" 
							VerticalOptions="FillAndExpand" 
							RowSpacing="10" 
							ColumnSpacing="10" 
							MaxColumns="2" 
							Padding="20" 
							ItemTemplate="{Binding TemplateMain}"
							ItemsSource="{Binding LstCustomData}" 
							Command="{Binding ClickCommand}"
							IsClippedToBounds="False">
         <custom:GridView.TileHeight>
           <OnPlatform x:TypeArguments="x:Single" iOS="120" Android="120" WinPhone="120" />
         </custom:GridView.TileHeight>
    </custom:GridView>
</ScrollView>
```

#Note: Please follow the same order of defining ItemTemplate and ItemSource. Its Mandatory.

Please create a Template file as below for creating cell.

```
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
```
The object that you will be getting here is the model that you have passed at the time itemsource.

##Recommendations:
- For better performance and getting rid of memory exceptions please do use Xamarin.FFImageLoading.Forms for Image Processing.
