using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using NeuralDigitRecognizer.Neural.Core;
using NeuralDigitRecognizer.Neural.Core.Model;
using static NeuralDigitRecognizer.Neural.Core.ActivationFunctions;

namespace NeuralDigitRecognizer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindowForm());
        }
    }
}
