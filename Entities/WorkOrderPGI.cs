using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FWMSC21Core_BusinessEntities.Entities
{
    public class WorkOrderPGI
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
            [XmlElement(ElementName = "issueWorkOrderComponent", Namespace = "")]
            public IssueWorkOrderComponent IssueWorkOrderComponent { get; set; }
        }
        public class IssueWorkOrderComponent
        {
            [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
            public DsSessionContext DsSessionContext { get; set; }
            [XmlElement(ElementName = "dsWorkOrderComponent")]
            public DsWorkOrderComponent DsWorkOrderComponent { get; set; }
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
        public class DsWorkOrderComponent
        {
            [XmlElement(ElementName = "workOrderComponent")]
            public WorkOrderComponent WorkOrderComponent { get; set; }
        }
        public class WorkOrderComponent
        {
            [XmlElement(ElementName = "operation")]
            public string Operation { get; set; }
            [XmlElement(ElementName = "woNbr")]
            public string WoNbr { get; set; }
            [XmlElement(ElementName = "woLot")]
            public string WoLot { get; set; }
            [XmlElement(ElementName = "woOp")]
            public string WoOp { get; set; }
            [XmlElement(ElementName = "effDate")]
            public string EffDate { get; set; }
            [XmlElement(ElementName = "fillAll")]
            public string FillAll { get; set; }
            [XmlElement(ElementName = "fillPick")]
            public string FillPick { get; set; }
            [XmlElement(ElementName = "yn")]
            public string Yn { get; set; }
            [XmlElement(ElementName = "yn1")]
            public string Yn1 { get; set; }
            [XmlElement(ElementName = "currWkctr")]
            public string CurrWkctr { get; set; }
            [XmlElement(ElementName = "currMch")]
            public string CurrMch { get; set; }
            [XmlElement(ElementName = "wiplot")]
            public string Wiplot { get; set; }
            [XmlElement(ElementName = "yn2")]
            public string Yn2 { get; set; }
            [XmlElement(ElementName = "yn3")]
            public string Yn3 { get; set; }
            [XmlElement(ElementName = "itemDetail")]
            public List<ItemDetail> ItemDetail { get; set; }
            [XmlElement(ElementName = "lotSerialDetail")]
            public LotSerialDetail LotSerialDetail { get; set; }
        }
        public class ItemDetail
        {
            [XmlElement(ElementName = "operation")]
            public string Operation { get; set; }
            [XmlElement(ElementName = "part")]
            public string Part { get; set; }
            [XmlElement(ElementName = "op")]
            public string Op { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public decimal LotserialQty { get; set; }
            [XmlElement(ElementName = "subComp")]
            public string SubComp { get; set; }
            [XmlElement(ElementName = "cancelBo")]
            public string CancelBo { get; set; }
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
            [XmlElement(ElementName = "issueDetail")]
            public IssueDetail IssueDetail { get; set; }
          
        }
        public class IssueDetail
        {
            [XmlElement(ElementName = "operation")]
            public string Operation { get; set; }
            [XmlElement(ElementName = "site")]
            public string Site { get; set; }
            [XmlElement(ElementName = "location")]
            public string Location { get; set; }
            [XmlElement(ElementName = "lotserial")]
            public string Lotserial { get; set; }
            [XmlElement(ElementName = "lotref")]
            public string Lotref { get; set; }
            [XmlElement(ElementName = "lotserialQty")]
            public decimal LotserialQty { get; set; }

        }
        public class LotSerialDetail
        {
            [XmlElement(ElementName = "operation")]
            public string Operation { get; set; }
            [XmlElement(ElementName = "biwlLotser")]
            public string BiwlLotser { get; set; }
            [XmlElement(ElementName = "biwlRef")]
            public string BiwlRef { get; set; }
            [XmlElement(ElementName = "biwlQty")]
            public string BiwlQty { get; set; }
        }
    }
}
