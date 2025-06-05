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
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Windows.Media.Animation;
using Newtonsoft.Json;
using Poll_ver2.MVVM.Model;
using System.IO;
using Path = System.IO.Path;
using System.Net;

namespace Poll_ver2.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для ResultView.xaml
    /// </summary>
    public partial class ResultView : Page
    {

        public ResultView(ResultViewModel resultViewModel)
        {
            InitializeComponent();
            DataContext = resultViewModel;
        }




        private void Popup_Opened(object sender, EventArgs e)
        {
            Overlay.Visibility = Visibility.Visible;
            var fadeIn = (Storyboard)Resources["FadeInOverlay"];
            fadeIn.Begin(Overlay);
        }

        private void Popup_Closed(object sender, EventArgs e)
        {
            var fadeOut = (Storyboard)Resources["FadeOutOverlay"];
            fadeOut.Completed += (s, _) => Overlay.Visibility = Visibility.Collapsed;
            fadeOut.Begin(Overlay);
        }


    }
}
