using System.Collections.Generic;

namespace IDENTITY_SERVICE.Models
{
    public class Personally
    {
        public string Login { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }
    }
}