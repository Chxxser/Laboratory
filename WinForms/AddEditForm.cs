using System;
using System.Drawing;
using System.Windows.Forms;
using Model;

namespace WinForms
{
    public class AddEditForm : Form
    {
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Year { get; private set; }
        public string CarColor { get; private set; }
        public decimal Price { get; private set; }
        public int Memory { get; private set; }
        public bool Availability { get; private set; }

        private TextBox txtBrand;
        private TextBox txtModel;
        private TextBox txtYear;
        private TextBox txtColor;
        private TextBox txtPrice;
        private TextBox txtMemory;
        private CheckBox chkAvailability;
        private Button btnSave;
        private Button btnCancel;

        public AddEditForm(Phone phone = null)
        {
            BuildForm();

            if (phone != null)
            {
                this.Text = "Редактирование телефона";
                txtBrand.Text = phone.Brand;
                txtModel.Text = phone.Model;
                txtYear.Text = phone.Year.ToString();
                txtColor.Text = phone.Color;
                txtPrice.Text = phone.Price.ToString();
                txtMemory.Text = phone.Memory.ToString();
                chkAvailability.Checked = phone.Availability;
            }
            else
            {
                this.Text = "Добавление телефона";
                chkAvailability.Checked = true;
            }
        }

        private void BuildForm()
        {
            this.Size = new Size(400, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;  

            int y = 20;
            int labelWidth = 140;
            int textBoxWidth = 180;
            int leftMargin = 30;

            txtBrand = CreateField("Бренд:", ref y, leftMargin, labelWidth, textBoxWidth);
            txtModel = CreateField("Модель:", ref y, leftMargin, labelWidth, textBoxWidth);
            txtYear = CreateField("Год выпуска:", ref y, leftMargin, labelWidth, textBoxWidth);
            txtColor = CreateField("Цвет:", ref y, leftMargin, labelWidth, textBoxWidth);
            txtPrice = CreateField("Цена (руб):", ref y, leftMargin, labelWidth, textBoxWidth);
            txtMemory = CreateField("Память (ГБ):", ref y, leftMargin, labelWidth, textBoxWidth);

            Label lblAvailable = new Label();
            lblAvailable.Text = "В наличии:";
            lblAvailable.Location = new Point(leftMargin, y);
            lblAvailable.Size = new Size(labelWidth, 25);
            lblAvailable.Font = new Font("Arial", 10);

            chkAvailability = new CheckBox();
            chkAvailability.Location = new Point(leftMargin + labelWidth, y);
            chkAvailability.Size = new Size(50, 25);
            chkAvailability.Checked = true;

            Controls.Add(lblAvailable);
            Controls.Add(chkAvailability);
            y = y + 45;

            btnSave = new Button();
            btnSave.Text = "Сохранить";
            btnSave.Location = new Point(60, y);
            btnSave.Size = new Size(120, 40);
            btnSave.BackColor = Color.LightGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Arial", 10, FontStyle.Bold);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Location = new Point(200, y);
            btnCancel.Size = new Size(120, 40);
            btnCancel.BackColor = Color.LightCoral;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Arial", 10, FontStyle.Bold);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += BtnCancel_Click;

            Controls.Add(btnSave);
            Controls.Add(btnCancel);
        }

        private TextBox CreateField(string label, ref int y, int leftMargin, int labelWidth, int textBoxWidth)
        {
            Label lbl = new Label();
            lbl.Text = label;
            lbl.Location = new Point(leftMargin, y);
            lbl.Size = new Size(labelWidth, 25);
            lbl.Font = new Font("Arial", 10);

            TextBox txt = new TextBox();
            txt.Location = new Point(leftMargin + labelWidth, y);
            txt.Size = new Size(textBoxWidth, 25);
            txt.Font = new Font("Arial", 10);

            Controls.Add(lbl);
            Controls.Add(txt);
            y = y + 35;
            return txt;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                MessageBox.Show("Введите бренд!");
                txtBrand.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Введите модель!");
                txtModel.Focus();
                return;
            }

            int year;
            if (!int.TryParse(txtYear.Text, out year))
            {
                MessageBox.Show("Год должен быть числом!");
                txtYear.Focus();
                return;
            }

            if (year < 2000 || year > DateTime.Now.Year + 1)
            {
                MessageBox.Show("Год от 2000 до " + (DateTime.Now.Year + 1));
                txtYear.Focus();
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Цена должна быть числом!");
                txtPrice.Focus();
                return;
            }

            if (price < 0)
            {
                MessageBox.Show("Цена не может быть отрицательной!");
                txtPrice.Focus();
                return;
            }

            int memory;
            if (!int.TryParse(txtMemory.Text, out memory))
            {
                MessageBox.Show("Память должна быть числом!");
                txtMemory.Focus();
                return;
            }

            if (memory <= 0)
            {
                MessageBox.Show("Память больше нуля!");
                txtMemory.Focus();
                return;
            }

            Brand = txtBrand.Text.Trim();
            Model = txtModel.Text.Trim();
            Year = year;
            CarColor = txtColor.Text.Trim();   
            Price = price;
            Memory = memory;
            Availability = chkAvailability.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}