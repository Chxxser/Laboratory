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
    public class MainForm : Form, Shared.IView
    {
        public DataGridView gridPhones;
        public Button btnAdd;
        public Button btnToggleView;
        public Button btnFindById;
        public Button btnEdit;
        public Button btnDelete;
        public Button btnGroupMemory;
        public Button btnTopExpensive;
        public Label lblTitle;
        public Label lblCount;
        public Label lblHint;

        public event Action AddPhoneClicked;
        public event Action<int> DeletePhoneClicked;
        public event Action RefreshClicked;
        public event Action<int> FindPhoneClicked;
        public event Action<int> EditPhoneClicked;
        public event Action GroupMemoryClicked;
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
        /// Отрисовать список телефонов в таблице
        /// </summary>
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
        /// Показать сообщение пользователю
        /// </summary>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void BtnAdd_Click(object sender, EventArgs e) => AddPhoneClicked?.Invoke();

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (gridPhones.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(gridPhones.SelectedRows[0].Cells["Id"].Value);
                DeletePhoneClicked?.Invoke(id);
            }
        }

        private void BtnFindById_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введите ID телефона:", "Поиск по ID", "");
            if (int.TryParse(input, out int id)) FindPhoneClicked?.Invoke(id);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (gridPhones.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(gridPhones.SelectedRows[0].Cells["Id"].Value);
                EditPhoneClicked?.Invoke(id);
            }
        }

        private void BtnGroupMemory_Click(object sender, EventArgs e) => GroupMemoryClicked?.Invoke();

        private void BtnTopExpensive_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Сколько показать?", "Топ дорогих", "3");
            if (int.TryParse(input, out int count)) TopExpensiveClicked?.Invoke(count);
        }


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