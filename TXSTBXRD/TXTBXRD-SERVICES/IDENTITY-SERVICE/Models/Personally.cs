using System.Collections.Generic;

namespace IDENTITY_SERVICE.Models
{
    public class Personally
    {
        public string UserName { get; set; }
        public Dictionary<string, bool> Permissions { get; set;}
    }
}