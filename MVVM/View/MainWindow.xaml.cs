using Poll_ver2.MVVM.ViewModel;
using System.Windows;


namespace Poll_ver2.MVVM.View
{

    public partial class MainWindow : Window
    {

        private readonly FrameNavigationService Navigation_Service;
        private readonly MainViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            ScaleWindowToFit();

            Navigation_Service = new FrameNavigationService(MainFrame);
            ViewModel = new MainViewModel(Navigation_Service);

            DataContext = ViewModel;

            Navigation_Service.NavigateTo("Home");
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