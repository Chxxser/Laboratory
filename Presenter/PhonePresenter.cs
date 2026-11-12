using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Logic;
using Model;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Presenter
{
    public class PhonePresenter
    {
        public readonly IView _view;
        public readonly ILogic _logic;
        public PhonePresenter(IView view, ILogic logic)
        {
            _view = view;
            _logic = logic;
            _view.AddPhoneClicked += OnAddPhone;
            _view.DeletePhoneClicked += OnDeletePhone;
            _view.RefreshClicked += OnRefresh;
        }
        public void OnAddPhone()
        {
            _logic.AddPhone("Test Brand", "Test Model", 2024, "Black", 1000, 128, true);
            _view.ShowMessage("Телефон добавлен!");
            OnRefresh();
        }
        public void OnDeletePhone(int id)
        {
            _logic.DeletePhone(id);
            _view.ShowMessage("Телефон удален!");
            OnRefresh();
        }
        public void OnRefresh()
        {
            var phones = _logic.AllPhone();
            _view.ShowPhones(phones.Cast<object> ().ToList());
        }
    }
}
