using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Poll_ver2.MVVM.ViewModel
{
    public class PopupViewModel : INotifyPropertyChanged
    {
        private bool _isPoliticPopupOpen;
        private bool _isAgreementPopupOpen;
        private bool _isErrorPopupOpen;

        public bool IsPoliticPopupOpen
        {
            get { return _isPoliticPopupOpen; }
            set
            {
                _isPoliticPopupOpen = value;
                OnPropertyChanged();
            }

        }

        public bool IsAgreementPopupOpen
        {
            get { return _isAgreementPopupOpen; }
            set
            {
                _isAgreementPopupOpen = value;
                OnPropertyChanged();
            }
        }

        public bool IsErrorPopupOpen
        {
            get { return _isErrorPopupOpen; }
            set
            {
                _isErrorPopupOpen = value;
                OnPropertyChanged();
            }

        }
        public ICommand ClosePoliticPopupCommand { get; }
        public ICommand CloseAgreementPopupCommand { get; }
        public ICommand CloseErrorPopupCommand { get; }

        public PopupViewModel()
        {
            ClosePoliticPopupCommand = new RelayCommand(() => IsPoliticPopupOpen = false);
            CloseAgreementPopupCommand = new RelayCommand(() => IsAgreementPopupOpen = false);
            CloseErrorPopupCommand = new RelayCommand(() => IsErrorPopupOpen = false);
            LoadPoliticText();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private string _politicText;
        public string PoliticText
        {
            get => _politicText;
            set
            {
                _politicText = value;
                OnPropertyChanged();
            }
        }

        private void LoadPoliticText()
        {
            try
            {
                string filePath = "/Assets/Texts/politic_text.txt";
                Debug.WriteLine($"Attempting to load file from: {Path.GetFullPath(filePath)}");

                if (File.Exists(filePath))
                {
                    PoliticText = File.ReadAllText(filePath);
                    Debug.WriteLine($"File loaded successfully. Length: {PoliticText?.Length ?? 0}");
                }
                else
                {
                    Debug.WriteLine("File not found!");
                    PoliticText = "Файл с текстом не найден.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading text: {ex}");
                PoliticText = $"Ошибка загрузки текста: {ex.Message}";
            }
        }

        private bool _isOverlayVisible;
        public bool IsOverlayVisible
        {
            get => _isOverlayVisible;
            set
            {
                _isOverlayVisible = value;
                OnPropertyChanged();
            }
        }

    }
}
