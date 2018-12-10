using InvoiceCreation.SOAPICCS;
using RestSharp;
using RightNow.AddIns.AddInViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace InvoiceCreation
{
    public partial class Invoice : Form
    {
        public bool DesingMode { get; set; }
        public IRecordContext RecordContext { get; set; }
        public IGlobalContext GlobalContext { get; set; }
        private RightNowSyncPortClient clientORN { get; set; }
        public bool created { get; set; }
        public string VoucheN { get; set; }

        public Invoice(bool inDesignMode, IRecordContext RecordContext, IGlobalContext globalContext)
        {
            this.GlobalContext = globalContext;
            this.RecordContext = RecordContext;
            this.DesingMode = inDesignMode;
            Init();
            InitializeComponent();
        }
        private void BtnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridServicios.Rows.Count > 0)
                {
                    if (ValidateRows())
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        List<int> internalinvoice = DiferentsInvoices();
                        foreach (int i in internalinvoice)
                        {
                            int y = InvoiceGenerate(i);
                            if (y > 0)
                            {
                                EjecutarBatch();
                                MessageBox.Show("Invoice No Send: " + i + "Services send " + y);
                            }
                        }
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show("Please select a Type and Number at any row");
                    }
                }
                else
                {
                    MessageBox.Show("You should have at least one service");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Det" + ex.StackTrace);
            }
        }
        private void EjecutarBatch()
        {
            try
            {
                string envelope = "<soapenv:Envelope " +
                                  "   xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"" +
                                  "   xmlns:typ=\"http://xmlns.oracle.com/apps/financials/commonModules/shared/model/erpIntegrationService/types/\">" +
                                  "<soapenv:Header/>" +
                                  "<soapenv:Body>" +
                                      "<typ:submitESSJobRequest>" +
                                          "<typ:jobPackageName>/oracle/apps/ess/financials/receivables/transactions/autoInvoices/</typ:jobPackageName>" +
                                          "<typ:jobDefinitionName>AutoInvoiceImportEss</typ:jobDefinitionName>" +
                                          "<typ:paramList>300000001746038</typ:paramList>" +
                                          "<typ:paramList>CX AR</typ:paramList>" +
                                          "<typ:paramList>" + DateTime.Today.ToString("yyyy-MM-dd") + "</typ:paramList>" +
                                      "</typ:submitESSJobRequest>" +
                                  "</soapenv:Body>" +
                              "</soapenv:Envelope>";

                byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
                // Construct the base 64 encoded string used as credentials for the service call
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("itotal" + ":" + "Oracle123");
                string credentials = System.Convert.ToBase64String(toEncodeAsBytes);
                // Create HttpWebRequest connection to the service
                HttpWebRequest request =
                 (HttpWebRequest)WebRequest.Create("https://egqy-test.fa.us6.oraclecloud.com:443/fscmService/ErpIntegrationService");
                // Configure the request content type to be xml, HTTP method to be POST, and set the content length
                request.Method = "POST";
                request.ContentType = "text/xml;charset=UTF-8";
                request.ContentLength = byteArray.Length;
                // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
                request.Headers.Add("Authorization", "Basic " + credentials);
                // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
                request.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/financials/commonModules/shared/model/erpIntegrationService/submitESSJobRequest");
                // Write the xml payload to the request
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        String responseString = reader.ReadToEnd();
                        int env = responseString.IndexOf("<?xml");
                        int envfi = responseString.IndexOf("env:Envelope>");
                        envfi = envfi + 13;
                        int tot = envfi - env;
                        string res = responseString.Substring(env, tot);
                        XmlDocument xmlDoc = new XmlDocument();
                        XmlTextReader readerxml = new XmlTextReader(new StringReader(res));
                        xmlDoc.Load(readerxml);
                        XmlNamespaceManager nms = new XmlNamespaceManager(xmlDoc.NameTable);
                        nms.AddNamespace("ns0", "http://xmlns.oracle.com/apps/financials/commonModules/shared/model/erpIntegrationService/types/");
                        XmlNode desiredNode = xmlDoc.SelectSingleNode("//ns0:submitESSJobRequestResponse", nms);
                        if (desiredNode.HasChildNodes)
                        {
                            for (int i = 0; i < desiredNode.ChildNodes.Count; i++)
                            {
                                if (desiredNode.ChildNodes[i].LocalName == "result")
                                {
                                    MessageBox.Show(desiredNode.InnerText);
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("EjecutarBatch" + ex.Message + "Det: " + ex.StackTrace);
            }


        }
        private int InvoiceGenerate(int x)
        {
            try
            {
                int y = 0;
                foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
                {
                    if (dgvRenglon.Cells[1].Value.ToString() == x.ToString())
                    {
                        string itemN = dgvRenglon.Cells[4].Value.ToString();
                        string itemDe = dgvRenglon.Cells[5].Value.ToString();
                        //DataGridViewComboBoxCell cbu = (DataGridViewComboBoxCell)dgvRenglon.Cells[2];
                        string BU = cboBU.SelectedValue.ToString();
                        string BUText = cboBU.Text;
                        DataGridViewComboBoxCell Ctt = (DataGridViewComboBoxCell)dgvRenglon.Cells[0];
                        string CustomerTrxTypeNameText = Ctt.FormattedValue.ToString();
                        string BatchSourceName = "";

                        if (BUText.Contains("USD"))
                        {
                            BatchSourceName = "ORIGEN ICCS US";
                            CustomerTrxTypeNameText = "CFCC_ICCS_US";
                        }
                        else
                        {
                            if (CustomerTrxTypeNameText == "Invoice")
                            {

                                CustomerTrxTypeNameText = "CFCC";
                            }
                            else
                            {
                                CustomerTrxTypeNameText = "RCBO ICCS";
                            }

                            BatchSourceName = "ORIGEN ICCS";
                        }
                        string IVA = dgvRenglon.Cells[16].Value.ToString();
                        double Costo = String.IsNullOrEmpty(dgvRenglon.Cells[7].Value.ToString()) ? 0 : Convert.ToDouble(dgvRenglon.Cells[7].Value.ToString());
                        double Precio = String.IsNullOrEmpty(dgvRenglon.Cells[9].Value.ToString()) ? 0 : Convert.ToDouble(dgvRenglon.Cells[9].Value.ToString());
                        string Sup = dgvRenglon.Cells[6].Value.ToString();
                        string lineDate = lblRN.Text.Substring(0, 6);
                        string lineVal = lblRN.Text.Substring(6).Replace("-", string.Empty).Replace("0", string.Empty);
                        Random rnd = new Random();
                        int intrnd = rnd.Next(1, 999);


                        string envelope = "<soapenv:Envelope " +
                        "   xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"" +
                        "   xmlns:typ=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/types/\"" +
                        "   xmlns:inv=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/\"" +
                        "   xmlns:tran=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/autoInvoices/model/flex/TransactionInterfaceGdf/\"" +
                        "   xmlns:tran1=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/autoInvoices/model/flex/TransactionLineInterfaceGdf/\"" +
                        "   xmlns:tran2=\"http://xmlns.oracle.com/apps/flex/financials/receivables/transactions/autoInvoices/TransactionLineInterfaceLineDff/\"" +
                        "   xmlns:tran3=\"http://xmlns.oracle.com/apps/flex/financials/receivables/transactions/autoInvoices/TransactionInterfaceLinkToDff/\"" +
                        "   xmlns:tran4=\"http://xmlns.oracle.com/apps/flex/financials/receivables/transactions/autoInvoices/TransactionInterfaceReferenceDff/\"" +
                        "   xmlns:tran5=\"http://xmlns.oracle.com/apps/flex/financials/receivables/transactions/autoInvoices/TransactionLineDff/\"" +
                        "   xmlns:tran6=\"http://xmlns.oracle.com/apps/flex/financials/receivables/transactions/autoInvoices/TransactionInterfaceHeaderDff/\"" +
                        "   xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">" +
                        " <soapenv:Header/> " +
                        " <soapenv:Body> " +
                        "<typ:createInterfaceLine>" +
                        "<typ:interfaceLine>" +
                        "<inv:OrgId>" + BU + "</inv:OrgId>" +
                        "<inv:BatchSourceName>" + BatchSourceName + "</inv:BatchSourceName>" +
                        "<inv:CustomerTrxTypeName>" + CustomerTrxTypeNameText + "</inv:CustomerTrxTypeName>" +
                        "<inv:BillCustomerAccountNumber>" + txtAccount.Text + "</inv:BillCustomerAccountNumber>" +
                        "<inv:BillCustomerSiteNumber>" + lblPSN.Text + "</inv:BillCustomerSiteNumber>" +
                        "<inv:TrxDate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</inv:TrxDate>" +
                        "<inv:CurrencyCode>" + cboCurrency.Text + "</inv:CurrencyCode>";
                        if (cboCurrency.Text == "USD")
                        {
                            envelope += "<inv:ConversionType>DOF</inv:ConversionType>";
                            //"<inv:ConversionRate>1</inv:ConversionRate>";
                        }
                        envelope += "<inv:GlDate>" + DateTime.Now.ToString("yyyy-MM-dd") + "</inv:GlDate>" +
                        "<inv:ItemNumber>" + itemN + "</inv:ItemNumber>" +
                       "<inv:Description>" + itemDe + "</inv:Description>" +
                       "<inv:LineType>LINE</inv:LineType>" +
                       "<inv:Quantity unitCode=\"SER\">1</inv:Quantity>" +
                       "<inv:TaxCode>" + IVA + "</inv:TaxCode>" +
                       //"<inv:PaymentTermsName>" + lblPayTerm.Text + "</inv:PaymentTermsName>" +
                       "<inv:PaymentTermsName>30 DIAS</inv:PaymentTermsName>" +
                       "<inv:UnitSellingPrice currencyCode=\"" + cboCurrency.Text + "\">" + Costo + "</inv:UnitSellingPrice>" +
                       "<inv:TransactionInterfaceLineDff xsi:type=\"tran2:InvoiceLineContext\">" +
                           "<tran2:__FLEX_Context>Invoice_Line_Context</tran2:__FLEX_Context>" +
                           // "<tran2:lines>" + ((lineDate + lineVal + intrnd).PadLeft(10)).Trim() + "</tran2:lines>" +
                           "<tran2:lines>" + ((lineDate + lineVal + x + y + "8").PadLeft(10)).Trim() + "</tran2:lines>" +
                           "</inv:TransactionInterfaceLineDff>" +
                           "<inv:TransactionLineDff>" +
                           "<tran5:xxProveedor>" + Sup + "</tran5:xxProveedor>" +
                           "<tran5:xxCostoRealFull>" + Costo + "</tran5:xxCostoRealFull>" +
                           "<tran5:xxCantidad>1</tran5:xxCantidad>" +
                           "</inv:TransactionLineDff>" +
                           "<inv:TransactionInterfaceHeaderDff>" +
                           "<tran6:xxServiceRequest>" + lblRN.Text + "</tran6:xxServiceRequest>" +
                           "<tran6:xxDatosFactura>" + lblTail.Text + "|" + lblICAO.Text + "|" + lblArrival.Text + "JUL-25-2018 1440 Z|JUL-25-2018 1800 Z|" + lblSRtype.Text + " " + DateTime.Now.ToString("yyyy-MM-dd") + "|" + lblTripNumber.Text + "|" + lblReservation.Text + "|" + lblCatOrder.Text + "|" + lblSNumber.Text + "||" + lblStatus.Text + "</tran6:xxDatosFactura>" +
                           "<tran6:xxDatosDeRutas>" + lblRoutes.Text + "</tran6:xxDatosDeRutas>" +
                           "<tran6:xxDatosCombistible>" + GetFuels() + " LTRS. / " + (GetFuels() * 3.7853).ToString() + " /GALS.| " + VoucheN + "</tran6:xxDatosCombistible>" +
                      "</inv:TransactionInterfaceHeaderDff>" +
                   "</typ:interfaceLine>" +
               "</typ:createInterfaceLine>" +
           "</soapenv:Body>" +
        "</soapenv:Envelope>";

                        byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
                        GlobalContext.LogMessage(envelope);
                        // Construct the base 64 encoded string used as credentials for the service call
                        byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("itotal" + ":" + "Oracle123");
                        string credentials = System.Convert.ToBase64String(toEncodeAsBytes);
                        // Create HttpWebRequest connection to the service
                        HttpWebRequest request =
                         (HttpWebRequest)WebRequest.Create("https://egqy-test.fa.us6.oraclecloud.com:443/fscmService/RecInvoiceService");
                        // Configure the request content type to be xml, HTTP method to be POST, and set the content length
                        request.Method = "POST";
                        request.ContentType = "text/xml;charset=UTF-8";
                        request.ContentLength = byteArray.Length;
                        // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
                        request.Headers.Add("Authorization", "Basic " + credentials);
                        // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
                        request.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/createInterfaceLine");
                        // Write the xml payload to the request
                        Stream dataStream = request.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();


                        // Write the xml payload to the request
                        XDocument doc;
                        XmlDocument docu = new XmlDocument();
                        string result;
                        using (WebResponse response = request.GetResponse())
                        {
                            using (Stream stream = response.GetResponseStream())
                            {
                                doc = XDocument.Load(stream);
                                result = doc.ToString();
                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.LoadXml(result);
                                XmlNamespaceManager nms = new XmlNamespaceManager(xmlDoc.NameTable);
                                nms.AddNamespace("ns2", "http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/");
                                nms.AddNamespace("ns1", "http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/types/");

                                XmlNode desiredNode = xmlDoc.SelectSingleNode("//ns2:TransactionLineDff", nms);
                                if (desiredNode.HasChildNodes)
                                {
                                    for (int i = 0; i < desiredNode.ChildNodes.Count; i++)
                                    {
                                        if (desiredNode.ChildNodes[i].LocalName == "InterfaceLineGuid")
                                        {
                                            y++;
                                        }
                                    }
                                }

                            }
                            response.Close();
                        }
                    }
                }
                return y;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mess: " + ex.Message + "Det" + ex.StackTrace);
                return 0;
            }
        }


        private void GenerarFactura(int x)
        {
            try
            {
                string dateTime = DateTime.Now.ToString();
                string createddate = Convert.ToDateTime(dateTime).ToString("yyyy-MM-dd");
                string envelope = "<soapenv:Envelope" +
                                   "   xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"" +
                                   "   xmlns:typ=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/types/\"" +
                                   "   xmlns:inv=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/\"" +
                                   "   xmlns:tran=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/shared/model/flex/TransactionInterfaceLineDff/\"" +
                                   "   xmlns:tran1=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/shared/model/flex/TransactionLineDff/\"" +
                                   "   xmlns:tran2=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/shared/model/flex/TransactionLineGdf/\"" +
                                   "   xmlns:tran3=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/shared/model/flex/TransactionHeaderDff/\"" +
                                   "   xmlns:tran4=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/shared/model/flex/TransactionHeaderGdf/\"" +
                                   "   xmlns:tran5=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/shared/model/flex/TransactionInterfaceHeaderDff/\"" +
                                   "   xmlns:tran6=\"http://xmlns.oracle.com/apps/financials/receivables/transactions/shared/model/flex/TransactionDistributionDff/\">" +
                                   "<soapenv:Header/>" +
                                   "<soapenv:Body>" +
                                   "<typ:createSimpleInvoice>" +
                                   "<typ:invoiceHeaderInformation>" +
                                   "<inv:BusinessUnit>BU_ICCS</inv:BusinessUnit>" +
                                   "<inv:TransactionSource>ORIGEN ICCS</inv:TransactionSource>" +
                                   "<inv:TransactionType>CFCC</inv:TransactionType>" +
                                   "<inv:TrxDate>" + createddate + "</inv:TrxDate>" +
                                   "<inv:GlDate>" + createddate + "</inv:GlDate>" +
                                   "<inv:BillToCustomerName>" + txtCustomerName.Text + "</inv:BillToCustomerName>" +
                                   "<inv:BillToAccountNumber>" + txtAccount.Text + "</inv:BillToAccountNumber>" +
                                   "<inv:PaymentTermsName>CONTADO</inv:PaymentTermsName>" +
                                   "<inv:InvoiceCurrencyCode>MXN</inv:InvoiceCurrencyCode>";
                envelope = envelope + getInvoiceItems(x);

                envelope = envelope +
                 "<inv:TransactionHeaderFLEX>" +
                 "<tran3:xxServiceRequest>" + lblRN.Text + "</tran3:xxServiceRequest>" +
                 "<tran3:xxDatosFactura>" + lblTail.Text + "|" + lblICAO.Text + "|MMAC|JUL-25-2018 1440 Z|JUL-25-2018 1800 Z|FBO JUL-25-2018|46789</tran3:xxDatosFactura>" +
                 "<tran3:xxDatosDeRutas>" + lblRoutes.Text + "</tran3:xxDatosDeRutas>" +
                 "<tran3:xxDatosCombistible>" + GetFuels() + " LTRS. / " + (GetFuels() * 3.7853).ToString() + " /GALS." + "</tran3:xxDatosCombistible>" +
                "</inv:TransactionHeaderFLEX>" +
                 "</typ:invoiceHeaderInformation>" +
                 "</typ:createSimpleInvoice>" +
                 "</soapenv:Body>" +
                 "</soapenv:Envelope>";

                byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
                GlobalContext.LogMessage(envelope);
                // Construct the base 64 encoded string used as credentials for the service call
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("itotal" + ":" + "Oracle123");
                string credentials = System.Convert.ToBase64String(toEncodeAsBytes);
                // Create HttpWebRequest connection to the service
                HttpWebRequest request =
                 (HttpWebRequest)WebRequest.Create("https://egqy-test.fa.us6.oraclecloud.com:443/fscmService/RecInvoiceService");
                // Configure the request content type to be xml, HTTP method to be POST, and set the content length
                request.Method = "POST";
                request.ContentType = "text/xml;charset=UTF-8";
                request.ContentLength = byteArray.Length;
                // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
                request.Headers.Add("Authorization", "Basic " + credentials);
                // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
                request.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/createSimpleInvoice");
                // Write the xml payload to the request
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                // Write the xml payload to the request
                XDocument doc;
                XmlDocument docu = new XmlDocument();
                string result;

                // Get the response and process it; In this example, we simply print out the response XDocument doc;
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        doc = XDocument.Load(stream);
                        result = doc.ToString();
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(result);
                        XmlNamespaceManager nms = new XmlNamespaceManager(xmlDoc.NameTable);
                        nms.AddNamespace("env", "http://schemas.xmlsoap.org/soap/envelope/");
                        nms.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");
                        nms.AddNamespace("ns0", "http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/types/");
                        nms.AddNamespace("ns2", "http://xmlns.oracle.com/apps/financials/receivables/transactions/invoices/invoiceService/");
                        XmlNode desiredNode = xmlDoc.SelectSingleNode("//ns0:result", nms);
                        if (desiredNode.HasChildNodes)
                        {
                            for (int i = 0; i < desiredNode.ChildNodes.Count; i++)
                            {
                                if (desiredNode.ChildNodes[i].LocalName == "TransactionNumber")
                                {
                                    UpdateTrxNumber(desiredNode.ChildNodes[i].InnerText, x);
                                }
                            }
                        }
                    }
                    response.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Mess: " + ex.Message + "Det" + ex.StackTrace);
            }
        }
        private string getInvoiceItems(int x)
        {
            string restotal = "";
            int i = 1;
            foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
            {
                if (dgvRenglon.Cells[1].Value.ToString() == x.ToString())
                {
                    string res = "<inv:InvoiceLine>" +
                                           "<inv:LineNumber>" + i + "</inv:LineNumber>" +
                                           "<inv:ItemNumber>" + dgvRenglon.Cells[4].Value.ToString() + "</inv:ItemNumber>" +
                                           "<inv:Description>" + dgvRenglon.Cells[6].Value.ToString() + "</inv:Description>" +
                                           "<inv:Quantity unitCode=\"SER\">1</inv:Quantity>" +
                                           "<inv:UnitSellingPrice currencyCode=\"MXN\">" + dgvRenglon.Cells[9].Value.ToString() + "</inv:UnitSellingPrice>" +
                                           "<inv:TaxClassificationCode>AR IVA 16%</inv:TaxClassificationCode>" +
                                           "<inv:TransactionLineFLEX>" +
                                           "<tran1:xxProveedor>" + dgvRenglon.Cells[6].Value.ToString() + "</tran1:xxProveedor>" +
                                           "<tran1:xxCostoRealFull>" + dgvRenglon.Cells[7].Value.ToString() + "</tran1:xxCostoRealFull>" +
                                           "<tran1:xxCantidad>1</tran1:xxCantidad>" +
                                           "</inv:TransactionLineFLEX>" +
                                           "</inv:InvoiceLine>";
                    restotal = restotal + res;
                    i++;
                }
            }
            return restotal;
        }
        private bool ValidateRows()
        {
            try
            {
                bool validate = true;
                foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
                {

                    if (String.IsNullOrEmpty(dgvRenglon.Cells[0].FormattedValue.ToString()) || String.IsNullOrEmpty(dgvRenglon.Cells[1].FormattedValue.ToString()))
                    {
                        validate = false;
                    }
                }

                return validate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Det" + ex.StackTrace);
                return false;
            }
        }
        private List<int> DiferentsInvoices()
        {
            List<int> invoice = new List<int>();
            foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
            {
                invoice.Add(Convert.ToInt32(dgvRenglon.Cells[1].Value));
            }
            return invoice.Distinct().ToList();
        }
        private double GetFuels()
        {
            try
            {
                double fuels = 0;
                List<int> fuel = new List<int>();
                foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
                {
                    fuel.Add(string.IsNullOrEmpty(dgvRenglon.Cells[12].Value.ToString()) ? 0 : Convert.ToInt32(dgvRenglon.Cells[12].Value));
                }
                fuel.Distinct().ToList();

                ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                clientInfoHeader.AppID = "Query Example";
                String queryString = "SELECT SUM(Liters),VoucherNumber FROM CO.Fueling  WHERE Incident =" + txtIncidentID.Text;
                //String queryString = "SELECT Liters, Liters * 3.7854 Gallons FROM CO.Fueling WHERE ID = " + id;
                clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
                foreach (CSVTable table in queryCSV.CSVTables)
                {
                    String[] rowData = table.Rows;
                    foreach (String data in rowData)
                    {
                        Char delimiter = '|';
                        string[] substrings = data.Split(delimiter);

                        fuels = string.IsNullOrEmpty(substrings[0]) ? 0 : Convert.ToDouble(substrings[0]);
                        VoucheN = substrings[1];
                    }
                }

                /*
                foreach (var id in fuel)
                {
                    ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                    APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                    clientInfoHeader.AppID = "Query Example";
                    String queryString = "SELECT Liters, Liters * 3.7854 Gallons FROM CO.Fueling WHERE ID = " + id;
                    clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 10000, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
                    foreach (CSVTable table in queryCSV.CSVTables)
                    {
                        String[] rowData = table.Rows;
                        foreach (String data in rowData)
                        {
                            fuels += data;
                        }
                    }
                }*/
                return fuels;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Det" + ex.StackTrace);
                return 0;
            }
        }
        private void UpdateTrxNumber(string Trx, int Invoicex)
        {
            try
            {
                foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
                {
                    if (dgvRenglon.Cells[8].Value.ToString() == Invoicex.ToString())
                    {
                        var client = new RestClient("https://iccsmx.custhelp.com/");
                        var request = new RestRequest("/services/rest/connect/latest/CO.Services/" + dgvRenglon.Cells[0].Value.ToString(), Method.POST);
                        request.RequestFormat = DataFormat.Json;
                        var body = "{";
                        // Información de precios costos
                        body += "\"ERPInvoice\":\"" + Trx + "\"";
                        body += "}";

                        request.AddParameter("application/json", body, ParameterType.RequestBody);
                        // easily add HTTP Headers
                        request.AddHeader("Authorization", "Basic ZW9saXZhczpTaW5lcmd5KjIwMTg=");
                        request.AddHeader("X-HTTP-Method-Override", "PATCH");
                        request.AddHeader("OSvC-CREST-Application-Context", "Update Incident" + txtIncidentID.Text);
                        // execute the request
                        IRestResponse response = client.Execute(request);
                        var content = response.Content;
                        if (content == "")
                        {
                            created = true;
                        }
                        else
                        {
                            MessageBox.Show(response.Content);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public bool Init()
        {
            try
            {
                bool result = false;
                EndpointAddress endPointAddr = new EndpointAddress(GlobalContext.GetInterfaceServiceUrl(ConnectServiceType.Soap));
                // Minimum required
                BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
                binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
                binding.ReceiveTimeout = new TimeSpan(0, 10, 0);
                binding.MaxReceivedMessageSize = 1048576; //1MB
                binding.SendTimeout = new TimeSpan(0, 10, 0);
                // Create client proxy class
                clientORN = new RightNowSyncPortClient(binding, endPointAddr);
                // Ask the client to not send the timestamp
                BindingElementCollection elements = clientORN.Endpoint.Binding.CreateBindingElements();
                elements.Find<SecurityBindingElement>().IncludeTimestamp = false;
                clientORN.Endpoint.Binding = new CustomBinding(elements);
                // Ask the Add-In framework the handle the session logic
                GlobalContext.PrepareConnectSession(clientORN.ChannelFactory);
                if (clientORN != null)
                {
                    result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en INIT: " + ex.Message);
                return false;

            }
        }
        private void Invoice_Load(object sender, EventArgs e)
        {

        }
    }



}
