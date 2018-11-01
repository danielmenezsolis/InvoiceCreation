namespace InvoiceCreation
{
    partial class Invoice
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoice));
            this.BtnInvoice = new System.Windows.Forms.Button();
            this.dataGridServicios = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUtilidad = new System.Windows.Forms.TextBox();
            this.txtCombustible = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.txtRoyalty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtEnvelope = new System.Windows.Forms.TextBox();
            this.txtIncidentID = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridServicios)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnInvoice
            // 
            this.BtnInvoice.Image = global::InvoiceCreation.Properties.Resources.receipt16;
            this.BtnInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnInvoice.Location = new System.Drawing.Point(10, 357);
            this.BtnInvoice.Name = "BtnInvoice";
            this.BtnInvoice.Size = new System.Drawing.Size(73, 23);
            this.BtnInvoice.TabIndex = 1;
            this.BtnInvoice.Text = "Generate";
            this.BtnInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnInvoice.UseVisualStyleBackColor = true;
            this.BtnInvoice.Click += new System.EventHandler(this.BtnInvoice_Click);
            // 
            // dataGridServicios
            // 
            this.dataGridServicios.AllowUserToAddRows = false;
            this.dataGridServicios.AllowUserToDeleteRows = false;
            this.dataGridServicios.AllowUserToOrderColumns = true;
            this.dataGridServicios.AllowUserToResizeColumns = false;
            this.dataGridServicios.AllowUserToResizeRows = false;
            this.dataGridServicios.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridServicios.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridServicios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridServicios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridServicios.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridServicios.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridServicios.Location = new System.Drawing.Point(9, 167);
            this.dataGridServicios.MultiSelect = false;
            this.dataGridServicios.Name = "dataGridServicios";
            this.dataGridServicios.RowHeadersVisible = false;
            this.dataGridServicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridServicios.Size = new System.Drawing.Size(752, 184);
            this.dataGridServicios.TabIndex = 5;
            this.dataGridServicios.TabStop = false;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(-3, 12);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(772, 2);
            this.label11.TabIndex = 43;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 13);
            this.label13.TabIndex = 45;
            this.label13.Text = "Customer Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Services Data";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(-3, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(772, 2);
            this.label2.TabIndex = 47;
            // 
            // txtUtilidad
            // 
            this.txtUtilidad.Location = new System.Drawing.Point(506, 75);
            this.txtUtilidad.Name = "txtUtilidad";
            this.txtUtilidad.ReadOnly = true;
            this.txtUtilidad.Size = new System.Drawing.Size(177, 20);
            this.txtUtilidad.TabIndex = 48;
            this.txtUtilidad.TabStop = false;
            // 
            // txtCombustible
            // 
            this.txtCombustible.Location = new System.Drawing.Point(506, 110);
            this.txtCombustible.Name = "txtCombustible";
            this.txtCombustible.ReadOnly = true;
            this.txtCombustible.Size = new System.Drawing.Size(177, 20);
            this.txtCombustible.TabIndex = 49;
            this.txtCombustible.TabStop = false;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(121, 37);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(177, 20);
            this.txtCustomerName.TabIndex = 50;
            this.txtCustomerName.TabStop = false;
            // 
            // txtRFC
            // 
            this.txtRFC.Location = new System.Drawing.Point(121, 75);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.ReadOnly = true;
            this.txtRFC.Size = new System.Drawing.Size(177, 20);
            this.txtRFC.TabIndex = 51;
            this.txtRFC.TabStop = false;
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(121, 110);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(177, 20);
            this.txtAccount.TabIndex = 52;
            this.txtAccount.TabStop = false;
            // 
            // txtRoyalty
            // 
            this.txtRoyalty.Location = new System.Drawing.Point(506, 37);
            this.txtRoyalty.Name = "txtRoyalty";
            this.txtRoyalty.ReadOnly = true;
            this.txtRoyalty.Size = new System.Drawing.Size(177, 20);
            this.txtRoyalty.TabIndex = 53;
            this.txtRoyalty.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "RFC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Account Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(430, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Combustible";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(430, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 58;
            this.label7.Text = "Utilidad";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(430, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 59;
            this.label8.Text = "Royalty";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 41);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Customer Name";
            // 
            // txtEnvelope
            // 
            this.txtEnvelope.Location = new System.Drawing.Point(143, 359);
            this.txtEnvelope.Name = "txtEnvelope";
            this.txtEnvelope.ReadOnly = true;
            this.txtEnvelope.Size = new System.Drawing.Size(618, 20);
            this.txtEnvelope.TabIndex = 61;
            this.txtEnvelope.Visible = false;
            // 
            // txtIncidentID
            // 
            this.txtIncidentID.Enabled = false;
            this.txtIncidentID.Location = new System.Drawing.Point(335, 63);
            this.txtIncidentID.Name = "txtIncidentID";
            this.txtIncidentID.ReadOnly = true;
            this.txtIncidentID.Size = new System.Drawing.Size(55, 20);
            this.txtIncidentID.TabIndex = 62;
            this.txtIncidentID.Visible = false;
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 388);
            this.Controls.Add(this.txtIncidentID);
            this.Controls.Add(this.txtEnvelope);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRoyalty);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.txtRFC);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.txtCombustible);
            this.Controls.Add(this.txtUtilidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridServicios);
            this.Controls.Add(this.BtnInvoice);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Invoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridServicios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnInvoice;
        private System.Windows.Forms.DataGridView dataGridServicios;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUtilidad;
        private System.Windows.Forms.TextBox txtCombustible;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.TextBox txtRoyalty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEnvelope;
        private System.Windows.Forms.TextBox txtIncidentID;
    }
}