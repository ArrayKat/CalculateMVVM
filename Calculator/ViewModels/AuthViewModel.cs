using System;
using System.Collections.Generic;
using Calculator.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Calculator.ViewModels
{
	public class AuthViewModel : ReactiveObject
	{
       
        string _login;
        string _password;
        public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
        public string Login { get => _login; set => this.RaiseAndSetIfChanged(ref _login, value); }

        public async void CheckData() {
            if (Password == AuthModels.truePassword && Login == AuthModels.trueLogin)
            {
                await MessageBoxManager.GetMessageBoxStandard("���������", "�� ������� ��������������!", ButtonEnum.Ok).ShowAsync();
            }
            else {
                await MessageBoxManager.GetMessageBoxStandard("���������", "�� �� ��������������! ���������� ��� ���!", ButtonEnum.Ok).ShowAsync();
            }
        }

        
    }
}