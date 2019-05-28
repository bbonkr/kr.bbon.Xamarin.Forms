using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace kr.bbon.Xamarin.Forms
{
    public class AppCenterProperties
    {
        private const int MAX_ITEMS_COUNT = 20;
        private const int MAX_CHARACTERS = 125;

        private const string MESSAGE_KEY = "message";
        private const string TYPE_KEY = "type";
        private const string METHOD_KEY = "method";

        /// <summary>
        /// AppCenterProperties 클래스의 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="isKeyIgnoreCase">키 대소문자 무시여부</param>
        public AppCenterProperties(bool isKeyIgnoreCase)
        {
            this.isKeyIgnoreCase = isKeyIgnoreCase;
            internalDictionary = new Dictionary<string, string>();
        }

        /// <summary>
        /// AppCenterProperties 클래스의 인스턴스를 초기화합니다.
        /// <para>키는 대소문자를 구분하지 않습니다.</para>
        /// </summary>
        public AppCenterProperties()
            : this(true)
        {

        }

        public static AppCenterProperties Create(bool isKeyIgnoreCase)
        {
            return new AppCenterProperties(isKeyIgnoreCase);
        }

        public static AppCenterProperties Create()
        {
            return new AppCenterProperties();
        }

        /// <summary>
        /// 키에 해당하는 값을 나타냅니다.
        /// <para>키에 해당하는 값이 없으면 null 입니다.</para>
        /// </summary>
        /// <param name="key">키</param>
        /// <returns></returns>
        public string this[string key]
        {
            get => TryGet(key);
            set => AddOrUpdateProperty(key, value);
        }

        /// <summary>
        /// 키에 해당하는 항목이 있는지 확인합니다.
        /// </summary>
        /// <param name="key">키</param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            var _key = NormalizeKey(key);

            return internalDictionary.ContainsKey(_key);
        }

        /// <summary>
        /// 컬렉션이 비었는지를 확인합니다.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => internalDictionary.Count == 0;


        /// <summary>
        /// 항목을 추가하거나, 갱신합니다.
        /// <para>키로 항목을 검색해서 없으면 추가하고 있으면 갱신합니다.</para>
        /// </summary>
        /// <param name="key">키</param>
        /// <param name="value">값</param>
        /// <returns></returns>
        public AppCenterProperties AddOrUpdateProperty(string key, string value)
        {
            var _key = NormalizeKey(key);
            var _value = AdjustStringValue(value);

            if (internalDictionary.ContainsKey(_key))
            {
                internalDictionary[_key] = _value;
            }
            else
            {
                internalDictionary.Add(_key, _value);
            }

            return this;
        }

        /// <summary>
        /// 항목을 추가합니다.
        /// <para>키에 해당하는 항목이 있으면 무시됩니다.</para>
        /// </summary>
        /// <param name="key">키</param>
        /// <param name="value">값</param>
        /// <returns></returns>
        public AppCenterProperties AddProperty(string key, string value)
        {
            var _key = NormalizeKey(key);
            var _value = AdjustStringValue(value);

            if (!ContainsKey(_key))
            {
                internalDictionary.Add(_key, _value);
            }

            return this;
        }

        /// <summary>
        /// 항목을 갱신합니다.
        /// <para>키에 해당하는 항목이 없으면 무시됩니다.</para>
        /// </summary>
        /// <param name="key">키</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public AppCenterProperties UpdateProperty(string key, string value)
        {
            var _key = NormalizeKey(key);
            var _value = AdjustStringValue(value);

            if (ContainsKey(_key))
            {
                internalDictionary[_key] = _value;
            }

            return this;
        }

        /// <summary>
        /// 항목을 제거합니다.
        /// <para>키에 해당하는 항목이 없으면 무시됩니다.</para>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public AppCenterProperties RemoveProperty(string key, string value)
        {
            var _key = NormalizeKey(key);

            if (!ContainsKey(_key))
            {
                internalDictionary.Remove(_key);
            }
            return this;
        }

        /// <summary>
        /// 키에 해당하는 항목의 값을 가져옵니다.
        /// <para>키에 해당하는 항목이 없으면 null입니다.</para>
        /// </summary>
        /// <param name="key">키</param>
        /// <returns></returns>
        public string TryGet(string key)
        {
            var _key = NormalizeKey(key);

            if (ContainsKey(_key))
            {
                return internalDictionary[_key];
            }

            return null;
        }

        public AppCenterProperties AddMessage(string message)
        {
            AddOrUpdateProperty(MESSAGE_KEY, message);

            return this;
        }

        public AppCenterProperties AddType(Type type)
        {
            return AddType(type.GetTypeInfo().FullName);
        }

        public AppCenterProperties AddType(string typeName)
        {
            AddOrUpdateProperty(TYPE_KEY, typeName);

            return this;
        }

        public AppCenterProperties AddMethod(string methodName)
        {
            AddOrUpdateProperty(METHOD_KEY, methodName);

            return this;
        }

        /// <summary>
        /// 컬렉션을 초기화합니다.
        /// </summary>
        /// <returns></returns>
        public AppCenterProperties Reset()
        {
            internalDictionary.Clear();
            return this;
        }

        public IDictionary<string, string> ToDictionary()
        {
            return internalDictionary;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("{");
            foreach (var item in internalDictionary)
            {
                builder.AppendLine("\t{");
                builder.AppendLine($"\t\t\"{item.Key}\": \"{item.Value}\"");
                builder.AppendLine("\t},");
            }
            if (builder.Length > 1)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            builder.Append("}");

            return builder.ToString();
        }

        private string NormalizeKey(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("키는 빈값을 허용하지 않습니다.", nameof(key));
            }

            var _key = key.Trim();

            CheckMaxCharacters(_key);

            if (isKeyIgnoreCase)
            {
                return _key.ToLower();
            }

            return _key;
        }

        private string AdjustStringValue(string value)
        {
            if (value == null || value.Length == 0) { return value; }
            var substringLength = value.Length;
            if (substringLength > MAX_CHARACTERS)
            {
                substringLength = MAX_CHARACTERS;
            }
            return value
                .Substring(0, substringLength)
                .Trim();
        }

        private void CheckMaxItemsCount()
        {
            if (internalDictionary.Count >= MAX_ITEMS_COUNT)
            {
                throw new NotSupportedException($"추가 속성은 {MAX_ITEMS_COUNT}개 이하의 항목으로 구성되어야 합니다. https://docs.microsoft.com/en-us/appcenter/diagnostics/limitations");
            }
        }

        private void CheckMaxCharacters(string value)
        {
            if (value.Length > MAX_CHARACTERS)
            {
                throw new NotSupportedException($"키, 값의 문자열은 {MAX_CHARACTERS}자 이하의 길이로 입력되어야 합니다. https://docs.microsoft.com/en-us/appcenter/diagnostics/limitations");
            }
        }

        private readonly IDictionary<string, string> internalDictionary;
        private bool isKeyIgnoreCase = true;
    }
}
