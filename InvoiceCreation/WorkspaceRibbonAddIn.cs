using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows.Forms;
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
        public List<Services> servicios { get; set; }
        public IIncident Incident { get; set; }
        public string Nombre { get; set; }
        public string RFC { get; set; }
        public string AccountNomber { get; set; }
        public string CatRoyalty { get; set; }
        public string CatUtilidad { get; set; }
        public string CatCombust { get; set; }
        public int IncidentID { get; set; }
        public string SrType { get; set; }
        public string ICAO { get; set; }
        public string AircraftCategory { get; set; }
        public string ClientType { get; set; }
        public string FuelType { get; set; }

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
                            AccountNomber = val.ValStr;
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
                    }

                    ICAO = getICAODesi(IncidentID);
                    SrType = GetSRType();
                    AircraftCategory = GetCargoGroup(ICAO);
                    ClientType = GetClientType();
                    FuelType = GetFuelType(IncidentID);



                    servicios = GetListServices();


                    invoice = new Invoice();
                    DgvServicios = ((DataGridView)invoice.Controls["dataGridServicios"]);
                    DgvServicios.DataSource = servicios.OrderBy(o => o.InternalInvoice).ToList();
                    //DgvServicios.Columns[2].Visible = false;
                    DgvServicios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    ((TextBox)invoice.Controls["txtIncidentID"]).Text = IncidentID.ToString();
                    ((TextBox)invoice.Controls["txtCustomerName"]).Text = Nombre;
                    ((TextBox)invoice.Controls["txtRFC"]).Text = RFC;
                    ((TextBox)invoice.Controls["txtAccount"]).Text = AccountNomber;
                    ((TextBox)invoice.Controls["txtRoyalty"]).Text = CatRoyalty;
                    ((TextBox)invoice.Controls["txtUtilidad"]).Text = CatUtilidad;
                    ((TextBox)invoice.Controls["txtCombustible"]).Text = CatCombust;
                    invoice.ShowDialog();

                    //MessageBox.Show("Clicked");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en Click: " + ex.Message + "Det: " + ex.StackTrace);
            }
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
                    clientORN.QueryCSV(clientInfoHeader, aPIAccessRequest, queryString, 1, "|", false, false, out CSVTableSet queryCSV, out byte[] FileData);
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
        public string getICAODesi(int Incident)
        {
            string Icao = "";
            ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
            APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
            clientInfoHeader.AppID = "Query Example";
            String queryString = "SELECT CustomFields.co.Aircraft.AircraftType1.ICAODesignator  FROM Incident WHERE ID =" + Incident;
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
        public string GetFuelType(int Incident)
        {
            try
            {
                string Type = "";
                ClientInfoHeader clientInfoHeader = new ClientInfoHeader();
                APIAccessRequestHeader aPIAccessRequest = new APIAccessRequestHeader();
                clientInfoHeader.AppID = "Query Example";
                String queryString = "SELECT CustomFields.co.Aircraft.AircraftType1.FuelType.Name  FROM Incident WHERE ID =" + Incident;
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
                String queryString = "SELECT  ID,ItemNumber,ItemDescription,IDProveedor,Costo,CuentaGasto,Precio,InternalInvoice,ERPInvoice FROM CO.Services WHERE Incident = " + IncidentID;
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
                        service.Description = substrings[2].Replace('"', ' ').Trim();
                        service.SupplierID = substrings[3].Replace('"', ' ').Trim();
                        service.Cost = substrings[4];
                        service.CuentaGasto = substrings[5];
                        service.Precio = substrings[6];
                        service.InternalInvoice = substrings[7];
                        service.ERPInvoice = substrings[8];
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
    }

    [AddIn("Create Invoice", Version = "1.0.0.0")]
    public class WorkspaceRibbonButtonFactory : IWorkspaceRibbonButtonFactory
    {

        IGlobalContext globalContext { get; set; }
        public IWorkspaceRibbonButton CreateControl(bool inDesignMode, IRecordContext RecordContext)
        {
            return new WorkspaceRibbonAddIn(inDesignMode, RecordContext, globalContext);
        }

        /// <summary>
        /// The 32x32 pixel icon displayed in the Workspace Ribbon.
        /// </summary>
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
            get { return "Invoice Creation"; }
        }

        public string Tooltip
        {
            get { return "Invoice Creation"; }
        }

        public bool Initialize(IGlobalContext GlobalContext)
        {
            this.globalContext = GlobalContext;
            return true;
        }

    }
}