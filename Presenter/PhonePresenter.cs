using System;
using System.Linq;
using Shared;
using Logic;
using WinForms;
using Model;
using ConsoleApp;

namespace Presenter
{
    /// <summary>
    /// Презентер, связывающий view и model
    /// </summary>
    public class PhonePresenter
    {
        /// <summary>
        /// Ссылка на представление (интерфейс IView)
        /// </summary>
        public readonly IView _view;

        /// <summary>
        /// Ссылка на бизнес-логику (интерфейс ILogic)
        /// </summary>
        public readonly ILogic _logic;

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
            _view.FindPhoneClicked += OnFindPhone;
            _view.RefreshClicked += () => _logic.AllPhone();
            _view.EditPhoneClicked += OnEditPhone;
            _view.GroupMemoryClicked += OnGroupMemory;
            _view.TopExpensiveClicked += OnTopExpensive;
            _view.AddPhoneRequested += OnAddPhoneData;
            _view.EditPhoneRequested += OnEditPhoneData;
            _logic.DataChanged += OnRefresh;
        }

        /// <summary>
        /// Обработчик добавления телефона (вызывается при нажатии кнопки)
        /// </summary>
        public void OnAddPhone()
        {
            if (_view is MainForm)
            {
                var form = new AddEditForm();
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _logic.AddPhone(form.Brand, form.Model, form.Year, form.PhoneColor, form.Price, form.Memory, form.Availability);
                    _view.ShowMessage("Телефон добавлен!");
                }
            }
            else if (_view is ConsoleView consoleView)
            {
                consoleView.RequestAddPhone();
            }
        }

        /// <summary>
        /// Обработчик данных добавления телефона (приходит из консоли)
        /// </summary>
        public void OnAddPhoneData(PhoneDto dto)
        {
            _logic.AddPhone(dto.Brand, dto.Model, dto.Year, dto.Color, dto.Price, dto.Memory, dto.Availability);
            _view.ShowMessage("Телефон добавлен!");
        }

        /// <summary>
        /// Обработчик удаления телефона
        /// </summary>
        /// <param name="id">ID телефона</param>
        public void OnDeletePhone(int id)
        {
            _logic.DeletePhone(id);
            _view.ShowMessage("Телефон удален!");
        }

        /// <summary>
        /// Обработчик обновления списка (перерисовка)
        /// </summary>
        public void OnRefresh(List<Phone> phones)
        {
            var dtos = phones.Select(p => new PhoneDto()
            {
                Id = p.Id,
                Brand = p.Brand,
                Model = p.Model,
                Year = p.Year,
                Color = p.Color,
                Memory = p.Memory,
                Price = p.Price,
                Availability = p.Availability
            }).ToList();
            _view.ShowPhones(dtos);
        }

        /// <summary>
        /// Обработчик поиска по ID
        /// </summary>
        /// <param name="id">ID телефона</param>
        public void OnFindPhone(int id)
        {
            var phone = _logic.PhoneId(id);
            if (phone != null) _view.ShowMessage(phone.ToString());
            else _view.ShowMessage("Телефон не найден!");
        }

        /// <summary>
        /// Обработчик редактирования телефона (вызывается при нажатии кнопки)
        /// </summary>
        /// <param name="id">ID телефона</param>
        public void OnEditPhone(int id)
        {
            var phone = _logic.PhoneId(id);
            if (phone == null) { _view.ShowMessage("Телефон не найден!"); return; }

            var dto = new PhoneDto()
            {
                Id = phone.Id,
                Brand = phone.Brand,
                Model = phone.Model,
                Year = phone.Year,
                Color = phone.Color,
                Memory = phone.Memory,
                Price = phone.Price,
                Availability = phone.Availability
            };

            if (_view is MainForm)
            {
                var form = new AddEditForm(dto);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _logic.UpdatePhone(id, form.Brand, form.Model, form.Year, form.PhoneColor, form.Price, form.Memory, form.Availability);
                    _view.ShowMessage("Телефон обновлён!");
                }
            }
            else if (_view is ConsoleView consoleView)
            {
                consoleView.RequestEditPhone(dto);
            }
        }

        /// <summary>
        /// Обработчик данных редактирования телефона (приходит из консоли)
        /// </summary>
        public void OnEditPhoneData(PhoneDto dto)
        {
            _logic.UpdatePhone(dto.Id, dto.Brand, dto.Model, dto.Year, dto.Color, dto.Price, dto.Memory, dto.Availability);
            _view.ShowMessage("Телефон обновлён!");
        }

        /// <summary>
        /// Обработчик группировки по памяти
        /// </summary>
        public void OnGroupMemory()
        {
            var groups = _logic.GroupMemory();
            string message = "Группировка по памяти:\n";
            foreach (var group in groups)
            {
                message += $"\n{group.Key} ГБ:\n";
                foreach (var p in group.Value) message += $"  - {p.Brand} {p.Model}\n";
            }
            _view.ShowMessage(message);
        }

        /// <summary>
        /// Обработчик топ-N самых дорогих
        /// </summary>
        /// <param name="count">Количество телефонов</param>
        public void OnTopExpensive(int count)
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