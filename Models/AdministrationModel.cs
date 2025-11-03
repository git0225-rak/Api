using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{
    public class GetMaterialTypeModel
    {
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
    }
    public class TenantsInputModel
    {
        public string prefix { get; set; }
    }

    public class UpsertMaterialTypeModel
    {
        public int MtypeID { get; set; }
        public int UserID { get; set; }
        public string Tenant { get; set; }
        public string Mtype { get; set; }
        public string Desc { get; set; }
        public int TenantID { get; set; }
    }
    public class DeleteMaterialInputModel
    {
        public int MtypeID { get; set; }
    }

    public class GetMaterialGroupModel
    {
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public int TenantID { get; set; }
    }
    public class UpsertMaterialGroupModel
    {
        public int MGroupID { get; set; }
        public string MaterialGroup { get; set; }
        public string MaterialGroupDesc { get; set; }
        public int TenantID { get; set; }
        public int UserID { get; set; }
    }
    public class DeleteMaterialGroupModel
    {
        public int MGroupID { get; set; }
    }

    public class EditMaterialGroupModel
    {
        public int MGroupID { get; set; }
    }

    public class GetContainerDataModel
    {
        public int AccountID { get; set; }
        public int ContainerTypeID { get; set; }
        public int WarehouseId { get; set; }

        public int SeriesTypeID { get; set; }
    }
    public class CreateNewCartonsModel
    {
        public int WarehouseId { get; set; }
        public int ContainerTypeID { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
    }
    public class PrintInputModel
    {
        public List<Containercode> ContainerCode { get; set; }
        public string ipaddress { get; set; }
        public int port { get; set; }
        public int PrinterType { get; set; }

        public int IsOldPallet { get; set; }
    }
    public class Containercode
    {
        public string ContainerCode { get; set; }
    }


    public class ContainercodeQrLables
    {
        public string FromContainerCode { get; set; }
        public int FromContainerID { get; set; }

        public string ToContainerCode { get; set; }
        public int ToContainerID { get; set; }
    }
    public class PrintBO
    {
        public int PrinterDPI { get; set; }
        //public string NoofLabels { get; set; }
    }

}
