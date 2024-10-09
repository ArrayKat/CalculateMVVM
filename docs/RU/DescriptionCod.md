# Содержание проекта

При создании проекта на Avalonia .NET MVVM App (Avalonia UI) уже построена с учетом паттерна MVVM.
[imgMVVM](/Рисунок1.png)
Теперь немного поподробнее о каждой папочке:
1. В папке [Models](../Calculator/Models) хранится Файл AuthModels.cs - где хранятся данные о тестовом аккаунте.
2. В папке [View](../Calculator/Views) хранится верстка экранов приложения. 
3. В папке [ViewModels](../Calculator/ViewModels) хранится бизнес логика всего приложения. 

# View
Есть экран MainVindow.axaml - В котором содержится следующая верстка:
```csharp
    <StackPanel>
	    <UserControl Content="{Binding Us}"></UserControl>
    </StackPanel>
```
По сути, из всего приложения мы биндимся к данному элементу UserControl - то есть передаем экраны для отображения. Также, есть еще 2 файла верстки двух экранов: экрана авторизации (Auth.axaml) и странички с самим калькулятором (Calculate.axaml). Из интересного, что бы перейти на экран калькулятора - пользователь должен авторизаироваться. Сам переход и проверка данных осуществляется по следующему коду:
```csharp
    <Button IsEnabled="{Binding AuthVM.IsVisibleBtnAuth}" Command="{Binding ToCalculateView}">Войти</Button>
```
Получается, мы обращаемся к главному экрану MainWindowViewModel.cs в котором есть метод ToCalculateView. О нем, немного позже.

Хотелось бы обратить ваше внимание на то, как мы обращаемсяф к полям и свойствам на сторонних страничках, при условии того, что нужные поля и свойства находятся на соответствующей вью модели соответсвующей ей экрану. К примеру, Auth.axaml и AuthViewModel.cs.
На стороне верстки мы биндимся к какому либо свойству при обращении к инициализированному в MainVindowViewModel объекту нашего ViewModel. То есть в MainVindowViewModel:
```csharp
    AuthViewModel authVM = new AuthViewModel();
    public AuthViewModel AuthVM { get => authVM; set => authVM = value; }
```
А обращаемся к нашему ViewModel из файла Auth.axaml:
```csharp
    <TextBox IsEnabled="{Binding AuthVM.IsVisibleBtnAuth}" Text="{Binding AuthVM.Password}"></TextBox>
```
В то время, как в файле AuthViewModel.cs. есть поле и свойство Password:
```csharp
    string _password;
    public string Password { get => _password; set => this.RaiseAndSetIfChanged(ref _password, value); }

```

# ViewModel
### MainWindowViewModel
```csharp
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
```
Теперь о каждой строчке немного по подробнее:

```csharp
    UserControl _us = new Auth();
    public UserControl Us { get => _us; set => this.RaiseAndSetIfChanged(ref _us, value); }
```
Поле и свойство, которое позволяет нам обновлять наши вкладываемые страницы в главный экран (MainVindow.axaml).
```csharp
    AuthViewModel authVM = new AuthViewModel();
    public AuthViewModel AuthVM { get => authVM; set => authVM = value; }
```
Объявление поля и свойства к нему для инициализации бизнес логики приложения на странице авторизации.

```csharp
    CalculateViewModel calculateVM = new CalculateViewModel();
    public CalculateViewModel CalculateVM { get => calculateVM; set => calculateVM = value; }
```
Объявление поля и свойства к нему для инициализации бизнес логики приложения на странице калькулятора.
```csharp
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
```
Который, в свою очередь, обрабатывает данные которые ввел пользователь в файле AuthViewModel.cs и выводит сообщения в всплывающие окна (в Message Box). 
```csharp
    Us = new Calculate();
```
При помощи этого мы создаем представление для нашего экрана калькулятора и кладем в контейнер UserControl в файле MainVindow.axaml.

### CalculateViewModel и AuthViewModel
В данных файлах есть поля и их свойства, а так же такие методы как:
```csharp
    public void ChangeOperationSymb() {...}
```
Этот метод позволяет красиво отрисовать в зависимости от выбранного арифметического действия необходимый знак (+, -, /, *).
```csharp
    public async void Calculate() {...}
```
Этот метод позволяет вычислить пример и выдать ошибку в случае деления на 0 или не выбранной арифметической операции.
О каждой функции подробнее можно ознакомиться в коде. 

# Навигация
[Назад](../README.md) |