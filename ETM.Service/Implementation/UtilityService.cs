using ETM.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ETM.Service.Implementation
{
    public class UtilityService : IUtilityService
    {
        private readonly ILogger<UtilityService> _logger;
        private readonly IConfiguration _configuration;

        public UtilityService(ILogger<UtilityService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public string Decrypt(string input)
        {
            try
            {
                string EncryptionKey = _configuration.GetSection("StaticDetails").GetSection("EncryptionKey").Value;
                input = input.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(input);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                    {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        input = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }

                return input;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return string.Empty;
            }

        }

        public string Encrypt(string input)
        {
            try
            {
                string EncryptionKey = _configuration.GetSection("StaticDetails").GetSection("EncryptionKey").Value;
                byte[] clearBytes = Encoding.Unicode.GetBytes(input);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                    {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        input = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return input;
            }
            catch (Exception ex)            {

                _logger.LogError(ex.ToString());
                return string.Empty;
            }
         
        }
    }
}
