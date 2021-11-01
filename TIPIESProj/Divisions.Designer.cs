
namespace TIPIESProj
{
    partial class Divisions
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
            this.dataGridViewDivisions = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxExpenseAccount = new System.Windows.Forms.ComboBox();
            this.buttonShow = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDel = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDivisions)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDivisions
            // 
            this.dataGridViewDivisions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDivisions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDivisions.Location = new System.Drawing.Point(2, -2);
            this.dataGridViewDivisions.MultiSelect = false;
            this.dataGridViewDivisions.Name = "dataGridViewDivisions";
            this.dataGridViewDivisions.ReadOnly = true;
            this.dataGridViewDivisions.RowHeadersVisible = false;
            this.dataGridViewDivisions.RowHeadersWidth = 51;
            this.dataGridViewDivisions.RowTemplate.Height = 24;
            this.dataGridViewDivisions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDivisions.Size = new System.Drawing.Size(596, 451);
            this.dataGridViewDivisions.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(658, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Счет затрат";
            // 
            // comboBoxExpenseAccount
            // 
            this.comboBoxExpenseAccount.FormattingEnabled = true;
            this.comboBoxExpenseAccount.Location = new System.Drawing.Point(617, 69);
            this.comboBoxExpenseAccount.Name = "comboBoxExpenseAccount";
            this.comboBoxExpenseAccount.Size = new System.Drawing.Size(171, 24);
            this.comboBoxExpenseAccount.TabIndex = 2;
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(617, 129);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(165, 38);
            this.buttonShow.TabIndex = 3;
            this.buttonShow.Text = "Показать";
            this.buttonShow.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(617, 185);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(165, 38);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(617, 238);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(165, 38);
            this.buttonUpdate.TabIndex = 5;
            this.buttonUpdate.Text = "Изменить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDel
            // 
            this.buttonDel.Location = new System.Drawing.Point(617, 295);
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(165, 38);
            this.buttonDel.TabIndex = 6;
            this.buttonDel.Text = "Удалить";
            this.buttonDel.UseVisualStyleBackColor = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(617, 353);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(165, 38);
            this.buttonClose.TabIndex = 7;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // Divisions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDel);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.comboBoxExpenseAccount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewDivisions);
            this.Name = "Divisions";
            this.Text = "Divisions";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDivisions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDivisions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxExpenseAccount;
        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDel;
        private System.Windows.Forms.Button buttonClose;
    }
}