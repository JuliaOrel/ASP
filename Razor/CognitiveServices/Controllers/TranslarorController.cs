using CognitiveServices.Models;
using CognitiveServices.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServices.Controllers
{
    public class TranslarorController : Controller
    {
        private readonly TranslationService _translationService;
        public TranslarorController(TranslationService translationService)
        {
            _translationService = translationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Translate()
        {
            SelectList from = new SelectList(Enum.GetValues(typeof(Languages)));
            MultiSelectList to = new MultiSelectList(Enum.GetValues(typeof(Languages)));

            TranslatorVM model = new TranslatorVM
            {
                From = from,
                To = to
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Translate(TranslatorVM model)
        {
            if(ModelState.IsValid==false)
            {
                model.From = new SelectList(Enum.GetValues(typeof(Languages)), model.FromValue.ToString());
                model.To = new MultiSelectList(Enum.GetValues(typeof(Languages)), model.ToValue.ToString());

                return View(model);
            }
            Languages from = (Languages)model.FromValue;
            Languages[] to = new Languages[model.ToValue.Length];
            for (int i = 0; i < model.ToValue.Length; i++)
            {
                to[i] = (Languages)model.ToValue[i];
            }

            TranslationResult[] results = await _translationService.Translate(text: model.Text, to: to, from: from,
                requestParameters: null);
            model.TranslationResults = results;
            return View(model);
        }
    }
}
