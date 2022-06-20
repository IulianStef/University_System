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
using System.Text.RegularExpressions;

namespace UniversitySystem
{
    public partial class curs : Form
    {
        SqlConnection Con;
        public curs()
        {
            string constr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Datapath.Filepath + ";Integrated Security=True;Connect Timeout=30";
            Con = new SqlConnection(@constr);

            InitializeComponent();
           
            GetIdFac();
            GetIdProf();
            ShowCourses();
        }
        private void ShowCourses()
        {
            Con.Open();
            string Query = "select * from CursTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CursDGV.DataSource = ds.Tables[0];
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
            IdFacCb.ValueMember = "NrFacultate";
            IdFacCb.DataSource = dt;
            Con.Close();
        }
        private void GetNumeFac()
        {
            Con.Open();
            string Query = "Select * from DepartamentTbl where NrFacultate=" + IdFacCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                NumeFacTb.Text = dr["NumeFacultate"].ToString();
            }
            Con.Close();
        }
        private void GetIdProf()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select NrPr from ProfesorTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("NrPr", typeof(int));
            dt.Load(Rdr);
            IdProfCb.ValueMember = "NrPr";
            IdProfCb.DataSource = dt;
            Con.Close();
        }
        private void GetNumeProf()
        {
            Con.Open();
            string Query = "Select * from ProfesorTbl where NrPr=" + IdProfCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                NumeProfTb.Text = dr["NumePr"].ToString();
            }
            Con.Close();
        }
        private void Reset()
        {
            NumeCursTb.Text = "";
            DurataTb.Text = "";
            IdFacCb.SelectedIndex = -1;
            NumeFacTb.Text = "";
            IdProfCb.SelectedIndex = -1;
            NumeProfTb.Text = "";
        }
        // event imagine sigla pentru afisare formular HOME
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }
        // event imagine home pentru afisare formular HOME
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }
        // event eticheta home pentru afisare formular HOME
        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }
        // event imagine student pentru afisare formular STUDENT
        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            student Obj = new student();
            Obj.Show();
            this.Hide();
        }
        // event eticheta student pentru afisare formular STUDENT
        private void label2_MouseClick(object sender, MouseEventArgs e)
        {
            student Obj = new student();
            Obj.Show();
            this.Hide();
        }
        // event imagine profesor pentru afisare formular PROFESOR
        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            profesor Obj = new profesor();
            Obj.Show();
            this.Hide();
        }
        // event eticheta profesor pentru afisare formular PROFESOR
        private void label3_MouseClick(object sender, MouseEventArgs e)
        {
            profesor Obj = new profesor();
            Obj.Show();
            this.Hide();
        }
        // event imagine departament pentru afisare formular DEPARTAMENT
        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            departament Obj = new departament();
            Obj.Show();
            this.Hide();
        }
        // event eticheta departament pentru afisare formular DEPARTAMENT
        private void label4_MouseClick(object sender, MouseEventArgs e)
        {
            departament Obj = new departament();
            Obj.Show();
            this.Hide();
        }
        //event pentru inchidere aplicatie
        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            new Login().Show();
        }
        //event pentru inchidere aplicatie
        private void label11_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
            new Login().Show();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (NumeCursTb.Text == "" || DurataTb.Text == "" ||  IdFacCb.SelectedIndex == -1 || NumeFacTb.Text == "" || IdProfCb.SelectedIndex == -1 || NumeProfTb.Text == "")
            {
                MessageBox.Show("Lipsesc informatii!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert into CursTbl(NumeCurs,DurataCurs,idFacCurs," +
                        "NumeFacCurs,IdProfCurs,NumeProfCurs)values(@NC,@DC,@IDFC,@NFC,@IDPC,@NPC)", Con);
                    cmd.Parameters.AddWithValue("@NC", NumeCursTb.Text);
                    cmd.Parameters.AddWithValue("@DC", DurataTb.Text);
                    cmd.Parameters.AddWithValue("@IDFC", IdFacCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@NFC", NumeFacTb.Text);
                    cmd.Parameters.AddWithValue("@IDPC", IdProfCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@NPC", NumeProfTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Curs adaugat");
                    Con.Close();
                    ShowCourses();
                    Reset();
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void IdFacCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetNumeFac();
        }

        private void IdProfCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetNumeProf();
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (NumeCursTb.Text == "" || DurataTb.Text == "" || IdFacCb.SelectedIndex == -1 || NumeFacTb.Text == "" || IdProfCb.SelectedIndex == -1 || NumeProfTb.Text == "")
            {
                MessageBox.Show("Lipsesc informatii!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CursTbl set NumeCurs=@NC,DurataCurs=@DC,idFacCurs=@IDFC," +
                        "NumeFacCurs=@NFC,IdProfCurs=@IDPC,NumeProfCurs=@NPC where NrCurs=@CKey", Con);
                    cmd.Parameters.AddWithValue("@NC", NumeCursTb.Text);
                    cmd.Parameters.AddWithValue("@DC", DurataTb.Text);
                    cmd.Parameters.AddWithValue("@IDFC", IdFacCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@NFC", NumeFacTb.Text);
                    cmd.Parameters.AddWithValue("@IDPC", IdProfCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@NPC", NumeProfTb.Text);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Curs editat");
                    Con.Close();
                    ShowCourses();
                    Reset();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int key = 0;
        
        private void CursDGV_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            NumeCursTb.Text = CursDGV.CurrentRow.Cells[1].Value.ToString();
            DurataTb.Text = CursDGV.CurrentRow.Cells[2].Value.ToString();
            IdFacCb.Text = CursDGV.CurrentRow.Cells[3].Value.ToString();
            NumeFacTb.Text = CursDGV.CurrentRow.Cells[4].Value.ToString();
            IdProfCb.Text = CursDGV.CurrentRow.Cells[5].Value.ToString();
            NumeProfTb.Text = CursDGV.CurrentRow.Cells[6].Value.ToString();

            if (NumeCursTb.Text == "")
            {
                key = 0;
                NumeCursTb.Text = "";
                DurataTb.Text = "";
                IdFacCb.SelectedIndex = -1;
                NumeFacTb.Text = "";
                IdProfCb.SelectedIndex = -1;
                NumeProfTb.Text = "";
            }
            else
            {
                key = Convert.ToInt32(CursDGV.CurrentRow.Cells[0].Value.ToString());
            }
        }
        //VALIDARI
        //
        //Validare caseta de text NumeCursTb
        void eventNumeCursTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NumeCursTb.Text))
            {
                lblEroareNumeCurs.Text = string.Empty;
            }
            else if (NumeCursTb.Text.Length > 15 || !NumeCursTb.Text.All(char.IsLetter))
            {
                lblEroareNumeCurs.Text = "Nume curs incorect";
            }
            else lblEroareNumeCurs.Text = string.Empty;
        }
        //Validare caseta de text DurataTb
        void eventDurataTb(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DurataTb.Text))
            {
                lblEroareDurata.Text = string.Empty;
            }
            else if (DurataTb.Text.Length > 5 || !DurataTb.Text.All(char.IsDigit))
            {
                lblEroareDurata.Text = "Durata incorecta";
            }
            else if(DurataTb.Text[0] == '0')
                lblEroareDurata.Text = "Prima cifra incorecta(=0)";
                else lblEroareDurata.Text = string.Empty;
        }
        //Validare checkBox IdFac
        void eventIdFac(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdFacCb.SelectedText))
            {
                lblEroareIdFac.Text = string.Empty;
            }
            else if(IdFacCb.SelectedIndex==-1)
            {
                lblEroareIdFac.Text = "Selectati ID-ul";
            }
            else
            {
                lblEroareIdFac.Text = string.Empty;
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
