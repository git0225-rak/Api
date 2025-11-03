using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Simpolo_Endpoint.DBUtil
{
    public class DBLibrary
    {
       
        public static String SQuote(String s)
        {
            try
            {

                return s != null ? "N'" + s.Replace("'", "''") + "'" : "''";

            }
            catch (Exception)
            {

                return "''";
            }
        }
        public static bool isAlphaNumeric(string N)
        {
            bool YesNumeric = false;
            bool YesAlpha = false;
            bool BothStatus = false;

            for (int i = 0; i < N.Length; i++)
            {
                if (char.IsLetter(N[i]))
                    YesAlpha = true;

                if (char.IsNumber(N[i]))
                    YesNumeric = true;
            }

            if (YesAlpha == true && YesNumeric == true)
            {
                BothStatus = true;
            }
            else
            {
                BothStatus = false;
            }
            return BothStatus;
        }
        public static string Formateddate(string inputdate)
        {
            inputdate = inputdate.Replace('-', '/');

            inputdate = inputdate.Split(' ')[0].ToString();
            string[] str = inputdate.Split('/');


            if (Convert.ToInt32(str[1]) > 12)
            {
                DateTime exdate = new DateTime(Convert.ToInt32(str[2]), Convert.ToInt32(str[0]), Convert.ToInt32(str[1]));
                inputdate = exdate.ToString("MM/dd/yyyy");
            }

            else
            {
                DateTime exdate = new DateTime(Convert.ToInt32(str[2]), Convert.ToInt32(str[1]), Convert.ToInt32(str[0]));
                inputdate = exdate.ToString("MM/dd/yyyy");
            }

            return inputdate;
        }
    }
}