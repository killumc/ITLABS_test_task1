using Poll_ver2.MVVM.ViewModel;
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

namespace Poll_ver2.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : Page
    {
        public HomeView(HomeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        //private void NavigateToPollView(object sender, RoutedEventArgs e)
        //{
        //    // Получаем родительское окно и его Frame
        //    if (Window.GetWindow(this) is MainWindow mainWindow)
        //    {
        //        mainWindow.MainFrame.Navigate(new PollView());
        //    }

        //}
    }
}
