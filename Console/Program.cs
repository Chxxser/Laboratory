using System;
using System.Collections.Generic;
using Logic;
using Model;

namespace ConsoleApp
{
    class Program
    {
        public static Logic.Logic _logic = new Logic.Logic();
        static void Main()
        {
            _logic.TestPhone();
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();
                switch (choice)
                {
                    case "1": AddPhone(); break;
                    case "2": ShowAllPhones(); break;
                    case "3": FindPhoneById(); break;
                    case "4": UpdatePhone(); break;
                    case "5": DeletePhone(); break;
                    case "6": GroupByMemory(); break;
                    case "7": ShowTopExpensive(); break;
                    case "0": Console.WriteLine("До свидания"); return;
                    default: Console.WriteLine("Такого выбора нет в меню"); break;
                }
            }
        }
        static void ShowMenu()
        {
            Console.WriteLine("=== На Горбушке у Ашота ===");
            Console.WriteLine("1. Добавить телефон");
            Console.WriteLine("2. Показать все телефоны");
            Console.WriteLine("3. Найти телефон по ID");
            Console.WriteLine("4. Редактировать телефон");
            Console.WriteLine("5. Удалить телефон");
            Console.WriteLine("6. Группировка по памяти");
            Console.WriteLine("7. Топ самых дорогих");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");
        }
        static void AddPhone()
        {
            Console.WriteLine("Добавление телефона");

            Console.Write("Бренд: ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) { Console.WriteLine("Пустой бренд"); return; }

            Console.Write("Модель: ");
            string model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model)) { Console.WriteLine("Пустая модель"); return; }

            Console.Write("Год выпуска: ");
            int year;
            if (int.TryParse(Console.ReadLine(), out year) == false) { Console.WriteLine("Введите целое число"); return; }

            Console.Write("Цвет: ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color)) { Console.WriteLine("Пустой цвет"); return; }

            Console.Write("Цена (руб): ");
            decimal price;
            if (decimal.TryParse(Console.ReadLine(), out price) == false) { Console.WriteLine("Введите число"); return; }

            Console.Write("Память (ГБ): ");
            int memory;
            if (int.TryParse(Console.ReadLine(), out memory) == false) { Console.WriteLine("Введите целое число"); return; }

            Console.Write("В наличии (да/нет): ");
            bool availability;
            if (Console.ReadLine().ToLower() == "да") { availability = true; }
            else { availability = false; }

            Phone newPhone = _logic.AddPhone(brand, model, year, color, price, memory, availability);
            Console.WriteLine($"Телефон добавлен");
            Console.WriteLine($"{newPhone}");
        }
        static void ShowAllPhones()
        {
            Console.WriteLine("Список всех телефонов");
            List<Phone> phones = _logic.AllPhone();
            if (phones.Count == 0) { Console.WriteLine("Список телефонов пуст"); return; }
            foreach (Phone phone in phones) { Console.WriteLine(phone); }
            Console.WriteLine($"Всего: {phones.Count} телефонов");
        }
        static void FindPhoneById()
        {
            Console.WriteLine("Поиск телефона по ID");
            Console.Write("Введите ID телефона: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("Введите целое число"); return; }
            Phone phone = _logic.PhoneId(id);
            if (phone != null)
            {
                Console.WriteLine($"Найден телефон:");
                Console.WriteLine(phone);
            }
            else { Console.WriteLine($"Телефон с ID {id} не найден"); }
        }
        static void UpdatePhone()
        {
            Console.WriteLine("Редактирование телефона");
            Console.Write("Введите ID телефона: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("Введите целое число"); return; }
            Phone existing = _logic.PhoneId(id);
            if (existing == null) { Console.WriteLine($"Телефон с ID {id} не найден"); return; }
            Console.WriteLine($"Текущие данные: {existing}");
            Console.WriteLine("(Если не хотите менять параметр, нажмите Enter)");

            Console.Write($"Бренд ({existing.Brand}): ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand) == true) { brand = existing.Brand; }

            Console.Write($"Модель ({existing.Model}): ");
            string model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model) == true) { model = existing.Model; }

            Console.Write($"Год выпуска ({existing.Year}): ");
            string year_ = Console.ReadLine();
            int year;
            if (string.IsNullOrWhiteSpace(year_) == true) { year = existing.Year; }
            else if (int.TryParse(year_, out year) == false) { Console.WriteLine("Введите целое число"); return; }
            else { year = int.Parse(year_); }

            Console.Write($"Цвет ({existing.Color}): ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color) == true) { color = existing.Color; }

            Console.Write($"Цена ({existing.Price}): ");
            string price_ = Console.ReadLine();
            decimal price;
            if (string.IsNullOrWhiteSpace(price_)) { price = existing.Price; }
            else if (decimal.TryParse(price_, out price) == false) { Console.WriteLine("Введите целое число"); return; }
            else { price = decimal.Parse(price_); }

            Console.Write($"Память ({existing.Memory} ГБ): ");
            string memory_ = Console.ReadLine();
            int memory;
            if (string.IsNullOrWhiteSpace(memory_)) { memory = existing.Memory; }
            else if (int.TryParse(memory_, out memory) == false) { Console.WriteLine("Введите целое число"); return; }
            else { memory = int.Parse(memory_); }

            string availStatus;
            if (existing.Availability) { availStatus = "да"; }
            else { availStatus = "нет"; }
            Console.Write($"В наличии ({availStatus}): ");
            string avail_ = Console.ReadLine();
            bool availability;
            if (string.IsNullOrWhiteSpace(avail_) == true) { availability = existing.Availability; }
            else
            {
                if (avail_.ToLower() == "да") { availability = true; }
                else { availability = false; }
            }

            bool success = _logic.UpdatePhone(id, brand, model, year, color, price, memory, availability);

            if (success) { Console.WriteLine("Телефон успешно обновлен"); }
        }

        static void DeletePhone()
        {
            Console.WriteLine("Удаление телефона");
            Console.Write("Введите ID телефона: ");
            int id;
            if (int.TryParse(Console.ReadLine(), out id) == false) { Console.WriteLine("Введите целое число"); return; }
            Phone phone = _logic.PhoneId(id);
            if (phone == null) { Console.WriteLine($"Телефон с ID  {id}  не найден"); return; }
            Console.WriteLine($"Вы уверены, что хотите удалить:");
            Console.WriteLine(phone);
            Console.Write("Удалить? (да/нет): ");
            if (Console.ReadLine().ToLower() == "да")
            {
                bool success = _logic.DeletePhone(id);
                if (success == true) { Console.WriteLine("Телефон успешно удален"); }
            }
            else { Console.WriteLine("Операция отменена"); }
        }
        static void GroupByMemory()
        {
            Console.WriteLine("Группировка по памяти");
            Dictionary<int, List<Phone>> groups = _logic.GroupMemory();
            if (groups.Count == 0) { Console.WriteLine("Нет телефонов для группировки"); return; }
            foreach (var group in groups)
            {
                Console.WriteLine($"{group.Key} ГБ:");
                foreach (Phone phone in group.Value)
                {
                    Console.WriteLine($"  - {phone.Brand} {phone.Model}");
                }
                Console.WriteLine();
            }
        }

        static void ShowTopExpensive()
        {
            Console.WriteLine("Топ самых дорогих телефонов");
            Console.Write("Введите число топа, который хотите увидеть: ");
            int count;
            if (int.TryParse(Console.ReadLine(), out count) == false || count <= 0) { Console.WriteLine("Введите целое число"); return; }
            List<Phone> top = _logic.ExpensivePhones(count);
            if (top.Count == 0) { Console.WriteLine("Нет телефонов"); return; }
            Console.WriteLine($"Топ-{top.Count} самых дорогих телефона:");
            int place = 1;
            foreach (Phone phone in top)
            {
                Console.WriteLine($"{place}. {phone.Brand} {phone.Model} - {phone.Price} руб.");
                place++;
            }
        }
    }
}