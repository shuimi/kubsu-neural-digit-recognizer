using System;

namespace NeuralDigitRecognizer.Neural.Core
{
    public class Activation: Tuple<Func<double, double>, Func<double, double>>
    {
        public Func<double, double> Transformation { get; private set; }
        public Func<double, double> Derivative { get; private set; }

        public Activation(Func<double, double> item1, Func<double, double> item2) : base(item1, item2)
        {
            Transformation = item1;
            Derivative = item2;
        }
    }
    
    public static class ActivationFunctions
    {
        public static readonly Activation AllPass = new Activation(x => x, x => 1);
        
        public static readonly Func<double, Activation> Custom = angleTan =>
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

            double Derivative(double x)
            {
                if (Math.Abs(x) <= 1)
                {
                    return 1;
                }
                return angleTan;
            }

            return new Activation(Transformation, Derivative);
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
        
        public static Func<double, Activation> Sigmoid = a =>
        {
            double Transformation(double x)
            {
                return 1 / (1 + Math.Exp(-a * x));
            }
            
            double Derivative(double x)
            {
                return Transformation(x) * (1 - Transformation(x));
            }
            
            return new Activation(Transformation, Derivative);
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
