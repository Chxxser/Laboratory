using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Интерфейс представления (view). Presenter знает только про него
    /// </summary>
    public interface IView
    {
        event Action AddPhoneClicked;
        event Action<int> DeletePhoneClicked;
        event Action RefreshClicked;
        void ShowPhones(IEnumerable<object> phones);
        void ShowMessage(string message);
    }
}
