using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwaitWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /* SAMPLE CODE FROM: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/control-flow-in-async-programs */

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            // STEP 1
            // The display lines in the example lead you through the control shifts.  
            resultsTextBox.Text += "ONE:   Entering startButton_Click.\r\n" +
                "           Calling AccessTheWebAsync.\r\n";

            Task<int> getLengthTask = AccessTheWebAsync();

            // STEP 4
            resultsTextBox.Text += "\r\nFOUR:  Back in startButton_Click.\r\n" +
                "           Task getLengthTask is started.\r\n" +
                "           About to await getLengthTask -- no caller to return to.\r\n";

            int contentLength = await getLengthTask;

            // STEP 6
            resultsTextBox.Text += "\r\nSIX:   Back in startButton_Click.\r\n" +
                "           Task getLengthTask is finished.\r\n" +
                "           Result from AccessTheWebAsync is stored in contentLength.\r\n" +
                "           About to display contentLength and exit.\r\n";

            resultsTextBox.Text +=
                String.Format("\r\nLength of the downloaded string: {0}.\r\n", contentLength);
        }

        async Task<int> AccessTheWebAsync()
        {
            // STEP 2
            resultsTextBox.Text += "\r\nTWO:   Entering AccessTheWebAsync.";

            // Declare an HttpClient object.  
            HttpClient client = new HttpClient();

            resultsTextBox.Text += "\r\n          Calling HttpClient.GetStringAsync.\r\n";

            // GetStringAsync returns a Task<string>.   
            Task<string> getStringTask = client.GetStringAsync("http://msdn.microsoft.com");

            // STEP 3
            resultsTextBox.Text += "\r\nTHREE: Back in AccessTheWebAsync.\r\n" +
                "           Task getStringTask is started.";

            // AccessTheWebAsync can continue to work until getStringTask is awaited.  

            resultsTextBox.Text +=
                "\r\n           About to await getStringTask and return a Task<int> to startButton_Click.\r\n";

            // Retrieve the website contents when task is complete.  
            string urlContents = await getStringTask;

            // STEP 5
            resultsTextBox.Text += "\r\nFIVE:  Back in AccessTheWebAsync." +
                "\r\n           Task getStringTask is complete." +
                "\r\n           Processing the return statement." +
                "\r\n           Exiting from AccessTheWebAsync.\r\n";

            return urlContents.Length;
        }
    }
}
