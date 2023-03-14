using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZoomAndPan;

namespace MuzU_Studio.view.pianoRoll
{
    public class PianoKeysControl : ListBox
    {
        public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
            nameof(ItemHeight), typeof(double), typeof(PianoKeysControl), new FrameworkPropertyMetadata(20.0));

        public double ItemHeight
        {
            get => (double)GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public PianoKeysControl()
        {
            // Bind to the PanAndZoomControl's ContentScaleY and ContentOffsetY properties
            SetBinding(ItemHeightProperty, new Binding("ContentScaleY") { Source = PanAndZoomControl, Mode = BindingMode.OneWay });
            SetBinding(ContentOffsetYProperty, new Binding("ContentOffsetY") { Source = PanAndZoomControl, Mode = BindingMode.OneWay });
        }

        // Override the ListBox's default item container generation to create custom containers
        protected override DependencyObject GetContainerForItemOverride() => new PianoKeyContainer();

        // Override the ListBox's default item container style to use our custom style
        static PianoKeysControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PianoKeysControl), new FrameworkPropertyMetadata(typeof(PianoKeysControl)));
        }

        // Reference to the PanAndZoomControl to bind to
        public ZoomAndPanControl PanAndZoomControl
        {
            get => (ZoomAndPanControl)GetValue(PanAndZoomControlProperty);
            set => SetValue(PanAndZoomControlProperty, value);
        }

        public static readonly DependencyProperty PanAndZoomControlProperty =
            DependencyProperty.Register(nameof(PanAndZoomControl), typeof(ZoomAndPanControl), typeof(PianoKeysControl));
    }

    // Custom item container that displays a PianoKeyViewModel as a rectangle with a label
    public class PianoKeyContainer : ListBoxItem
    {
        private readonly Rectangle _rectangle = new Rectangle();
        private readonly TextBlock _label = new TextBlock();

        public PianoKeyContainer()
        {
            _rectangle.SetBinding(FillProperty, new Binding("FillColor"));
            _rectangle.SetBinding(WidthProperty, new Binding("Width"));
            _rectangle.SetBinding(HeightProperty, new Binding("Height"));

            _label.SetBinding(TextBlock.TextProperty, new Binding("KeyName"));
            _label.SetBinding(TextBlock.ForegroundProperty, new Binding("TextColor"));

            Content = new StackPanel { Orientation = Orientation.Horizontal };
            ((StackPanel)Content).Children.Add(_rectangle);
            ((StackPanel)Content).Children.Add(_label);
        }
    }

}
