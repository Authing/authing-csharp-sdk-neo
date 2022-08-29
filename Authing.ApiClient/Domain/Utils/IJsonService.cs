using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Utils
{
    public interface IJsonService
    {
        string SerializeObject(object obj);

        T DeserializeObject<T>(string jsonStr);
    }
}
