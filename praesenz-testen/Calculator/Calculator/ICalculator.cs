using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp
{
    internal interface ICalculator
    {

        double Sum(double a, double b);
        int Sum(int a, int b);
        double Sub(double a, double b);
        int Sub(int a, int b);
        double Mul(double a, double b);
        int Mul(int a, int b);
        double Div(double a, double b);
        double Div(int a, int b);


    }
}
