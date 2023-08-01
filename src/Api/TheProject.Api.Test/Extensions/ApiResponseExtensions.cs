using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheProject.Domain.Helpers;

namespace TheProject.Api.Test.Extensions
{
    public static class ApiResponseExtensions
    {
        public static JsonSerializerSettings GetJsonSerializerOptions()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new NonPublicPropertiesResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };
        }

        public static T SerializeApiResponseTo<T>(this string responseText)
        {
            var dynObj = (JObject)JsonConvert.DeserializeObject(responseText, GetJsonSerializerOptions());
            return dynObj.GetValue("object").ToObject<T>();
        }
    }
}
