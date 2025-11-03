using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simpolo_Endpoint.Models
{
    public class CycleCountQADXML
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
            [XmlElement(ElementName = "enterCycleCountResults", Namespace = "")]
            public EnterCycleCountResults EnterCycleCountResults { get; set; }
        }
        public class EnterCycleCountResults
        {
            [XmlElement(ElementName = "dsSessionContext", Namespace = d)]
            public DsSessionContext DsSessionContext { get; set; }
            [XmlElement(ElementName = "dsCycleCountResults")]
            public DsCycleCountResults DsCycleCountResults { get; set; }
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
        public class DsCycleCountResults
        {
            [XmlElement(ElementName = "cycleCountResults")]
            public CycleCountResults CycleCountResults { get; set; }
        }
        public class CycleCountResults
        {
            [XmlElement(ElementName = "operation")]
            public string Operation { get; set; }
            [XmlElement(ElementName = "ccInitial")]
            public string CcInitial { get; set; }
            [XmlElement(ElementName = "cycleCount")]
            public List<CycleCount> LstCycleCount { get; set; }
        }
        public class CycleCount
        {
            [XmlElement(ElementName = "operation")]
            public string Operation { get; set; }
            [XmlElement(ElementName = "part")]
            public string Part { get; set; }
            [XmlElement(ElementName = "site")]
            public string Site { get; set; }
            [XmlElement(ElementName = "location")]
            public string Location { get; set; }
            [XmlElement(ElementName = "lotserial")]
            public string Lotserial { get; set; }
            [XmlElement(ElementName = "lotref")]
            public string Lotref { get; set; }
            [XmlElement(ElementName = "qtyCnt")]
            public string QtyCnt { get; set; }
            [XmlElement(ElementName = "um")]
            public string Um { get; set; }
            [XmlElement(ElementName = "conv")]
            public string Conv { get; set; }
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
            [XmlElement(ElementName = "yn")]
            public string Yn { get; set; }
        }
    }
}
