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
            set
            {
                int valueNum = 0;
                foreach (var neuron in Neurons)
                {
                    neuron.Inputs = value[valueNum];
                    valueNum++;
                }
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

        public double[][] ExportWeights()
        {
            var w = new double[WeightsMatrix.Count][];
            for (int i = 0; i < WeightsMatrix.Count; i++)
            {
                w[i] = new double[WeightsMatrix[i].Count];
                for (int j = 0; j < WeightsMatrix[i].Count; j++)
                {
                    w[i][j] = WeightsMatrix[i][j];
                }
            }
            return w;
        }
    }
}