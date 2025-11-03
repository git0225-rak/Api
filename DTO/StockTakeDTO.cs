using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simpolo_Endpoint.DTO
{
    public class StockTakeDTO
    {
        private List<StockTakeDetials> _StockTakeDetials;

        public List<StockTakeDetials> StockTake
        {
            get { return _StockTakeDetials; }
            set { _StockTakeDetials = value; }
        }
    }
    public class StockTakeDetials
    {
        private string _LocationCode;
        private string _CartonCode;
        private string _MaterialCode;
        private string _Quantity;

        public string LocationCode
        {
            get { return _LocationCode; }
            set { _LocationCode = value; }
        }

        public string CartonCode
        {
            get { return _CartonCode; }
            set { _CartonCode = value; }
        }
        public string MaterialCode
        {
            get { return _MaterialCode; }
            set { _MaterialCode = value; }
        }

        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }
    }
}