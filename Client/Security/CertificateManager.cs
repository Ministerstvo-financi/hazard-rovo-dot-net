using Client.Exceptions;
using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Client.Security
{
    public class CertificateManager
    {
        MyClientCredentials Credentials { get; set; }

        public CertificateManager(MyClientCredentials credentials)
        {
            Credentials = credentials;
        }

        public static CertificateManager For(MyClientCredentials credentials)
        {
            return new CertificateManager(credentials);
        }

        public void SetUpCertificates()
        {
            // store certifikatu umisteni Local Machine
            var store = new X509Store(StoreLocation.LocalMachine);
            try
            {
                // pouze pro cteni certifikatu
                store.Open(OpenFlags.ReadOnly);

                // kolekce certifikatu
                var certCollection = store.Certificates;

                // vytazeni certifikatu pro Service Encryption
                var serviceSigningCertificateSubjectName = ConfigurationManager.AppSettings.Get("serviceSigningCertificateSubjectName").ToString();
                var serviceSigningCertificat = certCollection.Find(X509FindType.FindBySubjectName, serviceSigningCertificateSubjectName, false);
                CheckCertificateWasFound(serviceSigningCertificat, serviceSigningCertificateSubjectName);

                // vytazeni certifikatu pro Client Signing
                var clientSigningCertificateSubjectName = ConfigurationManager.AppSettings.Get("clientSigningCertificateSubjectName").ToString();
                var clientSigningCertificate = certCollection.Find(X509FindType.FindBySubjectName, clientSigningCertificateSubjectName, false);
                CheckCertificateWasFound(clientSigningCertificate, clientSigningCertificateSubjectName);

                // nastaveni certifikatu pro potreby komunikace
                Credentials.ClientSigningCertificate = clientSigningCertificate[0];
                Credentials.ClientEncryptingCertificate = serviceSigningCertificat[0];
                Credentials.ServiceSigningCertificate = serviceSigningCertificat[0];
                Credentials.ServiceEncryptingCertificate = serviceSigningCertificat[0];
                Credentials.ServiceCertificate.DefaultCertificate = serviceSigningCertificat[0];

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Nastala chyba při nastavování certifikátů. Chyba: {ex.Message}");
                throw;
            }
            finally
            {
                store.Close();
            }
        }

        private void CheckCertificateWasFound(X509Certificate2Collection certificateCollection, string certSubjectName)
        {
            if (certificateCollection is null || certificateCollection.Count == 0)
            {
                throw new CertificateNotFoundException($"Certifikát {certSubjectName} nebyl nalezen.");
            }
        }
    }
}
