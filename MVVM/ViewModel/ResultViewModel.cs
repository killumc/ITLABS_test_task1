using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poll_ver2.MVVM.ViewModel
{
    class ResultViewModel : INotifyPropertyChanged
    {
        private int _totalScore;
        private string _headingText;
        private string _text1;
        private string _text2;

        public ResultViewModel(int totalScore)
        {
            _totalScore = totalScore;
            UpdateTexts();
        }

        public string HeadingText
        {
            get { return _headingText; }
            set
            {
                if (_headingText != value)
                {
                    _headingText = value;
                    OnPropertyChanged(nameof(HeadingText));
                }
            }
        }

        public string Text1
        {
            get { return _text1; }
            set
            {
                if (_text1 != value)
                {
                    _text1 = value;
                    OnPropertyChanged(nameof(Text1));
                }
            }
        }

        public string Text2
        {
            get { return _text2; }
            set
            {
                if (_text2 != value)
                {
                    _text2 = value;
                    OnPropertyChanged(nameof(Text2));
                }
            }
        }

        private void UpdateTexts()
        {
            if (_totalScore >= 0 && _totalScore <= 3)
            {
                HeadingText = "Ваш результат: 0-3 балла";
                Text1 = "Текст для 0-3 баллов";
                Text2 = "Дополнительный текст для 0-3 баллов";
            }
            else if (_totalScore >= 4 && _totalScore <= 6)
            {
                HeadingText = "Ваш результат: 4-6 баллов";
                Text1 = "Текст для 4-6 баллов";
                Text2 = "Дополнительный текст для 4-6 баллов";
            }
            else if (_totalScore >= 7 && _totalScore <= 10)
            {
                HeadingText = "Ваш результат: 7-10 баллов";
                Text1 = "Текст для 7-10 баллов";
                Text2 = "Дополнительный текст для 7-10 баллов";
            }
        }

        // Событие, которое уведомляет интерфейс об изменениях в свойствах
        public event PropertyChangedEventHandler? PropertyChanged;

        // Метод для вызова события PropertyChanged
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
