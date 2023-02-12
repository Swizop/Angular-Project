using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConAPI.Models.DTOs
{
    public class ReturnLoginDTO
    {
        public string Token { get; set; }
        public List<string> Roles { get; set; }
        public int Id { get; set; }
    }
}
