using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    [Route("api/[controller]")]
    public class CommonController : Controller
    {
        private readonly ILookupTableManager _lookupTableManager;

        public CommonController(ILookupTableManager lookupTableManager)
        {
            this._lookupTableManager = lookupTableManager;
        }

        [Route("RoleSubRoles")]
        public IActionResult RoleSubRoles()
        {
            return Ok(this._lookupTableManager.GetRoleSubRole());
        }

        [Route("RoleSubRoles/{roleId}")]
        public IActionResult RoleSubRoles(int roleId)
        {
            return Ok(this._lookupTableManager.GetRoleSubRole(roleId));
        }
    }
}
