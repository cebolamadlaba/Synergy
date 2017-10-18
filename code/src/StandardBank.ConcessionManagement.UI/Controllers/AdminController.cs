using MediatR;
using Microsoft.AspNetCore.Mvc;
using StandardBank.ConcessionManagement.BusinessLogic.Features.Administration;
using StandardBank.ConcessionManagement.Interface.BusinessLogic;
using StandardBank.ConcessionManagement.Model.UserInterface;
using System.Threading.Tasks;
using StandardBank.ConcessionManagement.Model.UserInterface.Administration;
using StandardBank.ConcessionManagement.UI.Helpers.Interface;

namespace StandardBank.ConcessionManagement.UI.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly ILookupTableManager lookupTableManager;
        private readonly IMediator mediator;
        private readonly IUserManager userManager;
        private readonly ISiteHelper _siteHelper;

        public AdminController(ILookupTableManager lookupTableManager, IMediator mediator , IUserManager userManager, ISiteHelper siteHelper)
        {
            this.lookupTableManager = lookupTableManager;
            this.mediator = mediator;
            this.userManager = userManager;
            _siteHelper = siteHelper;
        }
        [Route("user/lookup")]
        public IActionResult GetUserAdminLookupData()
        {
            var model = new UserAdminLookupModel
            {
                Centres = lookupTableManager.GetCentres(),
                Regions = lookupTableManager.GetRegions(),
                Roles = lookupTableManager.GetRoles()
            };
            return Ok(model);
        }
        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody]User model)
        {
            var id = await mediator.Send(new CreateUser {User = model, CurrentUser = _siteHelper.LoggedInUser(this)});

            return Ok(id);
        }
        [HttpPost("users/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody]User model, int id)
        {
            await mediator.Send(new UpdateUser { Model = model, CurrentUser = _siteHelper.LoggedInUser(this) });
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
            var id = await mediator.Send(new DeleteUser
            {
                aNumber = aNumber,
                CurrentUser = _siteHelper.LoggedInUser(this)
            });

            return Ok(id);
        }

    }
}
