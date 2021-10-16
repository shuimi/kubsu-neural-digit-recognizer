using NeuralDigitRecognizer.Neural.Core.Model;

namespace NeuralDigitRecognizer.Neural.Core
{
    public class DenseLayer : Layer
    {
        public DenseLayer(LayerTopology topology, Layer prevLayer)
        {
            for (var i = 0; i < topology.LayerSize; i++)
            {
                var neuron = new Neuron(0.0, topology.ActivationFunction);
                neuron.RandomWeights(prevLayer.LayerSize);
                neuron.NormalizeWeights();
                
                Neurons.Add(neuron);
            }
        }
    }
}