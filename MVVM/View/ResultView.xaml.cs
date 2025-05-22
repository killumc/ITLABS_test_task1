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
        private int _totalScore;
        public ResultView(int totalScore)
        {
            _totalScore = totalScore;
            InitializeComponent();
            DataContext = new PopupViewModel();

            // Отображение различных текстов в зависимости от общего балла
            if (totalScore >= 0 && totalScore <= 2)
            {
                Paragraph1_Heading_Page3.Text = "Upgrade: личностный интенсив";
                Paragraph2_Heading_Page3.Text = "Управление изменениями ";
                Paragraph3_Heading_Page3.Text = "Бизнес с AI: от теории к практике";
                Paragraph1_Text_Page3.Text = "позволит увидеть перспективы и найти свои точка роста";
                Paragraph2_Text_Page3.Text = "научит навигировать компанию в постоянно меняющемся окружении";
                Paragraph3_Text_Page3.Text = "программа направлена на изучение процессов разработки и внедрения AI-технологий в бизнесе с целью оптимизации процессов и повышение экономической эффективности";
                Heading_Page2.Text = "Ваш результат\r\n 0-2 баллов";
                Text1_Page3.Text = "Похоже, вы упускаете карьерные возможности, которые вас окружают, а вместе с ними и интересные перспективы. Пройдя этот тест, вы уже сделали первый шаг, поздравляем!";
                Text1_1_Page3.Text = "Для дальнейшего развития предлагаем ознакомиться с возможностями диагностики и карьерного консультирования SberQ.";
            }
            else if (totalScore >= 3 && totalScore <= 6)
            {
                Paragraph1_Heading_Page3.Text = "Upgrade 2: осознанное лидерство";
                Paragraph2_Heading_Page3.Text = "Мини MBA";
                Paragraph3_Heading_Page3.Text = "Цифровая трансформация бизнеса";
                Paragraph1_Text_Page3.Text = "позволит увидеть перспективы и развить свой лидерский потенциал";
                Paragraph2_Text_Page3.Text = "даст комплексный набор знаний, инструментов и навыков актуальных для повышения эффективности бизнес-процессов";
                Paragraph3_Text_Page3.Text = "поможет погрузиться в инновационные технологии и разработать свой проект по изменению процессов в компании";
                Heading_Page2.Text = "Ваш результат \r\n3-6 баллов";
                Text1_Page3.Text = "Вы явно не новичок в развитии, продолжайте в том же духе.";
                Text1_1_Page3.Text = "Чтобы выйти на следующий уровень и использовать собственный ресурс по максимуму предлагаем ознакомиться с возможностями диагностики и карьерного консультирования SberQ.";
            }
            else if (totalScore >= 7 && totalScore <= 10)
            {
                Paragraph1_Heading_Page3.Text = "Вызов сильных";
                Paragraph2_Heading_Page3.Text = "STEP";
                Paragraph3_Heading_Page3.Text = "Digital Strategy";
                Paragraph1_Text_Page3.Text = "новая уникальная программа, которая поможет лидерам исследовать свой внутренний ресурс и направить его на достижение новых результатов, даже если кажется, что они уже пройдены";
                Paragraph2_Text_Page3.Text = "международная программа сделает из вас лидера нового поколения, способного в условиях высокой неопределённости инициировать изменения и управлять ими";
                Paragraph3_Text_Page3.Text = "поможет перестроить текущую бизнес-модель и провести цифровую трансформацию бизнеса для завоевания и сохранения лидерской позиции на рынке";
                Heading_Page2.Text = "Ваш результат\r\n7-10 баллов";
                Text1_Page3.Text = "Приятно иметь дело с профессионалом! Продолжайте в том же духе!";
                Text1_1_Page3.Text = "Для достижения самых амбициозных целей предлагаем ознакомиться с возможностями диагностики и карьерного консультирования SberQ.";
            }
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

        private void HyperlinkPolitic_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new PopupViewModel();
            ((PopupViewModel)DataContext).IsPoliticPopupOpen = true;
        }

        private void HyperlinkAgreement_Click(object sender, RoutedEventArgs e)
        {
            ((PopupViewModel)DataContext).IsAgreementPopupOpen = true;
        }

        private SmtpSettings LoadEmailSettings()
        {
            Debug.WriteLine("Зашел в LoadEmailSettings()");
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "emailSettings.json");
                Debug.WriteLine($"Ищем файл настроек по пути: {path}");

                if (!File.Exists(path))
                {
                    Debug.WriteLine("Файл настроек не найден по пути: " + path);
                    return null;
                }

                string json = File.ReadAllText(path);
                var settings = JsonConvert.DeserializeObject<SmtpSettings>(json);
                Debug.WriteLine($"Настройки загружены: {JsonConvert.SerializeObject(settings)}");
                return settings;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки настроек: {ex}");
                return null;
            }
        }



        private void SendMessage(string email)
        {
            // Загрузка настроек из JSON
            var settings = LoadEmailSettings();
            Debug.WriteLine($"SMTP Settings: {JsonConvert.SerializeObject(settings)}");

            if (settings == null)
            {
                Debug.WriteLine("Не удалось загрузить настройки SMTP");
                throw new InvalidOperationException("Не удалось загрузить настройки SMTP");
            }

            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(settings.FromAddress))
                throw new ArgumentException("FromAddress не может быть пустым в настройках");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Адрес получателя не может быть пустым");

            using (MailMessage message = new MailMessage())
            {
                try
                {
                    message.From = new MailAddress(settings.FromAddress, settings.FromName);
                    message.To.Add(new MailAddress(email));
                    message.Subject = settings.Subject;
                    message.Body = string.Format(settings.BodyTemplate, _totalScore);

                    Debug.WriteLine($"Отправка письма: From: {settings.FromAddress}, To: {email}");

                    using (SmtpClient client = new SmtpClient(settings.Server, settings.Port))
                    {
                        client.EnableSsl = settings.UseSsl;
                        client.Credentials = new NetworkCredential(settings.Username, settings.Password);

                        // Добавляем таймаут
                        client.Timeout = 10000;


                        // Отключение проверки сертификата (только для тестирования)
                        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                        client.Send(message);
                        Debug.WriteLine("Письмо успешно отправлено");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Ошибка отправки письма: {ex}");
                    throw; // Перебрасываем исключение для обработки в вызывающем коде
                }
            }
        }




        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Используем регулярное выражение для проверки формата электронной почты
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private async void NavigateToFinal(object sender, RoutedEventArgs e)
        {
            string email = "dshmykova060904@mail.ru"; // Используйте тестовый email

            try
            {
                if (AgreementCheckBox.IsChecked != true)
                {
                    MessageBox.Show("Подтвердите обработку персональных данных!");
                    return;
                }

                if (!IsValidEmail(email))
                {
                    Debug.WriteLine("Неверный формат email");
                    ((PopupViewModel)DataContext).IsErrorPopupOpen = true;
                    return;
                }

                await Task.Run(() => SendMessage(email));

                if (Window.GetWindow(this) is MainWindow mainWindow)
                {
                    mainWindow.MainFrame.Navigate(new FinalView());
                }
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine($"Ошибка валидации: {ex.Message}");
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine($"Ошибка конфигурации: {ex.Message}");
                MessageBox.Show("Ошибка конфигурации почтового сервера. Пожалуйста, сообщите администратору.",
                               "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка отправки: {ex.Message}");
                ((PopupViewModel)DataContext).IsErrorPopupOpen = true;
            }
        }




        private void NavigateToHome(object sender, RoutedEventArgs e)
        {
            if (Window.GetWindow(this) is MainWindow mainWindow)
            {
                mainWindow.MainFrame.Navigate(new HomeView());
            }
        }
    }
}
