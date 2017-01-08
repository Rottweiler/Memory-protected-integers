using System;

namespace ECTest
{
    /// <summary>
    /// Memory protected integer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal struct MemoryProtectedInt<T> : IDisposable
    {
        private decimal _value;
        private decimal _offset;

        /// <summary>
        /// Random number generator
        /// </summary>
        public Random Rand { get; set; }

        /// <summary>
        /// Decl new memory protected int
        /// </summary>
        /// <param name="value"></param>
        public MemoryProtectedInt(T value)
        {
            if (!CanBeMemoryProtected(value))
                throw new Exception(value.GetType().ToString() + " can not be memory protected");

            Rand = new Random(Guid.NewGuid().GetHashCode());

            _offset = (decimal)Rand.Next(-100000000, 100000000);
            _value = GetConvertedValue(value) + _offset;
        }

        /// <summary>
        /// Get value
        /// </summary>
        /// <returns></returns>
        public T GetValue()
        {
            return (T)Convert.ChangeType(_value - _offset, typeof(T));
        }

        /// <summary>
        /// Get converted value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static decimal GetConvertedValue(T val)
        {
            return (decimal)Convert.ChangeType(val, typeof(decimal));
        }

        /// <summary>
        /// Simple check whether the type can be cast as Int32
        /// </summary>
        /// <param name="bar"></param>
        /// <returns></returns>
        private static bool CanBeMemoryProtected(T bar)
        {
            try
            {
                decimal foo = GetConvertedValue(bar);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _value = 0m;
            _offset = 0m;
            Rand = null;
        }

        /*
         * 
         * Todo: add operators
         * 
         */
    }
}
