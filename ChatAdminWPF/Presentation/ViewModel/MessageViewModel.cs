using ChatAdminWPF.ApplicationLayer.UseCases;
using ChatAdminWPF.DomainLayer;
using System.Collections.ObjectModel;

namespace Presentation.ViewModels
{
    public class MessageViewModel
    {
        private MessageService _messageService;
        public ObservableCollection<Message> Messages { get; }
        public MessageViewModel(MessageService messageService)
        {
            _messageService = messageService;
            Messages = new ObservableCollection<Message>();

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
    }
}
