using DataAccessLayer;
using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms
{
    public class MainForm : Form
    {
        public Logic.Logic _logic = new Logic.Logic( new EntityRepository<Phone>() );

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

        public MainForm()
        {
            BuildForm();
            _logic.TestPhone();
        }
        /// <summary>
        /// Интерфейс
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
        /// Логика кнопок
        /// </summary>
        /// <param name="text">Название</param>
        /// <param name="x">Координата x</param>
        /// <param name="y">Координата y</param>
        /// <returns>Созданная кнопка</returns>
        public Button CreateButton(string text, int x, int y)
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
        /// Все телефоны в таблице
        /// </summary>
        public void RefreshList()
        {
            List<Phone> phones = _logic.AllPhone();

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

            foreach (Phone phone in phones)
            {
                string status;
                if (phone.Availability == true) { status = "В наличии"; }
                else { status = "Нет в наличии"; }

                gridPhones.Rows.Add(
                    phone.Id,
                    phone.Brand,
                    phone.Model,
                    phone.Year,
                    phone.Color,
                    phone.Memory,
                    phone.Price,
                    status
                );
            }

            lblCount.Text = "Всего: " + phones.Count + " телефонов";
        }
        /// <summary>
        /// Показ и скрытие всех телефонов
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">параметры события</param>
        public void BtnToggleView_Click(object sender, EventArgs e)
        {
            if (gridPhones.Visible == false)
            {
                gridPhones.Visible = true;
                lblTitle.Visible = true;
                lblHint.Visible = false;
                btnToggleView.Text = "Скрыть";
                RefreshList();
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
        /// <summary>
        /// Меню добавления
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">параметры события</param>
        public void BtnAdd_Click(object sender, EventArgs e)
        {
            AddEditForm form = new AddEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _logic.AddPhone(form.Brand, form.Model, form.Year, form.PhoneColor,
                                form.Price, form.Memory, form.Availability);
                MessageBox.Show("Телефон добавлен", "Успех");

                if (gridPhones.Visible == true)
                {
                    RefreshList();
                }
            }
        }
        /// <summary>
        /// Меню поиска по ID
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">параметры события</param>
        public void BtnFindById_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите ID телефона:", "Поиск по ID", "");

            if (string.IsNullOrWhiteSpace(input)) { return; }

            int id;
            if (int.TryParse(input, out id) == false)
            {
                MessageBox.Show("ID должен быть числом", "Ошибка");
                return;
            }

            if (id <= 0)
            {
                MessageBox.Show("ID должен быть больше нуля", "Ошибка");
                return;
            }

            Phone phone = _logic.PhoneId(id);

            if (phone == null)
            {
                MessageBox.Show("Телефон с ID " + id + " не найден", "Результат");
                return;
            }

            MessageBox.Show(phone.ToString(), "Найден телефон ID " + id);
        }
        /// <summary>
        /// Меню обновления
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">параметры события</param>
        public void BtnEdit_Click(object sender, EventArgs e)
        {
            if (gridPhones.Visible == false)
            {
                MessageBox.Show("Сначала нажмите «Показать все»", "Внимание");
                return;
            }

            if (gridPhones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите телефон из списка", "Внимание");
                return;
            }

            int id = Convert.ToInt32(gridPhones.SelectedRows[0].Cells["Id"].Value);
            Phone selected = _logic.PhoneId(id);

            if (selected == null)
            {
                MessageBox.Show("Телефон не найден", "Ошибка");
                return;
            }

            AddEditForm form = new AddEditForm(selected);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _logic.UpdatePhone(selected.Id, form.Brand, form.Model, form.Year,
                                   form.PhoneColor, form.Price, form.Memory, form.Availability);
                RefreshList();
                MessageBox.Show("Телефон обновлён", "Успех");
            }
        }
        /// <summary>
        /// Меню удаления
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">параметры события</param>
        public void BtnDelete_Click(object sender, EventArgs e)
        {
            if (gridPhones.Visible == false)
            {
                MessageBox.Show("Сначала нажмите «Показать все»", "Внимание");
                return;
            }

            if (gridPhones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите телефон из списка", "Внимание");
                return;
            }

            int id = Convert.ToInt32(gridPhones.SelectedRows[0].Cells["Id"].Value);
            Phone selected = _logic.PhoneId(id);

            if (selected == null)
            {
                MessageBox.Show("Телефон не найден", "Ошибка");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Удалить " + selected.Brand + " " + selected.Model + "?",
                "Подтверждение", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _logic.DeletePhone(selected.Id);
                RefreshList();
                MessageBox.Show("Телефон удалён", "Успех");
            }
        }
        /// <summary>
        /// Меню группировки
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">параметры события</param>
        public void BtnGroupMemory_Click(object sender, EventArgs e)
        {
            Dictionary<int, List<Phone>> groups = _logic.GroupMemory();

            if (groups.Count == 0)
            {
                MessageBox.Show("Нет телефонов");
                return;
            }

            string message = "Группировка по памяти:\n\n";
            foreach (var group in groups)
            {
                message = message + group.Key + " ГБ:\n";
                foreach (Phone p in group.Value)
                {
                    message = message + "  - " + p.Brand + " " + p.Model + "\n";
                }
                message = message + "\n";
            }

            MessageBox.Show(message, "Группировка");
        }
        /// <summary>
        /// Меню топ дорогих
        /// </summary>
        /// <param name="sender">Кнопка вызов события</param>
        /// <param name="e">параметры события</param>
        private void BtnTopExpensive_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Сколько показать?", "Топ дорогих", "3");

            if (string.IsNullOrWhiteSpace(input)) return;

            int count;
            if (int.TryParse(input, out count) == false || count <= 0)
            {
                MessageBox.Show("Введите корректное число");
                return;
            }

            List<Phone> top = _logic.ExpensivePhones(count);

            if (top.Count == 0)
            {
                MessageBox.Show("Нет телефонов");
                return;
            }

            string message = "Топ-" + top.Count + ":\n\n";
            int place = 1;
            foreach (Phone p in top)
            {
                message = message + place + ". " + p.Brand + " " + p.Model + " - " + p.Price + " руб.\n";
                place++;
            }

            MessageBox.Show(message, "Топ дорогих");
        }
    }
}