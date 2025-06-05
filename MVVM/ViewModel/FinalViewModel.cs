using CommunityToolkit.Mvvm.ComponentModel;
using Poll_ver2.MVVM.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Poll_ver2.MVVM.ViewModel
{
    public class FinalViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public ICommand NavigateToHomeCommand { get; }

        public FinalViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateToHomeCommand = new RelayCommand(() => _navigationService.NavigateTo("Home"));
        }

    }
}
