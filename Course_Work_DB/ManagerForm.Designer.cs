namespace Course_Work_DB
{
    partial class ManagerForm
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
            this.components = new System.ComponentModel.Container();
            this.FullNameBox = new System.Windows.Forms.TextBox();
            this.ExpBox = new System.Windows.Forms.TextBox();
            this.PhoneNumberBox = new System.Windows.Forms.TextBox();
            this.dateOfAppoinment = new System.Windows.Forms.DateTimePicker();
            this.brancheComboBox = new System.Windows.Forms.ComboBox();
            this.branchesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bankDataSet = new Course_Work_DB.BankDataSet();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.managersTableAdapter = new Course_Work_DB.BankDataSetTableAdapters.ManagersTableAdapter();
            this.branchesTableAdapter = new Course_Work_DB.BankDataSetTableAdapters.BranchesTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.branchesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // FullNameBox
            // 
            this.FullNameBox.Location = new System.Drawing.Point(172, 13);
            this.FullNameBox.Name = "FullNameBox";
            this.FullNameBox.Size = new System.Drawing.Size(118, 20);
            this.FullNameBox.TabIndex = 0;
            // 
            // ExpBox
            // 
            this.ExpBox.Location = new System.Drawing.Point(172, 40);
            this.ExpBox.Name = "ExpBox";
            this.ExpBox.Size = new System.Drawing.Size(118, 20);
            this.ExpBox.TabIndex = 1;
            // 
            // PhoneNumberBox
            // 
            this.PhoneNumberBox.Location = new System.Drawing.Point(172, 67);
            this.PhoneNumberBox.Name = "PhoneNumberBox";
            this.PhoneNumberBox.Size = new System.Drawing.Size(118, 20);
            this.PhoneNumberBox.TabIndex = 2;
            // 
            // dateOfAppoinment
            // 
            this.dateOfAppoinment.Location = new System.Drawing.Point(172, 94);
            this.dateOfAppoinment.Name = "dateOfAppoinment";
            this.dateOfAppoinment.Size = new System.Drawing.Size(118, 20);
            this.dateOfAppoinment.TabIndex = 3;
            // 
            // brancheComboBox
            // 
            this.brancheComboBox.DataSource = this.branchesBindingSource;
            this.brancheComboBox.DisplayMember = "Id";
            this.brancheComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.brancheComboBox.FormattingEnabled = true;
            this.brancheComboBox.Location = new System.Drawing.Point(172, 121);
            this.brancheComboBox.Name = "brancheComboBox";
            this.brancheComboBox.Size = new System.Drawing.Size(118, 21);
            this.brancheComboBox.TabIndex = 4;
            this.brancheComboBox.ValueMember = "Id";
            // 
            // branchesBindingSource
            // 
            this.branchesBindingSource.DataMember = "Branches";
            this.branchesBindingSource.DataSource = this.bankDataSet;
            // 
            // bankDataSet
            // 
            this.bankDataSet.DataSetName = "BankDataSet";
            this.bankDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(159, 178);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(131, 23);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(12, 178);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(131, 23);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // managersTableAdapter
            // 
            this.managersTableAdapter.ClearBeforeFill = true;
            // 
            // branchesTableAdapter
            // 
            this.branchesTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Full Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Experience";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Phone Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Appoinment Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Branche Id";
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 213);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.brancheComboBox);
            this.Controls.Add(this.dateOfAppoinment);
            this.Controls.Add(this.PhoneNumberBox);
            this.Controls.Add(this.ExpBox);
            this.Controls.Add(this.FullNameBox);
            this.Name = "ManagerForm";
            this.Text = "ManagerForm";
            ((System.ComponentModel.ISupportInitialize)(this.branchesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FullNameBox;
        private System.Windows.Forms.TextBox ExpBox;
        private System.Windows.Forms.TextBox PhoneNumberBox;
        private System.Windows.Forms.DateTimePicker dateOfAppoinment;
        private System.Windows.Forms.ComboBox brancheComboBox;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonOk;
        private BankDataSetTableAdapters.ManagersTableAdapter managersTableAdapter;
        private BankDataSetTableAdapters.BranchesTableAdapter branchesTableAdapter;
        private BankDataSet bankDataSet;
        private System.Windows.Forms.BindingSource branchesBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}