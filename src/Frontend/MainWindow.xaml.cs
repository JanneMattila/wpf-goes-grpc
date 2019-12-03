using Grpc.Net.Client;
using System.Windows;
using Shared;
using System.Net.Http;

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
#if DEBUG
            // From https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.1
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            var httpClient = new HttpClient(handler);
#else
            var httpClient = new HttpClient();
#endif

            var channel = GrpcChannel.ForAddress(BackendAddress.Text,
                new GrpcChannelOptions 
                { 
                    HttpClient = httpClient 
                });
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest
                {
                    Name = "GreeterClient"
                });

            SayHelloResponse.Text = reply.Message;
        }
    }
}
