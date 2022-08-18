using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IDENTITY_SERVICE.Services;
using IDENTITY_SERVICE.Models;

namespace IDENTITY_SERVICE.Controllers
{
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> _logger;

        public IdentityController(ILogger<IdentityController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Personal Get()
        {
            return  IdentityService.AuthenticationResult();
        }

        [HttpPost]
        public Personal IdentificationUser(Personal unknownUser){
            return unknownUser;
        }
    }
}