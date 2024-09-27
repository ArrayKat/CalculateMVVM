using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using Calculator.Models;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;

namespace Calculator.ViewModels
{
	public class AuthViewModel : ReactiveObject
	{
        #region Инициализация полей, свойств
        int _countCheckData = 0;
        bool _isVisibleInputField = false;
        bool _isVisibleBtnAuth= true;
        string _login;
        string _password;
        string _tbCapcha;
        string _strCapchaGererated;
        Canvas _capcha;
        DispatcherTimer timer = new DispatcherTimer(); //создание объекта для таймера

        public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }
        public string Login { get => _login; set => this.RaiseAndSetIfChanged(ref _login, value); }
        public Canvas Capcha { get => _capcha; set => this.RaiseAndSetIfChanged(ref _capcha, value); }
        public int CountCheckData { get => _countCheckData; set => this.RaiseAndSetIfChanged(ref _countCheckData, value); }
        public bool IsVisibleInputField { get => _isVisibleInputField; set => this.RaiseAndSetIfChanged(ref _isVisibleInputField, value); }
        public bool IsVisibleBtnAuth { get => _isVisibleBtnAuth; set => this.RaiseAndSetIfChanged(ref _isVisibleBtnAuth, value); }
        public string TbCapcha { get => _tbCapcha; set => this.RaiseAndSetIfChanged(ref _tbCapcha, value); }
        public string StrCapchaGererated { get => _strCapchaGererated; set => this.RaiseAndSetIfChanged(ref _strCapchaGererated, value); }
        #endregion

        public AuthViewModel() {
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += new EventHandler(timer_tick);
        }
        private  void timer_tick(object sender, EventArgs e)
        {
            CreateCapcha();
            Password = "";
            Login = "";
            TbCapcha = "";
            IsVisibleInputField = true;
            IsVisibleBtnAuth = true;
            timer.Stop();
           
        }
        
        public async void CheckData() {
            IsVisibleBtnAuth = true;
            if (CountCheckData == 0)
            {
                if (Password == AuthModels.truePassword && Login == AuthModels.trueLogin)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Вы успешно авторизовались!", ButtonEnum.Ok).ShowAsync();

                }
                else
                {
                    await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Не верно введен логин или пароль!", ButtonEnum.Ok).ShowAsync();
                    Password = "";
                    Login = "";
                    IsVisibleInputField = true;
                    CreateCapcha();
                    CountCheckData++;
                }
            }
            else if (CountCheckData >= 1)
            {
                IsVisibleInputField = true;
                if (Password == AuthModels.truePassword && Login == AuthModels.trueLogin && TbCapcha == StrCapchaGererated)
                {
                    await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Вы успешно авторизовались!", ButtonEnum.Ok).ShowAsync();
                }
                else
                {
                    await MessageBoxManager.GetMessageBoxStandard("Сообщение", "Не верно введен логин или пароль!", ButtonEnum.Ok).ShowAsync();
                    CountCheckData++;
                    IsVisibleInputField = false;
                    IsVisibleBtnAuth = false;
                    timer.Start();
                }

            }
            
            
        }
        public void CreateCapcha() {
            Random rnd = new Random();
            const string alphabet = "abcdefghijklmnopqrstuvwxyz"; 
            int lenth = rnd.Next(7, 10);
            StrCapchaGererated = "";

            //сам контейнер для капчи
            Canvas cap = new Canvas()
            {
                Width = 350,
                Height = 100,
                Background = new SolidColorBrush(Colors.White)
             
            };

            double startX = rnd.Next(10, 20); // Начальная позиция по оси X

            //генерируем числа и буквы
            for (int i = 0; i < lenth; i++) {
                //генерация рандомного числа:
                TextBlock symbol = new TextBlock();
                symbol.FontSize = 25;
                symbol.Foreground = new SolidColorBrush(Colors.Black);
                //symbol.Foreground = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rnd.Next(256)), Convert.ToByte(rnd.Next(255)), Convert.ToByte(rnd.Next(255))));
                if (rnd.Next(10) % 2 == rnd.Next(0,1))
                {
                    int resultNumb = rnd.Next(10);
                    symbol.Text = resultNumb.ToString();
                    StrCapchaGererated += Convert.ToString(resultNumb);
                }
                else {

                    //генерация рандомной буквы:
                    int index = rnd.Next(alphabet.Length);
                    char result = alphabet[index];
                    symbol.Text = result.ToString();
                    StrCapchaGererated += Convert.ToString(result);
                }
                if (rnd.Next(9) % 3 == 0) symbol.FontWeight = FontWeight.Bold;
                else if (rnd.Next(9) % 3 == 2) symbol.FontStyle = FontStyle.Italic;
                else {
                    symbol.FontWeight = FontWeight.Bold;
                    symbol.FontStyle = FontStyle.Italic;
                } 

                
                Canvas.SetLeft(symbol, startX + (i * 35)); // Смещение по X
                Canvas.SetTop(symbol, rnd.Next(25,40)); // Позиция по Y

                cap.Children.Add(symbol);
            }
            //генерация линий
            for (int i = 0; i < rnd.Next(20,30); i++) { 
                Line line = new Line() { 
                    StartPoint = new Avalonia.Point(rnd.Next(340), rnd.Next(90)),
                    EndPoint = new Avalonia.Point(rnd.Next(340), rnd.Next(90)),
                    Stroke = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rnd.Next(256)), Convert.ToByte(rnd.Next(255)), Convert.ToByte(rnd.Next(255)))),
                    StrokeThickness = rnd.Next(3),
                
                };
                cap.Children.Add(line);

            }
            Capcha = cap;
        }
    }
}