namespace NeuralDigitRecognizer.Neural.Core.Optimizers
{
    public class Optimizer
    {
        public double LearningRate { get; internal set; }
        public double InertiaCoefficient { get; internal set; }

        public Optimizer(double learningRate, double inertiaCoefficient)
        {
            LearningRate = learningRate;
            InertiaCoefficient = inertiaCoefficient;
        }
    }
}