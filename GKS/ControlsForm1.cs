using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GKS
{
    class ControlsForm1
    {
        private RichTextBox labInput;
        private List<string> inputList;
        private string[] mainInput;
        private Button inputSubmit;
        private Button startCalc;
        private Button nextPage;
        private RichTextBox outputList;
        private RichTextBox outputKno;
        private RichTextBox outputGroupsList;
        private Label groupName;
        private Panel mainPanel;
        private DrawingForm1 df1;
        private ControlsForm2 cf2;
        private ControlsForm3 cf3;
        private ControlsForm4 cf4;
        private int itemsCount = 0;
        private int[][] outputMatrix;
        private int[][] outputGroups;
        private string[][] mainArray;
        private string[] Kno;
        private int formState = 1;

        public void StartControls(Panel panel)
        {
            mainPanel = panel;

            inputList = new List<string>();

            labInput = new RichTextBox
            {
                Location = new System.Drawing.Point(0, panel.Height / 10),
                Font = new System.Drawing.Font("Times New Roman", 36, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#66A5AD"),
                Size = new System.Drawing.Size(panel.Width / 4, panel.Height / 10),
                BorderStyle = BorderStyle.None
            };
            labInput.TextChanged += LabInput_TextChanged;
            panel.Controls.Add(labInput);

            inputSubmit = new Button
            {
                Location = new System.Drawing.Point(0, 2 * panel.Height / 10),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#07575B"),
                Size = new System.Drawing.Size(panel.Width / 4, panel.Height / 10),
                FlatStyle = FlatStyle.Flat,
                Text = "Submit Input"
            };
            inputSubmit.FlatAppearance.BorderSize = 0;
            inputSubmit.Click += InputSubmit_Click;
            panel.Controls.Add(inputSubmit);

            outputList = new RichTextBox
            {
                Location = new System.Drawing.Point(0, 3 * panel.Height / 10),
                Font = new System.Drawing.Font("Times New Roman", 22, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#66A5AD"),
                Size = new System.Drawing.Size(panel.Width / 4, 6 * panel.Height / 10),
                BorderStyle = BorderStyle.None,
                ReadOnly = true
            };
            outputList.Enter += OutputList_Enter;
            panel.Controls.Add(outputList);

            startCalc = new Button
            {
                Location = new System.Drawing.Point(0, 9 * panel.Height / 10),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#07575B"),
                Size = new System.Drawing.Size(panel.Width / 4, panel.Height / 10),
                FlatStyle = FlatStyle.Flat,
                Text = "Start Calculation"
            };
            startCalc.FlatAppearance.BorderSize = 0;
            startCalc.Click += StartCalc_Click;
            panel.Controls.Add(startCalc);

            outputKno = new RichTextBox
            {
                Location = new System.Drawing.Point(panel.Width / 4, 0),
                Font = new System.Drawing.Font("Times New Roman", 30, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#003B46"),
                Size = new System.Drawing.Size(panel.Width / 2, panel.Height / 10),
                BorderStyle = BorderStyle.None,
                ReadOnly = true
            };
            outputKno.Enter += OutputList_Enter;
            panel.Controls.Add(outputKno);

            outputGroupsList = new RichTextBox
            {
                Location = new System.Drawing.Point(3 * panel.Width / 4, panel.Height / 10),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#66A5AD"),
                Size = new System.Drawing.Size(panel.Width / 4, 9 * panel.Height / 10),
                BorderStyle = BorderStyle.None,
                ReadOnly = true
            };
            outputGroupsList.Enter += OutputList_Enter;
            panel.Controls.Add(outputGroupsList);

            groupName = new Label
            {
                Location = new System.Drawing.Point(3 * panel.Width / 4, 0),
                Font = new System.Drawing.Font("Times New Roman", 36, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#07575B"),
                Size = new System.Drawing.Size(panel.Width / 4, panel.Height / 10),
                BorderStyle = BorderStyle.None
            };
            groupName.Enter += OutputList_Enter;
            panel.Controls.Add(groupName);

            nextPage = new Button
            {
                Location = new System.Drawing.Point(0, 0),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#07575B"),
                Size = new System.Drawing.Size(panel.Width / 4, panel.Height / 10),
                FlatStyle = FlatStyle.Flat,
                Text = "Next Page"
            };
            nextPage.FlatAppearance.BorderSize = 0;
            nextPage.Click += NextPage_Click;
            panel.Controls.Add(nextPage);
        }

        private void NextPage_Click(object sender, EventArgs e)
        {
            switch (formState)
            {
                case 1:
                    formState = 2;
                    State2();
                    cf2 = new ControlsForm2(mainPanel, outputGroupsList);
                    cf2.ClearAndStart(outputGroups, mainArray);
                    break;
                case 2:
                    formState = 3;
                    cf3 = new ControlsForm3(mainPanel);
                    cf3.ClearAndStart(cf2.ChangeState(), mainArray);
                    cf2 = null;
                    State3();
                    break;
                case 3:
                    formState = 4;
                    cf4 = new ControlsForm4(mainPanel, outputGroupsList);
                    Dictionary<string, Dictionary<string, int>>[] relationMatrix;
                    cf4.ClearAndStart(cf3.ChangeState(out relationMatrix), relationMatrix);
                    cf3 = null;
                    //State4();
                    break;
                case 4:
                    formState = 1;
                    cf4.ChangeState();
                    cf4 = null;
                    State1();
                    break;

            }
        }

        private void OutputList_Enter(object sender, EventArgs e)
        {
            startCalc.Focus();
        }

        private void LabInput_TextChanged(object sender, EventArgs e)
        {
            labInput.Text = labInput.Text.ToUpper();
            labInput.SelectionStart = labInput.Text.Length;
        }

        private void InputSubmit_Click(object sender, EventArgs e)
        {
            if (labInput.Text != "")
            {
                itemsCount++;
                outputList.Text += itemsCount + "." + labInput.Text + "\r\n";
                inputList.Add(labInput.Text);
                labInput.Text = "";
                if (itemsCount == 14)
                {
                    labInput.ReadOnly = true;
                    outputList.Text += "Maximum amount of objects reached";
                }
            }
            labInput.Focus();
        }

        private void StartCalc_Click(object sender, EventArgs e)
        {
            if (inputList.Count != 0)
            {
                labInput.ReadOnly = true;
                mainInput = new string[inputList.Count];
                mainInput = inputList.ToArray<string>();
                Calculation1 calc = new Calculation1();
                calc.MainCalculation(mainInput, out outputMatrix, out outputGroups, out Kno, out mainArray);
                CalculationEnd();
            }
        }

        private void CalculationEnd()
        {
            df1 = new DrawingForm1();
            df1.StartMatrixDraw(mainPanel, outputMatrix);

            outputKno.Text = "К: {";
            foreach (string s in Kno)
            {
                outputKno.Text += s;
                outputKno.Text += " ";
            }
            outputKno.Text += "} = ";
            outputKno.Text += Kno.Count();

            groupName.Text = "Groups:";

            for(int i = 0; i < outputGroups.Length; i++)
            {
                 outputGroupsList.Text += "Group " + (i + 1) + ": {";
                 for (int j = 0; j < outputGroups[i].Length; j++)
                 {
                     outputGroupsList.Text += outputGroups[i][j] + ", ";
                 }
                 outputGroupsList.Text = outputGroupsList.Text.Substring(0, outputGroupsList.Text.Length - 2);

                 outputGroupsList.Text += "}\r\n";
            }

        }

        private void State3()
        {
            groupName.Text = "";
            outputGroupsList.Text = "";
        }

        private void State2()
        {
            groupName.Text = "New Groups";
            startCalc.Enabled = false;
            startCalc.Visible = false;
            inputSubmit.Enabled = false;
            inputSubmit.Visible = false;
            outputKno.Enabled = false;
            outputKno.Visible = false;

            df1.ChangeFormState(mainPanel);
        }

        private void State1()
        {
            groupName.Text = "Groups";
            startCalc.Enabled = true;
            startCalc.Visible = true;
            inputSubmit.Enabled = true;
            inputSubmit.Visible = true;
            outputKno.Enabled = true;
            outputKno.Visible = true;

            outputGroupsList.Text = "";

            CalculationEnd();
        }
    }
}
