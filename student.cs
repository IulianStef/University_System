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
using System.Net.Mail;
using System.IO;

namespace UniversitySystem
{
    public partial class student : Form
    {
        SqlConnection Con;
        public student()
        {
                string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Datapath.Filepath + ";Integrated Security=True;Connect Timeout=30";
                Con = new SqlConnection(@constr);

                InitializeComponent();
                ShowStudents();
                GetIdFac();
        }
        private void ShowStudents()
        {
            Con.Open();
            string Query = "select * from StudentTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            StdDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetIdFac()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select NrFacultate from DepartamentTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("NrFacultate", typeof(int));
            dt.Load(Rdr);
            IdFacStdCb.ValueMember = "NrFacultate";
            IdFacStdCb.DataSource = dt;
            Con.Close();
        }
        private void GetNumeFac()
        {
            Con.Open();
            string Query = "Select * from DepartamentTbl where NrFacultate=" + IdFacStdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                NumeFacStdTb.Text = dr["NumeFacultate"].ToString();
            }
            Con.Close();
        }
        private void Reset()
        {
            NumeStdTb.Text = "";
            PrenumeStdTb.Text = "";
            GenStdCb.SelectedIndex = -1;
            IdFacStdCb.SelectedIndex = -1;
            NumeFacStdTb.Text = "";
            cnpStdTb.Text = "";
            TelefonStdTb.Text = "";
            EmailStdTb.Text = "";
            AdresaStdTb.Text = "";
        }
        private bool IsValidEmail(string username)
        {
            try
            {
                var address = new MailAddress(username);
                return address.Address == username;
            }
            catch
            {
                return false;
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (NumeStdTb.Text == "" || PrenumeStdTb.Text == "" || GenStdCb.SelectedIndex == -1 || IdFacStdCb.SelectedIndex == -1 || NumeFacStdTb.Text == "" || cnpStdTb.Text == "" || TelefonStdTb.Text == "" || EmailStdTb.Text == "" || AdresaStdTb.Text == "")
            {
                MessageBox.Show("Lipsesc informatii!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into StudentTbl(NumeSt,PrenumeSt,GenSt,DataNastereSt,CnpStud,AdresaSt," +
                        "Id_Fac_St,NumeFacSt,TelefonSt,EmailSt,Semestru)values(@NS,@PS,@GS,@DNS,@CNPS,@AS,@IDFS,@NFS,@TS,@ES,@S)", Con);
                    cmd.Parameters.AddWithValue("@NS", NumeStdTb.Text);
                    cmd.Parameters.AddWithValue("@PS", PrenumeStdTb.Text);
                    cmd.Parameters.AddWithValue("@GS", GenStdCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DNS", DataNastereStd.Value.Date);
                    cmd.Parameters.AddWithValue("@CNPS", cnpStdTb.Text);
                    cmd.Parameters.AddWithValue("@AS", AdresaStdTb.Text);
                    cmd.Parameters.AddWithValue("@IDFS", IdFacStdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@NFS", NumeFacStdTb.Text);
                    cmd.Parameters.AddWithValue("@TS", TelefonStdTb.Text);
                    cmd.Parameters.AddWithValue("@ES", EmailStdTb.Text);
                    cmd.Parameters.AddWithValue("@S", SemestruCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student adaugat");
                    Con.Close();
                    ShowStudents();
                    Reset();
                    /*do
                    {
                        cmd.Parameters.AddWithValue("@ES", EmailStdTb.Text);
                        if (!IsValidEmail(EmailStdTb.Text))
                        {
                            MessageBox.Show("Invalid mail");
                            EmailStdTb.Text = string.Empty;
                        }
                    } while (IsValidEmail(EmailStdTb.Text));*/
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        //functia pt afisarea datelor studentului in casetele de text pentru a putea edita datele acestuia ulterior
        private void StdDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NumeStdTb.Text = StdDGV.CurrentRow.Cells[1].Value.ToString();
            PrenumeStdTb.Text = StdDGV.CurrentRow.Cells[2].Value.ToString();
            GenStdCb.Text = StdDGV.CurrentRow.Cells[3].Value.ToString();
            DataNastereStd.Text = StdDGV.CurrentRow.Cells[4].Value.ToString();
            cnpStdTb.Text = StdDGV.CurrentRow.Cells[5].Value.ToString();
            AdresaStdTb.Text = StdDGV.CurrentRow.Cells[6].Value.ToString();
            IdFacStdCb.Text = StdDGV.CurrentRow.Cells[7].Value.ToString();
            NumeFacStdTb.Text = StdDGV.CurrentRow.Cells[8].Value.ToString();
            TelefonStdTb.Text = StdDGV.CurrentRow.Cells[9].Value.ToString();
            EmailStdTb.Text = StdDGV.CurrentRow.Cells[10].Value.ToString();
            SemestruCb.Text = StdDGV.CurrentRow.Cells[11].Value.ToString();

            if (NumeStdTb.Text == "")
            {
                key = 0;
                NumeStdTb.Text = "";
                PrenumeStdTb.Text = "";
                GenStdCb.SelectedIndex = -1;
                IdFacStdCb.SelectedIndex = -1;
                NumeFacStdTb.Text = "";
                cnpStdTb.Text = "";
                TelefonStdTb.Text = "";
                EmailStdTb.Text = "";
                AdresaStdTb.Text = "";
            }
            else
            {
                key = Convert.ToInt32(StdDGV.CurrentRow.Cells[0].Value.ToString());
            }
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NumeStdTb.Text == "" || PrenumeStdTb.Text == "" || GenStdCb.SelectedIndex == -1 || IdFacStdCb.SelectedIndex == -1 || NumeFacStdTb.Text == "" || cnpStdTb.Text == "" || TelefonStdTb.Text == "" || EmailStdTb.Text == "" || AdresaStdTb.Text == "")
            {
                MessageBox.Show("Lipsesc informatii");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update StudentTbl set NumeSt=@NS,PrenumeSt=@PS,GenSt=@GS,DataNastereSt=@DNS," +
                        "CnpStud=@CNPS,AdresaSt=@AS,Id_Fac_St=@IDFS,NumeFacSt=@NFS,TelefonSt=@TS,EmailSt=@ES,Semestru=@S" +
                        " where NrStud=@Skey", Con);
                    cmd.Parameters.AddWithValue("@NS", NumeStdTb.Text);
                    cmd.Parameters.AddWithValue("@PS", PrenumeStdTb.Text);
                    cmd.Parameters.AddWithValue("@GS", GenStdCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DNS", DataNastereStd.Value.Date);
                    cmd.Parameters.AddWithValue("@CNPS", cnpStdTb.Text);
                    cmd.Parameters.AddWithValue("@AS", AdresaStdTb.Text);
                    cmd.Parameters.AddWithValue("@IDFS", IdFacStdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@NFS", NumeFacStdTb.Text);
                    cmd.Parameters.AddWithValue("@TS", TelefonStdTb.Text);
                    cmd.Parameters.AddWithValue("@ES", EmailStdTb.Text);
                    cmd.Parameters.AddWithValue("@S", SemestruCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Skey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Datele studentului au fost modificate");
                    Con.Close();
                    ShowStudents();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (NumeStdTb.Text == "" || PrenumeStdTb.Text == "" || GenStdCb.SelectedIndex == -1 || IdFacStdCb.SelectedIndex == -1 || NumeFacStdTb.Text == "" || cnpStdTb.Text == "" || TelefonStdTb.Text == "" || EmailStdTb.Text == "" || AdresaStdTb.Text == "")
            {
                MessageBox.Show("Selectati studentul pe care doriti sa il stergeti!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from StudentTbl where NrStud=@Skey", Con);
                    cmd.Parameters.AddWithValue("@Skey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Datele studentului au fost sterse");
                    Con.Close();
                    ShowStudents();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void CautaBtn_Click(object sender, EventArgs e)
        {
            if (CautareTb.Text == "")
            {
                MessageBox.Show("Introduceti numele studentului cautat!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string SearchData = CautareTb.Text;
                    string Query = "SELECT * from StudentTbl";
                    if (ColumnCb.SelectedIndex == 0)
                    {
                        Query += " WHERE NumeSt LIKE '%" + SearchData + "%' OR " +
                            "PrenumeSt LIKE '%" + SearchData + "%' OR NumeFacSt LIKE '%" + SearchData + "%'";
                        if (int.TryParse(SearchData, out _))
                        {
                            Query += "OR NrStud=" + SearchData;
                        }
                    }
                    else
                    {
                        if (ColumnCb.SelectedIndex == 1 && SearchData.Length > 0)
                        {
                            Query += " WHERE NrStud=" + SearchData;
                        }
                        else if (ColumnCb.SelectedIndex == 2)
                        {
                            Query += " WHERE NumeSt LIKE '%" + SearchData + "%'";
                        }
                        else if (ColumnCb.SelectedIndex == 3)
                        {
                            Query += " WHERE PrenumeSt LIKE '%" + SearchData + "%'";
                        }
                        else if (ColumnCb.SelectedIndex == 4)
                        {
                            Query += " WHERE NumeFacSt LIKE '%" + SearchData + "%'";
                        }
                    }
                    using (DataTable dt = new DataTable("StudentTbl"))
                    {
                        //SqlCommand cmd = new SqlCommand("SELECT * from StudentTbl WHERE NumeSt LIKE  @numest ", Con);
                        using (SqlCommand cmd = new SqlCommand(Query, Con))
                        {
                            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                            sda.Fill(dt);
                            StdDGV.DataSource = dt;
                        }
                    }
                    Con.Close();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void CautareTb_TextChanged(object sender, EventArgs e)
        {
            Con.Open();
            string SearchData = CautareTb.Text;
            string Query = "SELECT * from StudentTbl";
            if(ColumnCb.SelectedIndex==0)
            {
                Query += " WHERE NumeSt LIKE '%" + SearchData + "%' OR " +
                    "PrenumeSt LIKE '%" + SearchData + "%' OR NumeFacSt LIKE '%" + SearchData + "%' OR GenSt LIKE '%" + SearchData + "%'";
                if(int.TryParse(SearchData, out _))
                {
                    Query += "OR NrStud="+SearchData;
                }
            }
            else
            {
                if(ColumnCb.SelectedIndex==1 && SearchData.Length > 0)
                {
                    Query += " WHERE NrStud=" + SearchData;
                }
                else if(ColumnCb.SelectedIndex==2)
                {
                    Query += " WHERE NumeSt LIKE '%" + SearchData + "%'";
                }
                else if (ColumnCb.SelectedIndex == 3)
                {
                    Query += " WHERE PrenumeSt LIKE '%" + SearchData + "%'";
                }
                else if (ColumnCb.SelectedIndex ==4 )
                {
                    Query += " WHERE GenSt LIKE '%" + SearchData + "%'";
                }
                else if (ColumnCb.SelectedIndex == 5)
                {
                    Query += " WHERE NumeFacSt LIKE '%" + SearchData + "%'";
                }
            }
            using(DataTable dt= new DataTable("StudentTbl"))
            {
                using (SqlCommand cmd = new SqlCommand(Query, Con))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    StdDGV.DataSource = dt;
                }
            }
            Con.Close();
        }
        int key = 0;

        private void IdFacStdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetNumeFac();
        }
        // evenimente pentru click de mouse in partea stanga a formularului
        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            new Login().Show();
        }
        private void label11_MouseClick(object sender, MouseEventArgs e)
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
        // sfarsit evenimente pentru click de mouse in partea stanga a formularului
        //
        //
        //validari
        //Validare caseta text NumeStd
        void eventNumeStudentTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NumeStdTb.Text))
            {
                labelNume.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (NumeStdTb.Text.Length > 15 || !NumeStdTb.Text.All(char.IsLetter))
            {
                labelNume.ForeColor = Color.Red;
                errorProvider1.SetError(NumeStdTb, "Numele contine doar litere!");
            }
            else
            {
                labelNume.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //Validare caseta text PrenumeStd
        void eventPrenumeStudentTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PrenumeStdTb.Text))
            {
                labelPrenume.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (PrenumeStdTb.Text.Length > 15 || !PrenumeStdTb.Text.All(char.IsLetter))
            {
                labelPrenume.ForeColor = Color.Red;
                errorProvider1.SetError(PrenumeStdTb, "Prenumele contine doar litere!");
            }
            else
            {
                labelPrenume.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare combo box GEN
        void eventGenStdCb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(GenStdCb.SelectedText))
            {
                labelGen.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (GenStdCb.SelectedIndex == -1)
            {
                labelGen.ForeColor = Color.Red;
                errorProvider1.SetError(GenStdCb, "Alegeti genul studentului!");
            }
            else
            {
                labelGen.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare NumeFacultateTb
        void eventNumeFacStdTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NumeFacStdTb.Text))
            {
                labelNumeFac.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (NumeFacStdTb.Text.Length > 15 || !NumeFacStdTb.Text.All(char.IsLetter))
            {
                labelNumeFac.ForeColor = Color.Red;
                errorProvider1.SetError(NumeFacStdTb, "Numele contine doar litere!");
            }
            else
            {
                labelNumeFac.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare CNP
        void eventCnpStd(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cnpStdTb.Text))
            {
                labelCnp.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (cnpStdTb.Text.Length !=13 || !cnpStdTb.Text.All(char.IsDigit))
            {
                labelCnp.ForeColor = Color.Red;
                errorProvider1.SetError(cnpStdTb, "Cnp-ul contine 13 cifre!");
            }
            else
            {
                labelCnp.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare NrTelefon
        void eventTelefonStd(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TelefonStdTb.Text))
            {
                labelTelefon.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (TelefonStdTb.Text.Length >15 || !TelefonStdTb.Text.All(char.IsDigit))
            {
                labelTelefon.ForeColor = Color.Red;
                errorProvider1.SetError(TelefonStdTb, "Numarul de telefon contine doar cifre!");
            }
            else
            {
                labelTelefon.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare email student
        void eventEmailStd(object sender,EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailStdTb.Text))
            {
                labelEmail.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else
            {
                try
                {
                    var address = new MailAddress(EmailStdTb.Text);
                    labelEmail.ForeColor = Color.Black;
                    errorProvider1.Clear();
                }
                catch
                {
                    labelEmail.ForeColor = Color.Red;
                    errorProvider1.SetError(EmailStdTb, "Format Email invalid!");
                }
            }
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

        private void student_Load(object sender, EventArgs e)
        {
            ColumnCb.SelectedIndex = 0;
        }
    }
}
