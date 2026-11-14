using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shared;
using Model;

namespace WinForms
{
    /// <summary>
    /// Главная форма приложения (реализация View)
    /// </summary>
    public class MainForm : Form, IView
    {
        /// <summary>
        /// Таблица для отображения телефонов
        /// </summary>
        public DataGridView gridPhones;

        /// <summary>
        /// Кнопка добавления телефона
        /// </summary>
        public Button btnAdd;

        /// <summary>
        /// Кнопка показать/скрыть список
        /// </summary>
        public Button btnToggleView;

        /// <summary>
        /// Кнопка поиска телефона по ID
        /// </summary>
        public Button btnFindById;

        /// <summary>
        /// Кнопка редактирования телефона
        /// </summary>
        public Button btnEdit;

        /// <summary>
        /// Кнопка удаления телефона
        /// </summary>
        public Button btnDelete;

        /// <summary>
        /// Кнопка группировки по памяти
        /// </summary>
        public Button btnGroupMemory;

        /// <summary>
        /// Кнопка топ дорогих телефонов
        /// </summary>
        public Button btnTopExpensive;

        /// <summary>
        /// Заголовок списка телефонов
        /// </summary>
        public Label lblTitle;

        /// <summary>
        /// Счетчик количества телефонов
        /// </summary>
        public Label lblCount;

        /// <summary>
        /// Подсказка для пользователя
        /// </summary>
        public Label lblHint;

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
        /// Конструктор главной формы
        /// </summary>
        public MainForm()
        {
            BuildForm();
        }

        /// <summary>
        /// Построение интерфейса формы
        /// </summary>
        private void BuildForm()
        {
            this.Text = "Магазин телефонов 'У АШОТА НА ГОРБУШКЕ'";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            lblTitle = new Label();
            lblTitle.Text = "Список телефонов";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = Color.Black;
            lblTitle.Visible = false;

            gridPhones = new DataGridView();
            gridPhones.Location = new Point(20, 60);
            gridPhones.Size = new Size(780, 520);
            gridPhones.Font = new Font("Arial", 10);
            gridPhones.BackgroundColor = Color.White;
            gridPhones.BorderStyle = BorderStyle.FixedSingle;
            gridPhones.AllowUserToAddRows = false;
            gridPhones.AllowUserToDeleteRows = false;
            gridPhones.ReadOnly = true;
            gridPhones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridPhones.MultiSelect = false;
            gridPhones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridPhones.RowHeadersVisible = false;
            gridPhones.Visible = false;

            lblHint = new Label();
            lblHint.Text = "Нажмите «Показать все», чтобы увидеть список телефонов";
            lblHint.Font = new Font("Arial", 14, FontStyle.Italic);
            lblHint.Location = new Point(160, 280);
            lblHint.Size = new Size(500, 80);
            lblHint.TextAlign = ContentAlignment.MiddleCenter;
            lblHint.ForeColor = Color.Gray;

            btnAdd = CreateButton("Добавить", 820, 60);
            btnAdd.Click += BtnAdd_Click;

            btnToggleView = CreateButton("Показать все", 820, 105);
            btnToggleView.Click += BtnToggleView_Click;

            btnFindById = CreateButton("Найти по ID", 820, 150);
            btnFindById.Click += BtnFindById_Click;

            btnEdit = CreateButton("Редактировать", 820, 195);
            btnEdit.Click += BtnEdit_Click;

            btnDelete = CreateButton("Удалить", 820, 240);
            btnDelete.Click += BtnDelete_Click;

            btnGroupMemory = CreateButton("Группировка по памяти", 820, 310);
            btnGroupMemory.Click += BtnGroupMemory_Click;

            btnTopExpensive = CreateButton("Топ дорогих", 820, 355);
            btnTopExpensive.Click += BtnTopExpensive_Click;

            lblCount = new Label();
            lblCount.Text = "";
            lblCount.Font = new Font("Arial", 10, FontStyle.Italic);
            lblCount.Location = new Point(20, 590);
            lblCount.AutoSize = true;
            lblCount.ForeColor = Color.Gray;

            Controls.Add(lblHint);
            Controls.Add(lblTitle);
            Controls.Add(gridPhones);
            Controls.Add(btnAdd);
            Controls.Add(btnToggleView);
            Controls.Add(btnFindById);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnGroupMemory);
            Controls.Add(btnTopExpensive);
            Controls.Add(lblCount);
        }

        /// <summary>
        /// Создание кнопки с заданными параметрами
        /// </summary>
        /// <param name="text">Текст кнопки</param>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>Созданная кнопка</returns>
        private Button CreateButton(string text, int x, int y)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(240, 38);
            btn.Font = new Font("Arial", 10, FontStyle.Bold);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.Black;
            return btn;
        }

        /// <summary>
        /// Отрисовка списка телефонов в таблице
        /// </summary>
        /// <param name="phones">Список телефонов для отображения</param>
        public void ShowPhones(IEnumerable<object> phones)
        {
            gridPhones.Rows.Clear();
            gridPhones.Columns.Clear();

            gridPhones.Columns.Add("Id", "ID");
            gridPhones.Columns.Add("Brand", "Бренд");
            gridPhones.Columns.Add("Model", "Модель");
            gridPhones.Columns.Add("Year", "Год");
            gridPhones.Columns.Add("Color", "Цвет");
            gridPhones.Columns.Add("Memory", "Память (ГБ)");
            gridPhones.Columns.Add("Price", "Цена (руб)");
            gridPhones.Columns.Add("Availability", "Наличие");

            foreach (var item in phones)
            {
                var phone = (Phone)item;
                gridPhones.Rows.Add(
                    phone.Id,
                    phone.Brand,
                    phone.Model,
                    phone.Year,
                    phone.Color,
                    phone.Memory,
                    phone.Price,
                    phone.Availability ? "В наличии" : "Нет в наличии"
                );
            }
            lblCount.Text = "Всего: " + gridPhones.Rows.Count + " телефонов";
        }

        /// <summary>
        /// Показ всплывающего сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">Параметры события</param>
        private void BtnAdd_Click(object sender, EventArgs e) => AddPhoneClicked?.Invoke();

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">Параметры события</param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (gridPhones.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(gridPhones.SelectedRows[0].Cells["Id"].Value);
                DeletePhoneClicked?.Invoke(id);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Найти по ID"
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">Параметры события</param>
        private void BtnFindById_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введите ID телефона:", "Поиск по ID", "");
            if (int.TryParse(input, out int id)) FindPhoneClicked?.Invoke(id);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Редактировать"
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">Параметры события</param>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (gridPhones.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(gridPhones.SelectedRows[0].Cells["Id"].Value);
                EditPhoneClicked?.Invoke(id);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Группировка по памяти"
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">Параметры события</param>
        private void BtnGroupMemory_Click(object sender, EventArgs e) => GroupMemoryClicked?.Invoke();

        /// <summary>
        /// Обработчик нажатия кнопки "Топ дорогих"
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">Параметры события</param>
        private void BtnTopExpensive_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Сколько показать?", "Топ дорогих", "3");
            if (int.TryParse(input, out int count)) TopExpensiveClicked?.Invoke(count);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Показать все / Скрыть"
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">Параметры события</param>
        private void BtnToggleView_Click(object sender, EventArgs e)
        {
            if (gridPhones.Visible == false)
            {
                gridPhones.Visible = true;
                lblTitle.Visible = true;
                lblHint.Visible = false;
                btnToggleView.Text = "Скрыть";
                RefreshClicked?.Invoke();
            }
            else
            {
                gridPhones.Visible = false;
                lblTitle.Visible = false;
                lblHint.Visible = true;
                btnToggleView.Text = "Показать все";
                lblCount.Text = "";
            }
        }
    }
}