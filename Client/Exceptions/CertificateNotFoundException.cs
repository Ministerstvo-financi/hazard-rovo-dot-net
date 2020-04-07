﻿using System;
using System.Runtime.Serialization;

namespace Client.Exceptions
{
    [Serializable]
    public class CertificateNotFoundException : Exception
    {
        public CertificateNotFoundException()
        {
        }

        public CertificateNotFoundException(string message) : base(message)
        {
        }

        public CertificateNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CertificateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
