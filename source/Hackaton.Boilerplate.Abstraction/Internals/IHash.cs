using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.Abstraction.Internals
{
    public interface IHash
    {
        string Hash(string message, string salt);
        bool Compare(string hash, string salt, string message);
    }
}
