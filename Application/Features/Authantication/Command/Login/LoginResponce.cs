using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authantication.Command.Login
{
    public class LoginResponce
    {
        public bool Success { get; set; }
        public string? Token {  get; set; }
        public string? RefreshToken { get; set; }
        public string Message {  get; set; }
        public LoginResponce(bool success, string? token,string? refreshtoken,string message)
        {
            Success = success;
            Message = message;
            Token = token;
            RefreshToken = refreshtoken;
        }
    }
}
