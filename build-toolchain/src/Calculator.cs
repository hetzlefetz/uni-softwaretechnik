namespace CalculatorApp
{
    internal class Calculator : ICalculator
    {
        public double Div(double a, double b)
        {
            if(b == 0)
            {
                throw new InvalidOperationException("Nope");
            }
            return a / b;
        }

        public double Div(int a, int b) => Div((double)a, (double)b);

        public double Mul(double a, double b) => a * b;

        public int Mul(int a, int b) => (int)Mul((double)a, (double)b);

        public double Sub(double a, double b) => a - b;

        public int Sub(int a, int b) => (int)Sub((double)a, (double)b);

        public double Sum(double a, double b) => a + b;

        public int Sum(int a, int b) => (int)Sum((double)a, (double)b);
    }
}
