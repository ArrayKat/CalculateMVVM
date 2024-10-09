using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;

namespace Calculator.ViewModels
{
    public class CalculateViewModel : ReactiveObject
    {
        int _number1 = 0;
        int _number2 = 0;
        double _result = 0;
        string _selectedValue = "Сложение";
        List<string> _operation = new List<string>() { "Сложение", "Вычитание", "Умножение", "Деление" };
        string _operationSymb;


        public ObservableCollection<string> Operations { get; } = new ObservableCollection<string>
        {
            "+", "-", "*", ":"
        };

        public int Number1 { get => _number1; set => this.RaiseAndSetIfChanged(ref _number1, value); }
        public int Number2 { get => _number2; set => this.RaiseAndSetIfChanged(ref _number2, value); }
        public List<string> Operation { get => _operation; }
        public string SelectedValue { get => _selectedValue; set { this.RaiseAndSetIfChanged(ref _selectedValue, value); ChangeOperationSymb(); } }
        public double Result { get => _result; set => this.RaiseAndSetIfChanged(ref _result, value); }
        
        public string OperationSymb { get { return _operationSymb; } set => this.RaiseAndSetIfChanged(ref _operationSymb, value);}

        public void ChangeOperationSymb() {
            if (SelectedValue == "Сложение") OperationSymb = "+";
            else if (SelectedValue == "Вычитание") OperationSymb = "-";
            else if (SelectedValue == "Умножение") OperationSymb = "*";
            else OperationSymb = "/";

        }
        public async void Calculate() {
            if (SelectedValue != null)
            {
                if (SelectedValue == "Сложение") Result = Number1 + Number2;
                else if (SelectedValue == "Вычитание") Result = Number1 - Number2;
                else if (SelectedValue == "Умножение") Result = Number1 * Number2;
                else
                { 
                    if (Number2 != 0) Result =(double) Number1/Number2;
                    else await MessageBoxManager.GetMessageBoxStandard("Сообщение", "На нуль делить нельзя!", ButtonEnum.Ok).ShowAsync();
                }
            }
            else await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Вы не выбрали операцию!", ButtonEnum.Ok).ShowAsync();
        }
    }
}