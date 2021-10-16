using static NeuralDigitRecognizer.Neural.Core.ActivationFunctions;

namespace NeuralDigitRecognizer.Neural.Core
{
    public class OutputLayer: Layer
    {
        public OutputLayer(int dimension, Layer prevLayer)
        {
            for (var i = 0; i < dimension; i++)
            {
                var neuron = new Neuron(0.0, AllPass);
                neuron.RandomWeights(prevLayer.LayerSize);
                neuron.NormalizeWeights();
                
                Neurons.Add(neuron);
            }
        }
    }
}