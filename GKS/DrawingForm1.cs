using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace GKS
{
    class DrawingForm1
    {
        private int[][] mainMatrix;

        public void StartDraw(Panel panel)
        {
            panel.Paint += new PaintEventHandler(PanelPaint);
        }

        public void StartMatrixDraw(Panel panel, int[][] matrix)
        {
            mainMatrix = matrix;
            /*foreach (int i in matrix)
                System.Diagnostics.Debug.Write(i);*/
            panel.Paint += new PaintEventHandler(MatrixPaint);
            panel.Invalidate();
        }

        public void ChangeFormState(Panel panel)
        {
            panel.Paint -= MatrixPaint;
            panel.Paint -= PanelPaint;
        }

        private void PanelPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Graphics graphicsDraw = e.Graphics;

            int panelWidth = panel.Width;
            int panelHeight = panel.Height;

            SolidBrush deepAqua = new SolidBrush(ColorTranslator.FromHtml("#003B46"));
            SolidBrush ocean = new SolidBrush(ColorTranslator.FromHtml("#07575B"));
            SolidBrush wave = new SolidBrush(ColorTranslator.FromHtml("#66A5AD"));
            SolidBrush seafoam = new SolidBrush(ColorTranslator.FromHtml("#C4DFE6"));

            graphicsDraw.FillRectangle(wave, new Rectangle(0, 0, panelWidth / 4, panelHeight));
            graphicsDraw.FillRectangle(wave, new Rectangle(3 * panelWidth / 4, 0, panelWidth / 4, panelHeight));
            graphicsDraw.FillRectangle(seafoam, new Rectangle(panelWidth / 4, 0, panelWidth / 2, panelHeight));
            graphicsDraw.FillRectangle(ocean, new Rectangle(0, 0, panelWidth / 4, panelHeight / 10));
            graphicsDraw.FillRectangle(ocean, new Rectangle(0, 9 * panelHeight / 10, panelWidth / 4, panelHeight / 10));
            graphicsDraw.FillRectangle(deepAqua, new Rectangle(panelWidth / 4, 0, panelWidth / 2, panelHeight / 10));
            
            deepAqua.Dispose();
            ocean.Dispose();
            wave.Dispose();
            seafoam.Dispose();
        }

        private void MatrixPaint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            Graphics graphicsDraw = e.Graphics;

            int panelWidth = panel.Width;
            int panelHeight = panel.Height;

            SolidBrush deepAqua = new SolidBrush(ColorTranslator.FromHtml("#003B46"));
            SolidBrush ocean = new SolidBrush(ColorTranslator.FromHtml("#07575B"));
            SolidBrush wave = new SolidBrush(ColorTranslator.FromHtml("#66A5AD"));
            SolidBrush seafoam = new SolidBrush(ColorTranslator.FromHtml("#C4DFE6"));

            Pen deepAquaPen = new Pen(deepAqua);
            deepAquaPen.Width = 5;

            int cubeWidth = panel.Width / 2 / (mainMatrix.GetLength(0) + 1);
            int cubeHeight = (panel.Height - panel.Height / 10) / (mainMatrix.GetLength(0) + 1);

            for (int i = 1; i <= mainMatrix.Length; i++)
            {
                graphicsDraw.DrawLine(deepAquaPen, panel.Width / 4 + cubeWidth * i, panelHeight / 10, panel.Width / 4 + cubeWidth * i, panelHeight);
            }
            for (int j = 1; j <= mainMatrix.Length; j++)
            {
                graphicsDraw.DrawLine(deepAquaPen, panel.Width / 4, panelHeight / 10 + cubeHeight * j, 3 * panel.Width / 4, panelHeight / 10 + cubeHeight * j);
            }

            Font numbers = new Font("Times New Roman", 200 / mainMatrix.GetLength(0));
            for (int i = 1; i <= mainMatrix.Length; i++)
                graphicsDraw.DrawString(i.ToString(), numbers, deepAqua, panel.Width / 4, panel.Height / 10 + i * cubeHeight);
            for (int j = 1; j <= mainMatrix.Length; j++)
                graphicsDraw.DrawString(j.ToString(), numbers, deepAqua, panel.Width / 4 + j * cubeWidth, panel.Height / 10);
            for (int i = 0; i < mainMatrix.Length; i++)
            {
                for (int j = 0; j < mainMatrix[i].Length; j++)
                {                    
                    if(i == j)
                        graphicsDraw.DrawString("-", numbers, deepAqua, panel.Width / 4 + (j + 1) * cubeWidth, panel.Height / 10 + (i + 1) * cubeHeight);
                    else if(mainMatrix[i][j] != -1)
                        graphicsDraw.DrawString(mainMatrix[i][j].ToString(), numbers, deepAqua, panel.Width / 4 + (j + 1) * cubeWidth, panel.Height / 10 + (i + 1) * cubeHeight);
                    
                }
            }

            graphicsDraw.Dispose();
            deepAqua.Dispose();
            ocean.Dispose();
            wave.Dispose();
            seafoam.Dispose();
        }

    }
}
