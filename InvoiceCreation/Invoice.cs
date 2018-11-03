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

namespace InvoiceCreation
{
    public partial class Invoice : Form
    {
        public bool DesingMode { get; set; }
        public IRecordContext RecordContext { get; set; }
        public IGlobalContext GlobalContext { get; set; }
        private RightNowSyncPortClient clientORN { get; set; }
        public bool created { get; set; }

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
                        created = false;
                        List<int> internalinvoice = DiferentsInvoices();
                        foreach (int i in internalinvoice)
                        {
                            GenerarFactura(i);
                        }
                        if (created)
                        {
                            MessageBox.Show("Data saved");
                            this.Close();
                        }
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
                 "<tran3:xxDatosDeRutas>" + lblArrivalTo.Text + "</tran3:xxDatosDeRutas>" +
                 "<tran3:xxDatosCombistible>" + GetFuels() + "</tran3:xxDatosCombistible>" +
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
                MessageBox.Show(ex.Message + "Det" + ex.StackTrace);
            }
        }
        private string getInvoiceItems(int x)
        {
            string restotal = "";
            int i = 1;
            foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
            {
                if (dgvRenglon.Cells[7].Value.ToString() == x.ToString())
                {
                    string res = "<inv:InvoiceLine>" +
                                           "<inv:LineNumber>" + i + "</inv:LineNumber>" +
                                           "<inv:ItemNumber>" + dgvRenglon.Cells[3].Value.ToString() + "</inv:ItemNumber>" +
                                           "<inv:Description>" + dgvRenglon.Cells[5].Value.ToString() + "</inv:Description>" +
                                           "<inv:Quantity unitCode=\"SER\">1</inv:Quantity>" +
                                           "<inv:UnitSellingPrice currencyCode=\"MXN\">" + dgvRenglon.Cells[8].Value.ToString() + "</inv:UnitSellingPrice>" +
                                           "<inv:TaxClassificationCode>AR IVA 16%</inv:TaxClassificationCode>" +
                                           "<inv:TransactionLineFLEX>" +
                                           "<tran1:xxProveedor>" + dgvRenglon.Cells[5].Value.ToString() + "</tran1:xxProveedor>" +
                                           "<tran1:xxCostoRealFull>" + dgvRenglon.Cells[6].Value.ToString() + "</tran1:xxCostoRealFull>" +
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
        private string GetFuels()
        {
            try
            {
                string fuels = "";
                List<int> fuel = new List<int>();
                foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
                {
                    fuel.Add(Convert.ToInt32(dgvRenglon.Cells[11].Value));
                }
                fuel.Distinct().ToList();

                foreach (var id in fuel)
                {
                    MessageBox.Show(id.ToString());
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
                }
                return fuels;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Det" + ex.StackTrace);
                return "";
            }
        }
        private void UpdateTrxNumber(string Trx, int Invoicex)
        {
            try
            {
                foreach (DataGridViewRow dgvRenglon in dataGridServicios.Rows)
                {
                    if (dgvRenglon.Cells[7].Value.ToString() == Invoicex.ToString())
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
