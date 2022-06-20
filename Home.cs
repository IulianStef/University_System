using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace UniversitySystem
{
    public partial class Home : Form
    {
        SqlConnection Con;
        public Home()
        {
            string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Datapath.Filepath + ";Integrated Security=True;Connect Timeout=30";
            Con = new SqlConnection(@constr);
            InitializeComponent();
            CountStudents();
            CountProfs();
            CountDep();
        }
        private void CountStudents()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from StudentTbl",Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StudLbl.Text ="Nr: "+ dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountDep()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from DepartamentTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FacLbl.Text = "Nr: " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountProfs()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from ProfesorTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ProfLbl.Text = "Nr: " + dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void label11_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            student Obj = new student();
            Obj.Show();
            this.Hide();
        }

        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            student Obj = new student();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            profesor Obj = new profesor();
            Obj.Show();
            this.Hide();
        }

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {
            profesor Obj = new profesor();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            departament Obj = new departament();
            Obj.Show();
            this.Hide();
        }

        private void label4_MouseClick(object sender, MouseEventArgs e)
        {
            departament Obj = new departament();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            curs Obj = new curs();
            Obj.Show();
            this.Hide();
        }

        private void label5_MouseClick(object sender, MouseEventArgs e)
        {
            curs Obj = new curs();
            Obj.Show();
            this.Hide();
        }

        private void Closelbl_MouseClick(object sender, MouseEventArgs e)
        {
            string message = "Do you want to close the application?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}