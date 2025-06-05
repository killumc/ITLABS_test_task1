using System;
using System.Windows.Controls;
using Poll_ver2.MVVM.Navigation;
using Poll_ver2.MVVM.View;
using Poll_ver2.MVVM.ViewModel;

public class FrameNavigationService : INavigationService
{
    private readonly Frame _frame;

    public FrameNavigationService(Frame frame)
    {
        _frame = frame ?? throw new ArgumentNullException(nameof(frame));
    }

    public void NavigateTo(string pageKey, object parameter = null)
    {
        switch (pageKey)
        {
            case "Home":
                var homeVM = new HomeViewModel(this);
                var homeView = new HomeView(homeVM);
                _frame.Navigate(homeView);
                break;

            case "Poll":
                var pollVM = new PollViewModel(this);
                var pollView = new PollView(pollVM);
                _frame.Navigate(pollView);
                break;

            case "Result":
                if (parameter is PollViewModel pollViewModel)
                {
                    var resultVM = new ResultViewModel(pollViewModel, this);
                    var resultView = new ResultView(resultVM);
                    _frame.Navigate(resultView);
                }
                else
                {
                    throw new ArgumentException("Для навигации на Result нужно передать PollViewModel");
                }
                break;

            case "Final":
                        var finalVM = new FinalViewModel(this);
                        var finalView = new FinalView(finalVM);
                        _frame.Navigate(finalView);
                        break;
            default:
                        throw new ArgumentException($"Страница с ключом {pageKey} не найдена.");
             }
    }

    public void GoBack()
    {
        if (_frame.CanGoBack)
            _frame.GoBack();
    }
}
