using System;

namespace NeuralDigitRecognizer.Neural.Core.Model
{
    public class LayerTopology
    {
        public int LayerSize { get; }
        public Func<double, double> ActivationFunction { get; }

        public LayerTopology(int layerSize, Func<double, double> activationFunction)
        {
            LayerSize = layerSize;
            ActivationFunction = activationFunction;
        }
    }
}