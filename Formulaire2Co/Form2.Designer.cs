namespace WinFormsApp1
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            NomDesTables = new CheckedListBox();
            GButton = new Button();
            AffichageDeLaTable = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)AffichageDeLaTable).BeginInit();
            SuspendLayout();
            // 
            // NomDesTables
            // 
            NomDesTables.FormattingEnabled = true;
            NomDesTables.Location = new Point(12, 12);
            NomDesTables.Name = "NomDesTables";
            NomDesTables.Size = new Size(150, 114);
            NomDesTables.TabIndex = 0;
            NomDesTables.SelectedIndexChanged += NomDesTables_SelectedIndexChanged;
            // 
            // GButton
            // 
            GButton.Location = new Point(12, 132);
            GButton.Name = "GButton";
            GButton.Size = new Size(150, 29);
            GButton.TabIndex = 1;
            GButton.Text = "Gérer";
            GButton.UseVisualStyleBackColor = true;
            // 
            // AffichageDeLaTable
            // 
            AffichageDeLaTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AffichageDeLaTable.Location = new Point(195, 12);
            AffichageDeLaTable.Name = "AffichageDeLaTable";
            AffichageDeLaTable.RowHeadersWidth = 51;
            AffichageDeLaTable.Size = new Size(607, 305);
            AffichageDeLaTable.TabIndex = 2;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(812, 329);
            Controls.Add(AffichageDeLaTable);
            Controls.Add(GButton);
            Controls.Add(NomDesTables);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load_1;
            ((System.ComponentModel.ISupportInitialize)AffichageDeLaTable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CheckedListBox NomDesTables;
        private Button GButton;
        private DataGridView AffichageDeLaTable;
    }
}