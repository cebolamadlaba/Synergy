using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PricingConcessionsTool.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;

namespace PricingConcessionsTool.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.All;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
              routeTemplate: "api/api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //// Web API configuration and services
            //var formatters = GlobalConfiguration.Configuration.Formatters;
            //var jsonFormatter = formatters.JsonFormatter;
            //var settings = jsonFormatter.SerializerSettings;

            ////// serialize dates into ISO format with UTC timezone
            //settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            //settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;

            ////settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented,
            //    TypeNameHandling = TypeNameHandling.Objects,
            //    ContractResolver = new CamelCasePropertyNamesContractResolver()
            //};

            config.Services.Insert(typeof(ModelBinderProvider), 0,
           new SimpleModelBinderProvider(typeof(Concession), new JsonBodyModelBinder<Concession>()));
        }


        public class JsonBodyModelBinder<T> : IModelBinder
        {
            public bool BindModel(HttpActionContext actionContext,
                ModelBindingContext bindingContext)
            {
                if (bindingContext.ModelType != typeof(T))
                {
                    return false;
                }

                try
                {
                    var json = ExtractRequestJson(actionContext);

                    bindingContext.Model = DeserializeObjectFromJson(json);

                    return true;
                }
                catch (JsonException exception)
                {
                    bindingContext.ModelState.AddModelError("JsonDeserializationException", exception);

                    return false;
                }

            }

            private static T DeserializeObjectFromJson(string json)
            {
                var binder = new TypeNameSerializationBinder("");

                var obj = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Binder = binder
                });
                return obj;
            }

            private static string ExtractRequestJson(HttpActionContext actionContext)
            {
                var content = actionContext.Request.Content;
                string json = content.ReadAsStringAsync().Result;
                return json;
            }
        }

        public class TypeNameSerializationBinder : SerializationBinder
        {
            public string TypeFormat { get; private set; }

            public TypeNameSerializationBinder(string typeFormat)
            {
                TypeFormat = typeFormat;
            }

            public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
            {
                assemblyName = null;
                typeName = serializedType.Name;
            }

            public override Type BindToType(string assemblyName, string typeName)
            {
                string resolvedTypeName = string.Format(TypeFormat, typeName);

                return Type.GetType(resolvedTypeName, true);
            }
        }
    }
}
