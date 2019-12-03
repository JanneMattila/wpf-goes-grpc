using Grpc.Net.Client;
using System.Windows;
using Shared;

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
            var channel = GrpcChannel.ForAddress(BackendAddress.Text);
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
