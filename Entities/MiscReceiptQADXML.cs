using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FWMSC21Core_BusinessEntities.Entities
{
    public class MiscReceiptQADXML
    {
        private const string i = "urn:schemas-qad-com:xml-services";
        private const string d = "urn:schemas-qad-com:xml-services:common";
        private const string c = "http://www.w3.org/2005/08/addressing";
        private const string v = "http://schemas.xmlsoap.org/soap/envelope/";
        private const string t = "http://tasks.ws.com/";

        //  [XmlAttribute(AttributeName = "id")]
        // public string Id { get; set; }
        // [XmlAttribute(AttributeName = "root", Namespace = c)]
        // public int Root { get; set; }

        //[XmlElement(ElementName = "firm", Namespace = d)]
        //public string Firm { get; set; }

        //[XmlElement(ElementName = "login", Namespace = d)]
        //public string Login { get; set; }

        //[XmlElement(ElementName = "password", Namespace = d)]
        //public string Password { get; set; }

        //[XmlElement(ElementName = "device_id", Namespace = d)]
        //public string DeviceId { get; set; }
        //[XmlElement(ElementName = "receiveInventory")]
        //public receiveInventory ReceiveInventory { get; set; }
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
        public class receiveInventory
        {
            [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
            public DsSessionContext DsSessionContext { get; set; }
            [XmlElement(ElementName = "dsInventoryReceipt")]
            public DsInventoryReceipt DsInventoryReceipt { get; set; }

        }
        public class DsSessionContext
        {
            [XmlElement(ElementName = "ttContext", Namespace = d)]
            public List<TTContext> lstTTContext { get; set; }
        }
        public class DsInventoryReceipt
        {
            [XmlElement(ElementName = "inventoryReceipt")]
            public InventoryReceipt InventoryReceipt { get; set; }
        }

        public class InventoryReceipt
        {
            // [XmlElement(ElementName = "operation", Namespace = d)]
            [XmlElement(ElementName = "operation")]
            public string Operation { get; set; }
            [XmlElement(ElementName = "ptPart")]
            public string PtPart { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public string LotserialQty { get; set; }
            [XmlElement(ElementName = "um")]
            public string Um { get; set; }
            [XmlElement(ElementName = "conv")]
            public string Conv { get; set; }
            [XmlElement(ElementName = "site")]
            public string Site { get; set; }
            [XmlElement(ElementName = "location")]
            public string Location { get; set; }
            [XmlElement(ElementName = "lotserial")]
            public string Lotserial { get; set; }
            [XmlElement(ElementName = "lotref")]
            public string Lotref { get; set; }
            [XmlElement(ElementName = "multiEntry")]
            public string MultiEntry { get; set; }
            [XmlElement(ElementName = "ordernbr")]
            public string Ordernbr { get; set; }
            [XmlElement(ElementName = "orderline")]
            public string Orderline { get; set; }
            [XmlElement(ElementName = "soJob")]
            public string SoJob { get; set; }
            [XmlElement(ElementName = "addr")]
            public string Addr { get; set; }
            [XmlElement(ElementName = "rmks")]
            public string Rmks { get; set; }
            [XmlElement(ElementName = "effDate")]
            public string EffDate { get; set; }
            [XmlElement(ElementName = "crAcct")]
            public string CrAcct { get; set; }
            [XmlElement(ElementName = "crSub")]
            public string CrSub { get; set; }
            [XmlElement(ElementName = "crCc")]
            public string CrCc { get; set; }
            [XmlElement(ElementName = "crProj")]
            public string CrProj { get; set; }
            [XmlElement(ElementName = "absid")]
            public string Absid { get; set; }
            [XmlElement(ElementName = "shipdate")]
            public string Shipdate { get; set; }
            [XmlElement(ElementName = "invMov")]
            public string InvMov { get; set; }
            [XmlElement(ElementName = "yn")]
            public string Yn { get; set; }
            [XmlElement(ElementName = "yn1")]
            public string Yn1 { get; set; }
            [XmlElement(ElementName = "receiptDetail")]
            public ReceiptDetail ReceiptDetail { get; set; }
        }
        public class ReceiptDetail
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "site")]
            public string Site { get; set; }
            [XmlElement(ElementName = "location")]
            public string Location { get; set; }
            [XmlElement(ElementName = "lotserial")]
            public string Lotserial { get; set; }
            [XmlElement(ElementName = "lotref")]
            public string Lotref { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public string LotserialQty { get; set; }
            [XmlElement(ElementName = "serialsYn")]
            public string SerialsYn { get; set; }
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


       

        [XmlType(Namespace = d)]
        public class Body
        {
            //[XmlElement(ElementName = "login", Namespace = t)]
            //public LoginRequest LoginRequest { get; set; }
            [XmlElement(ElementName = "receiveInventory", Namespace = "")]
            public receiveInventory ReceiveInventory { get; set; }
        }
    }
    public class LoginRequest
    {

    }
}
