using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Poll_ver2.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HomeView());
            ScaleWindowToFit();
        }

        private void ScaleWindowToFit()
        {

            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double screenWidth = SystemParameters.PrimaryScreenWidth / 2;


            double scale = Math.Min(screenWidth / 1920, screenHeight / 1080);

            if (scale < 1.0)
            {
                this.Width = 1080 * scale;
                this.Height = 1920 * scale;
            }
        }
    }
}