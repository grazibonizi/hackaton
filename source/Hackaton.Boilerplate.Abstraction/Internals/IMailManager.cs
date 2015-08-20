using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.Abstraction.Internals
{
    public interface IMailManager
    {
        void Send(
            string from, 
            string to, 
            string subject, 
            string body, 
            Attachment attach
        );

        void Send(
            string from,
            string[] to,
            string subject,
            string body,
            Attachment attach
        );
    }
}
