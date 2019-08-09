using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataSum.model
{
    [Serializable]
    public class ConfigModel
    {
        [JsonProperty(PropertyName = "key")]
        public string key { get; private set; }

        [JsonProperty(PropertyName = "path")]
        public string[] Path { get; private set; }
    }
}
