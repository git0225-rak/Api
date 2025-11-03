using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Simpolo_Endpoint
{
    public static class EndpointConstants
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum DTO { None, LoginUserDTO, ProfileDTO, Inbound, Inventory, Exception, CycleCount, Outbound, DenestingDTO, PutAwayDTO, HouseKeepingDTO, POD, OutboundList, InboundList, POList, SOList, Tenant, Warehouse, Shipment, ScanDTO, InventoryDTO, APIInventoryDTO, MastersDTO, ReleaseOutbound, OutboundOrdersDTO, BulkOrderDTO, SupplierImportDTO, ItemMasterDTO, InboundDataDTO, SLOCToSLOCDTO, BinToBinDTO, SupplierReturnOBDDTO, CustomerReturnDTO, SupplierReturnDTO, UpdateItemMasterDTO, UpdateSupplierDTO, MiscReceipt, MiscIssue, CustomerImportDTO, SupplierCreationDTO, StockTakeDTO };
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum ScanType { Unloading, Putaway, Picking, Loading, DeNesting, Assortment };
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum LocationType { Bin, Dock, DeNesting, Staging, CrossDock, ExcessPicked, Quarantine };
    }
}
