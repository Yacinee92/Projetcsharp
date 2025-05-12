using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Prepare;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private MySqlConnection conn;
        private string selectedTable = "";
        public Form2()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
            LoadDatabaseTables();
        }
        private void InitializeDatabaseConnection()
        {
            string connectionString = "server=localhost;database=bdd;uid=root;pwd=";
            conn = new MySqlConnection(connectionString);
        }

        private void LoadDatabaseTables()
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SHOW TABLES", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    NomDesTables.Items.Add(reader[0].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des tables : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTable = NomDesTables.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedTable))
            {
                DisplayTableData();
            }
        }

        private void DisplayTableData()
        {
            try
            {
                conn.Open();
                string query = $"SELECT * FROM {selectedTable}";
                MySqlDataAdapter dataadapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                dataadapter.Fill(dt);

                AffichageDeLaTable.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur d'affichage : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnAjouter_Click(object send, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedTable)) return;

            string insertQuery = $"INSERT INTO {selectedTable} (nom, age) VALUES ('Nouveau Nom', 25)";
            ExecuteQuery(insertQuery);
            DisplayTableData();
        }

        private void btnModifier_Click(object send, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedTable) || AffichageDeLaTable.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(AffichageDeLaTable.SelectedRows[0].Cells[0].Value);
            string updateQuery = $"UPDATE {selectedTable} SET nom='Nom Modifié' WHERE id={id}";

            ExecuteQuery(updateQuery);
            DisplayTableData();
        }

        private void btnSupprimer_Click(object send, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedTable) || AffichageDeLaTable.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(AffichageDeLaTable.SelectedRows[0].Cells[0].Value);
            string deleteQuery = $"DELETE FROM {selectedTable} WHERE id={id}";

            ExecuteQuery(deleteQuery);
            DisplayTableData();
        }

        private void ExecuteQuery(string query)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur SQL : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        private void AffichageDeLaTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedTable)) return;
            {
                MessageBox.Show("Veuillez sélectionner une table : ");
                return;
            }
            if (AffichageDeLaTable_SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une ligne : ");
                return;
            }

            // Récupérer l'ID de la ligne sélectionnée
            int id = Convert.ToInt32(AffichageDeLaTable_SelectedRows[0].Cells[0].Value);

            // Récupérer les valeurs de toutes les colonnes de la ligne sélectionnée
            Dictionary<string, string> rowData = new Dictionary<string, string>();
            foreach (DataGridViewCell cell in AffichageDeLaTable_SelectedRows[0].Cells)
            {
                string columnName = AffichageDeLaTable.Columns[cell.ColumnIndex].Name;
                string cellValue = cell.Value?.ToString() ?? "";
                rowData[columnName] = cellValue;
            }
            // Ouvrir Form3 avec les données de la ligne sélectionnée
            Form3 form = new Form3(selectedTable, id, rowData);
            form.ShowDialog();

            DisplayTableData();
        }

        private void Form2_load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void NomDesTables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
    }
}
