/**
 * Remove JsonService #16
 * 
 */ 

//using kr.bbon.Xamarin.Forms.Abstractions;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Serialization;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace kr.bbon.Xamarin.Forms.Services
//{
//    public class JsonService: IJsonService
//    {
//        /// <summary>
//        /// 직렬화 합니다.
//        /// </summary>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public string Serialize(object obj)
//        {
//            return JsonConvert.SerializeObject(obj, GetDefaultSettings());
//        }

//        /// <summary>
//        /// 역직렬화합니다.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="json"></param>
//        /// <returns></returns>
//        public T Deserialize<T>(string json)
//        {
//            T obj = JsonConvert.DeserializeObject<T>(json, GetDefaultSettings());

//            return obj;
//        }

//        private JsonSerializerSettings GetDefaultSettings()
//        {
//            return new JsonSerializerSettings
//            {
//                // JSON 객체 속성 이름을 camelCase 형태로 처리합니다.
//                ContractResolver = new CamelCasePropertyNamesContractResolver(),
//                // 열거형 값을 문자열로 저장합니다.
//                //options.Converters.Add(new StringEnumConverter());
//                // 들여쓰기 형식을 지원합니다.
//                Formatting = Formatting.Indented,
//                // null 값을 가진 속성에 대한 직렬화/역직렬화 과정의 에러를 무시합니다.
//                NullValueHandling = NullValueHandling.Ignore,
//                // 정의되지 않은 멤버에 대한 직렬화/역직렬화 과정의 에러를 무시합니다.
//                MissingMemberHandling = MissingMemberHandling.Ignore,

//                DateParseHandling = DateParseHandling.DateTimeOffset,
//                DateFormatHandling = DateFormatHandling.IsoDateFormat,
//                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
//            };
//        }
//    }
//}
