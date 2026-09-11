using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logic;
using Model;

namespace WinForms
{
    public partial class MainForm : Form
    {
        private Logic.Logic _logic = new Logic.Logic();

        private ListBox listBoxPhones;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnGroupMemory;
        private Button btnTopExpensive;
        private Label lblTitle;
        private Label lblCount;

        public MainForm()
        {
            InitializeComponent();
            _logic.TestPhone(); 
            RefreshList();
        }

        private void InitializeComponent()
        {
            this.Text = "Магазин телефонов";
            this.Size = new Size(950, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;


            lblTitle = new Label();
            lblTitle.Text = "Список телефонов";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = Color.DarkBlue;

         
            listBoxPhones = new ListBox();
            listBoxPhones.Location = new Point(20, 60);
            listBoxPhones.Size = new Size(620, 520);
            listBoxPhones.Font = new Font("Consolas", 11);
            listBoxPhones.BackColor = Color.WhiteSmoke;
            listBoxPhones.BorderStyle = BorderStyle.FixedSingle;

            
            btnAdd = CreateButton("Добавить", 660, 60, Color.LightGreen);
            btnAdd.Click += BtnAdd_Click;

            
            btnEdit = CreateButton("Редактировать", 660, 105, Color.LightYellow);
            btnEdit.Click += BtnEdit_Click;

            
            btnDelete = CreateButton("Удалить", 660, 150, Color.LightCoral);
            btnDelete.Click += BtnDelete_Click;

            
            btnRefresh = CreateButton("Обновить", 660, 195, Color.LightBlue);
            btnRefresh.Click += BtnRefresh_Click;

      
            btnGroupMemory = CreateButton("Группировка по памяти", 660, 260, Color.LightCyan);
            btnGroupMemory.Click += BtnGroupMemory_Click;

        
            btnTopExpensive = CreateButton("Топ дорогих", 660, 305, Color.Orange);
            btnTopExpensive.Click += BtnTopExpensive_Click;

            
            lblCount = new Label();
            lblCount.Text = "Всего: 0 телефонов";
            lblCount.Font = new Font("Arial", 10, FontStyle.Italic);
            lblCount.Location = new Point(20, 590);
            lblCount.AutoSize = true;
            lblCount.ForeColor = Color.Gray;

      
            Controls.Add(lblTitle);
            Controls.Add(listBoxPhones);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Controls.Add(btnGroupMemory);
            Controls.Add(btnTopExpensive);
            Controls.Add(lblCount);
        }

        private Button CreateButton(string text, int x, int y, Color color)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Location = new Point(x, y);
            btn.Size = new Size(240, 38);
            btn.Font = new Font("Arial", 10, FontStyle.Bold);
            btn.BackColor = color;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void RefreshList()
        {
            List<Phone> phones = _logic.AllPhone();
            listBoxPhones.DataSource = null;
            listBoxPhones.DataSource = phones;
            listBoxPhones.DisplayMember = "ToString";
            lblCount.Text = "Всего: " + phones.Count + " телефонов";
        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddEditForm form = new AddEditForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                _logic.AddPhone(form.Brand, form.Model, form.Year, form.Color,
                                form.Price, form.Memory, form.Availability);
                RefreshList();
                MessageBox.Show("Телефон успешно добавлен", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listBoxPhones.SelectedItem == null)
            {
                MessageBox.Show("Выберите телефон для редактирования.", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Phone selected = (Phone)listBoxPhones.SelectedItem;
            AddEditForm form = new AddEditForm(selected);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _logic.UpdatePhone(selected.Id, form.Brand, form.Model, form.Year,
                                   form.Color, form.Price, form.Memory, form.Availability);
                RefreshList();
                MessageBox.Show("Телефон обновлен", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

   
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxPhones.SelectedItem == null)
            {
                MessageBox.Show("Выберите телефон для удаления", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Phone selected = (Phone)listBoxPhones.SelectedItem;

            DialogResult result = MessageBox.Show(
                "Удалить " + selected.Brand + " " + selected.Model + "?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _logic.DeletePhone(selected.Id);
                RefreshList();
                MessageBox.Show("Телефон удалён", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

     
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        
        private void BtnGroupMemory_Click(object sender, EventArgs e)
        {
            Dictionary<int, List<Phone>> groups = _logic.GroupMemory();

            if (groups.Count == 0)
            {
                MessageBox.Show("Нет телефонов для группировки.", "Результат",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string message = "Группировка по памяти:\n\n";

            foreach (var group in groups)
            {
                message = message + group.Key + " ГБ:\n";

                foreach (Phone phone in group.Value)
                {
                    message = message + "  - " + phone.Brand + " " + phone.Model + "\n";
                }

                message = message + "\n";
            }

            MessageBox.Show(message, "Группировка по памяти",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void BtnTopExpensive_Click(object sender, EventArgs e)
        {
          
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Сколько телефонов показать?",
                "Топ дорогих",
                "3");

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            int count;
            if (!int.TryParse(input, out count))
            {
                MessageBox.Show("Введите корректное число", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (count <= 0)
            {
                MessageBox.Show("Число должно быть больше нуля", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<Phone> top = _logic.ExpensivePhones(count);

            if (top.Count == 0)
            {
                MessageBox.Show("Нет телефонов", "Результат",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string message = "Топ-" + top.Count + " самых дорогих телефона:\n\n";
            int place = 1;

            foreach (Phone phone in top)
            {
                message = message + place + ". " + phone.Brand + " " + phone.Model +
                          " - " + phone.Price + " руб.\n";
                place = place + 1;
            }

            MessageBox.Show(message, "Топ дорогих",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}