using ColorPicker.Classes.Static;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace ColorPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer think;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Set title
            this.Title = $"Color Picker - Version {Assembly.GetEntryAssembly().GetName().Version}";

            // Init color
            SetViewColorValues(System.Drawing.Color.White);

            // Init timer
            think = new Timer();
            think.Interval = 10;
            think.Elapsed += Think_Elapsed;
        }

        private void Think_Elapsed(object sender, ElapsedEventArgs e)
        {
            Task.Run(() =>
            {
                System.Drawing.Point cursor = new System.Drawing.Point();
                WindowsLibExt.GetCursorPos(ref cursor);

                System.Drawing.Color drawingColor = WindowsLibExt.GetPixelAt(cursor);
                this.Dispatcher.Invoke(() =>
                {
                    SetViewColorValues(drawingColor);
                });
            });
        }

        #region Helper Methods
        private void SetViewColorValues(System.Drawing.Color color)
        {
            this.txtColorPlain.Text = ColorConverterExt.ColorToPlainString(color);
            this.txtColorHex.Text = ColorConverterExt.ColorToHexString(color);
            this.txtColorRGB.Text = ColorConverterExt.ColorToRgbString(color);
            this.txtColorHSL.Text = ColorConverterExt.ColorToHslString(color);
            this.colPreview.Background = ColorConverterExt.ColorToSolidColorBrush(color);
        }
        #endregion

        #region Control Events
        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e) => think.Start();
        private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e) => think.Stop();
        #endregion

    }
}
