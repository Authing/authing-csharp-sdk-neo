using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authing.Library.Domain.Model
{
    public class JWKS
    {
        public Key[] keys { get; set; }
    }

    public class Key
    {
        public string e { get; set; }
        public string n { get; set; }
        public string kty { get; set; }
        public string alg { get; set; }
        public string use { get; set; }
        public string kid { get; set; }
    }
}
