public partial class LoginForm : Form
    {
        warehoudDBEntities db = new warehoudDBEntities();
        public LoginForm()
        {
            InitializeComponent();
            var query = (from Account in db.Accounts
                         select Account).FirstOrDefault();
            if (query == null)
            {
                MainForm main = new MainForm();
                main.lbUsername.Text = "new user";
                main.ShowDialog();
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.ToString();
            string password = txtPassword.Text.ToString();
            
                var query = (from Account in db.Accounts
                             where Account.Login == login
                             where Account.Password == password
                             select Account).FirstOrDefault();
                if (query != null)
                {
                    MainForm main = new MainForm();
                    main.lbUsername.Text = login;
                    if(login != "Admin")
                    {
                        main.btnEmployees.Enabled = false;
                        main.btnAccounts.Enabled = false;
                        main.label7.Enabled = false;
                        main.label8.Enabled = false;
                    }/**/
                main.ShowDialog();
                    
                }
                txtLogin.Clear();
                txtPassword.Clear(); 
        }
    }