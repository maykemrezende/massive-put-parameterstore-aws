using System.Collections.Generic;

namespace ReadParameterStoreJson
{
    public class ParameterJsonRoot
    {
        public List<ParameterJson> Parameters { get; set; }
    }

    public class ParameterJson
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
