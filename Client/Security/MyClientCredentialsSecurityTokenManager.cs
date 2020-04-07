using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security.Tokens;

namespace Client.Security
{
    internal class MyClientCredentialsSecurityTokenManager :
    ClientCredentialsSecurityTokenManager
    {
        private readonly MyClientCredentials credentials;

        public MyClientCredentialsSecurityTokenManager( MyClientCredentials credentials) : base(credentials)
        {
            this.credentials = credentials;
        }

        public override SecurityTokenProvider CreateSecurityTokenProvider(
            SecurityTokenRequirement requirement)
        {
            SecurityTokenProvider result = null;
            if (requirement.TokenType == SecurityTokenTypes.X509Certificate)
            {
                MessageDirection direction = requirement.GetProperty
                    <MessageDirection>(ServiceModelSecurityTokenRequirement.
                    MessageDirectionProperty);
                if (direction == MessageDirection.Output)
                {
                    if (requirement.KeyUsage == SecurityKeyUsage.Signature)
                    {
                        result = new X509SecurityTokenProvider(
                            this.credentials.ClientSigningCertificate);
                    }
                    else
                    {
                        result = new X509SecurityTokenProvider(this.credentials.
                            ServiceEncryptingCertificate);
                    }
                }
                else
                {
                    if (requirement.KeyUsage == SecurityKeyUsage.Signature)
                    {
                        result = new X509SecurityTokenProvider(this.
                            credentials.ServiceSigningCertificate);
                    }
                    else
                    {
                        result = new X509SecurityTokenProvider(credentials.
                            ClientEncryptingCertificate);
                    }
                }
            }
            else
            {
                result = base.CreateSecurityTokenProvider(requirement);
            }

            return result;
        }

        public override SecurityTokenAuthenticator
            CreateSecurityTokenAuthenticator(SecurityTokenRequirement
            tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver)
        {
            SecurityTokenAuthenticator result;
            if (tokenRequirement.TokenType == SecurityTokenTypes.X509Certificate)
            {
                outOfBandTokenResolver = null;
                result = new MySecurityTokenAuthenticator(credentials);
            }
            else
            {
                result = base.CreateSecurityTokenAuthenticator(tokenRequirement, out outOfBandTokenResolver);
            }

            return result;
        }

    }
}
