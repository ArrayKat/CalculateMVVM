using Avalonia.Controls;

namespace Calculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        UserControl us = new Auth();

        public UserControl Us { get => us; set => us = value; }
        

        AuthViewModel authVM = new AuthViewModel();
        public AuthViewModel AuthVM { get => authVM; set => authVM = value; }

        
    }
}
