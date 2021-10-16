using System;
using System.Collections.Generic;
using System.Linq;


namespace NeuralDigitRecognizer.Neural.Core
{
    public class Neuron
    {
        public int WeightsAmount => Weights?.Count ?? 0;
        public double Bias { get; }
        public List<double> Weights { get; internal set; }
        public Func<double, double> ActivationFunction { get; internal set; }
        public double Output { get; private set; }
        
        private static readonly Random Random = new Random();

        public Neuron(double bias, Func<double, double> activationFunction)
        {
            Bias = bias;
            ActivationFunction = activationFunction;
        }

        public double FeedForward(List<double> input)
        {
            if(input.Count != WeightsAmount) 
            {
                throw new Exception("Invalid input dimension");
            }
            
            Double sum = Bias;
            
            for (int index = 0; index < input.Count; index++) 
            {
                sum += input[index] * Weights[index];
            }

            Output = ActivationFunction(sum);
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
                weights.Add(Random.NextDouble() * 2 - 1);
            }
            
            Weights = weights;
        }

        public void NormalizeWeights()
        {
            var mean = Weights.Aggregate((a, b) => a + b) / WeightsAmount;
            Weights = Weights.ConvertAll(weight => weight - mean);
        } 

    }
}
