using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Interface
{
    public interface IUtilityService
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }
}
