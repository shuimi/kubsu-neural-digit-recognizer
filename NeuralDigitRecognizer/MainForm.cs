using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using NeuralDigitRecognizer.Neural.Core;
using NeuralDigitRecognizer.Neural.Core.Model;
using NeuralDigitRecognizer.Neural.Core.Model.Topology;
using NeuralDigitRecognizer.Neural.Core.Optimizers;
using static NeuralDigitRecognizer.Neural.Core.ActivationFunctions;

namespace NeuralDigitRecognizer
{
    public partial class MainWindowForm : Form
    {
        private const int ButtonsAmount = 15;

        private readonly Button[] _buttons = new Button[ButtonsAmount];
        private readonly double[] _buttonsState = new double[ButtonsAmount];

        private readonly Model _model;
        private readonly Dataset _dataset;
        private List<double> ExpectedSignal { get; set; } = new List<double>();

        public MainWindowForm()
        {
            InitializeComponent();

            _buttons[0] = Button1;
            _buttons[1] = button2;
            _buttons[2] = button3;
            _buttons[3] = button4;
            _buttons[4] = button5;
            _buttons[5] = button6;
            _buttons[6] = button7;
            _buttons[7] = button8;
            _buttons[8] = button9;
            _buttons[9] = button10;
            _buttons[10] = button11;
            _buttons[11] = button12;
            _buttons[12] = button13;
            _buttons[13] = button14;
            _buttons[14] = button15;

            button2.Click += ButtonsEventHandler;
            button3.Click += ButtonsEventHandler;
            button4.Click += ButtonsEventHandler;
            button5.Click += ButtonsEventHandler;
            button6.Click += ButtonsEventHandler;
            button7.Click += ButtonsEventHandler;
            button8.Click += ButtonsEventHandler;
            button9.Click += ButtonsEventHandler;
            button10.Click += ButtonsEventHandler;
            button11.Click += ButtonsEventHandler;
            button12.Click += ButtonsEventHandler;
            button13.Click += ButtonsEventHandler;
            button14.Click += ButtonsEventHandler;
            button15.Click += ButtonsEventHandler;

            
            var activation = Sigmoid(1);

            var inputSamples = new List<List<double>>()
            {
                new List<double>()
                {
                    1, 1, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1
                },
                new List<double>()
                {
                    0, 0, 1,
                    0, 0, 1,
                    0, 0, 1,
                    0, 0, 1,
                    0, 0, 1
                },
                new List<double>()
                {
                    1, 1, 1,
                    0, 0, 1,
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1
                },
                new List<double>()
                {
                    1, 1, 1,
                    0, 0, 1,
                    1, 1, 1,
                    0, 0, 1,
                    1, 1, 1
                },
                new List<double>()
                {
                    1, 0, 1,
                    1, 0, 1,
                    1, 1, 1,
                    0, 0, 1,
                    0, 0, 1
                },
                new List<double>()
                {
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1,
                    0, 0, 1,
                    1, 1, 1
                },
                new List<double>()
                {
                    1, 1, 1,
                    1, 0, 0,
                    1, 1, 1,
                    1, 0, 1,
                    1, 1, 1
                },
                new List<double>()
                {
                    1, 1, 1,
                    0, 0, 1,
                    0, 0, 1,
                    0, 0, 1,
                    0, 0, 1
                },                
                new List<double>()
                {
                    1, 1, 1,
                    1, 0, 1,
                    1, 1, 1,
                    1, 0, 1,
                    1, 1, 1
                },
                new List<double>()
                {
                    1, 1, 1,
                    1, 0, 1,
                    1, 1, 1,
                    0, 0, 1,
                    1, 1, 1
                },
            };

            var outputSamples = new List<List<double>>()
            {
                new List<double>()
                {
                    1, 0, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new List<double>()
                {
                    0, 1, 0, 0, 0, 0, 0, 0, 0, 0
                },
                new List<double>()
                {
                    0, 0, 1, 0, 0, 0, 0, 0, 0, 0
                },
                new List<double>()
                {
                    0, 0, 0, 1, 0, 0, 0, 0, 0, 0
                },
                new List<double>()
                {
                    0, 0, 0, 0, 1, 0, 0, 0, 0, 0
                },
                new List<double>()
                {
                    0, 0, 0, 0, 0, 1, 0, 0, 0, 0
                },
                new List<double>()
                {
                    0, 0, 0, 0, 0, 0, 1, 0, 0, 0
                },
                new List<double>()
                {
                    0, 0, 0, 0, 0, 0, 0, 1, 0, 0
                },
                new List<double>()
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 1, 0
                },
                new List<double>()
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0, 1
                }
            };

            _dataset = new Dataset(inputSamples, outputSamples);
            
            _model = new Model(
                new Topology(
                    15,
                    10,
                    new LayerTopology(77, activation),
                    new LayerTopology(34, activation)
                ),
                new SGD(0.01, 0.3)
            );

        }

        private void SetButtonState(int index, double state) 
        {
            if (index < 0 || index > ButtonsAmount)
            {
                throw new Exception("Invalid index");
            }

            _buttonsState[index] = state;
            label1.Text = _buttonsState.Aggregate("", (current, next) => current + (next + " "));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonsEventHandler(object sender, EventArgs e)
        {
            var button = sender as Button;

            var buttonIndexStr = button?.Text;
            var buttonIndex = int.Parse(buttonIndexStr ?? string.Empty) - 1;

            if (_buttonsState[buttonIndex] == 0.0)
            {
                SetButtonState(buttonIndex, 1.0);
                _buttons[buttonIndex].BackColor = Color.FromArgb(117, 153, 255);
            }
            else 
            {
                SetButtonState(buttonIndex, 0.0);
                _buttons[buttonIndex].BackColor = Color.FromArgb(239, 253, 255);
            }

            var prediction = _model.FeedForward(new List<double>(_buttonsState));
            label2.Text = String.Join(Environment.NewLine, prediction.Select(value => Math.Round(value, 4)));

        }

        private void BackPropStepButton_Click(object sender, EventArgs e)
        {
            var err = _model.BackProp(ExpectedSignal, new List<double>(_buttonsState));
            label4.Text = err.ToString(CultureInfo.InvariantCulture);
            
            var prediction = _model.FeedForward(new List<double>(_buttonsState));
            label2.Text = string.Join(Environment.NewLine, prediction);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            ExpectedSignal = new List<double>();

            for (var i = 0; i < 10; i++)
            {
                ExpectedSignal.Add(0d);
            }
            
            var index = 0;

            if (textBox?.Text != string.Empty)
            {
                index = Convert.ToInt32(textBox?.Text ?? string.Empty);
            }
            
            if (Math.Abs(index) <= ExpectedSignal.Count)
            {
                ExpectedSignal[index] = 1d;
            }
            
            label3.Text = string.Join(" ", ExpectedSignal);
        }

        private void Fit()
        {
            for (int i = 0; i < 100000; i++)
            {
                var loss = Math.Round(_model.Fit(_dataset), 8);
                label4.Text = loss.ToString(CultureInfo.InvariantCulture);
                if (loss < 0.0001) break;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var thread2 = new Thread(Fit);
            thread2.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Export();
        }

        // private void ExportAllWeights()
        // {
        //     var path = "D:\\CSharpNeuralNetworks\\kubsu-neural-digit-recognizer\\weights.xml";
        //     System.IO.FileStream file = System.IO.File.Create(path);
        //     foreach (var layer in _model.Layers)
        //     {
        //         System.Xml.Serialization.XmlSerializer writer =
        //             new System.Xml.Serialization.XmlSerializer(layer.WeightsMatrix.GetType());
        //         writer.Serialize(file, layer.WeightsMatrix);
        //     }
        //
        //     file.Close();
        // }

        // private void ExportAllWeights()
        // {
        //     var path = "D:\\CSharpNeuralNetworks\\kubsu-neural-digit-recognizer\\weights.json";
        //     string json;
        //     foreach (var modelLayer in _model.Layers)
        //     {
        //         json = JsonSerializer.Serialize(modelLayer.WeightsMatrix);
        //         File.AppendAllText(path, json);
        //     }
        // }
        //
        // private void ImportAllWeights()
        // {
        //     var path = "D:\\CSharpNeuralNetworks\\kubsu-neural-digit-recognizer\\weights.json";
        //     String weights_str = File.ReadAllText(path);
        //     // for (int i = 0; i < _model.Layers.Count; i++)
        //     // {
        //     //     _model.Layers[i].WeightsMatrix = JsonSerializer.Deserialize<List<List<double>>>(weights_str);
        //     // }
        //     var layers = JsonSerializer.Deserialize<Layer>(weights_str);
        //     Console.WriteLine(layers.ToString());
        // }
        private void Export()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            var filename = saveFileDialog1.FileName;
            var serializer = new XmlSerializer(typeof(double[][][]));
            using var fs = new FileStream(filename, FileMode.Create);
            var w = new double[_model.Layers.Count][][];
            for (int i = 0; i < _model.Layers.Count; i++)
            {
                w[i] = _model.Layers[i].ExportWeights();
            }

            serializer.Serialize(fs, w);
            fs.Close();
        }

        private void Import()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            var filename = openFileDialog1.FileName;
            var serializer = new XmlSerializer(typeof(double[][][]));
            using var fs = new FileStream(filename, FileMode.OpenOrCreate);
            var inputWeights = (double[][][]) serializer.Deserialize(fs);
            fs.Close();

            for (var i = 0; i < inputWeights.Length; i++)
            {
                for (var i1 = 0; i1 < inputWeights[i].Length; i1++)
                {
                    for (var i2 = 0; i2 < inputWeights[i][i1].Length; i2++)
                    {
                        _model.Layers[i].WeightsMatrix[i1][i2] = inputWeights[i][i1][i2];
                    }
                }
            }
            // List<List<List<double>>> networkWeights = new List<List<List<double>>>();
            // for (int i = 0; i < w.Length; i++)
            // {
            //     List<List<double>> l = new List<List<double>>();
            //     int lCount = 0;
            //     foreach (var layerW in w)
            //     {
            //         foreach (var neuronW in layerW)
            //         {
            //             l[lCount] = neuronW.ToList();
            //         }
            //
            //         lCount++;
            //     }
            //
            //     networkWeights[i] = l;
            //     _model.Layers[i].WeightsMatrix = networkWeights[i];
            // }
        }

        private void button18_Click_1(object sender, EventArgs e)
        {
            Import();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }
    }
}