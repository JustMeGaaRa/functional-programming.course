using System;

namespace FP.Purity.SharedState
{
    public static class ItemExtensions
    {
        public static void Print(this string item)
        {
            Console.WriteLine(item != null ? $"{item}" : "None");
        }
    }
}
