using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        private string tableName;
        private string connectionString = "server=localhost;database=bdd;uid=root;pwd=";
        private Dictionary<string, TextBox> textBoxes = new Dictionary<string, TextBox>();
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnModify;
        private Button btnDelete;
        private Button btnCancel;
        private Button btnAdd;
        private int recordId;
        private Dictionary<string, string> rowData;

        public Form3(string selectedTable, int id, Dictionary<string, string> data)
        {
            InitializeComponent();
            tableName = selectedTable;
            recordId = id;
            rowData = data;
            InitializeTextBoxes();
        }

        private void InitializeTextBoxes()
        {
            // Initialisation des champs de texte
            LoadTableStructure();
        }

        private void LoadTableStructure()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"DESCRIBE {tableName}";
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    // Creation dynamique des champs d'éditions en fonction des colonnes de la table
                    foreach (DataRow row in dt.Rows)
                    {
                        string columnsName = row["Field"].ToString();

                        Label label = new Label();
                        label.Text = columnsName;
                        label.AutoSize = true;

                        TextBox textBox = new TextBox();
                        textBox.Name = $"txt{columnsName}";
                        textBox.Width = 200;

                        // Si le mdp, on le rend en lecture seule et masqué
                        if (columnsName.ToLower() == "mdp")
                        {
                            textBox.PasswordChar = '*';
                            textBox.ReadOnly = true;
                        }
                        else
                        {
                            // Remplir les autres champs comme d'habitude
                            if (rowData != null && rowData.ContainsKey(columnsName))
                            {
                                textBox.Text = rowData[columnsName];
                            }
                            else
                            {
                                textBox.Text = ""; // Vide pour un ajout
                            }
                        }

                        textBoxes[columnsName] = textBox;

                        flowLayoutPanel1.Controls.Add(label);
                        flowLayoutPanel1.Controls.Add(textBox);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    List<string> updateFields = new List<string>();

                    // Exclure le mot de passe de la maj
                    foreach (var entry in textBoxes)
                    {
                        if (entry.Key.ToLower() != "mdp")
                        {
                            updateFields.Add($"{entry.Key} = '{entry.Value.Text}'");
                        }
                    }

                    // Maj de l'enregistrement
                    string query = $"UPDATE {tableName} SET {string.Join(",", updateFields)} WHERE id = {recordId}";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Données mises a jour !");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = $"DELETE FROM {tableName} WHERE id = {recordId}";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Données supprimées !");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Construction de la liste des champs et des valeurs pour l'insertion
                    List<string> columns = new List<string>();
                    List<string> values = new List<string>();

                    foreach (var entry in textBoxes)
                    {
                        columns.Add(entry.Key);
                        values.Add($"'{entry.Value.Text}'");
                    }

                    // Création de la requête d'insertion
                    string query = $"INSERT INTO {tableName} ({string.Join(",", columns)}) VALUES ({string.Join(",", values)})";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Nouvelles données ajoutées !");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
            }
        }

        private void InitializeComponent()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnModify = new Button();
            btnDelete = new Button();
            btnCancel = new Button();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(305, 215);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // btnModify
            // 
            btnModify.Location = new Point(351, 63);
            btnModify.Name = "btnModify";
            btnModify.Size = new Size(135, 49);
            btnModify.TabIndex = 1;
            btnModify.Text = "Modifier";
            btnModify.UseVisualStyleBackColor = true;
            btnModify.Click += btnModify_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(492, 118);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(135, 49);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Supprimer";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(492, 63);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(135, 49);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Annuler";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(351, 118);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(135, 49);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "Ajouter";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // Form3
            // 
            ClientSize = new Size(659, 270);
            Controls.Add(btnAdd);
            Controls.Add(btnCancel);
            Controls.Add(btnDelete);
            Controls.Add(btnModify);
            Controls.Add(flowLayoutPanel1);
            Name = "Form3";
            Text = "Form3";
            Load += Form3_Load;
            ResumeLayout(false);
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}