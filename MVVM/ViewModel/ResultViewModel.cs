using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Poll_ver2.MVVM.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Poll_ver2.MVVM.Model;

namespace Poll_ver2.MVVM.ViewModel
{
    public partial class ResultViewModel : ObservableObject
    {
        [ObservableProperty]  private int _totalScore;
        [ObservableProperty]  private bool _isPoliticPopupOpen;
        [ObservableProperty] private bool _isAgreementPopupOpen;
        [ObservableProperty] private bool _isErrorPopupOpen;

        [ObservableProperty] private string _userEmail;

        [ObservableProperty] private bool _isChecked;

        public string Paragraph1Heading { get; private set; }
        public string Paragraph2Heading { get; private set; }
        public string Paragraph3Heading { get; private set; }

        public string Paragraph1Text { get; private set; }
        public string Paragraph2Text { get; private set; }
        public string Paragraph3Text { get; private set; }

        public string Heading { get; private set; }
        public string Text1 { get; private set; }
        public string Text1_1 { get; private set; }

        private readonly INavigationService _navigationService;

    [RelayCommand] private void PoliticPopup_Open() => IsPoliticPopupOpen = true;
        [RelayCommand] private void PoliticPopup_Close() => IsPoliticPopupOpen = false;
        [RelayCommand] private void AgreementPopup_Open() => IsAgreementPopupOpen = true;
        [RelayCommand] private void AgreementPopup_Close()=> IsAgreementPopupOpen = false;
        [RelayCommand] private void ErrorPopup_Close() => IsErrorPopupOpen = false;
        [RelayCommand] private void NavigateToHome() => _navigationService.NavigateTo("Home");
        [RelayCommand] private void NavigateToFinal()
        {
            if (IsChecked)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(UserEmail) || !UserEmail.Contains("@"))
                    {
                        IsErrorPopupOpen = true;
                        return;
                    }

                    var emailSender = new Poll_ver2.SendEmail.EmailSender();

                    string body = $"{Heading}\n\n{Text1}\n{Text1_1}\n\nСпасибо за прохождение теста!";

                    emailSender.SendMessage(UserEmail, "Результаты теста", body);

                    _navigationService.NavigateTo("Final");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Ошибка отправки письма: {ex.Message}");
                    IsErrorPopupOpen = true;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Перед отправкой ознакомтесь с политикой и дайте согласие на обработку персональных данных.", 
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        public ResultViewModel(PollViewModel pollViewModel, INavigationService navigationService)
        {
            _totalScore = pollViewModel.TotalScore;

            SetInfo();

            _navigationService = navigationService;

        }


        private void SetInfo()
        {
            var json = File.ReadAllText("ResultInfo.json");
            var results = JsonSerializer.Deserialize<ResultInfo>(json);

            Result ResultInfo = null;
            if (_totalScore > 0 && _totalScore <= 2) ResultInfo = results.ResultGroup1;
            else if (_totalScore >= 3 && _totalScore <= 6) ResultInfo = results.ResultGroup2;
            else if (_totalScore >= 7 && _totalScore <= 10) ResultInfo = results.ResultGroup3;

            if (ResultInfo != null)
            {
                Paragraph1Heading = ResultInfo.Paragraph1Heading;
                Paragraph2Heading = ResultInfo.Paragraph2Heading;
                Paragraph3Heading = ResultInfo.Paragraph3Heading;
                Paragraph1Text = ResultInfo.Paragraph1Text;
                Paragraph2Text = ResultInfo.Paragraph2Text;
                Paragraph3Text = ResultInfo.Paragraph3Text;
                Heading = ResultInfo.Heading;
                Text1 = ResultInfo.Text1;
                Text1_1 = ResultInfo.Text1_1;
            }
        }


    }
}
