using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GKS
{
    class ControlsForm5
    {
        private Panel mainPanel;
        private RichTextBox newGroupList;
        private DrawingForm4 df4;
        private string[][] finalGroups;

        public ControlsForm5(Panel panel, RichTextBox newGroupList)
        {
            mainPanel = panel;
            this.newGroupList = newGroupList;
        }

        public void ClearAndStart(string[][][] MGroup)
        {
            Calculation5 c5 = new Calculation5(MGroup);
            c5.StartCalculation(out finalGroups);

            df4 = new DrawingForm4();
            df4.StartDraw(mainPanel);

            GroupsOutput();
        }

        private void GroupsOutput()
        {
            newGroupList.Text = "";
            for (int i = 0; i < finalGroups.Length; i++)
            {
                newGroupList.Text += "M" + (i + 1) + ": {";
                for (int j = 0; j < finalGroups[i].Length; j++)
                {
                    newGroupList.Text += finalGroups[i][j] + ", ";
                }
                newGroupList.Text = newGroupList.Text.Substring(0, newGroupList.Text.Length - 2);

                newGroupList.Text += "}\r\n";
            }
        }

        public void ChangeState()
        {
            df4.ChangeFormState(mainPanel);
            mainPanel = null;
            newGroupList = null;
            df4 = null;
        }

        public void ChangeState(out string[][] finalGroups)
        {
            df4.ChangeFormState(mainPanel);
            mainPanel = null;
            newGroupList = null;
            df4 = null;

            finalGroups = this.finalGroups;
        }
    }
}
