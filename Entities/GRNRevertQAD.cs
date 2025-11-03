using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FWMSC21Core_BusinessEntities.Entities
{
    public class GRNRevertQAD
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
            [XmlElement(ElementName = "returnPurchaseOrder", Namespace = "")]
            public ReturnPurchaseOrder returnPurchaseOrder { get; set; }
        }
        public class ReturnPurchaseOrder
        {
            [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
            public DsSessionContext dsSessionContext { get; set; }
            [XmlElement(ElementName = "dsPurchaseOrderReturn")]
            public DsPurchaseOrderReturn dsPurchaseOrderReturn { get; set; }
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
        public class DsPurchaseOrderReturn
        {
            [XmlElement(ElementName = "purchaseOrderReturn")]
            public PurchaseOrderReturn purchaseOrderReturn { get; set; }
        }
        public class PurchaseOrderReturn
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "poNbr")]
            public string poNbr { get; set; }
            [XmlElement(ElementName = "yn")]
            public string yn { get; set; }
            [XmlElement(ElementName = "receivernbr")]
            public string receivernbr { get; set; }
            [XmlElement(ElementName = "vShipfrom")]
            public string vShipfrom { get; set; }
            [XmlElement(ElementName = "vShipto")]
            public string vShipto { get; set; }
            [XmlElement(ElementName = "effDate")]
            public string effDate { get; set; }
            [XmlElement(ElementName = "fillAll")]
            public string fillAll { get; set; }
            [XmlElement(ElementName = "replace")]
            public string replace { get; set; }
            [XmlElement(ElementName = "cmmtYn")]
            public string cmmtYn { get; set; }
            [XmlElement(ElementName = "move")]
            public string move { get; set; }
            [XmlElement(ElementName = "vRate")]
            public string vRate { get; set; }
            [XmlElement(ElementName = "vRate2")]
            public string vRate2 { get; set; }
            [XmlElement(ElementName = "yn1")]
            public string yn1 { get; set; }
            [XmlElement(ElementName = "yn2")]
            public string yn2 { get; set; }
            [XmlElement(ElementName = "purchaseOrderReturnTransComment")]
            public PurchaseOrderReturnTransComment purchaseOrderReturnTransComment { get; set; }
            [XmlElement(ElementName = "lineDetail")]
            public List<LineDetail> lineDetail { get; set; }


        }
        public class PurchaseOrderReturnTransComment
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "cmtSeq")]
            public string cmtSeq { get; set; }
            [XmlElement(ElementName = "cdRef")]
            public string cdRef { get; set; }
            [XmlElement(ElementName = "cdType")]
            public string cdType { get; set; }
            [XmlElement(ElementName = "cdLang")]
            public string cdLang { get; set; }
            [XmlElement(ElementName = "cdSeq")]
            public string cdSeq { get; set; }
            [XmlElement(ElementName = "prtOnQuote")]
            public string prtOnQuote { get; set; }
            [XmlElement(ElementName = "prtOnSo")]
            public string prtOnSo { get; set; }
            [XmlElement(ElementName = "prtOnInvoice")]
            public string prtOnInvoice { get; set; }
            [XmlElement(ElementName = "prtOnPacklist")]
            public string prtOnPacklist { get; set; }
            [XmlElement(ElementName = "prtOnPo")]
            public string prtOnPo { get; set; }
            [XmlElement(ElementName = "prtOnRct")]
            public string prtOnRct { get; set; }
            [XmlElement(ElementName = "prtOnRtv")]
            public string prtOnRtv { get; set; }
            [XmlElement(ElementName = "prtOnShpr")]
            public string prtOnShpr { get; set; }
            [XmlElement(ElementName = "prtOnBol")]
            public string prtOnBol { get; set; }
            [XmlElement(ElementName = "prtOnCus")]
            public string prtOnCus { get; set; }
            [XmlElement(ElementName = "prtOnProb")]
            public string prtOnProb { get; set; }
            [XmlElement(ElementName = "prtOnSchedule")]
            public string prtOnSchedule { get; set; }
            [XmlElement(ElementName = "prtOnIsrqst")]
            public string prtOnIsrqst { get; set; }
            [XmlElement(ElementName = "prtOnDo")]
            public string prtOnDo { get; set; }
            [XmlElement(ElementName = "prtOnIntern")]
            public string prtOnIntern { get; set; }
            [XmlElement(ElementName = "cmtCmmt")]
            public string cmtCmmt { get; set; }
        }
        public class LineDetail
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "line")]
            public string line { get; set; }
            [XmlElement(ElementName = "yn")]
            public string yn { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public string lotserialQty { get; set; }
            [XmlElement(ElementName = "packingQty")]
            public string packingQty { get; set; }
            [XmlElement(ElementName = "returnUm")]
            public string returnUm { get; set; }
            [XmlElement(ElementName = "wolot")]
            public string wolot { get; set; }
            [XmlElement(ElementName = "woop")]
            public string woop { get; set; }
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
            [XmlElement(ElementName = "podReason")]
            public string podReason { get; set; }
            [XmlElement(ElementName = "cmmtYn")]
            public string cmmtYn { get; set; }
           
            [XmlElement(ElementName = "yn1")]
            public string yn1 { get; set; }

            [XmlElement(ElementName = "issueDetail")]
            public IssueDetaill issueDetail { get; set; }
            [XmlElement(ElementName = "lineDetailTransComment")]
            public LineDetailTransComment lineDetailTransComment { get; set; }

        }
        public class IssueDetaill
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "location")]
            public string location { get; set; }
            [XmlElement(ElementName = "lotserial")]
            public string lotserial { get; set; }
            [XmlElement(ElementName = "lotref")]
            public string lotref { get; set; }
            [XmlElement(ElementName = "vendlot")]
            public string vendlot { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public string lotserialQty { get; set; }
            [XmlElement(ElementName = "serialsYn")]
            public string serialsYn { get; set; }
               
        }
        public class LineDetailTransComment
        {
            [XmlElement(ElementName = "operation")]
            public string operation { get; set; }
            [XmlElement(ElementName = "cmtSeq")]
            public string cmtSeq { get; set; }
            [XmlElement(ElementName = "cdRef")]
            public string cdRef { get; set; }
            [XmlElement(ElementName = "cdType")]
            public string cdType { get; set; }
            [XmlElement(ElementName = "cdLang")]
            public string cdLang { get; set; }
            [XmlElement(ElementName = "cdSeq")]
            public string cdSeq { get; set; }
            [XmlElement(ElementName = "prtOnQuote")]
            public string prtOnQuote { get; set; }
            [XmlElement(ElementName = "prtOnSo")]
            public string prtOnSo { get; set; }
            [XmlElement(ElementName = "prtOnInvoice")]
            public string prtOnInvoice { get; set; }
            [XmlElement(ElementName = "prtOnPacklist")]
            public string prtOnPacklist { get; set; }
            [XmlElement(ElementName = "prtOnPo")]
            public string prtOnPo { get; set; }
            [XmlElement(ElementName = "prtOnRct")]
            public string prtOnRct { get; set; }
            [XmlElement(ElementName = "prtOnRtv")]
            public string prtOnRtv { get; set; }
            [XmlElement(ElementName = "prtOnShpr")]
            public string prtOnShpr { get; set; }
            [XmlElement(ElementName = "prtOnBol")]
            public string prtOnBol { get; set; }
            [XmlElement(ElementName = "prtOnCus")]
            public string prtOnCus { get; set; }
            [XmlElement(ElementName = "prtOnProb")]
            public string prtOnProb { get; set; }
            [XmlElement(ElementName = "prtOnSchedule")]
            public string prtOnSchedule { get; set; }
            [XmlElement(ElementName = "prtOnIsrqst")]
            public string prtOnIsrqst { get; set; }
            [XmlElement(ElementName = "prtOnDo")]
            public string prtOnDo { get; set; }
            [XmlElement(ElementName = "prtOnIntern")]
            public string prtOnIntern { get; set; }
            [XmlElement(ElementName = "cmtCmmt")]
            public string cmtCmmt { get; set; }
        }

    }
}
