using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simpolo_Endpoint.DTO
{
    public class GroupOBDDTO
    {
        public int warehouseid { get; set; }
        public int tenantid { get; set; }
        public string obdnumber { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string customerids { get; set; }
        public int isfulldelivery { get; set; }
        public string sitecode { get; set; }
        public int accountid { get; set; }
        public int sotypeid { get; set; }

        public string VehicleNo { get; set; }

        public int LoadingPointID { get; set; }

        public string TokenNumber { get; set; }

    }


    public class Vehiclenumbeslist
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }


    public class SOTypeModelItems
    {
        public string prefix { get; set; }
    }


    public class DeliverySitesModelItems
    {
        public int loginaccountid { get; set; }
        public string prefix { get; set; }
        public int logintenantid { get; set; }
    }

    public class GroupOutboundCreationItemsModel
    {
        public string inputjson { get; set; }
        public int warehouseid { get; set; }
        public int deliverytypeid { get; set; }
        public int loginaccountid { get; set; }
        public int loginuserid { get; set; }
        public int logintanentid { get; set; }
        public int dockid { get; set; }
        public int deliveryflag { get; set; }
        public int sortingtype { get; set; }
    }

    public class VLPDDeliveryNoteModelItems
    {
        public int vlpdid { get; set; }
        public int loginaccountid { get; set; }
        public int loginuserid { get; set; }
        public int logintanentid { get; set; }
    }

    public class GroupOBDPopupModelitems
    {
        public int vlpdid { get; set; }
    }

    public class CartonModelItems
    {
        public string vlpdnumber { get; set; }
        public string prefix { get; set; }
    }

    public class vlpdpickitemfrombin
    {
        public string vlpdnumber { get; set; }
        public string location { get; set; }
        public decimal quantity { get; set; }
        public int createdby { get; set; }
        public string mfgdate { get; set; }
        public string expdate { get; set; }
        public string batchno { get; set; }
        public string serialno { get; set; }
        public string projrefno { get; set; }
        public string tocartoncode { get; set; }
        public int accountid { get; set; }
        public int assignedid { get; set; }
        public string mrp { get; set; }
        public int husize { get; set; }
        public int linenumber { get; set; }
        public int huno { get; set; }
        public int soheaderid { get; set; }
        public string mcode { get; set; }
        public int goodsmovementtypeid { get; set; }
        public int isdamaged { get; set; }
        public int lastmodifiedby { get; set; }
        public int hasdiscrepancy { get; set; }
        public int sodetailsidnew { get; set; }
        public int loginaccountid { get; set; }
        public int loginuserid { get; set; }
        public int logintanentid { get; set; }
    }

    public class vlpddeliverypicknote
    {
        public int vlpdid { get; set; }
        public int loginaccountid { get; set; }
        public int loginuserid { get; set; }
        public int logintanentid { get; set; }
    }
    public class UpdateOBDQtyinViewPopup
    {
        public string inputjson { get; set; }
        public int userid { get; set; }
    }

    public class Vlpdverifyopup
    {
        public int vlpdid { get; set; }
        public int userid { get; set; }
    }

    public class Pendingreleaselist
    {
        public int vlpdid { get; set; }
    }

    public class VLPDViewPickList
    {
        public int vlpdid { get; set; }
    }

    public class VLPDReleaseModelItems
    {
        public int vlpdid { get; set; }
        public int userid { get; set; }
        public int allowpartial { get; set; }
    }

    public class PickingListItemsModel
    {
        public string vlpdno { get; set; }
        public string fromdate { get; set; }
        public string todate { get; set; }
        public int warehouseid { get; set; }
        public int loginaccountid { get; set; }
        public int loginuserid { get; set; }
        public int logintanentid { get; set; }

        public string OBDNumber { get; set; }

        public string VehicleNo { get; set; }

        public int LoadingPointID { get; set; }
    }

    public class GroupOBDNumber
    {
        public int tenantid { get; set; }
        public int warehouseid { get; set; }
        public string prefix { get; set; }
    }


    public class PgipostingDTO
    {
        public int outboundid { get; set; }
    }
}
