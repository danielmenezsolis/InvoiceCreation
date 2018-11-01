using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceCreation
{
    public class Services
    {
        public string ServiceID { get; set; }
        public string ItemNumber { get; set; }
        public string Description { get; set; }
        public string SupplierID { get; set; }
        public string Cost { get; set; }
        public string CuentaGasto { get; set; }
        public string Precio { get; set; }
        public string InternalInvoice { get; set; }
        public string ERPInvoice { get; set; }
    }
}
