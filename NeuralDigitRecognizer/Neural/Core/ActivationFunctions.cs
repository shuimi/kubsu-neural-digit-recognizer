using System;

namespace NeuralDigitRecognizer.Neural.Core
{
    public static class ActivationFunctions
    {
        public static readonly Func<double, double> AllPass = (x => x);
        
        public static readonly Func<double, Func<double, double>> Custom = angleTan =>
        {
            double Transformation(double x)
            {
                if (Math.Abs(x) <= 1)
                {
                    return x;
                }

                if (x < -1)
                {
                    return angleTan * x - (1 - angleTan);
                }

                return angleTan * x + (1 - angleTan);
            }

            return Transformation;
        };

        public static Func<double, double> Threshold = x => x >= 0 ? 1 : 0;

        public static Func<double, double> PiecewiseLinear = x =>
        {
            if (x >= 0.5)
                return 1;
            if (x > -0.5 && x < 0.5)
                return x;
            return 0;
        };
        
        public static Func<double, Func<double, double>> Sigmoid = a =>
        {
            double Transformation(double x)
            {
                return 1 / (1 + Math.Exp(-a * x));
            }

            return Transformation;
        };

        public static Func<double, double> Sgn = x =>
        {
            if (x > 0)
                return 1;
            if (x == 0)
                return 0;
            return -1;
        };

        public static Func<double, double> Tanh = Math.Tanh;
        
        public static Func<double, double> Relu = x =>
        {
            if (x > 0)
                return x;
            return 0;
        };

    }
}
