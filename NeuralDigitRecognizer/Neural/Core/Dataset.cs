using System;
using System.Collections.Generic;

namespace NeuralDigitRecognizer.Neural.Core
{
    public class Dataset
    {
        public List<List<double>> InputSignals { get; }

        public List<List<double>> ExpectedSignals { get; }

        public List<Tuple<List<double>, List<double>>> Samples
        {
            get
            {
                CheckSize();

                var samples = new List<Tuple<List<double>, List<double>>>();
                
                for (var i = 0; i < SamplesAmount; i++)
                {
                    samples.Add(new Tuple<List<double>, List<double>>(InputSignals[i], ExpectedSignals[i]));
                }
                
                return samples;
            }
        }

        public int SamplesAmount
        {
            get
            {
                CheckSize();
                return InputSignals.Count;
            }
        }

        private void CheckSize()
        {
            if (InputSignals.Count != ExpectedSignals.Count)
            {
                throw new Exception("Input signals dimension and output signals samples amount is not equal");
            }
        }

        public Dataset(List<List<double>> inputSignals, List<List<double>> expectedSignals)
        {
            InputSignals = inputSignals;
            ExpectedSignals = expectedSignals;
            CheckSize();
        }
    }
}