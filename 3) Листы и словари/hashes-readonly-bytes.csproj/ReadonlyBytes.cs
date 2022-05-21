using System;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private int hash;
        private bool hashSet = false;
        private readonly byte[] bytes;

        public int this[int index]
        {
            get
            {
                return bytes[index];
            }
        }

        public ReadonlyBytes(params byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException();
            Length = bytes.Length;
            this.bytes = bytes;
        }

        public int Length
        {
            get;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ReadonlyBytes))
            {
                return false;
            }
            if (obj.GetType().IsSubclassOf(typeof(ReadonlyBytes)))
            {
                return false;
            }

            var item = obj as ReadonlyBytes;

            return (item.GetHashCode() == this.GetHashCode());
        }
        public override int GetHashCode()
        {
            unchecked
            {
                if (hashSet)
                {
                    return hash;
                }

                hash = 1;

                var constant = 69420;

                for (int z = 0; z < bytes.Length; z++)
                {
                    hash *= constant;
                    hash ^= bytes[z];
                }
                hashSet = true;
            }
            return hash;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("[");
            for (int v = 0; v < Length; v++)
            {
                if (v == Length - 1)
                {
                    stringBuilder.Append(this[v]);
                    break;
                }
                stringBuilder.Append(this[v]);
                stringBuilder.Append(", ");
            }
            stringBuilder.Append("]");
            return stringBuilder.ToString();
        }

        public IEnumerator<byte> GetEnumerator()
        {
            for (int p = 0; p < Length; p++)
            {
                yield return bytes[p];
            }

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}