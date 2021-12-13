
namespace TIPIESProj
{
    partial class FormOperationList
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
            this.gridOperations = new System.Windows.Forms.DataGridView();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonShow = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxOperationType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.datePickerTo = new System.Windows.Forms.DateTimePicker();
            this.datePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.buttonShowTrans = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridOperations)).BeginInit();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridOperations
            // 
            this.gridOperations.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.gridOperations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOperations.Location = new System.Drawing.Point(12, 12);
            this.gridOperations.Name = "gridOperations";
            this.gridOperations.Size = new System.Drawing.Size(787, 426);
            this.gridOperations.TabIndex = 0;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(806, 12);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(186, 23);
            this.buttonCreate.TabIndex = 1;
            this.buttonCreate.Text = "Создать";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(805, 41);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(186, 23);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "Редактировать";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(806, 70);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(186, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(0, 140);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(186, 23);
            this.buttonShow.TabIndex = 4;
            this.buttonShow.Text = "Показать";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.comboBoxOperationType);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.datePickerTo);
            this.groupBox.Controls.Add(this.datePickerFrom);
            this.groupBox.Controls.Add(this.buttonShow);
            this.groupBox.Location = new System.Drawing.Point(806, 158);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(186, 172);
            this.groupBox.TabIndex = 5;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Фильтр";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Тип операции";
            // 
            // comboBoxOperationType
            // 
            this.comboBoxOperationType.FormattingEnabled = true;
            this.comboBoxOperationType.Location = new System.Drawing.Point(7, 104);
            this.comboBoxOperationType.Name = "comboBoxOperationType";
            this.comboBoxOperationType.Size = new System.Drawing.Size(173, 21);
            this.comboBoxOperationType.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "По";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "С";
            // 
            // datePickerTo
            // 
            this.datePickerTo.Location = new System.Drawing.Point(32, 46);
            this.datePickerTo.Name = "datePickerTo";
            this.datePickerTo.Size = new System.Drawing.Size(148, 20);
            this.datePickerTo.TabIndex = 6;
            // 
            // datePickerFrom
            // 
            this.datePickerFrom.Location = new System.Drawing.Point(32, 20);
            this.datePickerFrom.Name = "datePickerFrom";
            this.datePickerFrom.Size = new System.Drawing.Size(148, 20);
            this.datePickerFrom.TabIndex = 5;
            // 
            // buttonShowTrans
            // 
            this.buttonShowTrans.Location = new System.Drawing.Point(806, 99);
            this.buttonShowTrans.Name = "buttonShowTrans";
            this.buttonShowTrans.Size = new System.Drawing.Size(186, 23);
            this.buttonShowTrans.TabIndex = 6;
            this.buttonShowTrans.Text = "К проводке";
            this.buttonShowTrans.UseVisualStyleBackColor = true;
            this.buttonShowTrans.Click += new System.EventHandler(this.buttonShowTrans_Click);
            // 
            // FormOperationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 450);
            this.Controls.Add(this.buttonShowTrans);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.gridOperations);
            this.Name = "FormOperationList";
            this.Text = "Журнал операций";
            ((System.ComponentModel.ISupportInitialize)(this.gridOperations)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridOperations;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxOperationType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePickerTo;
        private System.Windows.Forms.DateTimePicker datePickerFrom;
        private System.Windows.Forms.Button buttonShowTrans;
    }
}