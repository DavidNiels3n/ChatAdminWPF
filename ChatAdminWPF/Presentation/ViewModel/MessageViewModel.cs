using ChatAdminWPF.ApplicationLayer.UseCases;
using ChatAdminWPF.DomainLayer;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ChatAdminWPF.Presentation;


namespace Presentation.ViewModels
{
    public class MessageViewModel : INotifyPropertyChanged

    {
        private MessageService _messageService;
        public ObservableCollection<Message> Messages { get; }


        private Message _selectedMessage;
        public Message SelectedMessage
        {
            get { return _selectedMessage; }
            set
            {
                if (_selectedMessage != value)
                {
                    _selectedMessage = value;
                    OnPropertyChanged(nameof(SelectedMessage));
                    DeleteMessageCommand.RaiseCanExecuteChanged();
                }
            }
        }



        public RelayCommand DeleteMessageCommand { get; }
        public MessageViewModel(MessageService messageService)
        {
            _messageService = messageService;
            Messages = new ObservableCollection<Message>();
            DeleteMessageCommand = new RelayCommand(DeleteSelectedMessage, CanDeleteMessage);


            LoadMessages();
        }

        public void LoadMessages(MessageService messageService = null)
        {
            if (messageService != null)
            {
                _messageService = messageService;
            }

            if (_messageService != null)
            {
                var messages = _messageService.GetMessages();

         
                Messages.Clear();
                foreach (var message in messages)
                {
                    Messages.Add(message);
                }
            }
            else
            {
                Messages.Clear();
            }
        }

        private bool CanDeleteMessage()
        {
            return SelectedMessage != null;
        }


        public void DeleteSelectedMessage()
        {
            if (SelectedMessage != null)
            {
                _messageService.DeleteMessage(SelectedMessage.Content);
                Messages.Remove(SelectedMessage);
                SelectedMessage = null;
            }
        }


      

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
