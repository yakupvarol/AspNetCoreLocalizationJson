﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreLocalizationJson.Helper.Translation
{
    public class TranslationTransformer : DynamicRouteValueTransformer
    {
        private readonly TranslationDatabase _translationDatabase;

        public TranslationTransformer(TranslationDatabase translationDatabase)
        {
            _translationDatabase = translationDatabase;
        }

        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (!values.ContainsKey("lang") || !values.ContainsKey("controller") || !values.ContainsKey("action")) return values;

            var language = (string)values["lang"];
            var controller = await _translationDatabase.Resolve(language, (string)values["controller"]);
            if (controller == null) return values;
            values["controller"] = controller;

            var action = await _translationDatabase.Resolve(language, (string)values["action"]);
            if (action == null) return values;
            values["action"] = action;

            return values;
        }
    }
}
