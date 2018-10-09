using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKS
{
    class ControlsForm3
    {
        private Panel mainPanel;
        private Label groupCount;
        private Button arrowRight;
        private Button arrowLeft;
        private DrawingForm3 df3;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;
        private string[][] distinctGroups;
        private int currentGroup = 1;

        public ControlsForm3(Panel panel)
        {
            mainPanel = panel;
        }

        public void ClearAndStart(int[][] groups, string[][] mainArray)
        {
            Calculation3 c3 = new Calculation3(groups.Clone() as int[][], mainArray.Clone() as string[][]);
            c3.StartCalculation(out relationMatrix, out distinctGroups);

            df3 = new DrawingForm3();
            df3.StartDraw(mainPanel, relationMatrix, distinctGroups);

            groupCount = new Label
            {
                Location = new System.Drawing.Point(5 * mainPanel.Width / 12, 0),
                Font = new System.Drawing.Font("Times New Roman", 36, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#003B46"),
                Size = new System.Drawing.Size(mainPanel.Width / 6, mainPanel.Height / 10),
                BorderStyle = BorderStyle.None
            };
            groupCount.Enter += GroupCount_Enter;
            mainPanel.Controls.Add(groupCount);

            groupCount.Text = currentGroup.ToString();
            groupCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            arrowLeft = new Button
            {
                Location = new System.Drawing.Point(mainPanel.Width / 4, 0),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#003B46"),
                Size = new System.Drawing.Size(mainPanel.Width / 6, mainPanel.Height / 10),
                FlatStyle = FlatStyle.Flat,
                Text = "←"
            };
            arrowLeft.FlatAppearance.BorderSize = 0;
            arrowLeft.Click += ArrowLeft_Click;
            mainPanel.Controls.Add(arrowLeft);

            arrowRight = new Button
            {
                Location = new System.Drawing.Point(7 * mainPanel.Width / 12, 0),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#003B46"),
                Size = new System.Drawing.Size(mainPanel.Width / 6, mainPanel.Height / 10),
                FlatStyle = FlatStyle.Flat,
                Text = "→"
            };
            arrowRight.FlatAppearance.BorderSize = 0;
            arrowRight.Click += ArrowRight_Click;
            mainPanel.Controls.Add(arrowRight);

        }

        private void GroupCount_Enter(object sender, EventArgs e)
        {
            mainPanel.Focus();
        }

        private void ArrowRight_Click(object sender, EventArgs e)
        {
            if (currentGroup < distinctGroups.Length)
            {
                currentGroup++;
                df3.ChangeGroup(currentGroup, mainPanel);
                groupCount.Text = currentGroup.ToString();
            }
        }

        private void ArrowLeft_Click(object sender, EventArgs e)
        {
            if(currentGroup > 1)
            {
                currentGroup--;
                df3.ChangeGroup(currentGroup, mainPanel);
                groupCount.Text = currentGroup.ToString();
            }
        }

        public void ChangeState()
        {
            df3.ChangeFormState(mainPanel);
            arrowLeft.Click -= ArrowLeft_Click;
            mainPanel.Controls.Remove(arrowLeft);
            arrowRight.Click -= ArrowRight_Click;
            mainPanel.Controls.Remove(arrowRight);
            groupCount.Enter -= GroupCount_Enter;
            mainPanel.Controls.Remove(groupCount);
            mainPanel = null;
            arrowLeft = null;
            arrowRight = null;
            groupCount = null;
            groupCount = null;
            df3 = null;
        }
    }
}
