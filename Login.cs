using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniversitySystem
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = UsernameTb.Text;
            string pass = PassTb.Text;
            if (username.Equals("admin") && pass.Equals("admin"))
            {
                MessageBox.Show("Succes!");
                Home m = new Home();
                this.Hide();
                m.Show();
            }
            else
            {
                MessageBox.Show("Username sau parola incorecte! Incercati din nou!");
                UsernameTb.Clear();
                PassTb.Clear();
                UsernameTb.Focus();
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }
    }
}
