using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FWMSC21Core_BusinessEntities.Entities
{
    public class SalesOrderPGI
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
            [XmlElement(ElementName = "confirmShipper", Namespace = "")]
            public ConfirmShipper ConfirmShipper { get; set; }
        }
        public class ConfirmShipper
        {
            [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
            public DsSessionContext DsSessionContext { get; set; }
            [XmlElement(ElementName = "dsShipperConfirm")]
            public DsShipperConfirm DsShipperConfirm { get; set; }
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
        public class DsShipperConfirm
        {
            [XmlElement(ElementName = "shipperConfirm")]
            public ShipperConfirm ShipperConfirm { get; set; }
        }
        public class ShipperConfirm
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "absShipfrom")]
            public string absShipfrom { get; set; }
            [XmlElement(ElementName = "confType")]
            public string confType { get; set; }
            [XmlElement(ElementName = "absId")]
            public string absId { get; set; }
            [XmlElement(ElementName = "shipDt")]
            public string shipDt { get; set; }
            [XmlElement(ElementName = "effDate")]
            public string effDate { get; set; }
            [XmlElement(ElementName = "absVehRef")]
            public string absVehRef { get; set; }
            [XmlElement(ElementName = "shpTime")]
            public string shpTime { get; set; }
            [XmlElement(ElementName = "arrDate")]
            public string arrDate { get; set; }
            [XmlElement(ElementName = "arrTime")]
            public string arrTime { get; set; }
            [XmlElement(ElementName = "autoInv")]
            public string autoInv { get; set; }
            [XmlElement(ElementName = "autoPost")]
            public string autoPost { get; set; }
            [XmlElement(ElementName = "useShipper")]
            public string useShipper { get; set; }
            [XmlElement(ElementName = "consolidate")]
            public string consolidate { get; set; }
            [XmlElement(ElementName = "lCalcFreight")]
            public string lCalcFreight { get; set; }
            [XmlElement(ElementName = "sEffError")]
            public string sEffError { get; set; }
            [XmlElement(ElementName = "ivdate")]
            public string ivdate { get; set; }
            [XmlElement(ElementName = "yn")]
            public string yn { get; set; }
            [XmlElement(ElementName = "invOnly")]
            public string invOnly { get; set; }
            [XmlElement(ElementName = "printLotserials")]
            public string printLotserials { get; set; }
            [XmlElement(ElementName = "printOptions")]
            public string printOptions { get; set; }
            [XmlElement(ElementName = "compAddr")]
            public string compAddr { get; set; }
            [XmlElement(ElementName = "formCode")]
            public string formCode { get; set; }
            [XmlElement(ElementName = "discDet")]
            public string discDet { get; set; }
            [XmlElement(ElementName = "discSum")]
            public string discSum { get; set; }
            [XmlElement(ElementName = "msg")]
            public string msg { get; set; }
            [XmlElement(ElementName = "dev")]
            public string dev { get; set; }
            [XmlElement(ElementName = "yn1")]
            public string yn1 { get; set; }
         

        }
    }
}
