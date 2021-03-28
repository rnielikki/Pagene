using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using System;

namespace Pagene.Converter
{
    /// <summary>
    /// Represents hash comparison and writing hash logic for comparison.
    /// </summary>
    internal class ChangeDetector
    {
        private readonly HashAlgorithm _crypto;
        internal ChangeDetector(HashAlgorithm crypto)
        {
            _crypto = crypto;
        }

        /// <summary>
        /// Compares and detects if the file changed, by comparing to the hash.
        /// </summary>
        /// <param name="file">The file to compare.</param>
        /// <param name="hash">The stream of the <c>.hashfile</c></param>
        /// <returns><c>null</c> if the file is not changed. Otherwise returns the new hash (<see cref="byte[]"/>).</returns>
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

        /// <summary>
        /// Writes hash to hash stream.
        /// </summary>
        /// <param name="computedHash">The computed hash for writing - it's just byte array content to write.</param>
        /// <param name="hashStream">The target stream (.hashfile) to write</param>
        /// <remarks>This process truncates <c>hashStream</c>.</remarks>
        internal async Task WriteHashAsync(byte[] computedHash, Stream hashStream)
        {
            hashStream.SetLength(0);
            await hashStream.WriteAsync(computedHash).ConfigureAwait(false);
        }
     }
}
