namespace Entities.EmailServiceModels
{
    public class MessageContext
    {
        public MessageContext()
        {

        }

        public MessageContext(string email, string name, string topic, string message)
        {
            EmailTo = email;
            Topic = topic;
            Message = message;
            FullName = name;
        }

        public string EmailTo { get; set; }
        public string Topic { get; set; }
        public string Message { get; set; }
        public string FullName { get; set; }
    }
}
