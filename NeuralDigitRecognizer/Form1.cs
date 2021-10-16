using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeuralDigitRecognizer.Neural.Core;
using NeuralDigitRecognizer.Neural.Core.Model;
using static NeuralDigitRecognizer.Neural.Core.ActivationFunctions;

namespace NeuralDigitRecognizer
{
    public partial class MainWindowForm : Form
    {
        const int ButtonsAmount = 15;

        Button[] Buttons = new Button[ButtonsAmount];
        double[] ButtonsState = new double[ButtonsAmount];

        private Model _model;
        
        public MainWindowForm()
        {
            InitializeComponent();

            Buttons[0] = Button1;
            Buttons[1] = button2;
            Buttons[2] = button3;
            Buttons[3] = button4;
            Buttons[4] = button5;
            Buttons[5] = button6;
            Buttons[6] = button7;
            Buttons[7] = button8;
            Buttons[8] = button9;
            Buttons[9] = button10;
            Buttons[10] = button11;
            Buttons[11] = button12;
            Buttons[12] = button13;
            Buttons[13] = button14;
            Buttons[14] = button15;

            button2.Click += new EventHandler(buttonsEventHandler);
            button3.Click += new EventHandler(buttonsEventHandler);
            button4.Click += new EventHandler(buttonsEventHandler);
            button5.Click += new EventHandler(buttonsEventHandler);
            button6.Click += new EventHandler(buttonsEventHandler);
            button7.Click += new EventHandler(buttonsEventHandler);
            button8.Click += new EventHandler(buttonsEventHandler);
            button9.Click += new EventHandler(buttonsEventHandler);
            button10.Click += new EventHandler(buttonsEventHandler);
            button11.Click += new EventHandler(buttonsEventHandler);
            button12.Click += new EventHandler(buttonsEventHandler);
            button13.Click += new EventHandler(buttonsEventHandler);
            button14.Click += new EventHandler(buttonsEventHandler);
            button15.Click += new EventHandler(buttonsEventHandler);

            
            var activation = Custom(0.1);
            
            _model = new Model(new Topology(15, 10, 
                new [] {
                    new LayerTopology(77, activation),
                    new LayerTopology(34, activation),
                }
            ));

        }

        public void setButtonState(int index, double state) 
        {
            if (index < 0 || index > ButtonsAmount)
            {
                throw new Exception("Invalid index");
            }

            this.ButtonsState[index] = state;

            label1.Text = ButtonsState[0].ToString() + 
                " " + ButtonsState[1].ToString() +
                " " + ButtonsState[2].ToString() +
                " " + ButtonsState[3].ToString() +
                " " + ButtonsState[4].ToString() +
                " " + ButtonsState[5].ToString() +
                " " + ButtonsState[6].ToString() +
                " " + ButtonsState[7].ToString() +
                " " + ButtonsState[8].ToString() +
                " " + ButtonsState[9].ToString() +
                " " + ButtonsState[10].ToString() +
                " " + ButtonsState[11].ToString() +
                " " + ButtonsState[12].ToString() +
                " " + ButtonsState[13].ToString() +
                " " + ButtonsState[14].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonsEventHandler(object sender, EventArgs e)
        {
            Button button = sender as Button;

            string buttonIndexStr = button.Text;
            int buttonIndex = Int16.Parse(buttonIndexStr) - 1;

            if (ButtonsState[buttonIndex] == 0.0)
            {
                setButtonState(buttonIndex, 1.0);
                Buttons[buttonIndex].BackColor = Color.DarkBlue;
            }
            else 
            {
                setButtonState(buttonIndex, 0.0);
                Buttons[buttonIndex].BackColor = Color.White;
            }

            List<double> prediction = _model.FeedForward(new List<double>(ButtonsState));
            label2.Text = String.Join(Environment.NewLine, prediction);

        }

    }
}
