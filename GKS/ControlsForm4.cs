using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKS
{
    class ControlsForm4
    {
        private Panel mainPanel;
        private RichTextBox newGroupList;
        private DrawingForm4 df4;
        private Label groupCount;
        private Button arrowRight;
        private Button arrowLeft;
        private string[][][] MGroup;
        private int currentGroup = 1;

        public ControlsForm4(Panel panel, RichTextBox newGroupList)
        {
            mainPanel = panel;
            this.newGroupList = newGroupList;
        }

        public void ClearAndStart(string[][] distinctGroups, Dictionary<string, Dictionary<string, int>>[] relationMatrix)
        {
            Calculation4 c4 = new Calculation4(distinctGroups, relationMatrix);
            c4.StartCalculation(out MGroup);

            df4 = new DrawingForm4();
            df4.StartDraw(mainPanel);

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

            GroupsOutput();
        }

        private void GroupCount_Enter(object sender, EventArgs e)
        {
            mainPanel.Focus();
        }

        private void ArrowRight_Click(object sender, EventArgs e)
        {
            if (currentGroup < MGroup.Length)
            {
                currentGroup++;
                GroupsOutput();
                groupCount.Text = currentGroup.ToString();
            }
        }

        private void ArrowLeft_Click(object sender, EventArgs e)
        {
            if (currentGroup > 1)
            {
                currentGroup--;
                GroupsOutput();
                groupCount.Text = currentGroup.ToString();
            }
        }

        private void GroupsOutput()
        {
            newGroupList.Text = "";
            for (int i = 0; i < MGroup[currentGroup - 1].Length; i++)
            {
                newGroupList.Text += "M " + (i + 1) + ": {";
                for (int j = 0; j < MGroup[currentGroup - 1][i].Length; j++)
                {
                    newGroupList.Text += MGroup[currentGroup - 1][i][j] + ", ";
                }
                newGroupList.Text = newGroupList.Text.Substring(0, newGroupList.Text.Length - 2);

                newGroupList.Text += "}\r\n";
            }
        }

        public void ChangeState()
        {
            df4.ChangeFormState(mainPanel);
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
            newGroupList = null;
            df4 = null;
        }
    }
}
