using ChatAdminWPF.ApplicationLayer.UseCases;
using Presentation.ViewModels;
using ChatAdminWPF.Infastructure.Repositories;
using System.Text;
using System.Windows;
using System.IO;
using System.Linq;
using System.Windows.Controls;



namespace ChatAdminWPF.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SentimentAnalysis _sentimentAnalysis;
        private MessageService _messageService;
        private MessageViewModel _viewModel;
        private Dictionary<string, string> _xmlFiles = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();

            // Filepath for each sentiment txt file
            var sentimentFilePaths = new Dictionary<string, string>
            {
                { "happy", @"..\\..\\..\\Persistance\\HappyList.txt" },
                { "sad", @"..\\..\\..\\Persistance\\SadList.txt" },
                { "angry", @"..\\..\\..\\Persistance\\AngryList.txt" }
            };

            _sentimentAnalysis = new SentimentAnalysis(sentimentFilePaths);
            _viewModel = new MessageViewModel(null);
            DataContext = _viewModel;

            // Load available XML files into ChatSelector
            LoadXmlFileNames();

            // Initialize with the first XML file if possible 
            if (ChatSelector.Items.Count > 0)
            {
                ChatSelector.SelectedIndex = 0;
                ChatSelector_SelectionChanged(null, null);
            }
        }

        
        private void LoadXmlFileNames()
        {
            // Directory containing files
            string xmlDirectory = @"..\\..\\..\\Persistance";

            if (Directory.Exists(xmlDirectory))
            {
                var xmlFiles = Directory.GetFiles(xmlDirectory, "*.xml");

                foreach (var filePath in xmlFiles)
                {
                    var fileName = Path.GetFileName(filePath);
                    var displayName = Path.GetFileNameWithoutExtension(filePath);

                    _xmlFiles.Add(displayName, filePath);
                    ChatSelector.Items.Add(displayName);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Directory not found: {xmlDirectory}");
            }
        }

        // Event handler for ChatSelector selection change
        private void ChatSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedDisplayName = ChatSelector.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedDisplayName) && _xmlFiles.ContainsKey(selectedDisplayName))
            {
                string xmlFilePath = _xmlFiles[selectedDisplayName];

                // Load messages from the selected XML file
                var repository = new MessageRepository(xmlFilePath);
                _messageService = new MessageService(repository);

                
                _viewModel.LoadMessages(_messageService);

                // Re-run sentiment analysis
                PerformSentimentAnalysis();
            }
        }


        // Method to perform sentiment analysis
        private void PerformSentimentAnalysis()
        {
            // Convert ObservableCollection to List
            var messages = _viewModel.Messages.ToList();

            var sentimentCounts = _sentimentAnalysis.AnalyseSentiment(messages);
            var overallSentiment = _sentimentAnalysis.DetermineOverallSentiment(sentimentCounts);

            var sentimentResult = new StringBuilder();
            sentimentResult.AppendLine("Sentiment Counts:");
            foreach (var sentiment in sentimentCounts)
            {
                sentimentResult.AppendLine($"{sentiment.Key}: {sentiment.Value}");
            }
            sentimentResult.AppendLine($"Overall Sentiment: {overallSentiment}");

            SentimentLabel.Content = "Sentiment: " + overallSentiment;
        }



        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
           
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the DeleteSelectedMessage method in the ViewModel
            _viewModel.DeleteMessageCommand.Execute(null);
        }

    }
}