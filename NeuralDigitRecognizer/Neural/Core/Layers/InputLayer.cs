using NeuralDigitRecognizer.Neural.Core.Layers.Base;
using static NeuralDigitRecognizer.Neural.Core.ActivationFunctions;

namespace NeuralDigitRecognizer.Neural.Core.Layers
{
    public class InputLayer: Layer
    {
        const int InputDimension = 1;
        
        public InputLayer(int dimension)
        {
            for (var i = 0; i < dimension; i++)
            {
                var neuron = new Neuron(AllPass);
                neuron.FlatWeights(InputDimension, 1.0);
                
                Neurons.Add(neuron);
            }
        }
    }
}