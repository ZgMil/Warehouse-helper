public partial class UserModuleForm : Form
    {
        warehoudDBEntities db = new warehoudDBEntities();
        
        public UserModuleForm()
        {
            InitializeComponent();
            try
            {
                var query = from Employee in db.Employees
                            select new
                            {
                                surname = Employee.Surname,
                                name = Employee.Name
                            };
                foreach (var item in query)
                {
                    comboBox1.Items.Add(item.surname + " " + item.name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLogin.Text.ToString() != "" & txtPassword.Text.ToString() != "" & comboBox1.SelectedIndex != -1)
                {
                    string login = txtLogin.Text.ToString();
                    var query = (from Account in db.Accounts
                                 where Account.Login == login
                                 select Account).FirstOrDefault();
                    if (query != null)
                    {
                        MessageBox.Show("Record with this name already exists.");
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure you want to save this record?", "Saving Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            var a = db.Accounts.Create();
                            a.Login = txtLogin.Text.ToString();
                            a.Password = txtPassword.Text.ToString();
                            a.idEmployee = comboBox1.SelectedIndex + 1;
                            db.Accounts.Add(a);
                            db.SaveChanges();
                            this.Dispose();
                        }
                    }
                }
                else
                    MessageBox.Show("Please enter valid data first.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtLogin.Clear();
            txtPassword.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLogin.Text.ToString() != "" & txtPassword.Text.ToString() != "" & comboBox1.SelectedIndex != -1)
                {
                    string login = txtLogin.Text.ToString();
                    var query1 = (from Account in db.Accounts
                                 where Account.Login == login
                                 select Account).FirstOrDefault();
                    if (query1 != null)
                    {
                        MessageBox.Show("Record with this name already exists.");
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure you want to update this record?", "Updating Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int index = int.Parse(lbIndex.Text.ToString());

                            var query = from Account in db.Accounts
                                        where Account.idAccounts == index
                                        select Account;
                            query.First().Login = txtLogin.Text.ToString();
                            query.First().Password = txtPassword.Text.ToString();
                            query.First().idEmployee = comboBox1.SelectedIndex + 1;
                            db.SaveChanges();
                            this.Dispose();
                        }
                    }
                } else
                    MessageBox.Show("Please enter valid data first.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
}