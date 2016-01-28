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

namespace GestionDesStocks
{
    /// <summary>
    /// Logique d'interaction pour Interface.xaml
    /// </summary>
    public partial class Interface : Window
    {
        public Interface(int id_utilisateur)
        {
            InitializeComponent();
            //Remplir_champs(id_utilisateur);
        }

        //Fonctions
        private void Remplir_champs(int id_utilisateur)
        {
            SqlConnection cnSQL = null;
            SqlCommand cmSQL = null;
            SqlDataReader drSQL = null;
            string strSQL;

            try
            {
                string cnSTR = @"Server=(localdb)\MSSQLLocalDB; Database=db_gestion; Integrated Security=SSPI; Connect timeout=5";
                cnSQL = new SqlConnection(cnSTR);
                cnSQL.Open();
                strSQL = "SELECT nom, prenom, email, telephone, adresse, ville, code_postal FROM dbo.Utilisateur where id_utilisateur='" + id_utilisateur + "'";

                while (drSQL.Read())
                {
                    id.Text = id_utilisateur.ToString();
                    tb_nom.Text = drSQL["nom"].ToString();
                    tb_prenom.Text = drSQL["prenom"].ToString();
                    tb_mail.Text = drSQL["email"].ToString();
                    tb_tel.Text = drSQL["telephone"].ToString();
                    tb_adresse.Text = drSQL["adresse"].ToString();
                    tb_ville.Text = drSQL["ville"].ToString();
                    tb_cp.Text = drSQL["code_postal"].ToString();
                }


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
    }
}
