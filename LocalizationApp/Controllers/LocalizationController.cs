using LocalizationApp.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Linq;

namespace LocalizationApp.Controllers
{
    public class LocalizationController : Controller
    {
        public IActionResult GetLanguages()
        {
            var languages = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();
            return View(languages);
        }

        public IActionResult ChangeLanguage(string language)
        {
            var languages = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();
            var isExist = languages.Select(i=>i.ToString()).Contains(language);
            if (isExist)
            {
                CultureInfo cultureInfo = new CultureInfo(language);
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            }
            return Redirect("/");
        }
    }
}
