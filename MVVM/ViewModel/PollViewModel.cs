using Poll_ver2.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poll_ver2.MVVM.ViewModel
{
    class PollViewModel : INotifyPropertyChanged
    {
        private double _sliderValue1;
        private double _sliderValue2;
        private double _sliderValue3;
        private double _sliderValue4;
        private double _sliderValue5;

        public double SliderValue1
        {
            get { return _sliderValue1; }
            set
            {
                if (_sliderValue1 != value)
                {
                    _sliderValue1 = value;
                    OnPropertyChanged(nameof(SliderValue1));
                }
            }
        }

        public double SliderValue2
        {
            get { return _sliderValue2; }
            set
            {
                if (_sliderValue2 != value)
                {
                    _sliderValue2 = value;
                    OnPropertyChanged(nameof(SliderValue2));
                }
            }
        }

        public double SliderValue3
        {
            get { return _sliderValue3; }
            set
            {
                if (_sliderValue3 != value)
                {
                    _sliderValue3 = value;
                    OnPropertyChanged(nameof(SliderValue3));
                }
            }
        }

        public double SliderValue4
        {
            get { return _sliderValue4; }
            set
            {
                if (_sliderValue4 != value)
                {
                    _sliderValue4 = value;
                    OnPropertyChanged(nameof(SliderValue4));
                }
            }
        }

        public double SliderValue5
        {
            get { return _sliderValue5; }
            set
            {
                if (_sliderValue5 != value)
                {
                    _sliderValue5 = value;
                    OnPropertyChanged(nameof(SliderValue5));
                }
            }
        }

        public int TotalScore { get; private set; }

        public void CalculateTotalScore()
        {
            TotalScore = (int)(SliderValue1 + SliderValue2 + SliderValue3 + SliderValue4 + SliderValue5);
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
