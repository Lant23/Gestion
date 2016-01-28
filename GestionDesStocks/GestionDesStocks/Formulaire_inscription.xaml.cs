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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace GestionDesStocks
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        //Interface
        private void b_valider_Click(object sender, RoutedEventArgs e)
        {
            if (Verification_Champs())
            {
                AjouterUtilisateur();
                Retour();
            }
        }

        private void b_retour_Click(object sender, RoutedEventArgs e)
        {
            Retour();
        }

        //Fonctions
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

        private void AjouterUtilisateur()
        {
            SqlConnection cnSQL = null;
            SqlCommand cmSQL = null;
            string strSQL;

            try
            {
                string cnSTR = @"Server=(localdb)\MSSQLLocalDB; Database=db_gestion; Integrated Security=SSPI; Connect timeout=5";
                cnSQL = new SqlConnection(cnSTR);
                cnSQL.Open();
                strSQL = "INSERT INTO dbo.Utilisateur (nom, prenom, email, mot_de_passe, telephone, adresse, code_postal, ville)"
                         + "VALUES (@nom_u, @prenom_u, @email_u, @password_u, @telephone_u, @adresse_u, @code_postal_u, @ville_u)";

                cmSQL = new SqlCommand(strSQL, cnSQL);

                cmSQL.Parameters.Add(new SqlParameter("nom_u", tb_nom.Text.ToUpper()));
                cmSQL.Parameters.Add(new SqlParameter("prenom_u", tb_prenom.Text.ToUpper()));
                cmSQL.Parameters.Add(new SqlParameter("email_u", tb_mail.Text.ToUpper()));
                String password = CalculateMD5Hash(pb_pwd.Password); //Hash du mot de passe
                cmSQL.Parameters.Add(new SqlParameter("password_u", password));
                cmSQL.Parameters.Add(new SqlParameter("telephone_u", tb_telephone.Text));
                cmSQL.Parameters.Add(new SqlParameter("adresse_u", tb_adresse.Text.ToUpper()));
                cmSQL.Parameters.Add(new SqlParameter("code_postal_u", tb_cp.Text.ToUpper()));
                cmSQL.Parameters.Add(new SqlParameter("ville_u", tb_ville.Text.ToUpper()));

                cmSQL.ExecuteNonQuery();
            }
            catch (SqlException f)
            {
                MessageBox.Show(f.Message, "SQL Error");
            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message, "General Error");
            }
            finally
            {
                cnSQL.Close();
                cnSQL.Dispose();
                cmSQL.Dispose();
                MessageBox.Show("L'utilisateur a bien été créer !");
            }
        }

        private bool Verification_Champs()
        {
            if (tb_nom.Text == "" || tb_prenom.Text == "" || tb_mail.Text == "" || pb_pwd.Password == "" ||
                pb_pwd2.Password == "" || tb_telephone.Text == "" || tb_adresse.Text == "" || tb_ville.Text == "" || tb_cp.Text == "")
            {
                MessageBox.Show("Veuillez remplir tous les champs !");
                return false;
            }
            else if (pb_pwd.Password != pb_pwd2.Password)
            {
                MessageBox.Show("Les mots de passe ne sont pas identiques !");
                return false;
            }
            else if (tb_mail.Text != "")
            {
                if (!tb_mail.Text.Contains("@") || !tb_mail.Text.Contains("."))
                {
                    MessageBox.Show("L'adresse e-mail est incorrect !");
                    return false;
                }
            }
            return true;
        }

        private void Retour()
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }


    }

}
