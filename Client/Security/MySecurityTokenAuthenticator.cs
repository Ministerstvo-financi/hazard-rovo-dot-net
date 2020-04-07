using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace Client.Security
{
    internal class MySecurityTokenAuthenticator : SecurityTokenAuthenticator
    {
        private readonly MyClientCredentials credentials;

        public MySecurityTokenAuthenticator(MyClientCredentials credentials)
        {
            this.credentials = credentials;
        }

        protected override bool CanValidateTokenCore(SecurityToken token)
        {
            // Kontrola zdali je certifikat spravneho typu ktery chceme validovat
            return (token is X509SecurityToken);
        }

        protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
        {
            var x509Token = token as X509SecurityToken;
            var serviceEncryptingCertificateSubjectName = ConfigurationManager.AppSettings.Get("serviceDns").ToString();

            // Logika validace certifikatu

            if (x509Token.Certificate.Subject == credentials.ClientEncryptingCertificate.Subject)
            {
                DefaultClaimSet x509ClaimSet = new DefaultClaimSet(ClaimSet.System,
                    new Claim(ClaimTypes.Dns, serviceEncryptingCertificateSubjectName, Rights.PossessProperty));
                List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>(1);
                policies.Add(new MyAuthorizationPolicy(x509ClaimSet));
                return policies.AsReadOnly();
            }

            if (x509Token.Certificate.Subject == credentials.ServiceSigningCertificate.Subject)
            {
                X509CertificateClaimSet x509ClaimSet = new X509CertificateClaimSet(x509Token.Certificate);
                List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>(1);
                policies.Add(new MyAuthorizationPolicy(x509ClaimSet));
                return policies.AsReadOnly();
            }

            if (x509Token.Certificate.Subject == credentials.ClientSigningCertificate.Subject)
            {
                DefaultClaimSet x509ClaimSet = new DefaultClaimSet(ClaimSet.System,
                    new Claim(ClaimTypes.Name, serviceEncryptingCertificateSubjectName, Rights.PossessProperty));
                List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>(1);
                policies.Add(new MyAuthorizationPolicy(x509ClaimSet));
                return policies.AsReadOnly();

            }

            throw new SecurityTokenValidationException();
        }
    }
}
