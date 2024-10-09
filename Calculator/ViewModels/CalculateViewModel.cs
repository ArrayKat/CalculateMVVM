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
        string _selectedValue = "��������";
        List<string> _operation = new List<string>() { "��������", "���������", "���������", "�������" };
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
            if (SelectedValue == "��������") OperationSymb = "+";
            else if (SelectedValue == "���������") OperationSymb = "-";
            else if (SelectedValue == "���������") OperationSymb = "*";
            else OperationSymb = "/";

        }
        public async void Calculate() {
            if (SelectedValue != null)
            {
                if (SelectedValue == "��������") Result = Number1 + Number2;
                else if (SelectedValue == "���������") Result = Number1 - Number2;
                else if (SelectedValue == "���������") Result = Number1 * Number2;
                else
                { 
                    if (Number2 != 0) Result =(double) Number1/Number2;
                    else await MessageBoxManager.GetMessageBoxStandard("���������", "�� ���� ������ ������!", ButtonEnum.Ok).ShowAsync();
                }
            }
            else await MessageBoxManager.GetMessageBoxStandard("���������", "�� �� ������� ��������!", ButtonEnum.Ok).ShowAsync();
        }
    }
}