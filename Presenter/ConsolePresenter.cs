using System;
using System.Collections.Generic;
using Logic;
using Model;

namespace Presenter
{
    /// <summary>
    /// Презентер для консольного представления
    /// </summary>
    public class ConsolePresenter
    {
        private readonly ILogic _logic;

        /// <summary>
        /// Конструктор консольного презентера
        /// </summary>
        /// <param name="logic">Бизнес-логика приложения</param>
        public ConsolePresenter(ILogic logic)
        {
            _logic = logic;
        }

        /// <summary>
        /// Запуск консольного меню
        /// </summary>
        public void Run()
        {
            if (_logic.AllPhone().Count == 0) _logic.TestPhone();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== На Горбушке у Ашота (Консоль) ===");
                Console.WriteLine("1. Добавить телефон");
                Console.WriteLine("2. Показать все телефоны");
                Console.WriteLine("3. Найти телефон по ID");
                Console.WriteLine("4. Редактировать телефон");
                Console.WriteLine("5. Удалить телефон");
                Console.WriteLine("6. Группировка по памяти");
                Console.WriteLine("7. Топ самых дорогих");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddPhone(); break;
                    case "2": ShowAllPhones(); break;
                    case "3": FindPhoneById(); break;
                    case "4": UpdatePhone(); break;
                    case "5": DeletePhone(); break;
                    case "6": GroupByMemory(); break;
                    case "7": ShowTopExpensive(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Вспомогательный метод для вывода списка телефонов без ожидания клавиши
        /// </summary>
        private void PrintPhones()
        {
            var phones = _logic.AllPhone();
            if (phones.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }
            foreach (var p in phones) Console.WriteLine(p);
            Console.WriteLine($"Всего: {phones.Count}");
        }

        /// <summary>
        /// Добавление нового телефона с проверкой на пустые поля и корректность ввода
        /// </summary>
        private void AddPhone()
        {
            Console.Clear();
            Console.WriteLine("Добавление телефона");

            Console.Write("Бренд: ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) { ReturnToMenu(); return; }

            Console.Write("Модель: ");
            string model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model)) { ReturnToMenu(); return; }

            Console.Write("Год выпуска: ");
            if (!int.TryParse(Console.ReadLine(), out int year) || year < 2000 || year > DateTime.Now.Year + 1)
            {
                Console.WriteLine("Некорректный год.");
                ReturnToMenu();
                return;
            }

            Console.Write("Цвет: ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color)) { ReturnToMenu(); return; }

            Console.Write("Цена (руб): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
            {
                Console.WriteLine("Некорректная цена.");
                ReturnToMenu();
                return;
            }

            Console.Write("Память (ГБ): ");
            if (!int.TryParse(Console.ReadLine(), out int memory) || memory <= 0)
            {
                Console.WriteLine("Некорректная память.");
                ReturnToMenu();
                return;
            }

            Console.Write("В наличии (да/нет): ");
            bool avail = Console.ReadLine()?.ToLower() == "да";

            _logic.AddPhone(brand, model, year, color, price, memory, avail);
            Console.WriteLine("Телефон добавлен!");
            ReturnToMenu();
        }

        /// <summary>
        /// Отображение списка всех телефонов
        /// </summary>
        private void ShowAllPhones()
        {
            Console.Clear();
            PrintPhones();
            ReturnToMenu();
        }

        /// <summary>
        /// Поиск телефона по ID с проверкой на пустой ввод
        /// </summary>
        private void FindPhoneById()
        {
            Console.Clear();
            Console.Write("ID: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) { ReturnToMenu(); return; }

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID должен быть числом.");
                ReturnToMenu();
                return;
            }

            var phone = _logic.PhoneId(id);
            if (phone != null) Console.WriteLine(phone);
            else Console.WriteLine("Не найден.");
            ReturnToMenu();
        }

        /// <summary>
        /// Редактирование телефона по ID с проверкой на пустой ввод
        /// </summary>
        private void UpdatePhone()
        {
            Console.Clear();
            PrintPhones();

            Console.Write("\nID для редактирования: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) { ReturnToMenu(); return; }

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID должен быть числом.");
                ReturnToMenu();
                return;
            }

            var existing = _logic.PhoneId(id);
            if (existing == null)
            {
                Console.WriteLine("Не найден.");
                ReturnToMenu();
                return;
            }

            Console.Write($"Бренд ({existing.Brand}): ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) brand = existing.Brand;

            Console.Write($"Модель ({existing.Model}): ");
            string model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model)) model = existing.Model;

            Console.Write($"Год выпуска ({existing.Year}): ");
            string yearStr = Console.ReadLine();
            int year = existing.Year;
            if (!string.IsNullOrWhiteSpace(yearStr) && !int.TryParse(yearStr, out year)) year = existing.Year;

            Console.Write($"Цвет ({existing.Color}): ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color)) color = existing.Color;

            Console.Write($"Цена ({existing.Price}): ");
            string priceStr = Console.ReadLine();
            decimal price = existing.Price;
            if (!string.IsNullOrWhiteSpace(priceStr) && !decimal.TryParse(priceStr, out price)) price = existing.Price;

            Console.Write($"Память ({existing.Memory}): ");
            string memoryStr = Console.ReadLine();
            int memory = existing.Memory;
            if (!string.IsNullOrWhiteSpace(memoryStr) && !int.TryParse(memoryStr, out memory)) memory = existing.Memory;

            Console.Write($"В наличии ({(existing.Availability ? "да" : "нет")}): ");
            string availStr = Console.ReadLine();
            bool availability = string.IsNullOrWhiteSpace(availStr) ? existing.Availability : availStr.ToLower() == "да";

            _logic.UpdatePhone(id, brand, model, year, color, price, memory, availability);
            Console.WriteLine("Телефон обновлен!");
            ReturnToMenu();
        }

        /// <summary>
        /// Удаление телефона по ID с выводом списка и проверкой на пустой ввод
        /// </summary>
        private void DeletePhone()
        {
            Console.Clear();
            PrintPhones();

            Console.Write("\nID для удаления: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) { ReturnToMenu(); return; }

            if (!int.TryParse(input, out int id))
            {
                Console.WriteLine("ID должен быть числом.");
                ReturnToMenu();
                return;
            }

            _logic.DeletePhone(id);
            Console.WriteLine("Удалено!");
            ReturnToMenu();
        }

        /// <summary>
        /// Группировка телефонов по объему памяти
        /// </summary>
        private void GroupByMemory()
        {
            Console.Clear();
            var groups = _logic.GroupMemory();
            if (groups.Count == 0)
            {
                Console.WriteLine("Нет телефонов.");
                ReturnToMenu();
                return;
            }

            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Key} ГБ:");
                foreach (var p in g.Value) Console.WriteLine($"  - {p.Brand} {p.Model}");
            }
            ReturnToMenu();
        }

        /// <summary>
        /// Отображение топ-N самых дорогих телефонов
        /// </summary>
        private void ShowTopExpensive()
        {
            Console.Clear();
            Console.Write("Сколько показать? ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) { ReturnToMenu(); return; }

            if (!int.TryParse(input, out int count) || count <= 0)
            {
                Console.WriteLine("Некорректное число.");
                ReturnToMenu();
                return;
            }

            var top = _logic.ExpensivePhones(count);
            for (int i = 0; i < top.Count; i++)
                Console.WriteLine($"{i + 1}. {top[i].Brand} {top[i].Model} - {top[i].Price} руб.");
            ReturnToMenu();
        }

        /// <summary>
        /// Ожидание нажатия клавиши для возврата в главное меню
        /// </summary>
        private void ReturnToMenu()
        {
            Console.WriteLine("\nНажмите любую клавишу для возврата в меню...");
            Console.ReadKey();
        }
    }
}