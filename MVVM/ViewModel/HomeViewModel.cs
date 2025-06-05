using CommunityToolkit.Mvvm.ComponentModel;
using Poll_ver2.MVVM.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace Poll_ver2.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public ICommand NavigateToPollCommand { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToPollCommand = new RelayCommand(() => _navigationService.NavigateTo("Poll"));
        }

    }
}
