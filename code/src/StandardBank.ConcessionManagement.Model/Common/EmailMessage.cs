namespace StandardBank.ConcessionManagement.Model.Common
{
    public class EmailMessage
    {
        public string CCEmailAddress { get; set; }

        public string ToEmailAddress { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}