namespace WinFormsAppNET6
{
    using System.Diagnostics;
    using Auth0.OidcClient;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            /*
             * Authenticate user and calls the SampleFunctionApp.HelloFunction AZ function
             * 
             */


            var clientOptions = new Auth0ClientOptions
            {
                Domain = "dev-ni8yqtqf.eu.auth0.com",
                ClientId = "NMuFcFFRtHDWp2biGmyRtrUE67s9pSzN",

                Browser = new WebBrowserBrowser(() => new Form
                {
                    Name = "WebAuthentication",
                    Text = "Authenticating...",
                    Width = 1024,
                    Height = 1024
                })

                // https://community.auth0.com/t/howto-use-the-auth0-oidcclient-winforms-sdk-with-edge-chromium-webview2/57746
                //Browser = new WebBrowserBrowser()      // Use IE (IBrowser contained within Auth0 SDK)
                //Browser = new WebViewBrowser()         // Use Edge (DEFAULT Auth0 setting - Will revert to IE if run as admin)
                //Browser = new WebViewBrowserChromium() // Use Edge Chromium (requires MS Edge Webview2 be installed)
            };

            var client = new Auth0Client(clientOptions);
            clientOptions.PostLogoutRedirectUri = clientOptions.RedirectUri;

            var loginResult = await client.LoginAsync();
            if (loginResult.IsError)
            {
                Debug.WriteLine($"An error occurred during login: {loginResult.Error}");
            }
            else
            {
                Debug.WriteLine($"name: {loginResult.User.FindFirst(c => c.Type == "name")?.Value}");
                Debug.WriteLine($"email: {loginResult.User.FindFirst(c => c.Type == "email")?.Value}");

                foreach (var claim in loginResult.User.Claims)
                {
                    Debug.WriteLine($"{claim.Type} = {claim.Value}");
                }

                var apiClient = new RuntimeApiClient(loginResult.AccessToken);
                await apiClient.HelloFunctionAsync();

                await client.LogoutAsync();
            }
        }
    }
}
