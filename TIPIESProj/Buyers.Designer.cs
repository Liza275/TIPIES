
namespace TIPIESProj
{
    partial class Buyers
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
            this.dataGridViewBuyers = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBuyers)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewBuyers
            // 
            this.dataGridViewBuyers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBuyers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBuyers.Location = new System.Drawing.Point(0, 1);
            this.dataGridViewBuyers.MultiSelect = false;
            this.dataGridViewBuyers.Name = "dataGridViewBuyers";
            this.dataGridViewBuyers.ReadOnly = true;
            this.dataGridViewBuyers.RowHeadersVisible = false;
            this.dataGridViewBuyers.RowHeadersWidth = 51;
            this.dataGridViewBuyers.RowTemplate.Height = 24;
            this.dataGridViewBuyers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBuyers.Size = new System.Drawing.Size(632, 447);
            this.dataGridViewBuyers.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(655, 58);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(134, 37);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(654, 127);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(134, 37);
            this.buttonUpdate.TabIndex = 2;
            this.buttonUpdate.Text = "Изменить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(654, 203);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(134, 37);
            this.buttonDel.TabIndex = 3;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(655, 269);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(134, 37);
            this.buttonBack.TabIndex = 4;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = true;
            // 
            // Buyers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.dataGridViewBuyers);
            this.Name = "Buyers";
            this.Text = "Buyers";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBuyers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBuyers;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonBack;
    }
}