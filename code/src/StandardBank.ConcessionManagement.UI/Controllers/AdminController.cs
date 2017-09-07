using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using StandardBank.ConcessionManagement.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly ILookupTableManager lookupTableManager;
        private readonly IMediator mediator;
        private readonly IUserManager userManager;

        public AdminController(ILookupTableManager lookupTableManager, IMediator mediator , IUserManager userManager)
        {
            this.lookupTableManager = lookupTableManager;
            this.mediator = mediator;
            this.userManager = userManager;
        }
        [Route("user/lookup")]
        public IActionResult GetUserAdminLookupData()
        {
            var model = new UserAdminLookupModel();
            model.Centres = lookupTableManager.GetCentres();
            model.Regions = lookupTableManager.GetRegions();
            model.Roles = lookupTableManager.GetRoles();
            return Ok(model);
        }
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody]UserModel model)
        {
            var id = await mediator.Send(new CreateUser { User = model });
            return Ok(id);
        }
        [HttpPost("users/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody]UserModel model, int id)
        {
            await mediator.Send(new UpdateUser { Model = model });
            return Ok(true);
        }
        [HttpGet("users")]
        public  IActionResult GetUsers()
        {
            var users =  userManager.GetUsers();
            return Ok(users);
        }
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await mediator.Send(new GetUserById {  Id = id });
            return Ok(user);
        }
        [HttpDelete("user/{aNumber}")]
        public async Task<IActionResult> DeleteUser(string aNumber)
        {
            var id =await mediator.Send(new DeleteUser { aNumber = aNumber });
            return Ok(id);
        }

    }
}
