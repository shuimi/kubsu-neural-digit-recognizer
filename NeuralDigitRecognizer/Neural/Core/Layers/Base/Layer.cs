using System;
using System.Collections.Generic;

namespace NeuralDigitRecognizer.Neural.Core
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
            var result = new List<double>();
            foreach (var neuron in Neurons)
            {
                result.Add(neuron.Output);
            }
            return result;
        }

        void LoadWeights()
        {
            //TODO: implement
        }

        void ExportWeights()
        {
            //TODO: implement
        }
        
    }
}
