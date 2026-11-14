using System;
using System.Linq;
using Shared;
using Logic;
using WinForms; // Ссылка на проект View

namespace Presenter
{
    public class PhonePresenter
    {
        private readonly IView _view;
        private readonly ILogic _logic;

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

        private void OnAddPhone()
        {
            // Создаем форму добавления (она лежит в WinForms)
            var form = new AddEditForm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _logic.AddPhone(form.Brand, form.Model, form.Year, form.PhoneColor, form.Price, form.Memory, form.Availability);
                _view.ShowMessage("Телефон добавлен!");
                OnRefresh();
            }
        }

        private void OnDeletePhone(int id)
        {
            _logic.DeletePhone(id);
            _view.ShowMessage("Удалено!");
            OnRefresh();
        }

        private void OnRefresh()
        {
            var phones = _logic.AllPhone();
            // Передаем как object, чтобы View не зависел от Model
            _view.ShowPhones(phones.Cast<object>().ToList());
        }

        private void OnFindPhone(int id)
        {
            var p = _logic.PhoneId(id);
            if (p != null) _view.ShowMessage(p.ToString()); else _view.ShowMessage("Не найден");
        }

        private void OnEditPhone(int id)
        {
            var phone = _logic.PhoneId(id);
            if (phone == null) return;

            // Открываем форму редактирования, передавая туда текущий телефон
            var form = new AddEditForm(phone); // У тебя конструктор принимает Phone
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _logic.UpdatePhone(id, form.Brand, form.Model, form.Year, form.PhoneColor, form.Price, form.Memory, form.Availability);
                _view.ShowMessage("Обновлено!");
                OnRefresh();
            }
        }

        private void OnGroupMemory()
        {
            var groups = _logic.GroupMemory();
            string msg = "Группировка:\n";
            foreach (var g in groups)
            {
                msg += $"\n{g.Key} ГБ:\n";
                foreach (var p in g.Value) msg += $"  - {p.Brand} {p.Model}\n";
            }
            _view.ShowMessage(msg);
        }

        private void OnTopExpensive(int count)
        {
            var top = _logic.ExpensivePhones(count);
            string msg = "";
            for (int i = 0; i < top.Count; i++) msg += $"{i + 1}. {top[i].Brand} - {top[i].Price}\n";
            _view.ShowMessage(msg);
        }
    }
}