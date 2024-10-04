# Разработать лаунчер мини-игр и 2 простые мини-игры для тестов.

Особенностью ТЗ является то, что все данные и ассеты из игр должны находиться на сервере и загружаться\выгружаться с устройства пользователя по требованию самого пользователя (Кнопки на главном меню). 

Можно использовать любой доступный сервер. К примеру Unity Cloud Content Delivery бесплатен до определенного кол-ва загрузок. Во время самой игры доступа к интернету может не быть и это надо обработать.

Самое главное в задании - создание гибкой системы загрузки и выгрузки ресурсов мини-игр.

Цель для проверяющего
Оценить следующие параметры:

- Качество кода

- Умение работать со сторонними библиотеками и профайлером

- Знание C# и Unity

- Знание git

- Время выполнения задания

ЗАДАНИЕ

Главное меню

- Там находятся 2 кнопки по нажатию на которые осуществляется запуск выбранной игры. 
Также у каждой такой кнопки есть еще 2 на которые можно загрузить/удалить контент мини-игры. 

Game 1

- 1я игра это простой кликер. Картинка на которую можно нажать и получить 1 очко. Кол-во кликов по картинке сохраняется между сессиями и также после выгрузки игры из памяти. Кнопка выхода на главное меню также должна присутствовать. Кнопка клик должна быть спрайтом из любого доступного источника.
(Пример визуала на картинке ниже, но надо поменять кнопку клик на любой спрайт и добавить кнопку выхода в главное меню)

Game 2

- 2я игра это простая бродилка из точки A в точку B. Есть 3д модель главного героя (Можно взять любую из любого источника) и также модель финиша. При столкновении героя с финишом получаем окно победы в котором фиксируется текущее время за которое было пройдено и лучший результат. Лучший результат также должен сохраняться между сессиями и при выгрузке ресурсов. К примеру просто добежать до бара на WASD.

РЕЗУЛЬТАТ

- Время на выполнение задания - до 10 календарных дней. Результат должен быть предоставлен в ответном письме в виде ссылки на открытый репозиторий и содержать:

- Исходный код (версия Unity -> 22.3.19f1)

- Скомпилированное приложение с exe-файлом
