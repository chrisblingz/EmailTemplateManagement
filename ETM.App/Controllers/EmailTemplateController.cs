using ETM.App.Models;
using ETM.Entity;
using ETM.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ETM.App.Controllers
{
    public class EmailTemplateController : Controller
    {
        private readonly IEmailTemplateService _emailTemplateService;

        public EmailTemplateController(IEmailTemplateService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }
        public IActionResult Index()
        {
            var emailTemplates = _emailTemplateService.GetEmailTemplates().Select(x => new EmailTemplateViewModel()
            {
                Code = x.Code,
                Id = x.Id,
                Name = x.Name,
                Subject = x.Subject,
                Template = x.Template,
            }).ToList();
            return View(emailTemplates);
        }

        public IActionResult Create()
        {
            return View(new EmailTemplateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmailTemplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (_emailTemplateService.EmailTemplateExists(model.Code))
                {
                    ModelState.AddModelError("Email Tamplate", "Email Taplate Code already exist");
                    return View(model);
                }


                var template = new EmailTemplate()
                {
                    Name = model.Name,
                    Code = model.Code,
                    Subject = model.Subject,
                    Template = model.Template
                };

                bool status = await _emailTemplateService.CreateEmailTemplate(template);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult Edit(int Id)
        {
            var data = _emailTemplateService.GetEmailTemplate(Id);
            if(data == null)
            {
                ModelState.AddModelError("Email Tamplate", "Email Taplate not found");
                return RedirectToAction(nameof(Index));
            }

            var template = new EmailTemplateViewModel()
            {
                Id = data.Id,
                Name = data.Name,
                Subject = data.Subject,
                Code = data.Code,
                Template = data.Template

            };

            return View(template);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Edit(EmailTemplateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                if (_emailTemplateService.EmailTemplateExists(model.Code))
                {
                    ModelState.AddModelError("Email Tamplate", "Email Taplate Code already exist");
                    return View(model);
                }
                if(model.Id == 0)
                {
                    ModelState.AddModelError("Email Tamplate", "Email Taplate does already exist");
                    return View(model);
                }

                if (!_emailTemplateService.EmailTemplateExists(model.Id))
                {
                    ModelState.AddModelError("Email Tamplate", "Email Taplate does already exist");
                    return View(model);
                }


                var template = new EmailTemplate()
                {
                    Name = model.Name,
                    Code = model.Code,
                    Subject = model.Subject,
                    Template = model.Template
                };

                bool status = await _emailTemplateService.UpdateEmailTemplate(template);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
