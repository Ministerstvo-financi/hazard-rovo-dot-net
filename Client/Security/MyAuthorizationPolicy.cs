using System;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;

namespace Client.Security
{
    internal class MyAuthorizationPolicy : IAuthorizationPolicy
    {
        private readonly string id;
        private readonly ClaimSet tokenClaims;
        private readonly ClaimSet issuer;

        public MyAuthorizationPolicy(ClaimSet tokenClaims)
        {
            if (tokenClaims == null)
            {
                throw new ArgumentNullException("tokenClaims");
            }
            this.issuer = tokenClaims.Issuer;
            this.tokenClaims = tokenClaims;
            this.id = Guid.NewGuid().ToString();
        }

        public ClaimSet Issuer
        {
            get { return issuer; }
        }

        public string Id
        {
            get { return id; }
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            evaluationContext.AddClaimSet(this, tokenClaims);
            return true;
        }
    }
}
