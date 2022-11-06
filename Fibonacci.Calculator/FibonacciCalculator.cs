namespace Fibonacci.Calculator
{
    /// <summary>
    ///     NOTE
    ///     This is a simple but primitive implementation, only for testing out the interservice communication,
    ///     See FibonacciCalculatorService instead
    /// </summary>
    [Obsolete]
    public static class FibonacciCalculator
    {
        public static bool IsFibonacci(long n)
        {
            var a = 1.0 / 2 * (n + Math.Sqrt(5 * n * n + 4));
            var b = 1.0 / 2 * (n + Math.Sqrt(5 * n * n - 4));

            var roundA = Math.Round(a);
            var roundB = Math.Round(b);

            return AboutEqual(a, roundA) || AboutEqual(b, roundB);
        }

        public static long NextFibonacci(long n, long? prevN = null)
        {
            if (!IsFibonacci(n)) throw new ArgumentException("Bad fibonacci number n");
            if (prevN != null && !IsFibonacci(prevN.Value)) throw new ArgumentException("Bad fibonacci number prevN");

            switch (n)
            {
                case 0:
                case 1 when prevN == 0:
                    return 1;
                case 1 when prevN == 2:
                    return 2;
                default:
                    return (long) Math.Round(n * (1 + Math.Sqrt(5)) / 2.0);
            }
        }

        private static bool AboutEqual(double x, double y)
        {
            var epsilon = Math.Max(Math.Abs(x), Math.Abs(y)) * 1E-15;
            return Math.Abs(x - y) <= epsilon;
        }
    }
}
