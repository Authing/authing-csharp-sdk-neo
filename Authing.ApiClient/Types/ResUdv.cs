using Authing.ApiClient.Domain.Model;

namespace Authing.ApiClient.Types
{
    public class ResUdv
    {
        public string Key { get; set; }

        public UdfDataType DataType { get; set; }

        public object Value { get; set; }

        public string Label { get; set; }
    }
}