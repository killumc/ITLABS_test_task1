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
    public class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public ICommand NavigateHomeCommand { get; }
        public ICommand NavigatePollCommand { get; }

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigateHomeCommand = new RelayCommand(() => _navigationService.NavigateTo("Home"));
            NavigatePollCommand = new RelayCommand(() => _navigationService.NavigateTo("Poll"));
        }

        PropertyChangingEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
    }

}
