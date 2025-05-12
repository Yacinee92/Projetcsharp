using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BCrypt.Net;
using System.Linq.Expressions;
using WinFormsApp1;

namespace Formulaire2Co

{
    public partial class Form1 : Form
    {
        private MySqlConnection conn;
        public Form1()
        {
            InitializeComponent();
        }
        // L'�venement au clic bouton
        private void Button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text; // Assure-toi que tu as un textbox pour l'email
            string password = textBox2.Text; // Assure-toi que tu as un textbox pour le mot de passe

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Veuillez remplir toutes les informations.");
                return; // Sortie de la m�thode si des informations sont manquantes
            }
            string servername = "localhost"; // serveur
            string dbname = "bdd-cpx"; // bdd
            string dbusername = "root"; // nom d'utilisateur
            string dbpassword = ""; // mot de passe

            // Chaine de connexion
            string connectionString = $"Server={servername};Database={dbname};Uid={dbusername};Pwd={dbpassword};";

            try
            {
                // Initialisation de la connexion
                conn = new MySqlConnection(connectionString);
                conn.Open();

                // Pr�paration de la requ�te pour verifer l'meail et le mot de passe
                string query = "SELECT * FROM users WHERE email = @Email";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // L'email existe, maintenant on v�rifie le mot de passe
                    string storedPassword = reader["password"].ToString();
                    // V�rification du mdp en utilisant BCrypt
                    if (BCrypt.Net.BCrypt.Verify(password, storedPassword))
                    {
                        MessageBox.Show("Connexion r�ussie !");
                        Form2 form2 = new Form2(); // Ouvre le formulaire 2 apr�s une connexion r�ussie
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Mot de passe incorrect.");
                    }
                }
                else
                {
                    MessageBox.Show("Email non trouv�.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion : " + ex.Message);
            } 
            finally
            {
                // Tjrs fermer la connexion
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
