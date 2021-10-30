using System.Collections.Generic;

namespace NeuralDigitRecognizer.Neural.Core.Model.Topology
{
    public class Topology
    {
        public int InputDimension { get; }
        public int OutputDimension { get; }
        public List<LayerTopology> HiddenLayers { get; }

        public Topology(int inputDimension, int outputDimension, params LayerTopology[] layers)
        {
            InputDimension = inputDimension;
            OutputDimension = outputDimension;
            HiddenLayers = new List<LayerTopology>();
            HiddenLayers.AddRange(layers);
        }

    }
}