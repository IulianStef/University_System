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

namespace UniversitySystem
{
    public partial class profesor : Form
    {
        SqlConnection Con;
        public profesor()
        {
            string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Datapath.Filepath + ";Integrated Security=True;Connect Timeout=30";
            Con = new SqlConnection(@constr);
            InitializeComponent();
            ShowProfessors();
            GetIdFac();
        }
        //functie pentru afisare profesori in DataGridView
        private void ShowProfessors()
        {
            Con.Open();
            string Query = "select * from ProfesorTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProfDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        //functie pentru obtinere id facultate
        private void GetIdFac()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select NrFacultate from DepartamentTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("NrFacultate", typeof(int));
            dt.Load(Rdr);
            IdFacPrCb.ValueMember = "NrFacultate";
            IdFacPrCb.DataSource = dt;
            Con.Close();
        }
        //functie pentru obtinere nume facultate
        private void GetNumeFac()
        {
            Con.Open();
            string Query = "Select * from DepartamentTbl where NrFacultate=" + IdFacPrCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                NumeFacPrTb.Text = dr["NumeFacultate"].ToString();
            }
            Con.Close();
        }
        //functie pentru reset
        private void Reset()
        {
            NumePrTb.Text = "";
            PrenumePrTb.Text = "";
            GenPrCb.SelectedIndex = -1;
            IdFacPrCb.SelectedIndex = -1;
            NumeFacPrTb.Text = "";
            TelefonPrTb.Text = "";
            EmailPrTb.Text = "";
            AdresaPrTb.Text = "";
            CalificarePrCb.SelectedIndex = -1;
            ExperientaPrTb.Text = "";
            SalariuPrTb.Text = "";
        }

        private void IdFacPrCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetNumeFac();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (NumePrTb.Text == "" || PrenumePrTb.Text == "" || GenPrCb.SelectedIndex == -1 || IdFacPrCb.SelectedIndex == -1 ||
                NumeFacPrTb.Text == "" || TelefonPrTb.Text == "" || EmailPrTb.Text == "" || AdresaPrTb.Text == ""||
                CalificarePrCb.SelectedIndex == -1|| ExperientaPrTb.Text == ""|| SalariuPrTb.Text == "")
            {
                MessageBox.Show("Lipsesc informatii");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into ProfesorTbl(NumePr,PrenumePr,GenPr,DataNasterePr,AdresaPr," +
                        "TelefonPr,EmailPr,CalificarePr,ExperientaPr,IdFacPr,NumeFacPr,SalariuPr)values(@NP,@PP,@GP,@DNP," +
                        "@AP,@TP,@EP,@CP,@EXPP,@IDFP,@NFP,@SP)", Con);
                    cmd.Parameters.AddWithValue("@NP", NumePrTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PrenumePrTb.Text);
                    cmd.Parameters.AddWithValue("@GP", GenPrCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DNP", DataNasterePr.Value.Date);
                    cmd.Parameters.AddWithValue("@AP", AdresaPrTb.Text);
                    cmd.Parameters.AddWithValue("@TP", TelefonPrTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmailPrTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CalificarePrCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EXPP", ExperientaPrTb.Text);
                    cmd.Parameters.AddWithValue("@IDFP", IdFacPrCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@NFP", NumeFacPrTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SalariuPrTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Profesor adaugat");
                    Con.Close();
                    ShowProfessors();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        //functie pentru completarea automata a casetelor de text la click pe celula unuia dintre randurile DGV-ului
        int key = 0;
        private void ProfDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            NumePrTb.Text = ProfDGV.CurrentRow.Cells[1].Value.ToString();
            PrenumePrTb.Text = ProfDGV.CurrentRow.Cells[2].Value.ToString();
            DataNasterePr.Text = ProfDGV.CurrentRow.Cells[3].Value.ToString();
            GenPrCb.Text = ProfDGV.CurrentRow.Cells[4].Value.ToString();
            AdresaPrTb.Text = ProfDGV.CurrentRow.Cells[5].Value.ToString();
            TelefonPrTb.Text = ProfDGV.CurrentRow.Cells[6].Value.ToString();
            EmailPrTb.Text = ProfDGV.CurrentRow.Cells[7].Value.ToString();
            CalificarePrCb.Text = ProfDGV.CurrentRow.Cells[8].Value.ToString();
            ExperientaPrTb.Text = ProfDGV.CurrentRow.Cells[9].Value.ToString();
            IdFacPrCb.Text = ProfDGV.CurrentRow.Cells[10].Value.ToString();
            NumeFacPrTb.Text = ProfDGV.CurrentRow.Cells[11].Value.ToString();
            SalariuPrTb.Text = ProfDGV.CurrentRow.Cells[12].Value.ToString();

            if (NumePrTb.Text == "")
            {
                key = 0;
                NumePrTb.Text = "";
                PrenumePrTb.Text = "";
                GenPrCb.SelectedIndex = -1;
                IdFacPrCb.SelectedIndex = -1;
                NumeFacPrTb.Text = "";
                TelefonPrTb.Text = "";
                EmailPrTb.Text = "";
                AdresaPrTb.Text = "";
                CalificarePrCb.SelectedIndex = -1;
                ExperientaPrTb.Text = "";
                SalariuPrTb.Text = "";
            }
            else
            {
                key = Convert.ToInt32(ProfDGV.CurrentRow.Cells[0].Value.ToString());
            }
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NumePrTb.Text == "" || PrenumePrTb.Text == "" || GenPrCb.SelectedIndex == -1 || IdFacPrCb.SelectedIndex == -1 ||
                NumeFacPrTb.Text == "" || TelefonPrTb.Text == "" || EmailPrTb.Text == "" || AdresaPrTb.Text == "" ||
                CalificarePrCb.SelectedIndex == -1 || ExperientaPrTb.Text == "" || SalariuPrTb.Text == "")
            {
                MessageBox.Show("Lipsesc informatii");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ProfesorTbl Set NumePr=@NP,PrenumePr=@PP,GenPr=@GP,DataNasterePr=@DNP,AdresaPr=@AP," +
                        "TelefonPr=@TP,EmailPr=@EP,CalificarePr=@CP,ExperientaPr=@EXPP,IdFacPr=@IDFP,NumeFacPr=@NFP,SalariuPr=@SP where NrPr=@Pkey", Con);
                    cmd.Parameters.AddWithValue("@NP", NumePrTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PrenumePrTb.Text);
                    cmd.Parameters.AddWithValue("@GP", GenPrCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DNP", DataNasterePr.Value.Date);
                    cmd.Parameters.AddWithValue("@AP", AdresaPrTb.Text);
                    cmd.Parameters.AddWithValue("@TP", TelefonPrTb.Text);
                    cmd.Parameters.AddWithValue("@EP", EmailPrTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CalificarePrCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EXPP", ExperientaPrTb.Text);
                    cmd.Parameters.AddWithValue("@IDFP", IdFacPrCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@NFP", NumeFacPrTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SalariuPrTb.Text);
                    cmd.Parameters.AddWithValue("@Pkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Datele profesorului au fost actualizate!");
                    Con.Close();
                    ShowProfessors();
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
            if (NumePrTb.Text == "" || PrenumePrTb.Text == "" || GenPrCb.SelectedIndex == -1 || IdFacPrCb.SelectedIndex == -1 ||
                NumeFacPrTb.Text == "" || TelefonPrTb.Text == "" || EmailPrTb.Text == "" || AdresaPrTb.Text == "" ||
                CalificarePrCb.SelectedIndex == -1 || ExperientaPrTb.Text == "" || SalariuPrTb.Text == "")
            {
                MessageBox.Show("Selectati profesorul pe care doriti sa il stergeti!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from  ProfesorTbl where NrPr=@Pkey",Con);
                    cmd.Parameters.AddWithValue("@Pkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Profesor sters cu succes!");
                    Con.Close();
                    ShowProfessors();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        //functie pentru cautare in timp real in ProfDGV
        private void CautareTb_TextChanged(object sender, EventArgs e)
        {
            Con.Open();
            string SearchData = CautareTb.Text;
            string Query = "SELECT * from ProfesorTbl";
            if (ColumnCb.SelectedIndex == 0)
            {
                Query += " WHERE NumePr LIKE '%" + SearchData + "%' OR " +
                    "PrenumePr LIKE '%" + SearchData + "%' OR GenPr LIKE '%" + SearchData + "%' OR CalificarePr LIKE " +
                    "'%" + SearchData + "%' OR NumeFacPr LIKE '%" + SearchData + "%'";
                if (int.TryParse(SearchData, out _))
                {
                    Query += "OR NrPr=" + SearchData;
                }
            }
            else
            {
                if (ColumnCb.SelectedIndex == 1 && SearchData.Length > 0)
                {
                    Query += " WHERE NrPr=" + SearchData;
                }
                else if (ColumnCb.SelectedIndex == 2)
                {
                    Query += " WHERE NumePr LIKE '%" + SearchData + "%'";
                }
                else if (ColumnCb.SelectedIndex == 3)
                {
                    Query += " WHERE PrenumePr LIKE '%" + SearchData + "%'";
                }
                else if (ColumnCb.SelectedIndex == 4)
                {
                    Query += " WHERE GenPr LIKE '%" + SearchData + "%'";
                }
                else if (ColumnCb.SelectedIndex == 5)
                {
                    Query += " WHERE CalificarePr LIKE '%" + SearchData + "%'";
                }
                else if (ColumnCb.SelectedIndex == 6)
                {
                    Query += " WHERE NumeFacPr LIKE '%" + SearchData + "%'";
                }
            }
            using (DataTable dt = new DataTable("ProfesorTbl"))
            {
                using (SqlCommand cmd = new SqlCommand(Query, Con))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    ProfDGV.DataSource = dt;
                }
            }
            Con.Close();
        }

        private void CautaBtn_Click(object sender, EventArgs e)
        {
            if (CautareTb.Text == "")
            {
                MessageBox.Show("Introduceti numele profesorului cautat!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string SearchData = CautareTb.Text;
                    string Query = "SELECT * from ProfesorTbl";
                    if (ColumnCb.SelectedIndex == 0)
                    {
                        Query += " WHERE NumePr LIKE '%" + SearchData + "%' OR " +
                            "PrenumePr LIKE '%" + SearchData + "%' OR GenPr LIKE '%" + SearchData + "%' OR CalificarePr LIKE " +
                            "'%" + SearchData + "%' OR NumeFacPr LIKE '%" + SearchData + "%'";
                        if (int.TryParse(SearchData, out _))
                        {
                            Query += "OR NrPr=" + SearchData;
                        }
                    }
                    else
                    {
                        if (ColumnCb.SelectedIndex == 1 && SearchData.Length > 0)
                        {
                            Query += " WHERE NrPr=" + SearchData;
                        }
                        else if (ColumnCb.SelectedIndex == 2)
                        {
                            Query += " WHERE NumePr LIKE '%" + SearchData + "%'";
                        }
                        else if (ColumnCb.SelectedIndex == 3)
                        {
                            Query += " WHERE PrenumePr LIKE '%" + SearchData + "%'";
                        }
                        else if (ColumnCb.SelectedIndex == 4)
                        {
                            Query += " WHERE GenPr LIKE '%" + SearchData + "%'";
                        }
                        else if (ColumnCb.SelectedIndex == 5)
                        {
                            Query += " WHERE CalificarePr LIKE '%" + SearchData + "%'";
                        }
                        else if (ColumnCb.SelectedIndex == 6)
                        {
                            Query += " WHERE NumeFacPr LIKE '%" + SearchData + "%'";
                        }
                    }
                    using (DataTable dt = new DataTable("ProfesorTbl"))
                    {
                        using (SqlCommand cmd = new SqlCommand(Query, Con))
                        {
                            SqlDataAdapter sda = new SqlDataAdapter(cmd);
                            sda.Fill(dt);
                            ProfDGV.DataSource = dt;
                        }
                    }
                    Con.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

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

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }
        //validare date introduse in caseta de NumePr
        void eventNumeProfTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NumePrTb.Text))
            {
                LabelNume.ForeColor = Color.Black;
                NumePrTb.Clear();
            }
            else if (NumePrTb.Text.Length > 15 || !NumePrTb.Text.All(char.IsLetter))
            {
                LabelNume.ForeColor = Color.Red;
                errorProvider1.SetError(NumePrTb, "Numele contine doar litere!");
            }
            else
            {
                LabelNume.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare date introduse in caseta de PrenumePr
        void eventPrenumeProfTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PrenumePrTb.Text))
            {
                LabelPrenume.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (PrenumePrTb.Text.Length > 15 || !PrenumePrTb.Text.All(char.IsLetter))
            {
                LabelPrenume.ForeColor = Color.Red;
                errorProvider1.SetError(PrenumePrTb, "Numele contine doar litere!");
            }
            else
            {
                LabelPrenume.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare NumeFacPrTb
        void eventNumeFacPrTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NumeFacPrTb.Text))
            {
                LabelNumeFac.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (NumeFacPrTb.Text.Length > 15 || !NumeFacPrTb.Text.All(char.IsLetter))
            {
                LabelNumeFac.ForeColor = Color.Red;
                errorProvider1.SetError(NumeFacPrTb, "Numele contine doar litere!");
            }
            else
            {
                LabelNumeFac.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare caseta de text TelefonPr
        void eventTelefonPr(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TelefonPrTb.Text))
            {
                LabelNrTelefon.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (TelefonPrTb.Text.Length > 15 || !TelefonPrTb.Text.All(char.IsDigit))
            {
                LabelNrTelefon.ForeColor = Color.Red;
                errorProvider1.SetError(TelefonPrTb, "Numarul de telefon contine doar cifre!");
            }
            else
            {
                LabelNrTelefon.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare email Profesor
        void eventEmailPr(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(EmailPrTb.Text))
            {
                LabelEmail.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else
            {
                try
                {
                    var address = new MailAddress(EmailPrTb.Text);
                    LabelEmail.ForeColor = Color.Black;
                    errorProvider1.Clear();
                }
                catch
                {
                    LabelEmail.ForeColor = Color.Red;
                    errorProvider1.SetError(EmailPrTb, "Format Email invalid!");
                }
            }
        }
        //validare caseta de text ExperientaPr
        void eventExperientaPr(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ExperientaPrTb.Text))
            {
                LabelExperienta.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (ExperientaPrTb.Text.Length > 4 || !ExperientaPrTb.Text.All(char.IsDigit))
            {
                LabelExperienta.ForeColor = Color.Red;
                errorProvider1.SetError(ExperientaPrTb, "Experienta contine doar cifre!(reprezinta un numar de ani");
            }
            else
            {
                LabelExperienta.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
        }
        //validare caseta de text SalariuPr
        void eventSalariuPr(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SalariuPrTb.Text))
            {
                LabelSalariu.ForeColor = Color.Black;
                errorProvider1.Clear();
            }
            else if (SalariuPrTb.Text.Length > 4 || !SalariuPrTb.Text.All(char.IsDigit))
            {
                LabelSalariu.ForeColor = Color.Red;
                errorProvider1.SetError(SalariuPrTb, "Experienta contine doar cifre!(reprezinta un numar de ani");
            }
            else
            {
                LabelSalariu.ForeColor = Color.Black;
                errorProvider1.Clear();
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
    }
}
