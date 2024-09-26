using ChatAdminWPF.ApplicationLayer.UseCases;
using ChatAdminWPF.DomainLayer;
using System.Collections.ObjectModel;

namespace Presentation.ViewModels
{
    public class MessageViewModel
    {
        private readonly MessageService _messageService;

        public ObservableCollection<Message> Messages { get; set; }

        public MessageViewModel(MessageService messageService)
        {
            _messageService = messageService;
            Messages = new ObservableCollection<Message>();

            LoadMessages();
        }

        public void LoadMessages()
        {
            var messages = _messageService.GetMessages();
            Messages.Clear();
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }
    }
}
