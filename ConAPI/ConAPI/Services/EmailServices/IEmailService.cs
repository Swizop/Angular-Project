using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Services.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(string address, string projectTitle, string phone, string email, string name);
    }
}
