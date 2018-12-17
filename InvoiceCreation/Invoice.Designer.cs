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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Invoice));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.label9 = new System.Windows.Forms.Label();
            this.txtEnvelope = new System.Windows.Forms.TextBox();
            this.txtIncidentID = new System.Windows.Forms.TextBox();
            this.cboIVA = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboBU = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblRN = new System.Windows.Forms.Label();
            this.lblSRtype = new System.Windows.Forms.Label();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblICAO = new System.Windows.Forms.Label();
            this.lblAircraftCategory = new System.Windows.Forms.Label();
            this.lblTail = new System.Windows.Forms.Label();
            this.BtnChange = new System.Windows.Forms.Button();
            this.lblPayTerm = new System.Windows.Forms.Label();
            this.lblPSN = new System.Windows.Forms.Label();
            this.lblRoutes = new System.Windows.Forms.Label();
            this.lblArrival = new System.Windows.Forms.Label();
            this.lblDeparture = new System.Windows.Forms.Label();
            this.lblCatOrder = new System.Windows.Forms.Label();
            this.lblTripNumber = new System.Windows.Forms.Label();
            this.lblReservation = new System.Windows.Forms.Label();
            this.lblSNumber = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.lblExRate = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtExchangeRate = new System.Windows.Forms.TextBox();
            this.lblArrivalDate = new System.Windows.Forms.Label();
            this.lblDepartureDate = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridServicios)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnInvoice
            // 
            this.BtnInvoice.Image = ((System.Drawing.Image)(resources.GetObject("BtnInvoice.Image")));
            this.BtnInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnInvoice.Location = new System.Drawing.Point(9, 240);
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
            this.dataGridServicios.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
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
            this.dataGridServicios.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridServicios.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridServicios.Location = new System.Drawing.Point(0, 268);
            this.dataGridServicios.MultiSelect = false;
            this.dataGridServicios.Name = "dataGridServicios";
            this.dataGridServicios.RowHeadersVisible = false;
            this.dataGridServicios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridServicios.Size = new System.Drawing.Size(767, 163);
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
            this.label1.Location = new System.Drawing.Point(9, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Services Data";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(-3, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(772, 2);
            this.label2.TabIndex = 47;
            // 
            // txtUtilidad
            // 
            this.txtUtilidad.Location = new System.Drawing.Point(528, 100);
            this.txtUtilidad.Name = "txtUtilidad";
            this.txtUtilidad.ReadOnly = true;
            this.txtUtilidad.Size = new System.Drawing.Size(177, 20);
            this.txtUtilidad.TabIndex = 48;
            this.txtUtilidad.TabStop = false;
            // 
            // txtCombustible
            // 
            this.txtCombustible.Location = new System.Drawing.Point(528, 133);
            this.txtCombustible.Name = "txtCombustible";
            this.txtCombustible.ReadOnly = true;
            this.txtCombustible.Size = new System.Drawing.Size(177, 20);
            this.txtCombustible.TabIndex = 49;
            this.txtCombustible.TabStop = false;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(143, 34);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(177, 20);
            this.txtCustomerName.TabIndex = 50;
            this.txtCustomerName.TabStop = false;
            // 
            // txtRFC
            // 
            this.txtRFC.Location = new System.Drawing.Point(143, 67);
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.ReadOnly = true;
            this.txtRFC.Size = new System.Drawing.Size(177, 20);
            this.txtRFC.TabIndex = 51;
            this.txtRFC.TabStop = false;
            // 
            // txtAccount
            // 
            this.txtAccount.Location = new System.Drawing.Point(143, 104);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(177, 20);
            this.txtAccount.TabIndex = 52;
            this.txtAccount.TabStop = false;
            // 
            // txtRoyalty
            // 
            this.txtRoyalty.Location = new System.Drawing.Point(528, 67);
            this.txtRoyalty.Name = "txtRoyalty";
            this.txtRoyalty.ReadOnly = true;
            this.txtRoyalty.Size = new System.Drawing.Size(177, 20);
            this.txtRoyalty.TabIndex = 53;
            this.txtRoyalty.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "RFC";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 55;
            this.label4.Text = "Account Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(436, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 57;
            this.label6.Text = "Fuel";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 41);
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
            this.txtIncidentID.Location = new System.Drawing.Point(714, 169);
            this.txtIncidentID.Name = "txtIncidentID";
            this.txtIncidentID.ReadOnly = true;
            this.txtIncidentID.Size = new System.Drawing.Size(55, 20);
            this.txtIncidentID.TabIndex = 62;
            this.txtIncidentID.Visible = false;
            // 
            // cboIVA
            // 
            this.cboIVA.FormattingEnabled = true;
            this.cboIVA.Items.AddRange(new object[] {
            "0%",
            "16%"});
            this.cboIVA.Location = new System.Drawing.Point(528, 168);
            this.cboIVA.Name = "cboIVA";
            this.cboIVA.Size = new System.Drawing.Size(177, 21);
            this.cboIVA.TabIndex = 63;
            this.cboIVA.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(436, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 64;
            this.label5.Text = "Currency";
            // 
            // cboBU
            // 
            this.cboBU.FormattingEnabled = true;
            this.cboBU.Items.AddRange(new object[] {
            "BU ICCS",
            "BU ICCS US"});
            this.cboBU.Location = new System.Drawing.Point(143, 166);
            this.cboBU.Name = "cboBU";
            this.cboBU.Size = new System.Drawing.Size(177, 21);
            this.cboBU.TabIndex = 65;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 66;
            this.label10.Text = "Business Unit";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(525, 5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(68, 13);
            this.label19.TabIndex = 67;
            this.label19.Text = "SR Number: ";
            // 
            // lblRN
            // 
            this.lblRN.AutoSize = true;
            this.lblRN.Location = new System.Drawing.Point(610, 5);
            this.lblRN.Name = "lblRN";
            this.lblRN.Size = new System.Drawing.Size(0, 13);
            this.lblRN.TabIndex = 68;
            // 
            // lblSRtype
            // 
            this.lblSRtype.Location = new System.Drawing.Point(338, 14);
            this.lblSRtype.Name = "lblSRtype";
            this.lblSRtype.Size = new System.Drawing.Size(67, 21);
            this.lblSRtype.TabIndex = 69;
            this.lblSRtype.Visible = false;
            // 
            // lblCurrency
            // 
            this.lblCurrency.Location = new System.Drawing.Point(528, 192);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(177, 21);
            this.lblCurrency.TabIndex = 70;
            this.lblCurrency.Visible = false;
            // 
            // lblICAO
            // 
            this.lblICAO.Location = new System.Drawing.Point(338, 66);
            this.lblICAO.Name = "lblICAO";
            this.lblICAO.Size = new System.Drawing.Size(67, 21);
            this.lblICAO.TabIndex = 71;
            this.lblICAO.Visible = false;
            // 
            // lblAircraftCategory
            // 
            this.lblAircraftCategory.Location = new System.Drawing.Point(338, 87);
            this.lblAircraftCategory.Name = "lblAircraftCategory";
            this.lblAircraftCategory.Size = new System.Drawing.Size(67, 21);
            this.lblAircraftCategory.TabIndex = 72;
            this.lblAircraftCategory.Visible = false;
            // 
            // lblTail
            // 
            this.lblTail.Location = new System.Drawing.Point(338, 108);
            this.lblTail.Name = "lblTail";
            this.lblTail.Size = new System.Drawing.Size(67, 21);
            this.lblTail.TabIndex = 73;
            this.lblTail.Visible = false;
            // 
            // BtnChange
            // 
            this.BtnChange.Location = new System.Drawing.Point(647, 234);
            this.BtnChange.Name = "BtnChange";
            this.BtnChange.Size = new System.Drawing.Size(108, 23);
            this.BtnChange.TabIndex = 75;
            this.BtnChange.Text = "Change Currency";
            this.BtnChange.UseVisualStyleBackColor = true;
            this.BtnChange.Visible = false;
            // 
            // lblPayTerm
            // 
            this.lblPayTerm.Location = new System.Drawing.Point(338, 129);
            this.lblPayTerm.Name = "lblPayTerm";
            this.lblPayTerm.Size = new System.Drawing.Size(67, 15);
            this.lblPayTerm.TabIndex = 76;
            this.lblPayTerm.Visible = false;
            // 
            // lblPSN
            // 
            this.lblPSN.Location = new System.Drawing.Point(338, 56);
            this.lblPSN.Name = "lblPSN";
            this.lblPSN.Size = new System.Drawing.Size(67, 10);
            this.lblPSN.TabIndex = 77;
            this.lblPSN.Visible = false;
            // 
            // lblRoutes
            // 
            this.lblRoutes.Location = new System.Drawing.Point(338, 154);
            this.lblRoutes.Name = "lblRoutes";
            this.lblRoutes.Size = new System.Drawing.Size(67, 10);
            this.lblRoutes.TabIndex = 78;
            this.lblRoutes.Visible = false;
            // 
            // lblArrival
            // 
            this.lblArrival.Location = new System.Drawing.Point(338, 174);
            this.lblArrival.Name = "lblArrival";
            this.lblArrival.Size = new System.Drawing.Size(55, 13);
            this.lblArrival.TabIndex = 79;
            this.lblArrival.Visible = false;
            // 
            // lblDeparture
            // 
            this.lblDeparture.Location = new System.Drawing.Point(224, 207);
            this.lblDeparture.Name = "lblDeparture";
            this.lblDeparture.Size = new System.Drawing.Size(44, 21);
            this.lblDeparture.TabIndex = 80;
            // 
            // lblCatOrder
            // 
            this.lblCatOrder.Location = new System.Drawing.Point(338, 218);
            this.lblCatOrder.Name = "lblCatOrder";
            this.lblCatOrder.Size = new System.Drawing.Size(55, 10);
            this.lblCatOrder.TabIndex = 81;
            // 
            // lblTripNumber
            // 
            this.lblTripNumber.Location = new System.Drawing.Point(338, 234);
            this.lblTripNumber.Name = "lblTripNumber";
            this.lblTripNumber.Size = new System.Drawing.Size(67, 10);
            this.lblTripNumber.TabIndex = 82;
            this.lblTripNumber.Visible = false;
            // 
            // lblReservation
            // 
            this.lblReservation.Location = new System.Drawing.Point(338, 187);
            this.lblReservation.Name = "lblReservation";
            this.lblReservation.Size = new System.Drawing.Size(67, 10);
            this.lblReservation.TabIndex = 83;
            this.lblReservation.Visible = false;
            // 
            // lblSNumber
            // 
            this.lblSNumber.Location = new System.Drawing.Point(338, 164);
            this.lblSNumber.Name = "lblSNumber";
            this.lblSNumber.Size = new System.Drawing.Size(67, 10);
            this.lblSNumber.TabIndex = 84;
            this.lblSNumber.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(338, 247);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(55, 10);
            this.lblStatus.TabIndex = 85;
            this.lblStatus.Visible = false;
            // 
            // cboCurrency
            // 
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Items.AddRange(new object[] {
            "MXN",
            "USD"});
            this.cboCurrency.Location = new System.Drawing.Point(528, 166);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(177, 21);
            this.cboCurrency.TabIndex = 86;
            this.cboCurrency.SelectedIndexChanged += new System.EventHandler(this.cboCurrency_SelectedIndexChanged);
            // 
            // lblExRate
            // 
            this.lblExRate.Location = new System.Drawing.Point(338, 144);
            this.lblExRate.Name = "lblExRate";
            this.lblExRate.Size = new System.Drawing.Size(67, 10);
            this.lblExRate.TabIndex = 87;
            this.lblExRate.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(31, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 13);
            this.label12.TabIndex = 89;
            this.label12.Text = "Exchange Rate";
            // 
            // txtExchangeRate
            // 
            this.txtExchangeRate.Location = new System.Drawing.Point(143, 133);
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.ReadOnly = true;
            this.txtExchangeRate.Size = new System.Drawing.Size(177, 20);
            this.txtExchangeRate.TabIndex = 88;
            this.txtExchangeRate.TabStop = false;
            // 
            // lblArrivalDate
            // 
            this.lblArrivalDate.Location = new System.Drawing.Point(450, 207);
            this.lblArrivalDate.Name = "lblArrivalDate";
            this.lblArrivalDate.Size = new System.Drawing.Size(119, 21);
            this.lblArrivalDate.TabIndex = 90;
            this.lblArrivalDate.Visible = false;
            // 
            // lblDepartureDate
            // 
            this.lblDepartureDate.Location = new System.Drawing.Point(441, 242);
            this.lblDepartureDate.Name = "lblDepartureDate";
            this.lblDepartureDate.Size = new System.Drawing.Size(119, 21);
            this.lblDepartureDate.TabIndex = 91;
            this.lblDepartureDate.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(436, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 92;
            this.label14.Text = "Status";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(436, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(32, 13);
            this.label15.TabIndex = 58;
            this.label15.Text = "Utility";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(436, 74);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 13);
            this.label16.TabIndex = 59;
            this.label16.Text = "Royalty";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(528, 34);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(177, 20);
            this.txtStatus.TabIndex = 93;
            this.txtStatus.TabStop = false;
            // 
            // Invoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 431);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblDepartureDate);
            this.Controls.Add(this.lblArrivalDate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtExchangeRate);
            this.Controls.Add(this.lblExRate);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblSNumber);
            this.Controls.Add(this.lblReservation);
            this.Controls.Add(this.lblTripNumber);
            this.Controls.Add(this.lblCatOrder);
            this.Controls.Add(this.lblDeparture);
            this.Controls.Add(this.lblArrival);
            this.Controls.Add(this.lblRoutes);
            this.Controls.Add(this.lblPSN);
            this.Controls.Add(this.lblPayTerm);
            this.Controls.Add(this.BtnChange);
            this.Controls.Add(this.lblTail);
            this.Controls.Add(this.lblAircraftCategory);
            this.Controls.Add(this.lblICAO);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.lblSRtype);
            this.Controls.Add(this.lblRN);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cboBU);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboIVA);
            this.Controls.Add(this.txtIncidentID);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label15);
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
            this.Controls.Add(this.txtEnvelope);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Invoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice";
            this.Load += new System.EventHandler(this.Invoice_Load_1);
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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtEnvelope;
        private System.Windows.Forms.TextBox txtIncidentID;
        private System.Windows.Forms.ComboBox cboIVA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboBU;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblRN;
        private System.Windows.Forms.Label lblSRtype;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.Label lblICAO;
        private System.Windows.Forms.Label lblAircraftCategory;
        private System.Windows.Forms.Label lblTail;
        private System.Windows.Forms.Button BtnChange;
        private System.Windows.Forms.Label lblPayTerm;
        private System.Windows.Forms.Label lblPSN;
        private System.Windows.Forms.Label lblRoutes;
        private System.Windows.Forms.Label lblArrival;
        private System.Windows.Forms.Label lblDeparture;
        private System.Windows.Forms.Label lblCatOrder;
        private System.Windows.Forms.Label lblTripNumber;
        private System.Windows.Forms.Label lblReservation;
        private System.Windows.Forms.Label lblSNumber;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.Label lblExRate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtExchangeRate;
        private System.Windows.Forms.Label lblArrivalDate;
        private System.Windows.Forms.Label lblDepartureDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtStatus;
    }
}