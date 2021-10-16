using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralDigitRecognizer.Neural.Core.Model
{
    public class Model
    {
        public Topology Topology { get; }
        public List<Layer> Layers { get;  } = new List<Layer>();

        public Model(Topology topology)
        {
            Topology = topology;

            CreateInputLayer();
            CreateHiddenLayer();
            CreateOutputLayer();
        }
        
        public List<double> FeedForward(List<double> inputSignals)
        {
            if (inputSignals.Count != Layers.First().LayerSize)
            {
                throw new Exception(
                    "Incorrect input dimension, expected " + Layers.First().LayerSize 
                    + ", but got " + inputSignals.Count + "."
                );
            }

            SendSignalsToInputNeurons(inputSignals);
            FeedForwardLayersAfterInput();

            return Layers.Last().Neurons.Select(neuron => neuron.Output).ToList();
        }

        private void FeedForwardLayersAfterInput()
        {
            for (var i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                var prevLayerSignals = Layers[i - 1].Signals();

                foreach (var neuron in layer.Neurons)
                {
                    neuron.FeedForward(prevLayerSignals);
                }
            }
        }

        private void SendSignalsToInputNeurons(List<double> inputSignals)
        {
            for (var i = 0 ; i < inputSignals.Count; i++)
            {
                var signal = new List<double>() { inputSignals[i] };
                var neuron = Layers.First().Neurons[i];
                neuron.FeedForward(signal);
            }
        }

        private void CreateInputLayer()
        {
            var inputLayer = new InputLayer(Topology.InputDimension);
            Layers.Add(inputLayer);
        }

        private void CreateHiddenLayer()
        {
            foreach (var layerTopology in Topology.HiddenLayers)
            {
                var denseLayer = new DenseLayer(layerTopology, Layers.Last());  
                Layers.Add(denseLayer);
            }
        }
        
        private void CreateOutputLayer()
        {
            var outputLayer = new OutputLayer(Topology.OutputDimension, Layers.Last());
            Layers.Add(outputLayer);
        }

    }
}
