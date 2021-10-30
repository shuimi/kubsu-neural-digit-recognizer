using NeuralDigitRecognizer.Neural.Core.Layers.Base;
using static NeuralDigitRecognizer.Neural.Core.ActivationFunctions;

namespace NeuralDigitRecognizer.Neural.Core.Layers
{
    public class OutputLayer: Layer
    {
        public OutputLayer(int dimension, Layer prevLayer)
        {
            for (var i = 0; i < dimension; i++)
            {
                var neuron = new Neuron(AllPass);
                neuron.RandomWeights(prevLayer.LayerSize);
                neuron.NormalizeWeights();
                
                Neurons.Add(neuron);
            }
        }
    }
}