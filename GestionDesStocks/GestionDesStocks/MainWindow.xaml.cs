using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace GestionDesStocks
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int id_utilisateur;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void b_inscription_Click(object sender, RoutedEventArgs e)
        {
            formulaire_inscription();
        }

        private void b_valider_Click(object sender, RoutedEventArgs e)
        {
            id_utilisateur = Verif_Utilisateur();
            Interface_Utilisateur(id_utilisateur);
        }

        //Fonctions
        private void formulaire_inscription()
        {
            Window1 form = new Window1();
            this.Hide();
            form.Show();
        }

        private int Verif_Utilisateur()
        {
            String password = pb_pwd.Password;

            SqlConnection cnSQL = null;
            SqlCommand cmSQL = null;
            SqlDataReader drSQL = null;
            string strSQL;

            try
            {
                string cnSTR = @"Server=(localdb)\MSSQLLocalDB; Database=db_gestion; Integrated Security=SSPI; Connect timeout=5";
                cnSQL = new SqlConnection(cnSTR);
                cnSQL.Open();

                strSQL = "SELECT id_utilisateur, mot_de_passe FROM dbo.Utilisateur WHERE email='" + tb_mail.Text.ToUpper() + "'";
                cmSQL = new SqlCommand(strSQL, cnSQL);
                drSQL = cmSQL.ExecuteReader();

                if (drSQL.Read())
                {
                    if (drSQL["mot_de_passe"].ToString().Equals(CalculateMD5Hash(pb_pwd.Password)))
                    {
                        return drSQL.GetInt32(0);
                    }
                    else
                    {
                        MessageBox.Show("Le mot de passe est incorrect !");
                        return -1; ;
                    }
                }
                else
                {
                    MessageBox.Show("L'identifiant est incorrect !");
                    return -1;
                }

            }
            catch (SqlException f)
            {
                MessageBox.Show(f.Message, "SQL Error");
                return -1;
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message, "General Error");
                return -1;
            }
            finally
            {
                drSQL.Close();
                cnSQL.Close();
                cmSQL.Dispose();
                cnSQL.Dispose();
            }
        }

        private void Interface_Utilisateur(int id_utilisateur)
        {
            Interface inter = new Interface(id_utilisateur);
            inter.Show();
            this.Hide();
        }

        private String CalculateMD5Hash(String input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

    }
}
