using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DTO
{
    public partial class LoginUserData : object, System.ComponentModel.INotifyPropertyChanged
    {

        private int accountIDField;

        private int userIDField;

        private int sSOIDField;

        private string resultField;

        private string firstNameField;

        private string sessionIdetifierField;

        private string cleintIdetifierField;

        private string lastNameField;

        private string emailField;

        private string passwordField;

        private string rolesField;

        private string warehousesField;

        private string siteCodesField;

        private string departmentIDsField;

        private string machineIPAddressField;

        private int tenantIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int AccountID
        {
            get
            {
                return this.accountIDField;
            }
            set
            {
                this.accountIDField = value;
                this.RaisePropertyChanged("AccountID");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int UserID
        {
            get
            {
                return this.userIDField;
            }
            set
            {
                this.userIDField = value;
                this.RaisePropertyChanged("UserID");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public int SSOID
        {
            get
            {
                return this.sSOIDField;
            }
            set
            {
                this.sSOIDField = value;
                this.RaisePropertyChanged("SSOID");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
                this.RaisePropertyChanged("Result");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string FirstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
                this.RaisePropertyChanged("FirstName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string SessionIdetifier
        {
            get
            {
                return this.sessionIdetifierField;
            }
            set
            {
                this.sessionIdetifierField = value;
                this.RaisePropertyChanged("SessionIdetifier");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string CleintIdetifier
        {
            get
            {
                return this.cleintIdetifierField;
            }
            set
            {
                this.cleintIdetifierField = value;
                this.RaisePropertyChanged("CleintIdetifier");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string LastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
                this.RaisePropertyChanged("LastName");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
                this.RaisePropertyChanged("Email");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string Password
        {
            get
            {
                return this.passwordField;
            }
            set
            {
                this.passwordField = value;
                this.RaisePropertyChanged("Password");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string Roles
        {
            get
            {
                return this.rolesField;
            }
            set
            {
                this.rolesField = value;
                this.RaisePropertyChanged("Roles");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string Warehouses
        {
            get
            {
                return this.warehousesField;
            }
            set
            {
                this.warehousesField = value;
                this.RaisePropertyChanged("Warehouses");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string SiteCodes
        {
            get
            {
                return this.siteCodesField;
            }
            set
            {
                this.siteCodesField = value;
                this.RaisePropertyChanged("SiteCodes");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public string DepartmentIDs
        {
            get
            {
                return this.departmentIDsField;
            }
            set
            {
                this.departmentIDsField = value;
                this.RaisePropertyChanged("DepartmentIDs");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public string MachineIPAddress
        {
            get
            {
                return this.machineIPAddressField;
            }
            set
            {
                this.machineIPAddressField = value;
                this.RaisePropertyChanged("MachineIPAddress");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public int TenantID
        {
            get
            {
                return this.tenantIDField;
            }
            set
            {
                this.tenantIDField = value;
                this.RaisePropertyChanged("TenantID");
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
