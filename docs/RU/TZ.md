# Часть 1.
1. Пользователь вводит логин и пароль и нажимает на кнопку "Авторизация" (нужные значения логина и пароля нужно создать в отдельном классе как статические переменные, класс должен быть размещен в папке Models).
2. При успешной авторизации с первой попытки пользователь должен перейти на страницу, где будет в дальнейшем реализован простейший калькулятор. (пока никуда не переходите, просто выводите сообщение с успешной авторизацией)
3. Если пользователь ввел пароль неверно, то кнопка "Авторизация" пропадает и  пользователю должна стать доступной CAPTCHA и поле для ввода, куда нужно вводить указанный в ней текст. (компьютерный текст, используемый для того, чтобы определить, кем является пользователь системы: человеком или компьютером). Она должна соответствовать следующим требованиям:
 - состоять из рандомно сгенерированных линий разной длины и цвета;
 - текст внутри него должен состоять из случайно сгенерированных букв английского алфавита и цифр (длина текста от 7 до 10 символов). Между символами текста допускается одинаковое расстояние (но текст не должен быть расположен в одну линию). Текст обязательно должен находится внутри CAPTCHA и не выходить за пределы контейнера, в которой она находится. Также учтите тот момент, что символ должен быть рандомно или выделен полужирным шрифтом или быть курсивным (или и то и другое сразу).
4. Пользователь снова вводит в поля логин и пароль, а также  вводит сгенерированный текст в нужное поле и нажимает на кнопку проверить правильность ввода. Если все данные введены верно (в том числе и сгенерированный текст), то пользователь попадает на страницу с калькулятором.
5. Если и со второго раза не получилось авторизоваться, то капча и поле для ввода капчи исчезают (естественно, вместе с кнопкой), поля с введенными логином и паролем очищаются и становятся недоступными для ввода на 10 секунд (для этого реализуйте событие для таймера).
6. После истечения 10 секунд поля становятся доступными, появляется новая капча с полем для вводом и кнопкой для проверки введенных данных
7. При каждом последующем неправильном вводе пароля снова повторяются пункты 5 и 6.

# Часть 2
8. После успешной авторизации вы попадаете на страницу с простейшим калькулятором.
9. Также в проекте реализуйте какое-нибудь стилевое оформление

# Навигация
[Назад](../README.md) |