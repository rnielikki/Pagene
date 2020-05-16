using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
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
            await hash.ReadAsync(buffer, 0, buffer.Length);
            if (buffer.Length == newHash.Length && newHash.SequenceEqual(buffer))
            {
                return null; // same
            }
            else
            {
                return newHash; // not same
            }
        }
        internal async Task WriteHash(byte[] computedHash, Stream hashStream)
        {
            await hashStream.FlushAsync();
            await hashStream.WriteAsync(computedHash);
        }

        internal void CleanHashAsync(IEnumerable<IFileInfo> hashes)
        {
            foreach (IFileInfo target in hashes)
            {
                target.Delete();
            }
        }
    }
}
