using ChatAdminWPF.ApplicationLayer.UseCases;
using ChatAdminWPF.DomainLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChatAdminWPF.Infastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly string _filepath;

        public MessageRepository(string filepath)
        {
            _filepath = filepath;
        }

        public List<Message> GetMessages()
        {
            // Return an empty list if file does not exist
            if (!System.IO.File.Exists(_filepath))
            {
                return new List<Message>();
            }

            // Load XML file
            XDocument doc = XDocument.Load(_filepath);


            // LINQ to read XML
            var messages = doc.Descendants("message")
                                .Select(m => new Message
                                {
                                    Content = m.Value
                                })
                                .ToList();

            return messages;
        }

    }
}
