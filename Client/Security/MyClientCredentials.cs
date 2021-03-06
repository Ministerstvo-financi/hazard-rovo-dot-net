﻿using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;

namespace Client.Security
{
    public class MyClientCredentials : ClientCredentials
    {
        private X509Certificate2 clientSigningCert;
        private X509Certificate2 clientEncryptingCert;
        private X509Certificate2 serviceSigningCert;
        private X509Certificate2 serviceEncryptingCert;

        public MyClientCredentials()
        {
        }

        protected MyClientCredentials(MyClientCredentials other)
            : base(other)
        {
            this.clientEncryptingCert = other.clientEncryptingCert;
            this.clientSigningCert = other.clientSigningCert;
            this.serviceEncryptingCert = other.serviceEncryptingCert;
            this.serviceSigningCert = other.serviceSigningCert;
        }

        public X509Certificate2 ClientSigningCertificate
        {
            get
            {
                return this.clientSigningCert;
            }
            set
            {
                this.clientSigningCert = value;
            }
        }

        public X509Certificate2 ClientEncryptingCertificate
        {
            get
            {
                return this.clientEncryptingCert;
            }
            set
            {
                this.clientEncryptingCert = value;
            }
        }

        public X509Certificate2 ServiceSigningCertificate
        {
            get
            {
                return this.serviceSigningCert;
            }
            set
            {
                this.serviceSigningCert = value;
            }
        }

        public X509Certificate2 ServiceEncryptingCertificate
        {
            get
            {
                return this.serviceEncryptingCert;
            }
            set
            {
                this.serviceEncryptingCert = value;
            }
        }

        public override SecurityTokenManager CreateSecurityTokenManager()
        {
            return new MyClientCredentialsSecurityTokenManager(this);
        }

        protected override ClientCredentials CloneCore()
        {
            return new MyClientCredentials(this);
        }
    }
}
