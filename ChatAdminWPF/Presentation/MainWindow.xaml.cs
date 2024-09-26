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
        public MainWindow()
        {
            InitializeComponent();

            // Set the file path to your XML file
            var repository = new MessageRepository("C:\\Users\\david\\Source\\Repos\\ChatAdminWPF\\ChatAdminWPF\\Persistance\\happy.xml");
            var service = new MessageService(repository);
            var viewModel = new MessageViewModel(service);

            // Set the DataContext to the ViewModel for data binding
            DataContext = viewModel;
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}