using System;
using System.Text.Json;

namespace BrunoCampiol.CrossCutting.Common.Common
{
    public static class JsonExtension
    {
        public static string ToJson(this object objToJson)
        {
            return JsonSerializer.Serialize(objToJson);
        }

        public static T ToObject<T>(this string jsonStringToObject)
        {

            return JsonSerializer.Deserialize<T>(jsonStringToObject);
        }

        public static (bool IsParseOK, T ParseValue, string ErrorMessage) TryParseToObject<T>(this string jsonStringToObject)
        {
            try
            {
                return (true, JsonSerializer.Deserialize<T>(jsonStringToObject), string.Empty);
            }
            catch (Exception ex)
            {
                return (false, default, ex.AllExceptionMessages());
            }
        }

        //        return new JsonSerializerSettings
        //        {
        //            //NullValueHandling = NullValueHandling.Ignore,
        //            //DefaultValueHandling = DefaultValueHandling.Ignore,
        //            Formatting = Formatting.None,
        //            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        //            MissingMemberHandling = MissingMemberHandling.Ignore,
        //            FloatFormatHandling = FloatFormatHandling.DefaultValue,
        //            FloatParseHandling = FloatParseHandling.Decimal,
        //            DateFormatHandling = DateFormatHandling.IsoDateFormat,
        //            Converters = new[] { new IsoDateTimeConverter { DateTimeStyles = System.Globalization.DateTimeStyles.AssumeLocal } }
        //        };
    }
}