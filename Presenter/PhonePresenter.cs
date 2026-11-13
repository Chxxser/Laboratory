using System;
using System.Linq;
using Shared;
using Logic;

namespace Presenter
{
    /// <summary>
    /// Презентер, связывающий View и Model (бизнес-логику)
    /// </summary>
    public class PhonePresenter
    {
        private readonly IView _view;
        private readonly ILogic _logic;

        /// <summary>
        /// Конструктор презентера
        /// </summary>
        /// <param name="view">Представление (интерфейс)</param>
        /// <param name="logic">Бизнес-логика</param>
        public PhonePresenter(IView view, ILogic logic)
        {
            _view = view;
            _logic = logic;

            _view.AddPhoneClicked += OnAddPhone;
            _view.DeletePhoneClicked += OnDeletePhone;
            _view.RefreshClicked += OnRefresh;
            _view.FindPhoneClicked += OnFindPhone;
            _view.EditPhoneClicked += OnEditPhone;
            _view.GroupMemoryClicked += OnGroupMemory;
            _view.TopExpensiveClicked += OnTopExpensive;
        }

        /// <summary>
        /// Обработчик добавления телефона
        /// </summary>
        private void OnAddPhone()
        {
            _logic.AddPhone("Test Brand", "Test Model", 2024, "Black", 1000, 128, true);
            _view.ShowMessage("Телефон добавлен!");
            OnRefresh();
        }

        /// <summary>
        /// Обработчик удаления телефона
        /// </summary>
        /// <param name="id">ID телефона</param>
        private void OnDeletePhone(int id)
        {
            _logic.DeletePhone(id);
            _view.ShowMessage("Телефон удален!");
            OnRefresh();
        }

        /// <summary>
        /// Обработчик обновления списка (перерисовка)
        /// </summary>
        private void OnRefresh()
        {
            var phones = _logic.AllPhone();
            _view.ShowPhones(phones.Cast<object>().ToList());
        }

        /// <summary>
        /// Обработчик поиска по ID
        /// </summary>
        /// <param name="id">ID телефона</param>
        private void OnFindPhone(int id)
        {
            var phone = _logic.PhoneId(id);
            if (phone != null)
                _view.ShowMessage($"Найден: {phone.Brand} {phone.Model}, Цена: {phone.Price}");
            else
                _view.ShowMessage("Телефон не найден!");
        }

        /// <summary>
        /// Обработчик редактирования телефона (пример: цена +1000)
        /// </summary>
        /// <param name="id">ID телефона</param>
        private void OnEditPhone(int id)
        {
            var phone = _logic.PhoneId(id);
            if (phone != null)
            {
                _logic.UpdatePhone(id, phone.Brand, phone.Model, phone.Year, phone.Color, phone.Price + 1000, phone.Memory, phone.Availability);
                _view.ShowMessage("Телефон отредактирован (цена +1000)!");
                OnRefresh();
            }
        }

        /// <summary>
        /// Обработчик группировки по памяти
        /// </summary>
        private void OnGroupMemory()
        {
            var groups = _logic.GroupMemory();
            string message = "Группировка по памяти:\n";
            foreach (var group in groups)
            {
                message += $"\n{group.Key} ГБ:\n";
                foreach (var p in group.Value)
                    message += $"  - {p.Brand} {p.Model}\n";
            }
            _view.ShowMessage(message);
        }

        /// <summary>
        /// Обработчик топ-N самых дорогих
        /// </summary>
        /// <param name="count">Количество телефонов</param>
        private void OnTopExpensive(int count)
        {
            var top = _logic.ExpensivePhones(count);
            string message = $"Топ-{count} дорогих:\n";
            int place = 1;
            foreach (var p in top)
            {
                message += $"{place}. {p.Brand} {p.Model} - {p.Price} руб.\n";
                place++;
            }
            _view.ShowMessage(message);
        }
    }
}