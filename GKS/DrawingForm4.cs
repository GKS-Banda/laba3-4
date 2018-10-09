using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GKS
{
    class DrawingForm4
    {

        public void StartDraw(Panel panel)
        {
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

            deepAqua.Dispose();
            ocean.Dispose();
            wave.Dispose();
            seafoam.Dispose();
            graphicsDraw.Dispose();
        }
    }
}
