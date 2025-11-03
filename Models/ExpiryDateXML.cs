using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simpolo_Endpoint.Models
{
    public class ExpiryDateXML
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
            [XmlElement(ElementName = "maintainInventoryDetail", Namespace = "")]
            public MaintainInventoryDetail maintainInventoryDetail { get; set; }

        }
        public class MaintainInventoryDetail
        {
            [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
            public DsSessionContext dsSessionContext { get; set; }
            [XmlElement(ElementName = "dsInventoryDetail")]
            public DsInventoryDetail dsInventoryDetail { get; set; }
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
        public class DsInventoryDetail
        {
            [XmlElement(ElementName = "inventoryDetail")]
            public List<InventoryDetail> inventoryDetail { get; set; }
        }
        public class InventoryDetail
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "ldSite")]
            public string ldSite { get; set; }
            [XmlElement(ElementName = "ldLoc")]
            public string ldLoc { get; set; }
            [XmlElement(ElementName = "ldPart")]
            public string ldPart { get; set; }
            [XmlElement(ElementName = "ldLot")]
            public string ldLot { get; set; }
            [XmlElement(ElementName = "ldRef")]
            public string ldRef { get; set; }
            [XmlElement(ElementName = "ldExpire")]
            public string ldExpire { get; set; }
            [XmlElement(ElementName = "ldGrade")]
            public string ldGrade { get; set; }
            [XmlElement(ElementName = "ldAssay")]
            public string ldAssay { get; set; }
            [XmlElement(ElementName = "ldStatus")]
            public string ldStatus { get; set; }
        }
    }
}
