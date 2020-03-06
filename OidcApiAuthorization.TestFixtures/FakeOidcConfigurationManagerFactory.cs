﻿using OidcApiAuthorization.Abstractions;

namespace OidcApiAuthorization.TestFixtures
{
    public class FakeOidcConfigurationManagerFactory : IOidcConfigurationManagerFactory
    {
        public IOidcConfigurationManager IOidcConfigurationManagerToReturn { get; set; }

        public string IssuerUrlReceived { get; set; }

        public IOidcConfigurationManager New(string issuerUrl)
        {
            IssuerUrlReceived = issuerUrl;

            return IOidcConfigurationManagerToReturn;
        }
    }
}