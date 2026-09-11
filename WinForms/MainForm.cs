using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logic;
using Model;

namespace WinForms
{
    public class MainForm : Form
    {
        private Logic.Logic _logic = new Logic.Logic();

        private ListBox listBoxPhones;
        private Button btnAdd;
        private Button btnToggleView;
        private Button btnFindById;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnGroupMemory;
        private Button btnTopExpensive;
        private Label lblTitle;
        private Label lblCount;
        private Label lblHint;

        public MainForm()
        {
            BuildForm();
            _logic.TestPhone();
        }

        private void BuildForm()
        {
            this.Text = "Магазин телефонов Горбушка";
            this.Size = new Size(950, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            lblTitle = new Label();
            lblTitle.Text = "Список телефонов";
            lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = Color.Black;
            lblTitle.Visible = false;

            listBoxPhones = new ListBox();
            listBoxPhones.Location = new Point(20, 60);
            listBoxPhones.Size = new Size(620, 520);
            listBoxPhones.Font = new Font("Consolas", 11);
            listBoxPhones.BackColor = Color.White;
            listBoxPhones.BorderStyle = BorderStyle.FixedSingle;
            listBoxPhones.Visible = false;

            lblHint = new Label();
            lblHint.Text = "Нажмите «Показать все»,\nчтобы увидеть список телефонов";
            lblHint.Font = new Font("Arial", 14, FontStyle.Italic);
            lblHint.Location = new Point(120, 280);
            lblHint.Size = new Size(450, 80);
            lblHint.TextAlign = ContentAlignment.MiddleCenter;
            lblHint.ForeColor = Color.Gray;

      
            btnAdd = CreateButton("Добавить", 660, 60);
            btnAdd.Click += BtnAdd_Click;

          
            btnToggleView = CreateButton("Показать все", 660, 105);
            btnToggleView.Click += BtnToggleView_Click;

           
            btnFindById = CreateButton("Найти по ID", 660, 150);
            btnFindById.Click += BtnFindById_Click;

           
            btnEdit = CreateButton("Редактировать", 660, 195);
            btnEdit.Click += BtnEdit_Click;

         
            btnDelete = CreateButton("Удалить", 660, 240);
            btnDelete.Click += BtnDelete_Click;

        
            btnGroupMemory = CreateButton("Группировка по памяти", 660, 310);
            btnGroupMemory.Click += BtnGroupMemory_Click;

            
            btnTopExpensive = CreateButton("Топ дорогих", 660, 355);
            btnTopExpensive.Click += BtnTopExpensive_Click;

            lblCount = new Label();
            lblCount.Text = "";
            lblCount.Font = new Font("Arial", 10, FontStyle.Italic);
            lblCount.Location = new Point(20, 590);
            lblCount.AutoSize = true;
            lblCount.ForeColor = Color.Gray;

            Controls.Add(lblHint);
            Controls.Add(lblTitle);
            Controls.Add(listBoxPhones);
            Controls.Add(btnAdd);
            Controls.Add(btnToggleView);
            Controls.Add(btnFindById);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnGroupMemory);
            Controls.Add(btnTopExpensive);
            Controls.Add(lblCount);
        }

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

        private void RefreshList()
        {
            List<Phone> phones = _logic.AllPhone();
            listBoxPhones.DataSource = null;
            listBoxPhones.DataSource = phones;
            listBoxPhones.DisplayMember = "ToString";
            lblCount.Text = "Всего: " + phones.Count + " телефонов";
        }

     
        private void BtnToggleView_Click(object sender, EventArgs e)
        {
            if (listBoxPhones.Visible == false)
            {
             
                listBoxPhones.Visible = true;
                lblTitle.Visible = true;
                lblHint.Visible = false;
                btnToggleView.Text = "Скрыть";
                RefreshList();
            }
            else
            {
              
                listBoxPhones.Visible = false;
                lblTitle.Visible = false;
                lblHint.Visible = true;
                btnToggleView.Text = "Показать все";
                lblCount.Text = "";
            }
        }

  
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddEditForm form = new AddEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                _logic.AddPhone(form.Brand, form.Model, form.Year, form.CarColor,
                                form.Price, form.Memory, form.Availability);
                MessageBox.Show("Телефон добавлен", "Успех");

                if (listBoxPhones.Visible == true)
                {
                    RefreshList();
                }
            }
        }

        
        private void BtnFindById_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Введите ID телефона:", "Поиск по ID", "");

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            int id;
            if (!int.TryParse(input, out id))
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

    
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (listBoxPhones.Visible == false)
            {
                MessageBox.Show("Сначала нажмите «Показать все»", "Внимание");
                return;
            }

            if (listBoxPhones.SelectedItem == null)
            {
                MessageBox.Show("Выберите телефон из списка", "Внимание");
                return;
            }

            Phone selected = (Phone)listBoxPhones.SelectedItem;
            AddEditForm form = new AddEditForm(selected);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _logic.UpdatePhone(selected.Id, form.Brand, form.Model, form.Year,
                                   form.CarColor, form.Price, form.Memory, form.Availability);
                RefreshList();
                MessageBox.Show("Телефон обновлён", "Успех");
            }
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxPhones.Visible == false)
            {
                MessageBox.Show("Сначала нажмите «Показать все»", "Внимание");
                return;
            }

            if (listBoxPhones.SelectedItem == null)
            {
                MessageBox.Show("Выберите телефон из списка", "Внимание");
                return;
            }

            Phone selected = (Phone)listBoxPhones.SelectedItem;

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

        
        private void BtnGroupMemory_Click(object sender, EventArgs e)
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

        
        private void BtnTopExpensive_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Сколько показать?", "Топ дорогих", "3");

            if (string.IsNullOrWhiteSpace(input)) return;

            int count;
            if (!int.TryParse(input, out count) || count <= 0)
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