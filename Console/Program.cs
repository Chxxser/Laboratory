using System;
using System.Collections.Generic;
using Logic;
using Model;

namespace ConsoleApp
{
    class Program
    {
        private static Logic.Logic _logic = new Logic.Logic();

        static void Main()
        {
            _logic.TestPhone();
            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddPhone(); break;
                    case "2": ShowAllPhones(); break;
                    case "3": FindPhoneById(); break;
                    case "4": UpdatePhone(); break;
                    case "5": DeletePhone(); break;
                    case "6": GroupByMemory(); break;
                    case "7": ShowTopExpensive(); break;
                    case "0":
                        Console.WriteLine("До свидания");
                        return;
                    default:
                        Console.WriteLine("Такого выбора нет в меню");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу");
                Console.ReadKey();
            }
        }



        static void ShowMenu()
        {

            Console.WriteLine("Магазин Телефонов Горбушка");
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
            Console.WriteLine("Добавление телефона\n");

            Console.Write("Бренд (Apple/Samsung/Xiaomi): ");
            string brand = Console.ReadLine();

            Console.Write("Модель: ");
            string model = Console.ReadLine();

            Console.Write("Год выпуска: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Цвет: ");
            string color = Console.ReadLine();

            Console.Write("Цена (руб): ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Память (ГБ): ");
            int memory = int.Parse(Console.ReadLine());

            Console.Write("В наличии (да/нет): ");
            bool availability;
            if (Console.ReadLine().ToLower() == "да") { availability = true; }
            else { availability = false; }

            Phone newPhone = _logic.AddPhone(brand, model, year, color, price, memory, availability);
            Console.WriteLine($"\nТелефон добавлен");
            Console.WriteLine($"   {newPhone}");
        }


        static void ShowAllPhones()
        {
            Console.WriteLine("Список всех телефонов\n");

            List<Phone> phones = _logic.AllPhone();

            if (phones.Count == 0)
            {
                Console.WriteLine("Список телефонов пуст");
                return;
            }

            foreach (Phone phone in phones)
            {
                Console.WriteLine(phone);
            }
            Console.WriteLine($"\nВсего: {phones.Count} телефонов");
        }

        static void FindPhoneById()
        {
            Console.WriteLine("Поиск телефона по ID\n");

            Console.Write("Введите ID телефона: ");
            int id = int.Parse(Console.ReadLine());

            Phone phone = _logic.PhoneId(id);

            if (phone != null)
            {
                Console.WriteLine($"\nНайден телефон:");
                Console.WriteLine(phone);
            }
            else
            {
                Console.WriteLine($"\nТелефон с ID {id} не найден");
            }
        }

        static void UpdatePhone()
        {
            Console.WriteLine("Редактирование телефона\n");

            Console.Write("Введите ID телефона: ");
            int id = int.Parse(Console.ReadLine());

            Phone existing = _logic.PhoneId(id);

            if (existing == null)
            {
                Console.WriteLine($"\nТелефон с ID {id} не найден");
                return;
            }

            Console.WriteLine($"\nТекущие данные: {existing}\n");
            Console.WriteLine("(Если не хотите менять параметр, нажмите Enter)\n");

            Console.Write($"Бренд ({existing.Brand}): ");
            string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) { brand = existing.Brand; }

            Console.Write($"Модель ({existing.Model}): ");
            string model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model)) { model = existing.Model; }

            Console.Write($"Год выпуска ({existing.Year}): ");
            string yearInput = Console.ReadLine();
            int year;
            if (string.IsNullOrWhiteSpace(yearInput)) { year = existing.Year; }
            else { year = int.Parse(yearInput); }

            Console.Write($"Цвет ({existing.Color}): ");
            string color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color)) { color = existing.Color; }

            Console.Write($"Цена ({existing.Price}): ");
            string priceInput = Console.ReadLine();
            decimal price;
            if (string.IsNullOrWhiteSpace(priceInput)) { price = existing.Price; }
            else { price = decimal.Parse(priceInput); }

            Console.Write($"Память ({existing.Memory} ГБ): ");
            string memoryInput = Console.ReadLine();
            int memory;
            if (string.IsNullOrWhiteSpace(memoryInput)) { memory = existing.Memory; }
            else { memory = int.Parse(memoryInput); }

            string availStatus;
            if (existing.Availability) { availStatus = "да"; }
            else { availStatus = "нет"; }

            Console.Write($"В наличии ({availStatus}): ");
            string availInput = Console.ReadLine();
            bool availability;
            if (string.IsNullOrWhiteSpace(availInput)) { availability = existing.Availability; }
            else
            {
                if (availInput.ToLower() == "да") { availability = true; }
                else { availability = false; }
            }

            bool success = _logic.UpdatePhone(id, brand, model, year, color, price, memory, availability);

            if (success)
            {
                Console.WriteLine("\nТелефон успешно обновлен");
            }
        }

        static void DeletePhone()
        {
            Console.WriteLine("Удаление телефона\n");

            Console.Write("Введите ID телефона: ");
            int id = int.Parse(Console.ReadLine());

            Phone phone = _logic.PhoneId(id);

            if (phone == null)
            {
                Console.WriteLine($"\nТелефон с ID  {id}  не найден");
                return;
            }

            Console.WriteLine($"\nВы уверены, что хотите удалить:");
            Console.WriteLine(phone);
            Console.Write("\nУдалить? (да/нет): ");

            if (Console.ReadLine().ToLower() == "да")
            {
                bool success = _logic.DeletePhone(id);
                if (success) { Console.WriteLine("\nТелефон успешно удален"); }
            }
            else { Console.WriteLine("\nОперация отменена"); }
        }
        static void GroupByMemory()
        {
            Console.WriteLine("Группировка по памяти\n");

            Dictionary<int, List<Phone>> groups = _logic.GroupMemory();

            if (groups.Count == 0)
            {
                Console.WriteLine("Нет телефонов для группировки");
                return;
            }

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
            Console.WriteLine("Топ самых дорогих телефонов\n");

            Console.Write("Введите число топа, который хотите увидеть: ");
            int count = int.Parse(Console.ReadLine());

            List<Phone> top = _logic.ExpensivePhones(count);

            if (top.Count == 0)
            {
                Console.WriteLine("Нет телефонов");
                return;
            }

            Console.WriteLine($"\nТоп-{top.Count} самых дорогих телефона:\n");

            int place = 1;
            foreach (Phone phone in top)
            {
                Console.WriteLine($"{place}. {phone.Brand} {phone.Model} - {phone.Price} руб.");
                place++;
            }
        }
    }
}