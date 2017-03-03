using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections;

namespace DotnetMobile.CustomGrid
{
	public partial class GridView : Grid
	{
		public GridView()
		{
			for (var i = 0; i < MaxColumns; i++)
				ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
		}

		public static BindableProperty CommandParameterProperty = BindableProperty.Create(
																	propertyName: "CommandParameter",
																	returnType: typeof(object),
																	declaringType: typeof(GridView),
																	defaultValue: null,
																	defaultBindingMode: BindingMode.TwoWay,
																	propertyChanged: null);


		public static BindableProperty CommandProperty = BindableProperty.Create(
																	propertyName: "Command",
																	returnType: typeof(ICommand),
																	declaringType: typeof(GridView),
																	defaultValue: null,
																	defaultBindingMode: BindingMode.TwoWay,
																	propertyChanged: null);


		public static BindableProperty ItemTemplateProperty = BindableProperty.Create(
																			propertyName: "ItemTemplate",
																			returnType: typeof(object),
																			declaringType: typeof(GridView),
																			defaultValue: null,
																			defaultBindingMode: BindingMode.TwoWay,
																			propertyChanged: null);

		public static BindableProperty ItemsSourceProperty = BindableProperty.Create(
																	propertyName: "ItemsSource",
																	returnType: typeof(IEnumerable<object>),
																	declaringType: typeof(GridView),
																	defaultValue: null,
																	defaultBindingMode: BindingMode.TwoWay,
																	propertyChanged: async (bindable, oldValue, newValue) => {
																		await ((GridView)bindable).BuildTiles((IEnumerable<object>)newValue);
																	});


		private int _maxColumns = 2;
		private float _tileHeight = 0;


		public int MaxColumns {
			get { return _maxColumns; }
			set { _maxColumns = value; }
		}

		public float TileHeight {
			get { return _tileHeight; }
			set { _tileHeight = value; }
		}

		public object CommandParameter {
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public ICommand Command {
			get { return (ICommand)GetValue(CommandProperty); }
			set {
				SetValue(CommandProperty, value);
			}
		}

		public Type ItemTemplateParamater {
			get { return (Type)GetValue(ItemTemplateProperty); }
			set {
				SetValue(ItemTemplateProperty, value);
			}
		}

		public IEnumerable<object> ItemsSource {
			get { return (IEnumerable<object>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public async Task BuildTiles(IEnumerable<object> tiles)
		{
			var startTime = DateTime.Now;
			try {
				if (tiles == null || tiles.Count() == 0)
					Children?.Clear();

				// Wipe out the previous row definitions if they're there.
				RowDefinitions?.Clear();

				var enumerable = tiles as IList ?? tiles.ToList();
				var numberOfRows = Math.Ceiling(enumerable.Count / (float)MaxColumns);

				for (var i = 0; i < numberOfRows; i++)
					RowDefinitions?.Add(new RowDefinition { Height = TileHeight });

				for (var index = 0; index < enumerable.Count; index++) {
					var column = index % MaxColumns;
					var row = (int)Math.Floor(index / (float)MaxColumns);

					var tile = await BuildTile(enumerable[index]);

					Children?.Add(tile, column, row);
					InvalidateMeasure();
				}
				var endtime = DateTime.Now;
				TimeSpan duration = startTime - endtime;
				System.Diagnostics.Debug.WriteLine($" Total Time taken to Load {tiles.Count()} Records is {duration.TotalMilliseconds}");
			} catch (Exception ex) {
				throw ex;
			} finally {
				GC.Collect();
			}
		}

		private async Task<Layout> BuildTile(object item)
		{
			return await Task.Run(() => {
				var buildTile = (Layout)Activator.CreateInstance(ItemTemplateParamater, item);

				buildTile.InputTransparent = false;
				buildTile.BindingContext = item;

				var tapGestureRecognizer = new TapGestureRecognizer {
					Command = Command,
					CommandParameter = item
				};

				buildTile?.GestureRecognizers.Add(tapGestureRecognizer);

				return buildTile;
			});
		}
	}
}
