using Humanizer;
using Shared;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    /// <summary>
    /// Консольное представление (View)
    /// </summary>
    public class ConsoleView : Shared.IView
    {
        /// <summary>
        /// Событие: нажата кнопка "Добавить"
        /// </summary>
        public event Action AddPhoneClicked;

        /// <summary>
        /// Событие: нажата кнопка "Удалить" (передает ID)
        /// </summary>
        public event Action<int> DeletePhoneClicked;

        /// <summary>
        /// Событие: нажата кнопка "Обновить"
        /// </summary>
        public event Action RefreshClicked;

        /// <summary>
        /// Событие: нажата кнопка "Найти по ID" (передает ID)
        /// </summary>
        public event Action<int> FindPhoneClicked;

        /// <summary>
        /// Событие: нажата кнопка "Редактировать" (передает ID)
        /// </summary>
        public event Action<int> EditPhoneClicked;

        /// <summary>
        /// Событие: нажата кнопка "Группировка по памяти"
        /// </summary>
        public event Action GroupMemoryClicked;

        /// <summary>
        /// Событие: нажата кнопка "Топ дорогих" (передает количество)
        /// </summary>
        public event Action<int> TopExpensiveClicked;

        /// <summary>
        /// Событие: данные для добавления телефона
        /// </summary>
        public event Action<PhoneDto> AddPhoneRequested;

        /// <summary>
        /// Событие: данные для редактирования телефона
        /// </summary>
        public event Action<PhoneDto> EditPhoneRequested;

        /// <summary>
        /// Вывод списка телефонов в консол
        /// </summary>
        /// <param name="phones">Список телефонов</param>
        public void ShowPhones(IEnumerable<PhoneDto> phones)
        {
            Console.WriteLine("\n--- Список телефонов ---");
            if (phones == null) return;
            foreach (var p in phones)
            {
                Console.WriteLine($"{p.Id}. {p.Brand} {p.Model} ({p.Year}) - {p.Color}, {p.Memory} ГБ, {p.Price} руб. - {(p.Availability ? "В наличии" : "Нет в наличии")}");
            }
        }

        /// <summary>
        /// Вывод сообщения в консоль
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void ShowMessage(string message)
        {
            Console.WriteLine($"\n{message}");
        }

        /// <summary>
        /// Запуск консольного меню
        /// </summary>
        public void Run()
        {
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
                    case "1": AddPhoneClicked?.Invoke(); break;
                    case "2": RefreshClicked?.Invoke(); break;
                    case "3":
                        Console.Write("ID: ");
                        if (int.TryParse(Console.ReadLine(), out int findId)) FindPhoneClicked?.Invoke(findId);
                        break;
                    case "4":
                        Console.Write("ID: ");
                        if (int.TryParse(Console.ReadLine(), out int editId)) EditPhoneClicked?.Invoke(editId);
                        break;
                    case "5":
                        Console.Write("ID: ");
                        if (int.TryParse(Console.ReadLine(), out int delId)) DeletePhoneClicked?.Invoke(delId);
                        break;
                    case "6": GroupMemoryClicked?.Invoke(); break;
                    case "7":
                        Console.Write("Сколько показать? ");
                        if (int.TryParse(Console.ReadLine(), out int topCount)) TopExpensiveClicked?.Invoke(topCount);
                        break;
                    case "0": return;
                }
                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Безопасный ввод целого числа
        /// </summary>
        /// <param name="prompt">Приглашение к вводу</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Введенное число</returns>
        private int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) { Console.WriteLine("Пустой ввод. Попробуйте снова."); continue; }
                if (!int.TryParse(input, out result)) { Console.WriteLine("Введите целое число."); continue; }
                if (result < min || result > max) { Console.WriteLine($"Число должно быть от {min} до {max}."); continue; }
                return result;
            }
        }

        /// <summary>
        /// Безопасный ввод дробного числа
        /// </summary>
        /// <param name="prompt">Приглашение к вводу</param>
        /// <param name="min">Минимальное значение</param>
        /// <param name="max">Максимальное значение</param>
        /// <returns>Введенное число</returns>
        private decimal ReadDecimal(string prompt, decimal min = 0, decimal max = decimal.MaxValue)
        {
            decimal result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) { Console.WriteLine("Пустой ввод. Попробуйте снова."); continue; }
                if (!decimal.TryParse(input, out result)) { Console.WriteLine("Введите число."); continue; }
                if (result < min || result > max) { Console.WriteLine($"Число должно быть от {min} до {max}."); continue; }
                return result;
            }
        }

        /// <summary>
        /// Безопасный ввод строки
        /// </summary>
        /// <param name="prompt">Приглашение к вводу</param>
        /// <returns>Введенная строка</returns>
        private string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) { Console.WriteLine("Поле не может быть пустым."); continue; }
                return input.Trim();
            }
        }

        /// <summary>
        /// Безопасный ввод "да/нет"
        /// </summary>
        /// <param name="prompt">Приглашение к вводу</param>
        /// <returns>True если "да", иначе False</returns>
        private bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.ToLower();
                if (input == "да" || input == "д" || input == "yes" || input == "y") return true;
                if (input == "нет" || input == "н" || input == "no" || input == "n") return false;
                Console.WriteLine("Введите 'да' или 'нет'.");
            }
        }

        /// <summary>
        /// Запрос данных для добавления нового телефона
        /// </summary>
        public void RequestAddPhone()
        {
            Console.Clear();
            Console.WriteLine("--- Добавление телефона ---");

            string brand = ReadString("Бренд: ");
            string model = ReadString("Модель: ");
            int year = ReadInt("Год выпуска (2000-2026): ", 2000, 2026);
            string color = ReadString("Цвет: ");
            decimal price = ReadDecimal("Цена (руб, от 1): ", 1, 1_000_000);
            int memory = ReadInt("Память (ГБ, от 4): ", 4, 2048);
            bool avail = ReadYesNo("В наличии (да/нет): ");
            var dto = new PhoneDto
            {
                Brand = brand,
                Model = model,
                Year = year,
                Color = color,
                Price = price,
                Memory = memory,
                Availability = avail
            };
            AddPhoneRequested?.Invoke(dto);
        }

        /// <summary>
        /// Запрос данных для редактирования телефона
        /// </summary>
        /// <param name="phone">Телефон для редактирования</param>
        public void RequestEditPhone(PhoneDto phone)
        {
            Console.Clear();
            Console.WriteLine($"--- Редактирование телефона (ID {phone.Id}) ---");
            Console.WriteLine("(Оставьте пустым, чтобы не менять)");

            Console.Write($"Бренд ({phone.Brand}): ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) brand = phone.Brand;

            Console.Write($"Модель ({phone.Model}): ");
            string model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model)) model = phone.Model;

            int year = phone.Year;
            while (true)
            {
                Console.Write($"Год ({phone.Year}): ");
                string yearInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(yearInput)) break;
                if (int.TryParse(yearInput, out year) && year >= 2000 && year <= 2026) break;
                Console.WriteLine("Ошибка: год от 2000 до 2026.");
            }

            Console.Write($"Цвет ({phone.Color}): ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color)) color = phone.Color;

            decimal price = phone.Price;
            while (true)
            {
                Console.Write($"Цена ({phone.Price}): ");
                string priceInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(priceInput)) break;
                if (decimal.TryParse(priceInput, out price) && price > 0) break;
                Console.WriteLine("Ошибка: введите положительное число.");
            }

            int memory = phone.Memory;
            while (true)
            {
                Console.Write($"Память ({phone.Memory}): ");
                string memoryInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(memoryInput)) break;
                if (int.TryParse(memoryInput, out memory) && memory > 0) break;
                Console.WriteLine("Ошибка: введите положительное число.");
            }

            bool avail = phone.Availability;
            string availStatus = avail ? "да" : "нет";
            while (true)
            {
                Console.Write($"В наличии ({availStatus}): ");
                string availInput = Console.ReadLine()?.ToLower();
                if (string.IsNullOrWhiteSpace(availInput)) break;
                if (availInput == "да" || availInput == "д" || availInput == "yes" || availInput == "y") { avail = true; break; }
                if (availInput == "нет" || availInput == "н" || availInput == "no" || availInput == "n") { avail = false; break; }
                Console.WriteLine("Ошибка: введите 'да' или 'нет'.");
            }
            var dto = new PhoneDto
            {
                Id = phone.Id,
                Brand = brand,
                Model = model,
                Year = year,
                Color = color,
                Price = price,
                Memory = memory,
                Availability = avail
            };
            EditPhoneRequested?.Invoke(dto);
        }
    }
}