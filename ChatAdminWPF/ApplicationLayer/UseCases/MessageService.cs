using ChatAdminWPF.DomainLayer;
using ChatAdminWPF.Infastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAdminWPF.ApplicationLayer.UseCases
{
    public class MessageService : IMessageService
    {
        private readonly MessageRepository _messageRepo;

        public MessageService (MessageRepository messageRepo)
        {
            _messageRepo = messageRepo;
        }

        public List<Message> GetMessages()
        {
            return _messageRepo.GetMessages();
        }
    }
}
