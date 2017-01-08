using System;
using System.IO;
using System.Text;

namespace ECTest
{
    /*
     * 
     * Work in progress
     * 
     */

    public struct MemoryProtectedString
    {
        private Stream _value;
        private byte[] _key;
        private int _length;

        public MemoryProtectedString(string value)
        {
            /* Decl stream */
            _value = new MemoryStream();

            /* Init key */
            var rng = new Random(Guid.NewGuid().GetHashCode());
            _key = new byte[16];
            for (int i = 0; i < _key.Length; i++)
                _key[i] = (byte)rng.Next(0, 255);

            /* Encrypt value */
            byte[] val = ToBase64string(value);
            byte[] enc = Xor(val, _key);

            /* Populate stream */
            _value.Write(enc, 0, enc.Length);
            _value.Flush();

            /* Record length for retrieval */
            _length = enc.Length;
            enc = null;
        }

        public MemoryProtectedString(string value, byte[] key)
        {
            /* Decl stream */
            _value = new MemoryStream();

            /* Init key */
            _key = key;

            /* Encrypt value */
            byte[] val = Encoding.UTF8.GetBytes(value);
            byte[] enc = Xor(val, _key);

            /* Populate stream */
            _value.Write(enc, 0, enc.Length);
            _value.Flush();

            /* Record length for retrieval */
            _length = enc.Length;
            enc = null;
        }

        public MemoryProtectedString(byte[] value, int length, byte[] key)
        {
            /* Decl stream */
            _value = new MemoryStream();

            /* Init key */
            _key = key;

            /* Populate stream */
            _value.Write(value, 0, value.Length);
            _value.Flush();

            /* Record length */
            _length = length;
        }

        public string GetValue()
        {
            byte[] buff = new byte[_length];
            _value.Position = 0;
            _value.Read(buff, 0, buff.Length);
            return FromBase64string(Xor(buff, _key));
        }

        private static byte[] ToBase64string(string value)
        {
            return Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(value)));
        }

       private static string FromBase64string(byte[] value)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(value)));
        }

        private static byte[] Xor(byte[] data, byte[] key)
        {
            byte[] prot = (byte[])data.Clone();
            data = null; /* clear from mem */
            for(int i = 0; i < prot.Length; i++)
            {
                prot[i] ^= key[i % key.Length];
            }
            return prot;
        }
    }
}
