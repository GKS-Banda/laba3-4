using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GKS
{
    class DrawingForm3
    {
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;
        private string[][] distinctGroups;
        private int groupNumber = 0;

        public void StartDraw(Panel panel, Dictionary<string, Dictionary<string, int>>[] relationMatrix, string[][] distinctGroups)
        {
            this.relationMatrix = relationMatrix;
            this.distinctGroups = distinctGroups;
            panel.Paint += new PaintEventHandler(PanelPaint);
            panel.Invalidate();
        }

        public void ChangeFormState(Panel panel)
        {
            panel.Paint -= PanelPaint;
        }

        public void ChangeGroup(int groupNumber, Panel panel)
        {
            this.groupNumber = groupNumber - 1;
            panel.Invalidate();
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

            Pen deepAquaPen = new Pen(deepAqua);
            deepAquaPen.Width = 5;

            int cubeWidth = panel.Width / 2 / (relationMatrix[groupNumber].Count + 1);
            int cubeHeight = (panel.Height - panel.Height / 10) / (relationMatrix[groupNumber].Count + 1);

            for (int i = 1; i <= relationMatrix[groupNumber].Count; i++)
            {
                graphicsDraw.DrawLine(deepAquaPen, panel.Width / 4 + cubeWidth * i, panelHeight / 10, panel.Width / 4 + cubeWidth * i, panelHeight);
            }
            for (int j = 1; j <= relationMatrix[groupNumber].Count; j++)
            {
                graphicsDraw.DrawLine(deepAquaPen, panel.Width / 4, panelHeight / 10 + cubeHeight * j, 3 * panel.Width / 4, panelHeight / 10 + cubeHeight * j);
            }

            Font numbers = new Font("Times New Roman", 200 / relationMatrix[0].Count);
            for (int i = 1; i <= distinctGroups[groupNumber].Length; i++)
                graphicsDraw.DrawString(distinctGroups[groupNumber][i - 1], numbers, deepAqua, panel.Width / 4, panel.Height / 10 + i * cubeHeight);
            for (int j = 1; j <= distinctGroups[groupNumber].Length; j++)
                graphicsDraw.DrawString(distinctGroups[groupNumber][j - 1], numbers, deepAqua, panel.Width / 4 + j * cubeWidth, panel.Height / 10);
            for (int i = 0; i < distinctGroups[groupNumber].Length; i++)
            {
                for (int j = 0; j < distinctGroups[groupNumber].Length; j++)
                {
                    graphicsDraw.DrawString(relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][j]].ToString(), numbers, deepAqua, panel.Width / 4 + (j + 1) * cubeWidth, panel.Height / 10 + (i + 1) * cubeHeight);
                }
            }

            deepAqua.Dispose();
            ocean.Dispose();
            wave.Dispose();
            seafoam.Dispose();
        }

    }
}
