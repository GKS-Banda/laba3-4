using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKS
{
    class ControlsForm2
    {
        private Panel mainPanel;
        private RichTextBox newGroupList;
        private DrawingForm2 df2;
        private string[][] newArray;
        private int[][] newGroups;
        private int[] groupPosition;

        public ControlsForm2(Panel panel, RichTextBox newGroupList)
        {
            mainPanel = panel;
            this.newGroupList = newGroupList;
        }

        public void ClearAndStart(int[][] outputGroups, string[][] mainArray)
        {
            Calculation2 c2 = new Calculation2(outputGroups.Clone() as int[][], mainArray.Clone() as string[][]);
            c2.StartCalculation(out newArray, out newGroups, out groupPosition);

            df2 = new DrawingForm2();
            df2.StartDraw(mainPanel, newArray, groupPosition);

            GroupsOutput();
        }

        private void GroupsOutput()
        {
            newGroupList.Text = "";
            for (int i = 0; i < newGroups.Length; i++)
            {
                newGroupList.Text += "Group " + groupPosition[i] + ": {";
                for (int j = 0; j < newGroups[i].Length; j++)
                {
                    newGroupList.Text += newGroups[i][j] + ", ";
                }
                newGroupList.Text = newGroupList.Text.Substring(0, newGroupList.Text.Length - 2);

                newGroupList.Text += "}\r\n";
            }
        }

        public int[][] ChangeState()
        {
            df2.ChangeFormState(mainPanel);
            mainPanel = null;
            newGroupList = null;
            df2 = null;

            return newGroups;
        }
    }
}
