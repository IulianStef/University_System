using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace UniversitySystem
{
    static class Program
    {
        /// <summary>
         ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MessageBox.Show("Alegeti fisierul corespunzator bazei de date din folderul proiectului.\n" +
                "Odata intrat in folderul proiectului veti gasi un subfolder cu numele DataBaseDirectory.\n" +
                "Alegeti din acest subfolder fisierul UniversityDb.mdf");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    Datapath.Filepath = openFileDialog.FileName;
                }
            }
            Application.Run(new Login());
        }    
    }
}
