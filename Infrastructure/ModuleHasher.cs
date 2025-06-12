using System;

namespace Infrastructure
{
    public static class ModuleHasher
    {
        public static int GetDeterministicHashCode(string str)
        {
            unchecked
            {
                int hash = 23;
                foreach (char c in str)
                {
                    hash = hash * 31 + c;
                }

                return Math.Abs(hash);
            }
        }
    }
}