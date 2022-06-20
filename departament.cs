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
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace UniversitySystem
{
    public partial class departament : Form
    {
        SqlConnection Con;
        public departament()
        {
            string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Datapath.Filepath + ";Integrated Security=True;Connect Timeout=30";
            Con = new SqlConnection(@constr);
            InitializeComponent();
            ShowFac();
        }

        private void ShowFac()
        {
            Con.Open();
            string Query = "select * from DepartamentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            //SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FacDGV.AutoGenerateColumns = false;
            FacDGV.DataSource = dt;
            Con.Close();
        }
        private void Reset()
        {
            NumeFacultateTb.Text = "";
            NrLocuriTb.Text = "";
            TaxaFacultateTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (NumeFacultateTb.Text == "" || NrLocuriTb.Text == "" || TaxaFacultateTb.Text == "")
            {
                MessageBox.Show("Lipsesc informatii");
            }
            else
            {
                try
                {
                    //se testeaza cu ajutorul functiei VerificareEticheteErori() daca exista
                    //etichete de eroare cu lungime diferita de 0
                    if (!VerificareEticheteErori())
                    {
                        MessageBox.Show("Eroare\nIntroduceti datele corect! ");
                    }
                    else
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Insert into DepartamentTbl(NumeFacultate,NrLocuriFacultate,TaxaFacultate)values(@NF,@NLF,@TF)", Con);
                        cmd.Parameters.AddWithValue("@NF", NumeFacultateTb.Text);
                        cmd.Parameters.AddWithValue("@NLF", NrLocuriTb.Text);
                        cmd.Parameters.AddWithValue("@TF", TaxaFacultateTb.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Facultate adaugata");
                        Con.Close();
                        ShowFac();
                        Reset();
                    }
                
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int key = 0;
        private void FacDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NumeFacultateTb.Text = FacDGV.CurrentRow.Cells[1].Value.ToString();
            NrLocuriTb.Text = FacDGV.CurrentRow.Cells[2].Value.ToString();
            TaxaFacultateTb.Text = FacDGV.CurrentRow.Cells[3].Value.ToString();
            if (NumeFacultateTb.Text == "")
            {
                key = 0;
                NumeFacultateTb.Text = "";
                NrLocuriTb.Text = "";
                TaxaFacultateTb.Text = "";
            }
            else
            {
                key = Convert.ToInt32(FacDGV.CurrentRow.Cells[0].Value.ToString());
            }
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NumeFacultateTb.Text == "" || NrLocuriTb.Text == "" || TaxaFacultateTb.Text == "")
            {
                MessageBox.Show("Lipsesc informatii");
            }
            else
            {
                try
                {
                    //se testeaza cu ajutorul functiei VerificareEticheteErori() daca exista
                    //etichete de eroare cu lungime diferita de 0
                    if (!VerificareEticheteErori())
                    {
                        MessageBox.Show("Eroare\nIntroduceti datele corect! ");
                    }
                    else
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("Update DepartamentTbl Set NumeFacultate=@NF,NrLocuriFacultate=@NLF,TaxaFacultate=@TF where NrFacultate=@Dkey", Con);
                        cmd.Parameters.AddWithValue("@NF", NumeFacultateTb.Text);
                        cmd.Parameters.AddWithValue("@NLF", NrLocuriTb.Text);
                        cmd.Parameters.AddWithValue("@TF", TaxaFacultateTb.Text);
                        cmd.Parameters.AddWithValue("@Dkey", key);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Datele facultatii au fost modificate");
                        Con.Close();
                        ShowFac();
                        Reset();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (NumeFacultateTb.Text == "" || NrLocuriTb.Text == "" || TaxaFacultateTb.Text == "")
            {
                MessageBox.Show("Selectati facultatea!!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from DepartamentTbl where NrFacultate=@Dkey", Con);
                    cmd.Parameters.AddWithValue("@Dkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Facultatea aleasa a fost stearsa din baza de date");
                    Con.Close();
                    ShowFac();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        // evenimente pentru click de mouse in partea stanga a formularului
        private void label11_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            new Login().Show();
        }
        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            new Login().Show();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }
        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
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
        //sfarsit evenimente pentru click de mouse in partea stanga a formularului
        //
        //validare caseta text Nume Facultate
        void eventNumeFacTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NumeFacultateTb.Text))
            {
                lblEroareNumeFac.Text = string.Empty;
            }
            else if (NumeFacultateTb.Text.Length > 15 || !NumeFacultateTb.Text.All(char.IsLetter))
            {
                lblEroareNumeFac.Text = "Nume facultate incorect";
            }
            else lblEroareNumeFac.Text = string.Empty;
        }
        //validare caseta text NR Locuri
        void eventNrLocuriTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NrLocuriTb.Text))
            {
                lblEroareNrLoc.Text = string.Empty;
            }
            else if (NrLocuriTb.Text.Length > 5 || !NrLocuriTb.Text.All(char.IsDigit))
            {
                lblEroareNrLoc.Text = "!Este acceptat doar un numar";
            }
            else if (NrLocuriTb.Text[0] == '0')
                lblEroareNrLoc.Text = "Prima cifra incorecta(=0)";
            else lblEroareNrLoc.Text = string.Empty;
        }
        //
        //validare caseta text Taxa Anuala
        void eventTaxaFacultateTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TaxaFacultateTb.Text))
            {
                lblEroareTaxa.Text = string.Empty;
            }
            else if (TaxaFacultateTb.Text.Length > 10 || !TaxaFacultateTb.Text.All(char.IsDigit))
            {
                lblEroareTaxa.Text = "!Este acceptat doar un numar";
            }
            else lblEroareTaxa.Text = string.Empty;
        }
        //Verificare Etichete Erori
        private bool VerificareEticheteErori()
        {
            return (lblEroareNumeFac.Text == string.Empty 
                || lblEroareNrLoc.Text == string.Empty
                ||lblEroareTaxa.Text==string.Empty);
        }

        private void Closelbl_MouseClick(object sender, MouseEventArgs e)
        {
            string message = "Do you want to close this window?";
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
