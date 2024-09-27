using ChatAdminWPF.ApplicationLayer.UseCases;
using Presentation.ViewModels;
using ChatAdminWPF.Infastructure.Repositories;
using System.Text;
using System.Windows;



namespace ChatAdminWPF.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SentimentAnalysis _sentimentAnalysis;
        private readonly MessageService _messageService;

        public MainWindow()
        {
            InitializeComponent();

            //Filepath for each sentiment txt file
            var sentimentFilePaths = new Dictionary<string, string>
            {
                { "happy", @"..\\..\\..\\Persistance\\HappyList.txt" },
                { "sad", @"..\\..\\..\\Persistance\\SadList.txt" },
                { "angry", @"..\\..\\..\\Persistance\\AngryList.txt" }
            };

           
            _sentimentAnalysis = new SentimentAnalysis(sentimentFilePaths);

            // Filepath to XML
            var repository = new MessageRepository(@"..\\..\\..\\Persistance\\happy.xml");
            _messageService = new MessageService(repository);
            var viewModel = new MessageViewModel(_messageService);

            // Set the DataContext to the ViewModel for data binding
            DataContext = viewModel;

            PerformSentimentAnalysis();
        }

        // Method to perform sentiment analysis
        private void PerformSentimentAnalysis()
        {
            // Get messages from the MessageService
            var messages = _messageService.GetMessages();

       
            var sentimentCounts = _sentimentAnalysis.AnalyseSentiment(messages);
            var overallSentiment = _sentimentAnalysis.DetermineOverallSentiment(sentimentCounts);

           

            var sentimentResult = new StringBuilder();
            sentimentResult.AppendLine("Sentiment Counts:");
            foreach (var sentiment in sentimentCounts)
            {
                sentimentResult.AppendLine($"{sentiment.Key}: {sentiment.Value}");
            }
            sentimentResult.AppendLine($"Overall Sentiment: {overallSentiment}");

            // Display the sentiment result in label
            SentimentLabel.Content = "Sentiment: " + overallSentiment;
        }

       
        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
           
        }
    }
}