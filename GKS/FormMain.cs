using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKS
{
    public partial class FormMain : Form
    {
        Panel mainPanel;

        public int ScreenWidth { get; } = 1400;
        public int ScreenHeight { get; } = 700;

        public FormMain()
        {
            InitializeComponent();
            StartForm();
            DrawingForm1 df1 = new DrawingForm1();
            df1.StartDraw(mainPanel);
            ControlsForm1 cf1 = new ControlsForm1();
            cf1.StartControls(mainPanel);
        }

        private void StartForm()
        {
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;

            Width = ScreenWidth;
            Height = ScreenHeight;

            mainPanel = new Panel
            {
                Width = Width,
                Height = Height - 38
            };

            Controls.Add(mainPanel);
        }
    }
}
