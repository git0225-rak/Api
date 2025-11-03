using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DTO
{
    public partial class ValidateUserLogin : object, System.ComponentModel.INotifyPropertyChanged
    {

        private int apps_MST_Application_IDField;

        private int apps_MST_User_IDField;

        private string eMailField;

        private string passwordField;

        private string clientCookieIdentifierField;

        private string clientMACField;

        private string sessionIdentifierField;

        private string lastRequestTimestampField;

        private string erroCodeField;

        private string subscriptionEndDateField;

        private int accountIdField;
        //private int UserID;
        //private string  DeviceID;
        //private string LoginTimeStamp;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int Apps_MST_Application_ID
        {
            get
            {
                return this.apps_MST_Application_IDField;
            }
            set
            {
                this.apps_MST_Application_IDField = value;
                this.RaisePropertyChanged("Apps_MST_Application_ID");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int Apps_MST_User_ID
        {
            get
            {
                return this.apps_MST_User_IDField;
            }
            set
            {
                this.apps_MST_User_IDField = value;
                this.RaisePropertyChanged("Apps_MST_User_ID");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string EMail
        {
            get
            {
                return this.eMailField;
            }
            set
            {
                this.eMailField = value;
                this.RaisePropertyChanged("EMail");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
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
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string ClientCookieIdentifier
        {
            get
            {
                return this.clientCookieIdentifierField;
            }
            set
            {
                this.clientCookieIdentifierField = value;
                this.RaisePropertyChanged("ClientCookieIdentifier");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string ClientMAC
        {
            get
            {
                return this.clientMACField;
            }
            set
            {
                this.clientMACField = value;
                this.RaisePropertyChanged("ClientMAC");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string SessionIdentifier
        {
            get
            {
                return this.sessionIdentifierField;
            }
            set
            {
                this.sessionIdentifierField = value;
                this.RaisePropertyChanged("SessionIdentifier");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string LastRequestTimestamp
        {
            get
            {
                return this.lastRequestTimestampField;
            }
            set
            {
                this.lastRequestTimestampField = value;
                this.RaisePropertyChanged("LastRequestTimestamp");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string ErroCode
        {
            get
            {
                return this.erroCodeField;
            }
            set
            {
                this.erroCodeField = value;
                this.RaisePropertyChanged("ErroCode");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string SubscriptionEndDate
        {
            get
            {
                return this.subscriptionEndDateField;
            }
            set
            {
                this.subscriptionEndDateField = value;
                this.RaisePropertyChanged("SubscriptionEndDate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public int AccountId
        {
            get
            {
                return this.accountIdField;
            }
            set
            {
                this.accountIdField = value;
                this.RaisePropertyChanged("AccountId");
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public string DeviceID
        {
            get
            {
                return this.DeviceID;
            }
            set
            {
                this.DeviceID = value;
                this.RaisePropertyChanged("DeviceID");
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public int UserID
        {
            get
            {
                return this.UserID;
            }
            set
            {
                this.UserID = value;
                this.RaisePropertyChanged("UserID");
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public string LoginTimeStamp
        {
            get
            {
                return this.LoginTimeStamp;
            }
            set
            {
                this.LoginTimeStamp = value;
                this.RaisePropertyChanged("LoginTimeStamp");
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public int IsForceLogin
        {
            get
            {
                return this.IsForceLogin;
            }
            set
            {
                this.IsForceLogin = value;
                this.RaisePropertyChanged("IsForceLogin");
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
