using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using System;

namespace Pagene.Converter
{
    internal class ChangeDetector
    {
        private readonly HashAlgorithm _crypto;
        internal ChangeDetector(HashAlgorithm crypto)
        {
            _crypto = crypto;
        }
        //returns new hash algorithm if not same
        //returns null if same
        internal async Task<byte[]> DetectAsync(Stream file, Stream hash)
        {
            var newHash = _crypto.ComputeHash(file);

            byte[] buffer = new byte[newHash.Length];
            await hash.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);
            if (buffer.Length == newHash.Length && newHash.SequenceEqual(buffer))
            {
                return null; // same
            }
            else
            {
                return newHash; // not same
            }
        }
        internal static async Task WriteHashAsync(byte[] computedHash, Stream hashStream)
        {
            hashStream.SetLength(0);
            await hashStream.WriteAsync(computedHash).ConfigureAwait(false);
        }
     }
}
