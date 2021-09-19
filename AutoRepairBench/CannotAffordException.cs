using System;

namespace AutoRepairBench
{
    public class CannotAffordException: Exception
    {
        public int Count { get; }
        public CannotAffordException(int count)
        {
            this.Count = count;
        }
    }
}