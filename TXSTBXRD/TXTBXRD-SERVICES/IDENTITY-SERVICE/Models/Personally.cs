using System.Collections.Generic;
using IDENTITY_SERVICE.Models.Enum;

namespace IDENTITY_SERVICE.Models
{
    public class Personally
    {
        public string UserId { get; set; }
        public List<Permissions> Permissions { get; set;}
    }
}