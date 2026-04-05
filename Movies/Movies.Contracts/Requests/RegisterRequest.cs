using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Requests
{
    public class RegisterRequest
    {
        [DataType(DataType.EmailAddress)]
        public required string Username { get; set; }
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public required string[] Roles { get; set; }
    }
}
