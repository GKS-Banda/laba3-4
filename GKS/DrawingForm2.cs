using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace GKS
{
    class DrawingForm2
    {
        private string[][] mainArray;
        private int[] groupPosition;

        public void StartDraw(Panel panel, string[][] mainArray, int[] groupPosition)
        {
            this.mainArray = mainArray;
            this.groupPosition = groupPosition;
            panel.Paint += new PaintEventHandler(PanelPaint);
            panel.Invalidate();
        }

        public void ChangeFormState(Panel panel)
        {
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

            int fontSize = mainArray.Length > mainArray[0].Length ? 200 / mainArray.Length : 200 / mainArray[0].Length;
            Font numbers = new Font("Times New Roman", fontSize);
            int letterJ = panelWidth / 2 / (mainArray[0].Length + 3);
            int letterI = 9 * panelHeight / 10 / (mainArray.Length + 2);

            for (int i = 0; i < mainArray.Length; i++)
            {
                graphicsDraw.DrawString(groupPosition[i] + ":", numbers, deepAqua, panelWidth / 4 + letterJ, panelHeight / 10 + (i + 1) * letterI);
                for (int j = 0; j < mainArray[i].Length; j++)
                    graphicsDraw.DrawString(mainArray[i][j], numbers, deepAqua, panelWidth / 4 + (j + 2) * letterJ, panelHeight / 10 + (i + 1) * letterI);
            }

            deepAqua.Dispose();
            ocean.Dispose();
            wave.Dispose();
            seafoam.Dispose();
            graphicsDraw.Dispose();
        }

    }
}
