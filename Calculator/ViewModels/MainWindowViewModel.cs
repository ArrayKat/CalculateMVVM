using Avalonia.Controls;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;

namespace Calculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        UserControl _us = new Auth();

        public UserControl Us { get => _us; set => this.RaiseAndSetIfChanged(ref _us, value); }
        

        AuthViewModel authVM = new AuthViewModel();
        public AuthViewModel AuthVM { get => authVM; set => authVM = value; }
       

        CalculateViewModel calculateVM = new CalculateViewModel();
        public CalculateViewModel CalculateVM { get => calculateVM; set => calculateVM = value; }

        public async void ToCalculateView() {
            if (AuthVM.CheckData())
            {
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Вы успешно авторизовались!", ButtonEnum.Ok).ShowAsync();
                Us = new Calculate();
            }
            else {
                await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Не верно введен логин или пароль!", ButtonEnum.Ok).ShowAsync();
            }
        }
        
    }
}
