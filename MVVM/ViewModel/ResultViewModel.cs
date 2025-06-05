using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Poll_ver2.MVVM.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Poll_ver2.MVVM.ViewModel
{
    public class ResultViewModel : ObservableObject
    {
        private int _totalScore;
        private bool _isPoliticPopupOpen;
        private bool _isAgreementPopupOpen;
        private bool _isErrorPopupOpen;
        private string _userEmail;

        public string Paragraph1Heading { get; private set; }
        public string Paragraph2Heading { get; private set; }
        public string Paragraph3Heading { get; private set; }

        public string Paragraph1Text { get; private set; }
        public string Paragraph2Text { get; private set; }
        public string Paragraph3Text { get; private set; }

        public string Heading { get; private set; }
        public string Text1 { get; private set; }
        public string Text1_1 { get; private set; }


        public bool IsPoliticPopupOpen
        {
            get => _isPoliticPopupOpen;
            set => SetProperty(ref _isPoliticPopupOpen, value);
        }
        public bool IsAgreementPopupOpen
        {
            get => _isAgreementPopupOpen;
            set => SetProperty(ref _isAgreementPopupOpen, value);
        }
        public bool IsErrorPopupOpen
        {
            get => _isErrorPopupOpen;
            set => SetProperty(ref _isErrorPopupOpen, value);
        }

        public string UserEmail
        {
            get => _userEmail;
            set => SetProperty(ref _userEmail, value);
        }

        private readonly INavigationService _navigationService;

 
        public ICommand NavigateToHomeCommand { get; }
        public ICommand NavigateToFinalCommand { get; }
        public ICommand PoliticPopup_OpenCommand { get; }
        public ICommand PoliticPopup_ClouseCommand { get; }
        public ICommand AgreementPopup_OpeenCommand { get; }
        public ICommand AgreementPopup_ClouseCommand { get; }
        public ICommand ErrorPopup_ClouseCommand { get; }

        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                if (SetProperty(ref isChecked, value))
                {
                    // Обновляем состояние команды при изменении IsChecked
                    ((AsyncRelayCommand)NavigateToFinalCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ResultViewModel(PollViewModel pollViewModel, INavigationService navigationService)
        {
            _totalScore = pollViewModel.TotalScore;

            SetInfo();

            _navigationService = navigationService;
            NavigateToHomeCommand = new RelayCommand(() => _navigationService.NavigateTo("Home"));
            NavigateToFinalCommand = new AsyncRelayCommand(async () =>
            {
                if (IsChecked == false)
                {
                    System.Windows.MessageBox.Show("Перед отправкой ознакомтесь с политикой и дайте согласие на обработку персональных данных.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        // Проверка email
                        if (string.IsNullOrWhiteSpace(UserEmail) || !UserEmail.Contains("@"))
                        {
                            IsErrorPopupOpen = true;
                            return;
                        }



                        var emailSender = new Poll_ver2.SendEmail.EmailSender();

                        // Формируем тело письма с результатами (пример)
                        string body = $"{Heading}\n\n{Text1}\n{Text1_1}\n\nСпасибо за прохождение теста!";

                        // Отправляем письмо (предполагается, что есть асинхронный метод)
                        await emailSender.SendMessageAsync(UserEmail, "Результаты теста", body);

                        // Если отправка успешна — навигация
                        _navigationService.NavigateTo("Final");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Ошибка отправки письма: {ex.Message}");
                        IsErrorPopupOpen = true;
                    }
                }
            });
            PoliticPopup_OpenCommand = new RelayCommand(() => IsPoliticPopupOpen = true);
            PoliticPopup_ClouseCommand = new RelayCommand(() => IsPoliticPopupOpen=false);

            AgreementPopup_OpeenCommand = new RelayCommand(()=> IsAgreementPopupOpen=true);
            AgreementPopup_ClouseCommand = new RelayCommand(() => IsAgreementPopupOpen = false);

            ErrorPopup_ClouseCommand = new RelayCommand(() => IsErrorPopupOpen = false);

        }

        private void SetInfo()
        {
            if (_totalScore >= 0 && _totalScore <= 2)
            {
                Paragraph1Heading = "Upgrade: личностный интенсив";
                Paragraph2Heading = "Управление изменениями ";
                Paragraph3Heading = "Бизнес с AI: от теории к практике";
                Paragraph1Text = "позволит увидеть перспективы и найти свои точка роста";
                Paragraph2Text = "научит навигировать компанию в постоянно меняющемся окружении";
                Paragraph3Text = "программа направлена на изучение процессов разработки и внедрения AI-технологий в бизнесе с целью оптимизации процессов и повышение экономической эффективности";
                Heading = "Ваш результат\r\n 0-2 баллов";
                Text1 = "Похоже, вы упускаете карьерные возможности, которые вас окружают, а вместе с ними и интересные перспективы. Пройдя этот тест, вы уже сделали первый шаг, поздравляем!";
                Text1_1 = "Для дальнейшего развития предлагаем ознакомиться с возможностями диагностики и карьерного консультирования SberQ.";
            }

            else if (_totalScore >= 3 && _totalScore <= 6)
            {
                Paragraph1Heading = "Upgrade 2: осознанное лидерство";
                Paragraph2Heading = "Мини MBA";
                Paragraph3Heading = "Цифровая трансформация бизнеса";
                Paragraph1Text = "позволит увидеть перспективы и развить свой лидерский потенциал";
                Paragraph2Text = "даст комплексный набор знаний, инструментов и навыков актуальных для повышения эффективности бизнес-процессов";
                Paragraph3Text = "поможет погрузиться в инновационные технологии и разработать свой проект по изменению процессов в компании";
                Heading = "Ваш результат \r\n3-6 баллов";
                Text1 = "Вы явно не новичок в развитии, продолжайте в том же духе.";
                Text1_1 = "Чтобы выйти на следующий уровень и использовать собственный ресурс по максимуму предлагаем ознакомиться с возможностями диагностики и карьерного консультирования SberQ.";
            }

            else if(_totalScore>=7  && _totalScore<=10)
            {
                Paragraph1Heading = "Вызов сильных";
                Paragraph2Heading = "STEP";
                Paragraph3Heading = "Digital Strategy";
                Paragraph1Text = "новая уникальная программа, которая поможет лидерам исследовать свой внутренний ресурс и направить его на достижение новых результатов, даже если кажется, что они уже пройдены";
                Paragraph2Text = "международная программа сделает из вас лидера нового поколения, способного в условиях высокой неопределённости инициировать изменения и управлять ими";
                Paragraph3Text = "поможет перестроить текущую бизнес-модель и провести цифровую трансформацию бизнеса для завоевания и сохранения лидерской позиции на рынке";
                Heading = "Ваш результат\r\n7-10 баллов";
                Text1 = "Приятно иметь дело с профессионалом! Продолжайте в том же духе!";
                Text1_1 = "Для достижения самых амбициозных целей предлагаем ознакомиться с возможностями диагностики и карьерного консультирования SberQ.";
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
