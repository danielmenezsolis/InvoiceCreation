using System;
using System.AddIn;
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
using InvoiceCreation.SOAPICCS;
using RightNow.AddIns.AddInViews;
using RightNow.AddIns.Common;


namespace InvoiceCreation
{
    public class WorkspaceRibbonAddIn : Panel, IWorkspaceRibbonButton
    {
        Invoice invoice;
        private IRecordContext recordContext { get; set; }
        private IGlobalContext global { get; set; }
        private bool inDesignMode { get; set; }
        private RightNowSyncPortClient clientORN { get; set; }
        public DataGridView DgvServicios { get; set; }
        public ComboBox BUnitS { get; set; }
        public List<Services> servicios { get; set; }
        public IIncident Incident { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string AccountNumber { get; set; }
        public string CatRoyalty { get; set; }
        public string CatUtilidad { get; set; }
        public string CatCombust { get; set; }
        public int IncidentID { get; set; }
        public string SrType { get; set; }
        public string ICAO { get; set; }
        public string Tail { get; set; }
        public string AircraftCategory { get; set; }
        public string ClientType { get; set; }
        public string FuelType { get; set; }
        public double ExRate { get; set; }
        public string PayTerm { get; set; }
        public string PartyId { get; set; }
        public string PartySiteNumber { get; set; }
        public string CatOrder { get; set; }
        public string TripNumber { get; set; }
        public string Reservation { get; set; }
        public string SNumber { get; set; }

        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public DateTime CatDate { get; set; }
        public DateTime GYCDate { get; set; }
        public DateTime SeneamDate { get; set; }
        public Dictionary<string, string> BUS { get; set; }



        public WorkspaceRibbonAddIn(bool inDesignMode, IRecordContext RecordContext, IGlobalContext globalContext)
        {
            if (inDesignMode == false)
            {
                global = globalContext;
                this.recordContext = RecordContext;
                this.inDesignMode = inDesignMode;
            }
        }

        public new void Click()
        {
            try
            {
                if (Init())
                {
                    Incident = (IIncident)recordContext.GetWorkspaceRecord(WorkspaceRecordType.Incident);
                    IncidentID = Incident.ID;
                    IList<ICfVal> campos = Incident.CustomField;
                    foreach (ICfVal val in campos)
                    {

                        if (val.CfId == 29)
                        {
                            TripNumber = val.ValStr;
                        }
                        if (val.CfId == 30)
                        {
                            Reservation = val.ValStr;
                        }
                        if (val.CfId == 33)
                        {
                            SNumber = val.ValStr;
                        }
                        if (val.CfId == 35)
                        {
                            CatOrder = val.ValStr;
                        }
                        if (val.CfId == 37)
                        {
                            CatDate = Convert.ToDateTime(val.ValDttm);
                        }
                        if (val.CfId == 47)
                        {
                            GYCDate = Convert.ToDateTime(val.ValDate);
                        }
                        if (val.CfId == 66)
                        {
                            SeneamDate = Convert.ToDateTime(val.ValDate);
                        }
                        if (val.CfId == 57)
                        {
                            PartyId = val.ValStr;
                        }
                        if (val.CfId == 58)
                        {
                            Nombre = val.ValStr;
                        }
                        if (val.CfId == 59)
                        {
                            RFC = val.ValStr;
                        }
                        if (val.CfId == 60)
                        {
                            AccountNumber = val.ValStr;
                        }
                        if (val.CfId == 61)
                        {
                            CatRoyalty = val.ValStr;
                        }
                        if (val.CfId == 62)
                        {
                            CatUtilidad = val.ValStr;
                        }
                        if (val.CfId == 63)
                        {
                            CatCombust = val.ValStr;
                        }

                        if (val.CfId == 100)
                        {
                            ArrivalDate = val.ValDate;
                        }
                        if (val.CfId == 94)
                        {
                            ArrivalTime = val.ValStr;
                        }
                        if (val.CfId == 101)
                        {
                            DepartureDate = val.ValDate;
                        }
                        if (val.CfId == 95)
                        {
                            DepartureTime = val.ValStr;
                        }
                    }

                    if (SrType == "FUEL")
                    {
                        Departure = Convert.ToDateTime(DepartureDate);
                        Departure = Convert.ToDateTime(Departure.ToString("yyyy-MM-dd") + " " + DepartureTime.Insert(2, ":"));
                        Arrival = Convert.ToDateTime(ArrivalDate);
                        Arrival = Convert.ToDateTime(Arrival.ToString("yyyy-MM-dd") + " " + ArrivalTime.Insert(2, ":"));
                    }
                    ICAO = getICAODesi();
                    SrType = GetSRType();
                    AircraftCategory = GetCargoGroup(ICAO);
                    Tail = GetTail();
                    ClientType = GetClientType();
                    FuelType = GetFuelType();
                    ExRate = getExchangeRate(DateTime.Now);
                    PayTerm = GetPaymentTermns();
                    PartySiteNumber = getPartySiteNumber();
                    BUS = GetBUS();
                    servicios = GetListServices();
                    invoice = new Invoice(inDesignMode, recordContext, global);
                    DgvServicios = ((DataGridView)invoice.Controls["dataGridServicios"]);
                    BUnitS = ((ComboBox)invoice.Controls["cboBU"]);
                    BUnitS.DataSource = new BindingSource(BUS, null);
                    BUnitS.DisplayMember = "Value";
                    BUnitS.ValueMember = "Key";

                    DataGridViewComboBoxColumn combType = new DataGridViewComboBoxColumn();
                    combType.HeaderText = "Type";
                    combType.Name = "Type";
                    combType.MaxDropDownItems = 2;
                    combType.Items.Add("Invoice");
                    combType.Items.Add("Recipe");


                    DataGridViewComboBoxColumn combBU = new DataGridViewComboBoxColumn();
                    combBU.HeaderText = "Business Unit";
                    combBU.Name = "Business Unit";
                    combBU.MaxDropDownItems = 2;
                    combBU.DataSource = new BindingSource(BUS, null);
                    combBU.DisplayMember = "Value";
                    combBU.ValueMember = "Key";
                    combBU.Visible = false;

                    DataGridViewComboBoxColumn combNumber = new DataGridViewComboBoxColumn();
                    combNumber.HeaderText = "Number";
                    combNumber.Name = "Number";
                    combNumber.MaxDropDownItems = 2;

                    for (int i = 1; i <= 10; i++)
                    {
                        combNumber.Items.Add(i.ToString());
                    }
                    DgvServicios.Columns.Add(combType);
                    DgvServicios.Columns.Add(combNumber);
                    DgvServicios.Columns.Add(combBU);
                    DgvServicios.DataSource = servicios.OrderBy(o => o.ServiceID).ToList();

                    DgvServicios.Columns[3].ReadOnly = true;
                    DgvServicios.Columns[4].ReadOnly = true;
                    DgvServicios.Columns[5].ReadOnly = true;
                    DgvServicios.Columns[6].ReadOnly = true;
                    DgvServicios.Columns[7].ReadOnly = true;
                    DgvServicios.Columns[8].ReadOnly = true;
                    DgvServicios.Columns[9].ReadOnly = true;
                    DgvServicios.Columns[10].ReadOnly = true;
                    DgvServicios.Columns[11].ReadOnly = true;

                    DgvServicios.Columns[3].Visible = false;
                    DgvServicios.Columns[8].Visible = false;
                    DgvServicios.Columns[10].Visible = false;
                    DgvServicios.Columns[11].Visible = false;
                    DgvServicios.Columns[12].Visible = false;
                    DgvServicios.Columns[13].Visible = false;
                    DgvServicios.Columns[14].Visible = false;
                    DgvServicios.Columns[16].Visible = false;
                    DgvServicios.Columns[17].Visible = false;
                    DgvServicios.Columns[19].Visible = false;
                    DgvServicios.Columns[20].Visible = false;

                    ((TextBox)invoice.Controls["txtIncidentID"]).Text = IncidentID.ToString();
                    ((TextBox)invoice.Controls["txtCustomerName"]).Text = Nombre;
                    ((TextBox)invoice.Controls["txtRFC"]).Text = RFC;
                    ((TextBox)invoice.Controls["txtAccount"]).Text = AccountNumber;
                    ((TextBox)invoice.Controls["txtRoyalty"]).Text = CatRoyalty;
                    ((TextBox)invoice.Controls["txtUtilidad"]).Text = CatUtilidad;
                    ((TextBox)invoice.Controls["txtCombustible"]).Text = CatCombust;
                    ((TextBox)invoice.Controls["txtExchangeRate"]).Text = Math.Round(ExRate, 4).ToString();
                    ((TextBox)invoice.Controls["txtStatus"]).Text = GetStatus();
                    ((System.Windows.Forms.Label)invoice.Controls["lblRN"]).Text = GetReferenceNumber();
                    ((System.Windows.Forms.Label)invoice.Controls["lblSRtype"]).Text = SrType;
                    ((System.Windows.Forms.Label)invoice.Controls["lblCurrency"]).Text = GetSrCurrency();
                    ((System.Windows.Forms.Label)invoice.Controls["lblICAO"]).Text = ICAO;
                    ((System.Windows.Forms.Label)invoice.Controls["lblAircraftCategory"]).Text = AircraftCategory;
                    ((System.Windows.Forms.Label)invoice.Controls["lblTail"]).Text = Tail;
                    ((System.Windows.Forms.Label)invoice.Controls["lblPayTerm"]).Text = PayTerm;
                    ((System.Windows.Forms.Label)invoice.Controls["lblPSN"]).Text = PartySiteNumber;
                    ((System.Windows.Forms.Label)invoice.Controls["lblRoutes"]).Text = GetRoutes();
                    ((System.Windows.Forms.Label)invoice.Controls["lblArrival"]).Text = GetArrival();
                    ((System.Windows.Forms.Label)invoice.Controls["lblDeparture"]).Text = GetDeparture();
                    ((System.Windows.Forms.Label)invoice.Controls["lblTripNumber"]).Text = TripNumber;
                    ((System.Windows.Forms.Label)invoice.Controls["lblCatOrder"]).Text = CatOrder;
                    ((System.Windows.Forms.Label)invoice.Controls["lblReservation"]).Text = Reservation;
                    ((System.Windows.Forms.Label)invoice.Controls["lblSNumber"]).Text = SNumber;
                    ((System.Windows.Forms.Label)invoice.Controls["lblStatus"]).Text = GetStatus();
                    

                    if (SrType == "FUEL")
                    {
                        ((System.Windows.Forms.Label)invoice.Controls["lblArrivalDate"]).Text = Arrival.ToString("yyyy-MM-dd HH:mm");
                        ((System.Windows.Forms.Label)invoice.Controls["lblDepartureDate"]).Text = Departure.ToString("yyyy-MM-dd HH:mm");
                    }
                    if (SrType == "CATERING")
                    {
                        ((System.Windows.Forms.Label)invoice.Controls["lblArrivalDate"]).Text = CatDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                        ((System.Windows.Forms.Label)invoice.Controls["lblDepartureDate"]).Text = CatDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                    }
                    if (SrType == "GYCUSTODIA")
                    {
                        ((System.Windows.Forms.Label)invoice.Controls["lblArrivalDate"]).Text = GYCDate.ToString("yyyy-MM-dd HH:mm");
                        ((System.Windows.Forms.Label)invoice.Controls["lblDepartureDate"]).Text = GYCDate.ToString("yyyy-MM-dd HH:mm");
                    }
                    if (SrType == "SENEAM")
                    {
                        ((System.Windows.Forms.Label)invoice.Controls["lblArrivalDate"]).Text = SeneamDate.ToString("yyyy-MM-dd HH:mm");
                        ((System.Windows.Forms.Label)invoice.Controls["lblDepartureDate"]).Text = SeneamDate.ToString("yyyy-MM-dd HH:mm");
                    }

                    invoice.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en Click: " + ex.Message + "Det: " + ex.StackTrace);
            }
        }

        public Dictionary<string, string> GetBUS()
        {
            Dictionary<string, string> test = new Dictionary<string, string>();
            string envelope = "<soapenv:Envelope" +
                "   xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"" +
                "   xmlns:typ=\"http://xmlns.oracle.com/apps/hcm/organizations/organizationServiceV2/types/\"" +
                "   xmlns:typ1=\"http://xmlns.oracle.com/adf/svc/types/\">" +
                "<soapenv:Header/>" +
                "<soapenv:Body>" +
                    "<typ:findOrganization>" +
                        "<typ:findCriteria>" +
                            "<typ1:fetchStart>0</typ1:fetchStart>" +
                            "<typ1:fetchSize>-1</typ1:fetchSize>" +
                            "<typ1:filter>" +
                                "<typ1:group>" +
                                    "<typ1:item>" +
                                        "<typ1:attribute>ClassificationCode</typ1:attribute>" +
                                        "<typ1:operator>=</typ1:operator>" +
                                        "<typ1:value>FUN_BUSINESS_UNIT</typ1:value>" +
                                    "</typ1:item>" +
                                "</typ1:group>" +
                            "</typ1:filter>" +
                            "<typ1:findAttribute>Name</typ1:findAttribute>" +
                            "<typ1:findAttribute>OrganizationId</typ1:findAttribute>" +
                        "</typ:findCriteria>" +
                        "<typ:findControl>" +
                            "<typ1:retrieveAllTranslations>false</typ1:retrieveAllTranslations>" +
                        "</typ:findControl>" +
                    "</typ:findOrganization>" +
                "</soapenv:Body>" +
            "</soapenv:Envelope>";
            byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
            // Construct the base 64 encoded string used as credentials for the service call
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("itotal" + ":" + "Oracle123");
            string credentials = System.Convert.ToBase64String(toEncodeAsBytes);
            // Create HttpWebRequest connection to the service
            HttpWebRequest request =
             (HttpWebRequest)WebRequest.Create("https://egqy-test.fa.us6.oraclecloud.com:443/hcmService/OrganizationServiceV2");
            // Configure the request content type to be xml, HTTP method to be POST, and set the content length
            request.Method = "POST";
            request.ContentType = "text/xml;charset=UTF-8";
            request.ContentLength = byteArray.Length;
            // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
            request.Headers.Add("Authorization", "Basic " + credentials);
            // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards

            request.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/hcm/organizations/organizationServiceV2/findOrganization");
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
                    nms.AddNamespace("ns1", "http://xmlns.oracle.com/apps/hcm/organizations/organizationServiceV2/");
                    XmlNodeList nodeList = xmlDoc.SelectNodes("//ns1:Value", nms);
                    foreach (XmlNode node in nodeList)
                    {

                        if (node.HasChildNodes)
                        {
                            string BName = "";
                            string BId = "";
                            if (node.LocalName == "Value")
                            {
                                XmlNodeList nodeListvalue = node.ChildNodes;
                                foreach (XmlNode nodeValue in nodeListvalue)
                                {
                                    if (nodeValue.LocalName == "OrganizationId")
                                    {
                                        BId = nodeValue.InnerText;
                                    }
                                    if (nodeValue.LocalName == "Name")
                                    {
                                        BName = nodeValue.InnerText;

                                    }
                                }
                                if (!BName.Contains("NA"))
                                {
                                    test.Add(BId, BName);
                                }
                            }

                        }
                    }
                }
            }
            return test;
        }

        public string getPartySiteNumber()
        {
            try
            {
                string val = "";
                string envelope = "<soapenv:Envelope " +
                      "   xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"" +
                      "   xmlns:typ=\"http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/types/\">" +
                      "<soapenv:Header/>" +
                      "<soapenv:Body>" +
                      "<typ:getOrganization>" +
                              "<typ:partyId>" + PartyId + "</typ:partyId>" +
                          "</typ:getOrganization>" +
                      "</soapenv:Body>" +
                  "</soapenv:Envelope>";
                global.LogMessage(envelope);
                byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
                // Construct the base 64 encoded string used as credentials for the service call
                byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("itotal" + ":" + "Oracle123");
                string credentials = System.Convert.ToBase64String(toEncodeAsBytes);
                // Create HttpWebRequest connection to the service
                HttpWebRequest request =
                 (HttpWebRequest)WebRequest.Create("https://egqy-test.fa.us6.oraclecloud.com:443/crmService/FoundationPartiesOrganizationService");
                // Configure the request content type to be xml, HTTP method to be POST, and set the content length
                request.Method = "POST";
                request.ContentType = "text/xml;charset=UTF-8";
                request.ContentLength = byteArray.Length;
                // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
                request.Headers.Add("Authorization", "Basic " + credentials);
                // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
                request.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/getOrganization");
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
                        nms.AddNamespace("ns1", "http://xmlns.oracle.com/apps/cdm/foundation/parties/partyService/");
                        XmlNode desiredNode = xmlDoc.SelectSingleNode("//ns1:PartySiteNumber", nms);
                        val = desiredNode.FirstChild == null ? "" : desiredNode.FirstChild.InnerText;
                    }
                }

                return val;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Det: " + ex.StackTrace);
                return "";
            }
        }


        public string GetPaymentTermns()
        {
            string val = "";
            string envelope = "<soapenv:Envelope " +
              "   xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"" +
              "   xmlns:typ=\"http://xmlns.oracle.com/apps/financials/receivables/customers/customerProfileService/types/\"" +
              "   xmlns:cus=\"http://xmlns.oracle.com/apps/financials/receivables/customers/customerProfileService/\"" +
              "   xmlns:cus1=\"http://xmlns.oracle.com/apps/financials/receivables/customerSetup/customerProfiles/model/flex/CustomerProfileDff/\"" +
              "   xmlns:cus2=\"http://xmlns.oracle.com/apps/financials/receivables/customerSetup/customerProfiles/model/flex/CustomerProfileGdf/\">" +
              "<soapenv:Header/>" +
              "<soapenv:Body>" +
                  "<typ:getActiveCustomerProfile>" +
                      "<typ:customerProfile>" +
                          "<cus:AccountNumber>" + AccountNumber + "</cus:AccountNumber>" +
                      "</typ:customerProfile>" +
                  "</typ:getActiveCustomerProfile>" +
              "</soapenv:Body>" +
             "</soapenv:Envelope>";
            global.LogMessage(envelope);
            byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
            // Construct the base 64 encoded string used as credentials for the service call
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes("itotal" + ":" + "Oracle123");
            string credentials = System.Convert.ToBase64String(toEncodeAsBytes);
            // Create HttpWebRequest connection to the service
            HttpWebRequest request =
             (HttpWebRequest)WebRequest.Create("https://egqy-test.fa.us6.oraclecloud.com:443/fscmService/ReceivablesCustomerProfileService");
            // Configure the request content type to be xml, HTTP method to be POST, and set the content length
            request.Method = "POST";
            request.ContentType = "text/xml;charset=UTF-8";
            request.ContentLength = byteArray.Length;
            // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
            request.Headers.Add("Authorization", "Basic " + credentials);
            // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards

            request.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/financials/receivables/customers/customerProfileService/getActiveCustomerProfile");
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
                    nms.AddNamespace("ns3", "http://xmlns.oracle.com/apps/financials/receivables/customers/customerProfileService/");
                    XmlNode desiredNode = xmlDoc.SelectSingleNode("//ns3:PaymentTerms", nms);
                    val = desiredNode.FirstChild == null ? "" : desiredNode.FirstChild.InnerText;
                }
            }

            return val;

        }
        public string GetRoutes()
        {
            string routes = "";
            ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
            APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
            clientInfoHeader.AppID = "Query Example";
            String queryString = "SELECT FromAirport.LookupName,ArrivalAirport.LookupName,ToAirport.LookupName FROM CO.Itinerary WHERE Incident1 = " + IncidentID + " ORDER BY Incident1";
            clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 10, "/", false, false, out CSVTableSet queryCSV, out byte[] FileData);
            foreach (CSVTable table in queryCSV.CSVTables)
            {
                String[] rowData = table.Rows;
                foreach (String data in rowData)
                {
                    routes += data + ",";
                }
            }

            return routes;
        }
        public string GetClientType()
        {
            try
            {
                string ClientType = "Nacional";
                if (IncidentID != 0)
                {
                    ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                    APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                    clientInfoHeader.AppID = "Query Example";
                    String queryString = "SELECT CustomFields.c.rfcerp FROM Incident WHERE ID =" + IncidentID + "";
                    clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "/", false, false, out CSVTableSet queryCSV, out byte[] FileData);
                    foreach (CSVTable table in queryCSV.CSVTables)
                    {
                        String[] rowData = table.Rows;
                        foreach (String data in rowData)
                        {
                            ClientType = data;
                        }
                    }
                }
                if (ClientType == "XEXX010101000")
                {
                    ClientType = "Internacional";
                }

                return ClientType;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetType: " + ex.InnerException.ToString());
                return "";
            }
        }
        public string GetSRType()
        {
            try
            {
                string SRTYPE = "";
                if (IncidentID != 0)
                {
                    ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                    APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                    clientInfoHeader.AppID = "Query Example";
                    String queryString = "SELECT I.Customfields.c.sr_type.LookupName FROM Incident I WHERE id=" + IncidentID + "";
                    clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
                    foreach (CSVTable table in queryCSV.CSVTables)
                    {
                        String[] rowData = table.Rows;
                        foreach (String data in rowData)
                        {
                            SRTYPE = data;
                        }
                    }
                }
                switch (SRTYPE)
                {
                    case "Catering":
                        SRTYPE = "CATERING";
                        break;
                    case "FCC":
                        SRTYPE = "FCC";
                        break;
                    case "FBO":
                        SRTYPE = "FBO";
                        break;
                    case "Fuel":
                        SRTYPE = "FUEL";
                        break;
                    case "Hangar Space":
                        SRTYPE = "GYCUSTODIA";
                        break;
                    case "SENEAM Fee":
                        SRTYPE = "SENEAM";
                        break;
                    case "Permits":
                        SRTYPE = "PERMISOS";
                        break;
                }
                return SRTYPE;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetType: " + ex.Message + "Detail: " + ex.StackTrace);
                return "";
            }
        }
        public string GetCargoGroup(string strIcao)
        {
            string cGroup = "";
            ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
            APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
            clientInfoHeader.AppID = "Query Example";
            String queryString = "SELECT CargoGroup.LookupName FROM CO.AircraftType WHERE ICAODesignator = '" + strIcao + "'";
            clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
            foreach (CSVTable table in queryCSV.CSVTables)
            {
                String[] rowData = table.Rows;
                foreach (String data in rowData)
                {
                    cGroup = data;
                }
            }
            return cGroup;
        }
        public string getICAODesi()
        {
            string Icao = "";
            ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
            APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
            clientInfoHeader.AppID = "Query Example";
            String queryString = "SELECT CustomFields.co.Aircraft.AircraftType1.ICAODesignator  FROM Incident WHERE ID =" + IncidentID;
            clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
            foreach (CSVTable table in queryCSV.CSVTables)
            {
                String[] rowData = table.Rows;
                foreach (String data in rowData)
                {
                    Icao = data;
                }
            }
            return Icao;
        }
        public string GetTail()
        {
            string tail = "";
            ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
            APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
            clientInfoHeader.AppID = "Query Example";
            String queryString = "SELECT CustomFields.CO.Aircraft.Tail FROM Incident WHERE ID = " + IncidentID;
            clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 10000, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
            foreach (CSVTable table in queryCSV.CSVTables)
            {
                String[] rowData = table.Rows;
                foreach (String data in rowData)
                {
                    tail = data;
                }
            }
            return tail;
        }
        public string GetArrival()
        {
            string arrivalto = "";
            ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
            APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
            clientInfoHeader.AppID = "Query Example";
            String queryString = "SELECT CustomFields.co.Airports.LookupName FROM Incident WHERE ID = " + IncidentID;
            clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 10000, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
            foreach (CSVTable table in queryCSV.CSVTables)
            {
                String[] rowData = table.Rows;
                foreach (String data in rowData)
                {
                    arrivalto = data;
                }
            }
            return arrivalto;
        }
        public string GetStatus()
        {
            string status = "";
            ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
            APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
            clientInfoHeader.AppID = "Query Example";
            String queryString = "SELECT StatusWithType.Status.Name  FROM Incident WHERE ID = " + IncidentID;
            clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
            foreach (CSVTable table in queryCSV.CSVTables)
            {
                String[] rowData = table.Rows;
                foreach (String data in rowData)
                {
                    status = data.ToUpper();
                }
            }
            return status;
        }
        public string GetDeparture()
        {
            string arrivalto = "";
            ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
            APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
            clientInfoHeader.AppID = "Query Example";
            String queryString = "SELECT CustomFields.co.Airports1.LookupName FROM Incident WHERE ID = " + IncidentID;
            clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
            foreach (CSVTable table in queryCSV.CSVTables)
            {
                String[] rowData = table.Rows;
                foreach (String data in rowData)
                {
                    arrivalto = data;
                }
            }
            return arrivalto;
        }
        public string GetFuelType()
        {
            try
            {
                string Type = "";
                ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                clientInfoHeader.AppID = "Query Example";
                String queryString = "SELECT CustomFields.co.Aircraft.AircraftType1.FuelType.Name  FROM Incident WHERE ID =" + IncidentID;
                clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
                foreach (CSVTable table in queryCSV.CSVTables)
                {
                    String[] rowData = table.Rows;
                    foreach (String data in rowData)
                    {
                        Type = data;
                    }
                }
                return Type;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return "0";

            }
        }
        public string GetReferenceNumber()
        {
            try
            {
                string Reference = "";
                ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                clientInfoHeader.AppID = "Query Example";
                String queryString = "SELECT ReferenceNumber FROM Incident  WHERE ID =  " + IncidentID;
                clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
                foreach (CSVTable table in queryCSV.CSVTables)
                {
                    String[] rowData = table.Rows;
                    foreach (String data in rowData)
                    {
                        Reference = data;
                    }
                }
                return Reference;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return "0";

            }
        }
        public string GetSrCurrency()
        {
            try
            {
                string Currency = "";
                ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                clientInfoHeader.AppID = "Query Example";
                String queryString = "SELECT Customfields.c.sr_currency.name FROM Incident WHERE ID =" + IncidentID;
                clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
                foreach (CSVTable table in queryCSV.CSVTables)
                {
                    String[] rowData = table.Rows;
                    foreach (String data in rowData)
                    {
                        Currency = data;
                    }
                }
                return Currency;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return "";

            }
        }
        public bool Init()
        {
            try
            {
                bool result = false;
                EndpointAddress endPointAddr = new EndpointAddress(global.GetInterfaceServiceUrl(ConnectServiceType.Soap));
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
                global.PrepareConnectSession(clientORN.ChannelFactory);
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
        public List<Services> GetListServices()
        {
            try
            {
                List<Services> services = new List<Services>();
                ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                clientInfoHeader.AppID = "Query Example";
                String queryString = "SELECT  ID,ItemNumber,ItemDescription,IDProveedor,Costo,CuentaGasto,Precio,InternalInvoice,ERPInvoice,Fuel_Id,Iva,Site,Facturado,Itinerary,Aircraft.LookupName FROM CO.Services WHERE Informativo = '0' AND (Componente IS NULL OR Componente  = '0') AND Incident = " + IncidentID;
                global.LogMessage(queryString);
                clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 10000, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
                foreach (CSVTable table in queryCSV.CSVTables)
                {
                    String[] rowData = table.Rows;
                    foreach (String data in rowData)
                    {
                        Services service = new Services();
                        Char delimiter = '|';
                        string[] substrings = data.Split(delimiter);
                        service.ServiceID = substrings[0];
                        service.ItemNumber = substrings[1];
                        service.Description = substrings[2];
                        service.SupplierID = substrings[3];
                        service.Cost = String.IsNullOrEmpty(substrings[4]) ? "0" : (Math.Round(Convert.ToDouble(substrings[4]), 4)).ToString();
                        service.CuentaGasto = substrings[5];
                        service.Precio = String.IsNullOrEmpty(substrings[6]) ? "0" : (Math.Round(Convert.ToDouble(substrings[6]), 4)).ToString();
                        service.InvoiceNumber = substrings[7];
                        service.ERPInvoice = substrings[8];
                        service.FuelId = substrings[9];
                        service.Tax = substrings[10];
                        service.Site = substrings[11];
                        service.Facturado = substrings[12] == "1" ? "1" : "0";
                        service.Itinerary = substrings[13];
                        service.Aircraft = substrings[14];
                        services.Add(service);
                    }
                }
                return services;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en GetServices: " + ex.Message);
                global.LogMessage(ex.Message);
                return null;
            }
        }
        private double getExchangeRate(DateTime date)
        {
            try
            {
                double rate = 1;
                string envelope = "<soap:Envelope " +
                "	xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\"" +
     "	xmlns:pub=\"http://xmlns.oracle.com/oxp/service/PublicReportService\">" +
       "<soap:Header/>" +
     "	<soap:Body>" +
     "		<pub:runReport>" +
     "			<pub:reportRequest>" +
     "			<pub:attributeFormat>xml</pub:attributeFormat>" +
     "				<pub:attributeLocale>en</pub:attributeLocale>" +
     "				<pub:attributeTemplate>default</pub:attributeTemplate>" +

                 "<pub:parameterNameValues>" +
                      "<pub:item>" +
                   "<pub:name>P_EXCHANGE_DATE</pub:name>" +
                   "<pub:values>" +
                      "<pub:item>" + date.ToString("yyyy-MM-dd") + "</pub:item>" +
                   "</pub:values>" +
                "</pub:item>" +
                 "</pub:parameterNameValues>" +

     "				<pub:reportAbsolutePath>Custom/Integracion/XX_DAILY_RATES_REP.xdo</pub:reportAbsolutePath>" +
     "				<pub:sizeOfDataChunkDownload>-1</pub:sizeOfDataChunkDownload>" +
     "			</pub:reportRequest>" +
     "		</pub:runReport>" +
     "	</soap:Body>" +
     "</soap:Envelope>";
                global.LogMessage(envelope);
                byte[] byteArray = Encoding.UTF8.GetBytes(envelope);
                // Construct the base 64 encoded string used as credentials for the service call
                byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes("itotal" + ":" + "Oracle123");
                string credentials = Convert.ToBase64String(toEncodeAsBytes);
                // Create HttpWebRequest connection to the service
                HttpWebRequest request =
                 (HttpWebRequest)WebRequest.Create("https://egqy-test.fa.us6.oraclecloud.com:443/xmlpserver/services/ExternalReportWSSService");
                // Configure the request content type to be xml, HTTP method to be POST, and set the content length
                request.Method = "POST";

                request.ContentType = "application/soap+xml; charset=UTF-8;action=\"\"";
                request.ContentLength = byteArray.Length;
                // Configure the request to use basic authentication, with base64 encoded user name and password, to invoke the service.
                request.Headers.Add("Authorization", "Basic " + credentials);
                // Set the SOAP action to be invoked; while the call works without this, the value is expected to be set based as per standards
                //request.Headers.Add("SOAPAction", "http://xmlns.oracle.com/apps/cdm/foundation/parties/organizationService/applicationModule/findOrganizationProfile");
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
                        nms.AddNamespace("env", "http://schemas.xmlsoap.org/soap/envelope/");
                        nms.AddNamespace("ns2", "http://xmlns.oracle.com/oxp/service/PublicReportService");

                        XmlNode desiredNode = xmlDoc.SelectSingleNode("//ns2:runReportReturn", nms);
                        if (desiredNode.HasChildNodes)
                        {
                            for (int i = 0; i < desiredNode.ChildNodes.Count; i++)
                            {
                                if (desiredNode.ChildNodes[i].LocalName == "reportBytes")
                                {
                                    byte[] data = Convert.FromBase64String(desiredNode.ChildNodes[i].InnerText);
                                    string decodedString = Encoding.UTF8.GetString(data);
                                    XmlTextReader reader = new XmlTextReader(new System.IO.StringReader(decodedString));
                                    reader.Read();
                                    XmlSerializer serializer = new XmlSerializer(typeof(DATA_DS_RATES));
                                    DATA_DS_RATES res = (DATA_DS_RATES)serializer.Deserialize(reader);
                                    if (res.G_N_RATES != null)
                                    {
                                        rate = Convert.ToDouble(res.G_N_RATES.G_1_RATES.CONVERSION_RATE);
                                    }
                                }
                            }
                        }
                    }
                }

                return rate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return 1;
            }
        }

    }

    [AddIn("Generate Invoice", Version = "1.0.0.0")]
    public class WorkspaceRibbonButtonFactory : IWorkspaceRibbonButtonFactory
    {

        IGlobalContext globalContext { get; set; }
        public IWorkspaceRibbonButton CreateControl(bool inDesignMode, IRecordContext RecordContext)
        {
            return new WorkspaceRibbonAddIn(inDesignMode, RecordContext, globalContext);
        }
        public System.Drawing.Image Image32
        {
            get { return Properties.Resources.receipt32; }
        }


        public System.Drawing.Image Image16
        {
            get { return Properties.Resources.receipt16; }
        }

        public string Text
        {
            get { return "Generate Invoice"; }
        }

        public string Tooltip
        {
            get { return "Generate Invoice"; }
        }

        public bool Initialize(IGlobalContext GlobalContext)
        {
            this.globalContext = GlobalContext;
            return true;
        }

    }

    //RATES
    [XmlRoot(ElementName = "G_1_RATES")]
    public class G_1_RATES
    {
        [XmlElement(ElementName = "CONVERSION_RATE")]
        public string CONVERSION_RATE { get; set; }
        [XmlElement(ElementName = "CONVERSION_DATE")]
        public string CONVERSION_DATE { get; set; }
    }

    [XmlRoot(ElementName = "G_N_RATES")]
    public class G_N_RATES
    {
        [XmlElement(ElementName = "USER_CONVERSION_TYPE")]
        public string USER_CONVERSION_TYPE { get; set; }
        [XmlElement(ElementName = "G_1_RATES")]
        public G_1_RATES G_1_RATES { get; set; }
    }

    [XmlRoot(ElementName = "DATA_DS_RATES")]
    public class DATA_DS_RATES
    {
        [XmlElement(ElementName = "P_EXCHANGE_DATE")]
        public string P_EXCHANGE_DATE { get; set; }
        [XmlElement(ElementName = "G_N_RATES")]
        public G_N_RATES G_N_RATES { get; set; }
    }

}