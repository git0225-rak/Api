using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FWMSC21Core_BusinessEntities.Entities
{
    public class WorkOrderGRNXML
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
            [XmlElement(ElementName = "receiveWorkOrder", Namespace = "")]
            public ReceiveworkOrder receiveWorkOrder { get; set; }
        }
        public class ReceiveworkOrder
        {
            [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
            public DsSessionContext dsSessionContext { get; set; }
            [XmlElement(ElementName = "dsWorkOrderReceipt")]
            public DsWorkOrderReceipt dsWorkOrderReceipt { get; set; }
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
        public class DsWorkOrderReceipt
        {
            [XmlElement(ElementName = "workOrderReceipt")]
            public WorkOrderReceipt workOrderReceipt { get; set; }
        }
        public class WorkOrderReceipt
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "woNbr")]
            public string woNbr { get; set; }
            [XmlElement(ElementName = "woLot")]
            public string woLot { get; set; }
            [XmlElement(ElementName = "currWkctr")]
            public string currWkctr { get; set; }
            [XmlElement(ElementName = "currMch")]
            public string currMch { get; set; }
            [XmlElement(ElementName = "yn")]
            public string yn { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public string lotserialQty { get; set; }
            [XmlElement(ElementName = "um")]
            public string um { get; set; }
            [XmlElement(ElementName = "conv")]
            public string conv { get; set; }
            [XmlElement(ElementName = "rejectQty")]
            public string rejectQty { get; set; }
            [XmlElement(ElementName = "rejectUm")]
            public string rejectUm { get; set; }
            [XmlElement(ElementName = "rejectConv")]
            public string rejectConv { get; set; }
            [XmlElement(ElementName = "site")]
            public string site { get; set; }
            [XmlElement(ElementName = "location")]
            public string location { get; set; }
            [XmlElement(ElementName = "lotserial")]
            public string lotserial { get; set; }
            [XmlElement(ElementName = "lotref")]
            public string lotref { get; set; }
            [XmlElement(ElementName = "multiEntry")]
            public string multiEntry { get; set; }
            [XmlElement(ElementName = "chgAttr")]
            public string chgAttr { get; set; }
            [XmlElement(ElementName = "chgAssay")]
            public string chgAssay { get; set; }
            [XmlElement(ElementName = "assayActv")]
            public string assayActv { get; set; }
            [XmlElement(ElementName = "chgGrade")]
            public string chgGrade { get; set; }
            [XmlElement(ElementName = "gradeActv")]
            public string gradeActv { get; set; }
            [XmlElement(ElementName = "chgExpire")]
            public string chgExpire { get; set; }
            [XmlElement(ElementName = "expireActv")]
            public string expireActv { get; set; }
            [XmlElement(ElementName = "chgStatus")]
            public string chgStatus { get; set; }
            [XmlElement(ElementName = "statusActv")]
            public string statusActv { get; set; }
            [XmlElement(ElementName = "resetattr")]
            public string resetattr { get; set; }
            [XmlElement(ElementName = "rmks")]
            public string rmks { get; set; }
            [XmlElement(ElementName = "effDate")]
            public string effDate { get; set; }
            [XmlElement(ElementName = "closeWo")]
            public string closeWo { get; set; }
            [XmlElement(ElementName = "yn1")]
            public string yn1 { get; set; }
            [XmlElement(ElementName = "yn2")]
            public string yn2 { get; set; }
            [XmlElement(ElementName = "yn3")]
            public string yn3 { get; set; }
            [XmlElement(ElementName = "lotSerialDetail")]
            public LotSerialDetail lotSerialDetail { get; set; }
            [XmlElement(ElementName = "receiptDetail")]
            public ReceiptDetail receiptDetail { get; set; }
        }
        public class LotSerialDetail
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "bowlLotser")]
            public string bowlLotser { get; set; }
            [XmlElement(ElementName = "bowlRef")]
            public string bowlRef { get; set; }
            [XmlElement(ElementName = "bowlQty")]
            public string bowlQty { get; set; }
            
        }
        public class ReceiptDetail
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "site")]
            public string site { get; set; }
            [XmlElement(ElementName = "location")]
            public string location { get; set; }
            [XmlElement(ElementName = "lotserial")]
            public string lotserial { get; set; }
            [XmlElement(ElementName = "lotref")]
            public string lotref { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public string lotserialQty { get; set; }
        }
  
         

    }
}
