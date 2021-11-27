using System;
using System.Collections.Generic;
using System.Linq;
using NeuralDigitRecognizer.Neural.Core.Layers;
using NeuralDigitRecognizer.Neural.Core.Layers.Base;
using NeuralDigitRecognizer.Neural.Core.Optimizers;

namespace NeuralDigitRecognizer.Neural.Core.Model
{
    public class Model
    {
        public Topology.Topology Topology { get; }
        public List<Layer> Layers { get; } = new List<Layer>();

        public Optimizer Optimizer { get; internal set; }
        private static Random rng = new Random(); 

        public Model(Topology.Topology topology, Optimizer optimizer)
        {
            Optimizer = optimizer;
            Topology = topology;

            CreateInputLayer();
            CreateHiddenLayer();
            CreateOutputLayer();
        }

        private List<T> Shuffle<T>(List<T> list)  
        {
            int n = list.Count;  
            while (n > 1) {  
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
            return list;
        }

        public double Fit(Dataset dataset)
        {
            var error = 0d;

            Shuffle<Tuple<List<double>, List<double>>>(dataset.Samples);
            
            foreach (var sample in dataset.Samples)
            {
                error += BackProp(sample.Item2, sample.Item1);
            }

            return error;
        }

        public double BackProp(List<double> expectation, List<double> inputs)
        {
            var prediction = FeedForward(inputs);
            var lastLayer = Layers.Last().Neurons;
            
            for (var i = 0; i < prediction.Count; i++)
            {
                var error = expectation[i] - prediction[i];
                lastLayer[i].BackProp(error, Optimizer);
            }

            for (var j = Layers.Count - 2; j >= 0; j--)
            {
                var layer = Layers[j];
                var prevLayer = Layers[j + 1];

                for (var i = 0; i < layer.LayerSize; i++)
                {
                    var diff = 0d;
                    
                    for (var l = 0; l < prevLayer.LayerSize; l++)
                    {
                        var prevNeuron = prevLayer.Neurons[l];
                        diff += prevNeuron.Weights[i] * prevNeuron.Delta;
                    }
                    
                    layer.Neurons[i].BackProp(diff, Optimizer);
                }
            }
            
            return Loss(prediction, expectation);
        }
        
        private double Loss(List<double> prediction, List<double> expectation)
        {
            if (prediction.Count != expectation.Count)
            {
                throw new Exception($"Prediction and expectation dimensions is not equal: " +
                                    $"got {prediction.Count} and {expectation.Count}");
            }
            var sum = prediction.Select((val, index) => Math.Pow(val - expectation[index], 2)).Sum();

            return Math.Sqrt(sum) ;
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
