using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SixLabors.ImageSharp;
using static Simpolo_Endpoint.DBUtil.DBLibrary;
using System.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using Microsoft.Extensions.Configuration;
using System.Net;
using Simpolo_Endpoint.DTO;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http;

namespace Simpolo_Endpoint.DAO.Services
{
    public class GroupOBDService : AppDBService, IGroupOBD
    {
        private readonly IWebHostEnvironment _webHostEnvironment;


        public GroupOBDService(IOptions<AppSettings> appSettings, IWebHostEnvironment webHostEnvironment)
        : base(appSettings)
        {
            _webHostEnvironment = webHostEnvironment;
        }



        public async Task<Payload<string>> GetGroupOutboundDetails(GroupOBDDTO items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                  {"@warehouseid",items.warehouseid},
                  {"@tenantid",items.tenantid},
                  {"@obdnumber",items.obdnumber},
                  {"@customerids",items.customerids},
                  {"@fromdate",items.fromdate},
                  {"@todate",items.todate},
                  {"@isfulldelivery",items.isfulldelivery},
                  {"@sitecode",items.sitecode},
                  {"@sotypeid", items.sotypeid},
                  {"@accountid",items.accountid},
                  {"@vehiclenumber",items.VehicleNo},
                  {"@LoadingPointID",items.LoadingPointID},
                  {"@TokenNumber",items.TokenNumber}

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GET_Pending_OrdersList", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> GetOBDDetailsCustomerWise(GroupOBDDTO items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                  {"@warehouseid",items.warehouseid},
                  {"@tenantid",items.tenantid},
                  {"@obdnumber",items.obdnumber},
                  {"@customerids",items.customerids},
                  {"@fromdate",items.fromdate},
                  {"@todate",items.todate},
                  {"@isfulldelivery",items.isfulldelivery},
                  {"@sitecode",items.sitecode},
                  {"@sotypeid", items.sotypeid},
                  {"@accountid",items.accountid},
                  {"@vehiclenumber",items.VehicleNo}

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GET_Pending_OrdersListCustomerWise", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }




        public async Task<Payload<string>> GetSOType(SOTypeModelItems items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@prefix",items.prefix==null?"":items.prefix}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_getsotype", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetDeliverySites(DeliverySitesModelItems items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                  {"@prefix",items.prefix==null?"":items.prefix},
                  {"@loginaccountid",items.loginaccountid},
                  {"@logintenantid",items.logintenantid}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_getdeliverysite", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }




        public async Task<Payload<string>> GroupOBDCreation(GroupOutboundCreationItemsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                 var DecryptObj = new ModelsLibrary.Encrypting();
                var sqlParams = new Dictionary<string, object>{
                  {"inputjson", DecryptObj.Decryptword(items.inputjson)},
                  {"@warehouseid",items.warehouseid},
                  {"@deliverytypeid",items.deliverytypeid},
                  {"@loginaccountid",items.loginaccountid},
                  {"@loginuserid",items.loginuserid},
                  {"@logintanentid",items.logintanentid},
                  {"@dockid",items.dockid},
                  {"@deliveryflag",items.deliveryflag},
                  {"@sortingtype",items.sortingtype}
                  
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_upsert_batch_obd_creation", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> VLPDDeliveryNoteDetails(VLPDDeliveryNoteModelItems Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdidid",Modelitems.vlpdid},
                  {"@loginaccountid",Modelitems.loginaccountid},
                  {"@logintanentid",Modelitems.logintanentid},
                  {"@loginuserid",Modelitems.loginuserid}
                
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_obd_get_vlpd_deliverynote", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> GetGroupOBDPopupDetails(GroupOBDPopupModelitems Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitems.vlpdid}
                
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_vlpd_groupobdpopup", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> GetCartonDetails(CartonModelItems Modelitem)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@prefix",Modelitem.prefix},
                  {"@vlpdnumber",Modelitem.vlpdnumber}
                 
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_vlpd_carton", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetVLPDPickingItem(vlpdpickitemfrombin Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdnumber",string.IsNullOrEmpty(Modelitems.vlpdnumber)?"":Modelitems.vlpdnumber},
                  {"@location",Modelitems.location},
                  {"@quantity",Modelitems.quantity},
                  {"@createdby",Modelitems.createdby},
                  {"@batchno",Modelitems.batchno},
                  {"@projrefno",Modelitems.projrefno},
                  {"@serialno",Modelitems.serialno},
                  {"@mfgdate",Modelitems.mfgdate},
                  {"@expdate",Modelitems.expdate},
                  {"@tocartoncode",Modelitems.tocartoncode},
                  {"@assignedid",Modelitems.assignedid},
                  {"@accountid",Modelitems.accountid},
                  {"@mrp",Modelitems.mrp},
                  {"@huno",Modelitems.huno},
                  {"@husize",Modelitems.husize},
                  {"@linenumber",Modelitems.linenumber},
                  {"@soheaderid",Modelitems.soheaderid},
                  {"@mcode",Modelitems.mcode},
                  {"@goodsmovementtypeid",Modelitems.goodsmovementtypeid},
                  {"@isdamaged",Modelitems.isdamaged},
                  {"@lastmodifiedby",Modelitems.lastmodifiedby},
                  {"@hasdiscrepancy",Modelitems.hasdiscrepancy},
                  {"@sodetailsidnew",Modelitems.sodetailsidnew},
                  {"@loginaccountid",Modelitems.loginaccountid},
                  {"@logintanentid",Modelitems.logintanentid},
                  {"@loginuserid",Modelitems.loginuserid}
                 
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_inv_vlpd_pickitemfrombin", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> GetVLPDPickingByGroupOBDNumber(vlpddeliverypicknote Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitems.vlpdid},
                  {"@loginuserid",Modelitems.loginuserid},
                  {"@logintanentid",Modelitems.logintanentid},
                  {"@loginaccountid",Modelitems.loginaccountid}
                  
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_obd_vlpd_deliverypicknote", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }



        public async Task<Payload<string>> UpdateOBDQty(UpdateOBDQtyinViewPopup Modelitem)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@inputjson",Modelitem.inputjson},
                  {"@userid",Modelitem.userid}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_update_obd_customerpo", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> VerifyPopUp(Vlpdverifyopup Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitems.vlpdid},
                  {"@userid",Modelitems.userid}
                 
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_vlpd_verify", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetPendingSKUList(Pendingreleaselist Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitems.vlpdid}
                 
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_vlpd_pendingreleaselist", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }



        public async Task<Payload<string>> GetReservedSKUList(Pendingreleaselist Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitems.vlpdid}

            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_vlpd_ReserveSKUlist", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        
        public async Task<Payload<string>> VLPDViewPickList(VLPDViewPickList Modelitem)
        {
            Payload<string> response = new Payload<string>();   
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitem.vlpdid}
                  
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_vlpd_view_picklist", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }



        public async Task<Payload<string>> VLPDViewPickListSummarize(VLPDViewPickList Modelitem)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitem.vlpdid}

            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_vlpd_view_picklist_Summarize", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> VLPDReleaseItems(VLPDReleaseModelItems Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitems.vlpdid},
                  {"@userid",Modelitems.userid},
                  {"@allowpartial",Modelitems.allowpartial}
                 
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_upsert_GroupOBD_PickingSuggestions", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }



        public async Task<Payload<string>> VLPDRegenerateReleaseItems(VLPDReleaseModelItems Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                  {"@vlpdid",Modelitems.vlpdid},
                  {"@userid",Modelitems.userid},
                  {"@allowpartial",Modelitems.allowpartial}

            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_Upsert_Regenarte_Suggestions_VLPD", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> UpsertOBDLoadPointData(GetLoadingPoints Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
                {
                  {"@WarehouseID",Modelitems.Warehouseid},
                  {"@TenantID",Modelitems.TenantID},
                  {"@OutboundIDs",Modelitems.OutboundIDs},
                  {"@UserID",Modelitems.UserID},
                  {"@LoadingPointData",Modelitems.inputjson},



                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_Upsert_LoadingPoint_OBD", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }



        public async Task<Payload<string>> UpdateOBDLoadPointData(GetLoadingPoints Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
                {
                  {"@WarehouseID",Modelitems.Warehouseid},
                  {"@TenantID",Modelitems.TenantID},
                  {"@OutboundID",Modelitems.OutboundID},
                  {"@UserID",Modelitems.UserID},
                  {"@LoadingPointID",Modelitems.LoadPointID},



                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_Update_LoadingPoint_OBD", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> GetPickingListDetails(PickingListItemsModel Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
                {
                  {"@vlpdno",Modelitems.vlpdno},
                  {"@fromdate",Modelitems.fromdate},
                  {"@todate",Modelitems.todate},
                  {"@warehouseid",Modelitems.warehouseid},
                  {"@loginaccountid",Modelitems.loginaccountid},
                  {"@logintanentid",Modelitems.logintanentid},
                  {"@loginuserid",Modelitems.loginuserid},
                  {"@OBDNumber",Modelitems.OBDNumber },
                  {"@VehicleNo",Modelitems.VehicleNo },
                  {"@LoadingPointID",Modelitems.LoadingPointID }
                 
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_vlpd_list", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetGroupOBDNumber(GroupOBDNumber Modelitems)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object>
            {
                 
                  {"@warehouseid",Modelitems.warehouseid},
                  {"@tenantid",Modelitems.tenantid},
                  {"@prefix",Modelitems.prefix}
                
                 
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_vlpd_VLPDNUMBER", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<bool> PostPGIDataToSAP(PgipostingDTO items)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
              
                var endpointUrl = $"{ServiceURL}/SAP/PGI";
                var requestBody = new
                {
                    outboundid = items.outboundid
                   
                };

                var apiResponse = await CallApiAsync(endpointUrl, requestBody);

                if (apiResponse.IsSuccessStatusCode)
                {


                    return true;
                }
                else
                {
                    throw new Exception($"Failed to Post data to SAP. Status Code: {apiResponse.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while posting data to SAP: {ex.Message}", ex);
            }
        }

        private async Task<HttpResponseMessage> CallApiAsync(string url, object requestBody)
        {
            using (var client = new HttpClient())
            {
                
                var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

                
                var response = await client.PostAsync(url, content);
                return response;
            }
        }






    }
}
