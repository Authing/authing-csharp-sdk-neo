using System.Collections.Generic;

namespace Authing.ApiClient.Types
{
    public class KeyValueDictionary : Dictionary<string, string>
    {
        public new void Add(string key, string value)
        {
            base.Add(key, value);
        }

        public new string this[string key]
        {
            get { return base[key]; }
            set { base[key] = value; }
        }
    }
}