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
    /// Логика взаимодействия для PollView.xaml
    /// </summary>
    public partial class PollView : Page
    {
        public PollView()
        {
            InitializeComponent();
        }


        private void GoBack(object sender, RoutedEventArgs e)
        {
            // Получаем родительское окно и его Frame
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.MainFrame.Navigate(new HomeView());
            }

        }
        private void NavigateToResult(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as PollViewModel;
            if (viewModel != null)
            {
                viewModel.CalculateTotalScore();
                var resultView = new ResultView(viewModel.TotalScore);
                if (Window.GetWindow(this) is MainWindow mainWindow)
                {
                    mainWindow.MainFrame.Navigate(resultView);
                }
            }
        }
    }
}
