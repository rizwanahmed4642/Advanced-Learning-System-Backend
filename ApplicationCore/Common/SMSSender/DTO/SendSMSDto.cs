using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSender.DTO
{
    public class SendSMSDto
    {
        public int Id { get; set; }
        public string SmssessionId { get; set; }
        public string MessageId { get; set; }
        public string Fkid { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public string StatusResponse { get; set; }
        public string Mask { get; set; }
        public DateTime? SentDate { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
    }
}
