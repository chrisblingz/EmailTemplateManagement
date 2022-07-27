using ETM.App.Models;
using ETM.Entity;
using ETM.Service.Implementation;
using ETM.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ETM.App.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IEmailLogService _emailLogService;
        private readonly IUtilityService _utilityService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;

        public UserController(ILogger<UserController> logger, IUserService userService, IEmailLogService emailLogService,
            IUtilityService utilityService, IEmailTemplateService emailTemplateService, IEmailService emailService)
        {
            _logger = logger;
            _userService = userService;
            _emailLogService = emailLogService;
            _utilityService = utilityService;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            try
            {
                var users = _userService.GetUsers().Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FirstName = x.Firstname,
                    Middlename = x.Middlename,
                    Lastname = x.Lastname,
                    Passcode = x.Passcode,
                    PhoneNumber = x.PhoneNumber,
                    Username = x.Username,
                    Channel = (Channels)x.Channel

                }).ToList();
                return View(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + " : " + ex.StackTrace);
                return View(new UserViewModel());
            }

        }

        public IActionResult Create()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (_userService.UserExists(model.Username))
                {
                    ModelState.AddModelError("User ", "User already exist");
                    return View(model);
                }


                var user = new User();
                user.Username = model.Username;
                user.Email = model.Email;
                user.Firstname = model.FirstName;
                user.Middlename = model.Middlename;
                user.Lastname = model.Lastname;
                user.PhoneNumber = model.PhoneNumber;
                user.Channel = (int)model.Channel;
                user.Passcode = _utilityService.Encrypt(model.Passcode);

                bool status = await _userService.CreateUser(user);
                if (!status)
                {
                    ModelState.AddModelError("User ", "Unable to add");
                    return View(model);
                }

                //Send Mail
                var template = _emailTemplateService.GetEmailTemplate((int)model.Channel);

                var email = new EmailLog();
                email.EmailTemplateId = template.Id;
                email.Receiver = model.Email;
                email.Cc = "";
                email.Bcc = "";
                email.Subject = template.Subject;

                email.MessageBody = template.Template.Replace("[[Firstname]]", model.FirstName)
                    .Replace("[[PhoneNumber]]", model.PhoneNumber).Replace("[[Fullname]]", model.Fullname)
                    .Replace("[[Passcode]]", model.Passcode);

                await _emailService.SendEmailAsync(email);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ":" + ex.StackTrace);
                return View(model);
            }
        }

    }
}
