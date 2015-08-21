using Hackaton.Boilerplate.Abstraction.Internals;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Hackaton.Boilerplate.Shared.Internals
{
    public class SHA512Hash : IHash
    {
        SHA512 _sha512Provider;

        public SHA512Hash()
        {
            _sha512Provider = SHA512.Create();
        }

        public bool Compare(string hash, string salt, string message)
        {
            var hashToCompare = Hash(message, salt);
            return hash.Equals(hashToCompare);
        }

        public string Hash(string message, string salt)
        {
            var byteHash = _sha512Provider.ComputeHash(
                Encoding.Default.GetBytes(
                    string.Format(
                        "{0}{1}",
                        message,
                        salt
                    )
                )
            );

            var stringHashed = BitConverter.ToString(
                byteHash
            ).Replace(
                "-",
                string.Empty
            );

            return stringHashed;
        }
    }
}
