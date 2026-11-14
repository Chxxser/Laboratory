using System;
using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Интерфейс представления (View). Presenter знает только о нем.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Событие: нажата кнопка "Добавить"
        /// </summary>
        event Action AddPhoneClicked;

        /// <summary>
        /// Событие: нажата кнопка "Удалить" (передает ID)
        /// </summary>
        event Action<int> DeletePhoneClicked;

        /// <summary>
        /// Событие: нажата кнопка "Обновить"
        /// </summary>
        event Action RefreshClicked;

        /// <summary>
        /// Событие: нажата кнопка "Найти по ID" (передает ID)
        /// </summary>
        event Action<int> FindPhoneClicked;

        /// <summary>
        /// Событие: нажата кнопка "Редактировать" (передает ID)
        /// </summary>
        event Action<int> EditPhoneClicked;

        /// <summary>
        /// Событие: нажата кнопка "Группировка по памяти"
        /// </summary>
        event Action GroupMemoryClicked;

        /// <summary>
        /// Событие: нажата кнопка "Топ дорогих" (передает количество)
        /// </summary>
        event Action<int> TopExpensiveClicked;

        /// <summary>
        /// Отрисовать список телефонов в таблице
        /// </summary>
        /// <param name="phones">Список телефонов для отображения</param>
        void ShowPhones(IEnumerable<PhoneDto> phones);

        /// <summary>
        /// Показать всплывающее сообщение пользователю
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        void ShowMessage(string message);
    }
}