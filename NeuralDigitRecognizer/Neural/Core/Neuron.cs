using System;
using System.Collections.Generic;
using System.Linq;
using NeuralDigitRecognizer.Neural.Core.Optimizers;


namespace NeuralDigitRecognizer.Neural.Core
{
    public class Neuron
    {
        public Activation ActivationFunction { get; internal set; }
        
        public List<double> Weights { get; internal set; }
        public int InputDimension => Weights?.Count ?? 0;
        
        public List<double> Inputs { get; private set; }
        public double Output { get; private set; }
        
        public double Delta { get; private set; }
        
        private static readonly Random Random = new Random();

        
        public Neuron(Activation activationFunction)
        {
            ActivationFunction = activationFunction;
            Inputs = new List<double>();
        }

        public void BackProp(double error, Optimizer optimizer)
        {
            Delta = error * ActivationFunction.Derivative(Output);
            
            for (var i = 0; i < InputDimension; i++)
            {
                var updatedWeight = Weights[i] - Inputs[i] * Delta * optimizer.LearningRate;

                Weights[i] = updatedWeight;
            }
        }

        public double FeedForward(List<double> input)
        {
            if(input.Count != InputDimension) 
            {
                throw new Exception("Invalid input dimension");
            }
            Inputs = input;

            var sum = input.Select((val, index) => val * Weights[index]).Sum();

            Output = ActivationFunction.Transformation(sum);
            return Output;
        }

        public void FlatWeights(int weightsAmount, double weight)
        {
            var weights = new List<double>();

            for (var i = 0; i < weightsAmount; i++)
            {
                weights.Add(weight);
            }

            Weights = weights;
        }

        public void RandomWeights(int weightsAmount)
        {
            var weights = new List<double>();

            for (var i = 0; i < weightsAmount; i++)
            {
                weights.Add(Random.NextDouble() * 0.2 - 0.1);
            }
            
            Weights = weights;
        }

        public void NormalizeWeights()
        {
            var mean = Weights.Aggregate((a, b) => a + b) / InputDimension;
            Weights = Weights.ConvertAll(weight => weight - mean);
        } 

    }
}
