using ChatAdminWPF.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAdminWPF.ApplicationLayer.UseCases
{
    public interface IMessageRepository
    {
        List<Message> GetMessages();
    }
}
