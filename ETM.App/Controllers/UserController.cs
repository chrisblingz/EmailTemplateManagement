using ETM.App.Models;
using ETM.Service.Implementation;
using ETM.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ETM.App.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailLogService _emailLogService;

        public UserController(IUserService userService, IEmailLogService emailLogService)
        {
            _userService = userService;
            _emailLogService = emailLogService;
        }
        public IActionResult Index()
        {
            var users = _userService.GetUsers().Select(x => new UserViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                FullName = x.FullName,
                Passcode = x.Passcode,
                PhoneNumber = x.PhoneNumber,
                Username = x.Username

            }).ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View(new UserViewModel());
        }
    }
}
