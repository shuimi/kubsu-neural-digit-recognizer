using System.Collections.Generic;
using System.Linq;

namespace NeuralDigitRecognizer.Neural.Core.Layers.Base
{
    public abstract class Layer
    {
        public List<Neuron> Neurons { get; protected set; }
        public int LayerSize => Neurons?.Count ?? 0;

        public List<List<double>> WeightsMatrix
        {
            get
            {
                var matrix = new List<List<double>>();
                Neurons.ForEach(neuron => matrix.Add(neuron.Weights));
                return matrix;
            }
        }

        protected Layer()
        {
            Neurons = new List<Neuron>();
        }

        public List<double> Signals()
        {
            return Neurons.Select(neuron => neuron.Output).ToList();
        }

        public void LoadWeights()
        {
            //TODO: implement
        }

        public void ExportWeights()
        {
            //TODO: implement
        }
        
    }
}
