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
        private Button deleteObj;
        private Button arrowRight;
        private Button arrowLeft;
        private Label labNumber;
        private RichTextBox outputList;
        private RichTextBox outputKno;
        private RichTextBox outputGroupsList;
        private Label groupName;
        private Panel mainPanel;
        private DrawingForm1 df1;
        private ControlsForm2 cf2;
        private ControlsForm3 cf3;
        private ControlsForm4 cf4;
        private ControlsForm5 cf5;
        private ControlsForm6 cf6;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;
        private int[][] outputMatrix;
        private int[][] outputGroups;
        private int[][] cf2changeState;
        private string[][] cf3changeState;
        private string[][][] cf4changeState;
        private string[][] cf5changeState;
        private string[][] mainArray;
        private string[] Kno;
        private int formState = 1;
        private int curentLab = 1;
        private int labsMade = 6;

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
                Size = new System.Drawing.Size(panel.Width / 4, 5 * panel.Height / 10),
                BorderStyle = BorderStyle.None,
                ReadOnly = true
            };
            outputList.Enter += OutputList_Enter;
            panel.Controls.Add(outputList);

            deleteObj = new Button
            {
                Location = new System.Drawing.Point(0, 8 * panel.Height / 10),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#003B46"),
                Size = new System.Drawing.Size(panel.Width / 4, panel.Height / 10),
                FlatStyle = FlatStyle.Flat,
                Text = "Delete Last Object"
            };
            deleteObj.FlatAppearance.BorderSize = 0;
            deleteObj.Click += DeleteObj_Click;
            panel.Controls.Add(deleteObj);

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

            arrowLeft = new Button
            {
                Location = new System.Drawing.Point(0, 0),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#07575B"),
                Size = new System.Drawing.Size(panel.Width / 16, panel.Height / 10),
                FlatStyle = FlatStyle.Flat,
                Text = "←"
            };
            arrowLeft.FlatAppearance.BorderSize = 0;
            arrowLeft.Click += ArrowLeft_Click;
            arrowLeft.Click += NextPage_Click;
            panel.Controls.Add(arrowLeft);

            arrowRight = new Button
            {
                Location = new System.Drawing.Point(3 * mainPanel.Width / 16, 0),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#07575B"),
                Size = new System.Drawing.Size(panel.Width / 16, panel.Height / 10),
                FlatStyle = FlatStyle.Flat,
                Text = "→"
            };
            arrowRight.FlatAppearance.BorderSize = 0;
            arrowRight.Click += ArrowRight_Click;
            arrowRight.Click += NextPage_Click;
            panel.Controls.Add(arrowRight);

            labNumber = new Label
            {
                Location = new System.Drawing.Point(mainPanel.Width / 16, 0),
                Font = new System.Drawing.Font("Times New Roman", 24, System.Drawing.FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#C4DFE6"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#07575B"),
                Size = new System.Drawing.Size(mainPanel.Width / 8, mainPanel.Height / 10),
                BorderStyle = BorderStyle.None
            };
            labNumber.Enter += LabNumber_Enter;
            panel.Controls.Add(labNumber);

            labNumber.Text = "Lab " + curentLab;
            labNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }

        private void LabNumber_Enter(object sender, EventArgs e)
        {
            mainPanel.Focus();
        }

        private void ArrowRight_Click(object sender, EventArgs e)
        {
            if (curentLab < labsMade)
            {
                curentLab++;
                labNumber.Text = "Lab " + curentLab;
            }
        }

        private void ArrowLeft_Click(object sender, EventArgs e)
        {
            if (curentLab > 1)
            {
                curentLab--;
                labNumber.Text = "Lab " + curentLab;
            }
        }

        private void DeleteObj_Click(object sender, EventArgs e)
        {
            if (inputList.Count > 1)
            {
                inputList.RemoveAt(inputList.Count - 1);
                outputList.Text = "";
                int i = 1;
                foreach (string s in inputList)
                {
                    outputList.Text += i +"." + s + "\r\n";
                    i++;
                }
            }
            else if(inputList.Count == 1)
            {
                outputList.Text = "";
                inputList.Clear();
            }

            labInput.Focus();

            outputList.SelectionStart = outputList.Text.Length;
            outputList.ScrollToCaret();
        }

        private void NextPage_Click(object sender, EventArgs e)
        {
            switch (curentLab)
            {
                case 1:
                    cf2changeState = cf2.ChangeState();
                    cf2 = null;
                    State1();
                    formState = 1;
                    break;
                case 2:
                    if (formState == 3)
                    {
                        cf3.ChangeState();
                        cf3 = null;
                    }
                    cf2 = new ControlsForm2(mainPanel, outputGroupsList);
                    cf2.ClearAndStart(outputGroups, mainArray);
                    State2();
                    formState = 2;
                    break;
                case 3:
                    if (formState == 2)
                    {
                        cf2changeState = cf2.ChangeState();
                        cf2 = null;
                    }
                    else
                    {
                        cf4.ChangeState();
                        cf4 = null;
                    }
                    cf3 = new ControlsForm3(mainPanel);
                    cf3.ClearAndStart(cf2changeState, mainArray);
                    State3();
                    formState = 3;
                    break;
                case 4:

                    cf4 = new ControlsForm4(mainPanel, outputGroupsList);
                    if (formState == 3)
                    {
                        cf3changeState = cf3.ChangeState(out relationMatrix);
                        cf3 = null;
                    }
                    else
                    {
                        cf5.ChangeState();
                        cf5 = null;
                    }
                    cf4.ClearAndStart(mainArray, cf3changeState, relationMatrix);
                    //State4();
                    formState = 4;
                    break;
                case 5:
                    cf5 = new ControlsForm5(mainPanel, outputGroupsList);
                    if (formState == 4)
                    {
                        cf4.ChangeState(out cf4changeState);
                        cf4 = null;
                    }
                    else
                    {
                        cf6.ChangeState();
                        cf6 = null;
                    }
                    cf5.ClearAndStart(cf4changeState);
                    formState = 5;
                    break;
                case 6:
                    cf6 = new ControlsForm6(mainPanel, outputGroupsList);
                    cf5.ChangeState(out cf5changeState);
                    cf5 = null;
                    cf6.ClearAndStart(mainArray, cf5changeState);
                    formState = 6;
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
                inputList.Add(labInput.Text);
                outputList.Text += inputList.Count + "." + labInput.Text + "\r\n";
                labInput.Text = "";
                if (inputList.Count == 14)
                {
                    labInput.ReadOnly = true;
                    outputList.Text += "Maximum amount of objects reached";
                }
            }
            labInput.Focus();

            outputList.SelectionStart = outputList.Text.Length;
            outputList.ScrollToCaret();
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
            deleteObj.Enabled = false;
            deleteObj.Visible = false;
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
            deleteObj.Enabled = true;
            deleteObj.Visible = true;
            inputSubmit.Enabled = true;
            inputSubmit.Visible = true;
            outputKno.Enabled = true;
            outputKno.Visible = true;

            outputGroupsList.Text = "";

            CalculationEnd();
        }
    }
}
