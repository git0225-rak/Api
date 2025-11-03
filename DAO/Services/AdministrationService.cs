using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Simpolo_Endpoint.DAO.Services
{
    public class AdministrationService : AppDBService, IAdministration
    {
        public AdministrationService(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }
        public async Task<Payload<string>> GetMaterialType(GetMaterialTypeModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID_New", items.TenantID },
                    { "@UserID", items.UserID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetMtypedata", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetTenantsInMaterialType(TenantsInputModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@prefix", items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Admin_INL_GetData1", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> UpsertMaterialType(UpsertMaterialTypeModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                if (items.MtypeID != 0)
                {
                    string sp = "Exec USP_Admin_INL_GetCountMType @MTypeID=" + items.MtypeID + "";
                    int resultinfo = DbUtility.GetSqlN(sp, ConnectionString);
                    if (resultinfo != 0)
                    {
                        response.Result = "-1"; // Could not update Material Type as item is mapped to this.
                        return response;
                    }
                }
                var sqlParams = new Dictionary<string, object>
                {
                        {"@mtypeID",items.MtypeID},
                        {"@Description",items.Desc},
                        {"@tenantId",items.TenantID},
                        {"@CreatedBy",items.UserID},
                        {"@Mtype",items.Mtype},
                        {"@IsActive",1},
                        {"@IsDeleted",0}
                };
                var jsonresult = await DbUtility.GetjsonData(this.ConnectionString, "sp_MMT_UpsetMType", sqlParams).ConfigureAwait(false);
                JArray jsonArray = JArray.Parse(jsonresult);
                int result = jsonArray[0]["Result"].Value<int>();

                if (result == 2)
                {
                    response.Result = "1";//Material Type Saved Successfully
                    return response;
                }
                else
                {
                    response.Result = "2";//Material Type Updated Successfully
                    return response;
                }
            }
            catch (SqlException Sqlex)
            {
                //response.addError(Sqlex.Message);
                if (Sqlex.Message.StartsWith("Violation of UNIQUE KEY constraint 'UK_MMT_MType'"))
                {
                    response.Result = ("3");   //Mtype already exists under this Tenant
                }
                else
                {
                    response.Result = ("4");    //Error while submitting the data
                }
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> DeleteMaterialType(DeleteMaterialInputModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string sp = "Exec USP_Admin_INL_GetCountMType @MTypeID=" + items.MtypeID + "";
                var result = DbUtility.GetSqlN(sp, ConnectionString);
                if (result != 0)
                {
                    response.Result = "1"; //Cannot delete Material Type as item is mapped to this.
                    return response;
                }
                else
                {
                    string Query = "Exec USP_Admin_INL_Getupdate @MTypeID=" + items.MtypeID + "";
                    var resultinfo = DbUtility.GetSqlN(Query, ConnectionString);//deleted successfully
                    response.Result = "2";
                }
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

        public async Task<Payload<string>> GetMaterialGroup(GetMaterialGroupModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID_New", items.TenantID },
                    { "@UserID_New", items.UserID },
                    { "@UserTypeID_New", items.UserID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetMaterialGroupdata", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> UpsertMaterialGroup(UpsertMaterialGroupModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                if (items.MGroupID != 0)
                {
                    string sp = "Exec USP_Admin_INL_GetMGroup @MGroupID=" + items.MGroupID + "";
                    var result = DbUtility.GetSqlN(sp, ConnectionString);
                    if (result != 0)
                    {
                        response.Result = "2"; //Cannot Update as Material Group is mapped an Item
                        return response;
                    }
                }
                var sqlParams = new Dictionary<string, object>
                {
                    { "@MaterialGroup", items.MaterialGroup },
                    { "@MaterialGroupDesc", items.MaterialGroupDesc },
                    { "@MGroupID", items.MGroupID },
                    { "@TenantID", items.TenantID },
                    { "@Isactive", 1 },
                    { "@IsDeleted", 0 },
                    { "@CreatedBy", items.UserID }
                };
                var resultinfo = await DbUtility.GetjsonData(this.ConnectionString, "sp_MMT_UpsetMGroup", sqlParams).ConfigureAwait(false);
                if (resultinfo == "[]")
                {
                    response.Result = "1";//Created
                    return response;
                }
            }
            catch (SqlException Sqlex)
            {
                //response.addError(Sqlex.Message);
                if (Sqlex.Message.Contains("Cannot insert duplicate key in object 'dbo.MMT_MGroup'"))
                {
                    response.Result = ("3");   //M Group code already exists under this Tenant
                }
                else
                {
                    response.Result = ("-2");    //Error while updating
                }
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> DeleteMaterialGroup(DeleteMaterialGroupModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string sp = "Exec USP_Admin_INL_GetMGroup @MGroupID=" + items.MGroupID + "";
                var result = DbUtility.GetSqlN(sp, ConnectionString);
                if (result != 0)
                {
                    response.Result = "1"; //Cannot delete Material Group as item is mapped to this.
                    return response;
                }
                else
                {
                    string Query = "Exec USP_Admin_INL_GetMaterialGroupUpdate @MGroupID=" + items.MGroupID + "";
                    var resultinfo = DbUtility.GetSqlN(Query, ConnectionString);
                    response.Result = "2"; //Data Record Deleted Successfully"
                }
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

        public async Task<Payload<string>> EditMaterialGroup(EditMaterialGroupModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@MGroupID", items.MGroupID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Admin_INL_GetMaterialGroup", sqlParams).ConfigureAwait(false);
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


        //pageload and search
        public async Task<Payload<string>> GetContainerData(GetContainerDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@AccountID", items.AccountID },
                    { "@ContainerTypeID", items.ContainerTypeID },
                    { "@WareHouseID", items.WarehouseId },
                    { "@SeriesTypeID", items.SeriesTypeID },

                    
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "Get_Containers", sqlParams).ConfigureAwait(false);
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> CreateNewCartons(CreateNewCartonsModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string sp = "Exec sp_INV_NewContainerCreation @WarehouseId=" + items.WarehouseId + ",@ContainerTypeID=" + items.ContainerTypeID + ",@UserId=" + items.UserID + "";
                var result = DbUtility.GetSqlN(sp, ConnectionString);
                if (result == 0)
                {
                    response.Result = "1"; // Successfully Created
                }
                else
                {
                    response.Result = "-1";//"Error while generating new container"
                }
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        //        public async Task<Payload<string>> GetContainerPrint(PrintInputModel printobj)
        //#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        //        {
        //            Payload<string> response = new Payload<string>();
        //            try
        //            {
        //                DBFactory factory = new DBFactory();
        //                IDBUtility DbUtility = factory.getDBUtility();
        //                string ZPL = "";
        //                int Port = 0;

        //                List<Containercode> containercodes = printobj.ContainerCode;

        //                string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
        //                using (SqlConnection connection = new SqlConnection(ConnectionString))
        //                {
        //                    connection.Open();
        //                    using (SqlCommand command = new SqlCommand(sp, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@DeviceIP", printobj.ipaddress);
        //                        Port = (int)command.ExecuteScalar();
        //                    }
        //                }

        //                for (var i = 0; i < containercodes.Count; i++)
        //                {
        //                    PrintBO printBo = new PrintBO();
        //                    printBo.PrinterDPI = 203;
        //                    string zplData = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND LabelType='Container'", ConnectionString);
        //                    ZPL += zplData.Replace("ContainerName", containercodes[i].ContainerCode);
        //                    Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, Port , ZPL);
        //                    response.Result = ZPL;
        //                }
        //                containercodes.Clear();
        //            }
        //            catch (SqlException Sqlex)
        //            {
        //                response.addError(Sqlex.Message);
        //            }
        //            catch (Exception ex)
        //            {
        //                response.addError(ex.Message);
        //            }
        //            return response;// "Printed Successfully";
        //        }

        public async Task<Payload<string>> GetContainerPrint(PrintInputModel printobj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string ZPL = "";
                int Port = 0;

                List<Containercode> containercodes = printobj.ContainerCode;

                /*    for (var i = 0; i < containercodes.Count; i++)

                    {
                        PrintBO printBo = new PrintBO();
                        printBo.PrinterDPI = 203;
                        string zplData = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND LabelType='Container'", ConnectionString);
                        ZPL += zplData.Replace("ContainerName", containercodes[i].ContainerCode);
 
                        if (printobj.PrinterType == 1 || printobj.PrinterType == 0)
                        {
                            Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
                        }
                        else if (printobj.PrinterType == 2)
                        {
                            string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
                            using (SqlConnection connection = new SqlConnection(ConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(sp, connection))
                                {
                                    command.Parameters.AddWithValue("@DeviceIP", printobj.ipaddress);
                                    Port = (int)command.ExecuteScalar();
                                }
                            }
                            Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, Port, ZPL);
                        }
                        else if (printobj.PrinterType == 4)
                        {
                            response.Result = ZPL;
                        } 
                        else
                        {
                            Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, printobj.port, ZPL);
                        }
                        response.Result = ZPL;
                    } 

                    containercodes.Clear(); */
                for (var i = 0; i < containercodes.Count; i++)
                {

                    PrintBO printBo = new PrintBO();

                    printBo.PrinterDPI = 203;

                    // Reset ZPL before every iteration

                    ZPL = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND LabelType='Container'", ConnectionString);

                    ZPL = ZPL.Replace("ContainerName", containercodes[i].ContainerCode);

                    if (printobj.PrinterType == 1 || printobj.PrinterType == 0)
                    {
                        Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
                    }
                    else if (printobj.PrinterType == 2)
                    {
                        string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(sp, connection))
                            {
                                command.Parameters.AddWithValue("@DeviceIP", printobj.ipaddress);
                                Port = (int)command.ExecuteScalar();
                            }
                        }
                        Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, Port, ZPL);
                    }
                    else if (printobj.PrinterType == 4)
                    {
                        response.Result = ZPL;                        
                    }
                    else
                    {
                        Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, printobj.port, ZPL);
                    }
                    response.Result = ZPL;
                }
                // Clear container codes after the loop
                containercodes.Clear();
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;// "Printed Successfully";
        }

        public async Task<Payload<string>> GetContainerPrint_Network(PrintInputModel printobj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int Port = 0;
                List<Containercode> containercodes = printobj.ContainerCode;

                for (var i = 0; i < containercodes.Count; i++)
                {
                    if (printobj.IsOldPallet == 1)
                    {
                        var Palletcode = DbUtility.GetSqlS("EXEC SP_Check_Pallet @containercodes = " + DBLibrary.SQuote(containercodes[i].ContainerCode) + "", this.ConnectionString);
                        if (Palletcode.Contains("Error"))
                        {
                            response.Result = Palletcode;
                            return response;
                        }
                    }


                    string ZPL = "";
                    PrintBO printBo = new PrintBO();
                    printBo.PrinterDPI = 203;
                    string zplData = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND LabelType='Container'", ConnectionString);
                    ZPL += zplData.Replace("ContainerName", containercodes[i].ContainerCode);

                    if (printobj.PrinterType == 1 || printobj.PrinterType == 0)
                    {
                        Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
                    }
                    else if (printobj.PrinterType == 2)
                    {
                        string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand(sp, connection))
                            {
                                command.Parameters.AddWithValue("@DeviceIP", printobj.ipaddress);
                                Port = (int)command.ExecuteScalar();
                            }
                        }

                        Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, Port, ZPL);
                    }
                    else if (printobj.PrinterType == 4)
                    {
                        response.Result = ZPL;
                    }

                    else
                    {
                        Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, printobj.port, ZPL);
                    }

                    response.Result = ZPL;
                    string Query = "Exec Upsert_ContainerPrint @containercodes = " + DBLibrary.SQuote(containercodes[i].ContainerCode) + "";
                    var result = DbUtility.GetSqlN(Query, ConnectionString);
                }
                containercodes.Clear();
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;// "Printed Successfully";
        }

        public async Task<DataSet> GetPalletData(ContainercodeQrLables items)
        {

            IDBUtility DbUtility = new SqlServerUtility();
            DataSet DS = DbUtility.GetDS("Exec SP_GetPalletData  @FromCartonID="+items.FromContainerID+",@ToCartonID="+items.ToContainerID+",@Con="+DBLibrary.SQuote(this.ConnectionString), this.ConnectionString);
            return DS;
        }


    }
}
