using System;

namespace NeuralDigitRecognizer.Neural.Core.Model
{
    public class LayerTopology
    {
        public int LayerSize { get; }
        public Activation ActivationFunction { get; }

        public LayerTopology(int layerSize, Activation activationFunction)
        {
            LayerSize = layerSize;
            ActivationFunction = activationFunction;
        }
    }
}