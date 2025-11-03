using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simpolo_Endpoint.Models
{
    public class InventoryStatusUpdateXML
    {
        private const string i = "urn:schemas-qad-com:xml-services";
        private const string d = "urn:schemas-qad-com:xml-services:common";
        private const string c = "http://www.w3.org/2005/08/addressing";
        private const string v = "http://schemas.xmlsoap.org/soap/envelope/";
        private const string t = "http://tasks.ws.com/";

        [XmlRoot(Namespace = v)]
        public class Envelope
        {
            public Header Header { get; set; }
            public Body Body { get; set; }
            static Envelope()
            {
                staticxmlns = new XmlSerializerNamespaces();
                //staticxmlns.Add("i", i);
                //staticxmlns.Add("d", d);
                //staticxmlns.Add("c", c);
                //staticxmlns.Add("soapenv", v);
                staticxmlns.Add("", "urn:schemas-qad-com:xml-services");
                staticxmlns.Add("qcom", "urn:schemas-qad-com:xml-services:common");
                staticxmlns.Add("wsa", "http://www.w3.org/2005/08/addressing");
                staticxmlns.Add("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            }
            private static XmlSerializerNamespaces staticxmlns;
            [XmlNamespaceDeclarations]
            public XmlSerializerNamespaces xmlns { get { return staticxmlns; } set { } }
        }
        [XmlType(Namespace = c)]
        public class Header
        {
            [XmlElement(ElementName = "Action", Namespace = c)]
            public string Action { get; set; }
            [XmlElement(ElementName = "To", Namespace = c)]
            public string To { get; set; }
            [XmlElement(ElementName = "MessageID", Namespace = c)]
            public string MessageID { get; set; }
            public ReferenceParameters ReferenceParameters { get; set; }
            public ReplyTo ReplyTo { get; set; }
        }
        public class ReferenceParameters
        {
            [XmlElement(ElementName = "suppressResponseDetail", Namespace = d)]
            public bool suppressResponseDetail { get; set; }
        }
        public class ReplyTo
        {
            [XmlElement(ElementName = "Address", Namespace = c)]
            public string Address { get; set; }
        }
        [XmlType(Namespace = d)]
        public class Body
        {
            [XmlElement(ElementName = "transferInventory", Namespace = "")]
            public TransferInventory transferInventory { get; set; }

        }
        public class TransferInventory
        {
            [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
            public DsSessionContext dsSessionContext { get; set; }
            [XmlElement(ElementName = "dsInventoryTransfer")]
            public DsInventoryTransfer dsInventoryTransfer { get; set; }
        }
        public class DsSessionContext
        {
            [XmlElement(ElementName = "ttContext", Namespace = d)]
            public List<TTContext> lstTTContext { get; set; }
        }
        public class TTContext
        {
            [XmlElement(ElementName = "propertyQualifier", Namespace = d)]
            public string PropertyQualifier { get; set; }
            [XmlElement(ElementName = "propertyName", Namespace = d)]
            public string PropertyName { get; set; }
            [XmlElement(ElementName = "propertyValue", Namespace = d)]
            public string PropertyValue { get; set; }
        }
        public class DsInventoryTransfer
        {
            [XmlElement(ElementName = "inventoryTransfer")]
            public List<InventoryTransfer> inventoryTransfer { get; set; }
        }
        public class InventoryTransfer
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "part")]
            public string part { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public string lotserialQty { get; set; }
            [XmlElement(ElementName = "effDate")]
            public string effDate { get; set; }
            [XmlElement(ElementName = "inventoryDetail")]
            public List<InventoryDetail> inventoryDetail { get; set; }

        }
        public class InventoryDetail
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public decimal lotserialQty { get; set; }
            [XmlElement(ElementName = "effDate")]
            public string effDate { get; set; }
            [XmlElement(ElementName = "nbr")]
            public string nbr { get; set; }
            [XmlElement(ElementName = "soJob")]
            public string soJob { get; set; }
            [XmlElement(ElementName = "rmks")]
            public string rmks { get; set; }
            [XmlElement(ElementName = "siteFrom")]
            public string siteFrom { get; set; }
            [XmlElement(ElementName = "locFrom")]
            public string locFrom { get; set; }
            [XmlElement(ElementName = "lotserFrom")]
            public string lotserFrom { get; set; }
            [XmlElement(ElementName = "lotrefFrom")]
            public string lotrefFrom { get; set; }
            [XmlElement(ElementName = "siteTo")]
            public string siteTo { get; set; }
            [XmlElement(ElementName = "locTo")]
            public string locTo { get; set; }
            [XmlElement(ElementName = "lotserTo")]
            public string lotserTo { get; set; }
            [XmlElement(ElementName = "lotrefTo")]
            public string lotrefTo { get; set; }
            [XmlElement(ElementName = "yn")]
            public string yn { get; set; }
        }
    }
}
