﻿namespace Course_Work_DB
{
    partial class Account
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
            this.balanceBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Capitalisation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_UserId = new System.Windows.Forms.ComboBox();
            this.clientsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bankDataSet = new Course_Work_DB.BankDataSet();
            this.label6 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.accountsTableAdapter = new Course_Work_DB.BankDataSetTableAdapters.AccountsTableAdapter();
            this.clientsTableAdapter = new Course_Work_DB.BankDataSetTableAdapters.ClientsTableAdapter();
            this.comboBox_Type = new System.Windows.Forms.ComboBox();
            this.accountsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.managersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.managerWorkAddressLabel = new System.Windows.Forms.Label();
            this.managerNameLabel = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.managersTableAdapter = new Course_Work_DB.BankDataSetTableAdapters.ManagersTableAdapter();
            this.agreementTableAdapter = new Course_Work_DB.BankDataSetTableAdapters.AgreementTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.clientsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.managersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // balanceBox
            // 
            this.balanceBox.Location = new System.Drawing.Point(146, 18);
            this.balanceBox.Name = "balanceBox";
            this.balanceBox.Size = new System.Drawing.Size(148, 20);
            this.balanceBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Balance";
            // 
            // textBox_Capitalisation
            // 
            this.textBox_Capitalisation.Location = new System.Drawing.Point(146, 75);
            this.textBox_Capitalisation.Name = "textBox_Capitalisation";
            this.textBox_Capitalisation.Size = new System.Drawing.Size(148, 20);
            this.textBox_Capitalisation.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Capitalisation";
            // 
            // comboBox_UserId
            // 
            this.comboBox_UserId.AllowDrop = true;
            this.comboBox_UserId.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.clientsBindingSource, "Id", true));
            this.comboBox_UserId.DataSource = this.clientsBindingSource;
            this.comboBox_UserId.DisplayMember = "Full Name";
            this.comboBox_UserId.FormattingEnabled = true;
            this.comboBox_UserId.Location = new System.Drawing.Point(146, 101);
            this.comboBox_UserId.Name = "comboBox_UserId";
            this.comboBox_UserId.Size = new System.Drawing.Size(148, 21);
            this.comboBox_UserId.TabIndex = 13;
            this.comboBox_UserId.ValueMember = "Id";
            this.comboBox_UserId.TextChanged += new System.EventHandler(this.comboBox_UserId_TextChanged);
            // 
            // clientsBindingSource
            // 
            this.clientsBindingSource.DataMember = "Clients";
            this.clientsBindingSource.DataSource = this.bankDataSet;
            // 
            // bankDataSet
            // 
            this.bankDataSet.DataSetName = "BankDataSet";
            this.bankDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(80, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "User ID";
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(12, 232);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(126, 23);
            this.button_OK.TabIndex = 15;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Location = new System.Drawing.Point(168, 232);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(126, 23);
            this.button_Exit.TabIndex = 16;
            this.button_Exit.Text = "Exit";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // accountsTableAdapter
            // 
            this.accountsTableAdapter.ClearBeforeFill = true;
            // 
            // clientsTableAdapter
            // 
            this.clientsTableAdapter.ClearBeforeFill = true;
            // 
            // comboBox_Type
            // 
            this.comboBox_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Type.FormattingEnabled = true;
            this.comboBox_Type.Items.AddRange(new object[] {
            "Депозитный",
            "Текущий"});
            this.comboBox_Type.Location = new System.Drawing.Point(146, 45);
            this.comboBox_Type.Name = "comboBox_Type";
            this.comboBox_Type.Size = new System.Drawing.Size(148, 21);
            this.comboBox_Type.TabIndex = 17;
            // 
            // accountsBindingSource
            // 
            this.accountsBindingSource.DataMember = "Accounts";
            this.accountsBindingSource.DataSource = this.bankDataSet;
            // 
            // comboBox1
            // 
            this.comboBox1.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.accountsBindingSource, "Id", true));
            this.comboBox1.DataSource = this.managersBindingSource;
            this.comboBox1.DisplayMember = "Id";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(146, 128);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(148, 21);
            this.comboBox1.TabIndex = 18;
            this.comboBox1.ValueMember = "Id";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // managersBindingSource
            // 
            this.managersBindingSource.DataMember = "Managers";
            this.managersBindingSource.DataSource = this.bankDataSet;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Manager Id";
            // 
            // managerWorkAddressLabel
            // 
            this.managerWorkAddressLabel.AutoSize = true;
            this.managerWorkAddressLabel.Location = new System.Drawing.Point(146, 156);
            this.managerWorkAddressLabel.Name = "managerWorkAddressLabel";
            this.managerWorkAddressLabel.Size = new System.Drawing.Size(0, 13);
            this.managerWorkAddressLabel.TabIndex = 20;
            // 
            // managerNameLabel
            // 
            this.managerNameLabel.AutoSize = true;
            this.managerNameLabel.Location = new System.Drawing.Point(146, 184);
            this.managerNameLabel.Name = "managerNameLabel";
            this.managerNameLabel.Size = new System.Drawing.Size(0, 13);
            this.managerNameLabel.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(82, 156);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Works";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Manager Full Name";
            // 
            // managersTableAdapter
            // 
            this.managersTableAdapter.ClearBeforeFill = true;
            // 
            // agreementTableAdapter
            // 
            this.agreementTableAdapter.ClearBeforeFill = true;
            // 
            // Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(312, 267);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.managerNameLabel);
            this.Controls.Add(this.managerWorkAddressLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBox_Type);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox_UserId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Capitalisation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.balanceBox);
            this.Name = "Account";
            this.Text = "Account";
            this.Load += new System.EventHandler(this.Account_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clientsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bankDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accountsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.managersBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox balanceBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Capitalisation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_UserId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Exit;
        private BankDataSet bankDataSet;
        private BankDataSetTableAdapters.AccountsTableAdapter accountsTableAdapter;
        private BankDataSetTableAdapters.ClientsTableAdapter clientsTableAdapter;
        private System.Windows.Forms.BindingSource clientsBindingSource;
        private System.Windows.Forms.ComboBox comboBox_Type;
        private System.Windows.Forms.BindingSource accountsBindingSource;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label managerWorkAddressLabel;
        private System.Windows.Forms.Label managerNameLabel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.BindingSource managersBindingSource;
        private BankDataSetTableAdapters.ManagersTableAdapter managersTableAdapter;
        private BankDataSetTableAdapters.AgreementTableAdapter agreementTableAdapter;
    }
}