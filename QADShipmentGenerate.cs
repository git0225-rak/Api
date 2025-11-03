using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simpolo_Endpoint
{
        public class QADShipmentGenerate
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
                [XmlElement(ElementName = "maintainSalesOrderShipper", Namespace = "")]
                public MaintainSalesOrderShipper MaintainSalesOrderShipper { get; set; }
            }
            public class MaintainSalesOrderShipper
            {
                [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
                public DsSessionContext DsSessionContext { get; set; }
                [XmlElement(ElementName = "dsSalesOrderShipper")]
                public DsSalesOrderShipper DsSalesOrderShipper { get; set; }
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
            public class DsSalesOrderShipper
            {
                [XmlElement(ElementName = "salesOrderShipper")]
                public SalesOrderShipper SalesOrderShipper { get; set; }
            }
            public class SalesOrderShipper
            {
                [XmlElement(ElementName = "operation")]
                public string operation { get; set; }
                [XmlElement(ElementName = "absShipfrom")]
                public string absShipfrom { get; set; }
                [XmlElement(ElementName = "absId")]
                public string absId { get; set; }
                [XmlElement(ElementName = "absShipto")]
                public string absShipto { get; set; }
                [XmlElement(ElementName = "vInvmov")]
                public string vInvmov { get; set; }
                [XmlElement(ElementName = "vCont")]
                public string vCont { get; set; }
                [XmlElement(ElementName = "vCont1")]
                public string vCont1 { get; set; }
                [XmlElement(ElementName = "vCarrier")]
                public string vCarrier { get; set; }
                [XmlElement(ElementName = "vMulti")]
                public string vMulti { get; set; }
                [XmlElement(ElementName = "absShipvia")]
                public string absShipvia { get; set; }
                [XmlElement(ElementName = "vFob")]
                public string vFob { get; set; }
                [XmlElement(ElementName = "vTransMode")]
                public string vTransMode { get; set; }
                [XmlElement(ElementName = "vCarrRef")]
                public string vCarrRef { get; set; }
                [XmlElement(ElementName = "vVehRef")]
                public string vVehRef { get; set; }
                [XmlElement(ElementName = "vFormat")]
                public string vFormat { get; set; }
                [XmlElement(ElementName = "vConsShip")]
                public string vConsShip { get; set; }
                [XmlElement(ElementName = "absLang")]
                public string absLang { get; set; }
                [XmlElement(ElementName = "vCmmts")]
                public string vCmmts { get; set; }
                [XmlElement(ElementName = "vStatus")]
                public string vStatus { get; set; }
                [XmlElement(ElementName = "vCmmts1")]
                public string vCmmts1 { get; set; }
                [XmlElement(ElementName = "vShipCmmts")]
                public string vShipCmmts { get; set; }
                [XmlElement(ElementName = "vPackCmmts")]
                public string vPackCmmts { get; set; }
                [XmlElement(ElementName = "vFeatures")]
                public string vFeatures { get; set; }
                [XmlElement(ElementName = "vPrintSodet")]
                public string vPrintSodet { get; set; }
                [XmlElement(ElementName = "lSoUm")]
                public string lSoUm { get; set; }
                [XmlElement(ElementName = "compAddr")]
                public string compAddr { get; set; }
                [XmlElement(ElementName = "lPrintLotserials")]
                public string lPrintLotserials { get; set; }
                [XmlElement(ElementName = "dev")]
                public string dev { get; set; }
                [XmlElement(ElementName = "vOk")]
                public string vOk { get; set; }
                [XmlElement(ElementName = "sequenceDetail")]
                public SequenceDetail SequenceDetail { get; set; }
                [XmlElement(ElementName = "carrierDetail")]
                public CarrierDetail CarrierDetail { get; set; }
                [XmlElement(ElementName = "salesOrderShipperTransComment")]
                public SalesOrderShipperTransComment SalesOrderShipperTransComment { get; set; }
                [XmlElement(ElementName = "itemDetail")]
                public List<ItemDetail> ItemDetail { get; set; }
            }
            public class SequenceDetail
            {
                [XmlElement(ElementName = "operation")]
                public string operation { get; set; }
                [XmlElement(ElementName = "absdFldSeq")]
                public string absdFldSeq { get; set; }
                [XmlElement(ElementName = "absdFldName")]
                public string absdFldName { get; set; }
                [XmlElement(ElementName = "absdFldValue")]
                public string absdFldValue { get; set; }

            }
            public class CarrierDetail
            {
                [XmlElement(ElementName = "operation")]
                public string operation { get; set; }
                [XmlElement(ElementName = "seq")]
                public string seq { get; set; }
                [XmlElement(ElementName = "abscCarrier")]
                public string abscCarrier { get; set; }
            }
            public class SalesOrderShipperTransComment
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
                [XmlElement(ElementName = "cmtCmmt")]
                public string cmtCmmt { get; set; }
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
            }
            public class ItemDetail
            {
                [XmlElement(ElementName = "operation")]
                public string operation { get; set; }
                [XmlElement(ElementName = "scxPart")]
                public string scxPart { get; set; }
                [XmlElement(ElementName = "scxPo")]
                public string scxPo { get; set; }
                [XmlElement(ElementName = "scxCustref")]
                public string scxCustref { get; set; }
                [XmlElement(ElementName = "scxModelyr")]
                public string scxModelyr { get; set; }
                [XmlElement(ElementName = "scxOrder")]
                public string scxOrder { get; set; }
                [XmlElement(ElementName = "scxLine")]
                public string scxLine { get; set; }
                [XmlElement(ElementName = "srQty")]
                public string srQty { get; set; }
                [XmlElement(ElementName = "transUm")]
                public string transUm { get; set; }
                [XmlElement(ElementName = "transConv")]
                public string transConv { get; set; }
                [XmlElement(ElementName = "srSite")]
                public string srSite { get; set; }
                [XmlElement(ElementName = "srLoc")]
                public string srLoc { get; set; }
                [XmlElement(ElementName = "srLotser")]
                public string srLotser { get; set; }
                [XmlElement(ElementName = "srRef")]
                public string srRef { get; set; }
                [XmlElement(ElementName = "multiEntry")]
                public string multiEntry { get; set; }
                [XmlElement(ElementName = "vCmmts")]
                public string vCmmts { get; set; }
                [XmlElement(ElementName = "ioprmt")]
                public string ioprmt { get; set; }
                [XmlElement(ElementName = "issueDetail")]
                public IssueDetail IssueDetail { get; set; }
                [XmlElement(ElementName = "additionalLineChargeDetail")]
                public AdditionalLineChargeDetail AdditionalLineChargeDetail { get; set; }
                [XmlElement(ElementName = "itemDetailTransComment")]
                public ItemDetailTransComment ItemDetailTransComment { get; set; }
            }
            public class IssueDetail
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
            public class AdditionalLineChargeDetail
            {
                [XmlElement(ElementName = "operation")]
                public string operation { get; set; }
                [XmlElement(ElementName = "abslLcLine")]
                public string abslLcLine { get; set; }
                [XmlElement(ElementName = "abslTrlCode")]
                public string abslTrlCode { get; set; }
                [XmlElement(ElementName = "abslLcAmt")]
                public string abslLcAmt { get; set; }
                [XmlElement(ElementName = "abslChargeType")]
                public string abslChargeType { get; set; }
                [XmlElement(ElementName = "abslRef")]
                public string abslRef { get; set; }
            }
            public class ItemDetailTransComment
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
                [XmlElement(ElementName = "cmtCmmt")]
                public string cmtCmmt { get; set; }
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
            }
        }
}
