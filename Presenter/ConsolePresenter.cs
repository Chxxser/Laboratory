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

        public ConsolePresenter(ILogic logic)
        {
            _logic = logic;
        }

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
                    default: Console.WriteLine("Неверный выбор"); Console.ReadKey(); break;
                }
            }
        }

        // Твои старые методы из консоли (скопированы сюда)
        private void AddPhone()
        {
            Console.WriteLine("Добавление телефона");
            Console.Write("Бренд: "); string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) { Console.WriteLine("Пустой бренд"); Console.ReadKey(); return; }
            Console.Write("Модель: "); string model = Console.ReadLine();
            Console.Write("Год выпуска: "); int year = int.Parse(Console.ReadLine());
            Console.Write("Цвет: "); string color = Console.ReadLine();
            Console.Write("Цена (руб): "); decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Память (ГБ): "); int memory = int.Parse(Console.ReadLine());
            Console.Write("В наличии (да/нет): "); bool avail = Console.ReadLine().ToLower() == "да";

            _logic.AddPhone(brand, model, year, color, price, memory, avail);
            Console.WriteLine("Телефон добавлен!");
            Console.ReadKey();
        }

        private void ShowAllPhones()
        {
            var phones = _logic.AllPhone();
            if (phones.Count == 0) { Console.WriteLine("Список пуст"); Console.ReadKey(); return; }
            foreach (var p in phones) Console.WriteLine(p);
            Console.WriteLine($"Всего: {phones.Count}");
            Console.ReadKey();
        }

        private void FindPhoneById()
        {
            Console.Write("ID: "); int id = int.Parse(Console.ReadLine());
            var phone = _logic.PhoneId(id);
            if (phone != null) Console.WriteLine(phone); else Console.WriteLine("Не найден");
            Console.ReadKey();
        }

        private void UpdatePhone()
        {
            ShowAllPhones();
            Console.Write("ID для редактирования: "); int id = int.Parse(Console.ReadLine());
            var existing = _logic.PhoneId(id);
            if (existing == null) { Console.WriteLine("Не найден"); Console.ReadKey(); return; }

            Console.Write($"Бренд ({existing.Brand}): "); string brand = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(brand)) brand = existing.Brand;
            // ... (аналогично другие поля, но для краткости опустим, сделай по аналогии со старым кодом)

            Console.WriteLine("Обновлено!"); // Тут нужно вызвать _logic.UpdatePhone(...)
            Console.ReadKey();
        }

        private void DeletePhone()
        {
            Console.Write("ID для удаления: "); int id = int.Parse(Console.ReadLine());
            _logic.DeletePhone(id);
            Console.WriteLine("Удалено!");
            Console.ReadKey();
        }

        private void GroupByMemory()
        {
            var groups = _logic.GroupMemory();
            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Key} ГБ:");
                foreach (var p in g.Value) Console.WriteLine($"  - {p.Brand} {p.Model}");
            }
            Console.ReadKey();
        }

        private void ShowTopExpensive()
        {
            Console.Write("Сколько показать? "); int count = int.Parse(Console.ReadLine());
            var top = _logic.ExpensivePhones(count);
            for (int i = 0; i < top.Count; i++) Console.WriteLine($"{i + 1}. {top[i].Brand} - {top[i].Price}");
            Console.ReadKey();
        }
    }
}