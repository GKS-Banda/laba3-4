using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation4
    {
        private string[][] distinctGroups;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;
        private string[][][] MGroup;

        public Calculation4(string[][] distinctGroups, Dictionary<string, Dictionary<string, int>>[] relationMatrix)
        {
            this.distinctGroups = distinctGroups;
            this.relationMatrix = relationMatrix;
        }

        public void StartCalculation(out string[][][] MGroup)
        {
            GroupForm(ThirdOperation(), FourthOperation(), FifthOperation());

            MGroup = this.MGroup;
        }

        private void GroupForm(int[][][] first, int[][][] second, int[][][] third)
        {
            MGroup = new string[distinctGroups.Length][][];
            for(int i = 0; i < distinctGroups.Length; i++)
            {
                string[][][] MGroupTemp = new string[3][][];

                List<List<string>> temp = MatrixForm(first[i], i);
                MGroupTemp[0] = new string[temp.Count][];
                for (int j = 0; j < temp.Count; j++)
                    MGroupTemp[0][j] = temp[j].ToArray();

                temp = MatrixForm(second[i], i);
                MGroupTemp[1] = new string[temp.Count][];
                for (int j = 0; j < temp.Count; j++)
                    MGroupTemp[1][j] = temp[j].ToArray();

                temp = MatrixForm(third[i], i);
                MGroupTemp[2] = new string[temp.Count][];
                for (int j = 0; j < temp.Count; j++)
                    MGroupTemp[2][j] = temp[j].ToArray();

                for(int k = 0; k < 3; k++)
                {
                    for(int n = k + 1; n < 3; n++)
                    {
                        for (int l = 0; l < MGroupTemp[k].Length; k++)
                        {
                            for (int m = 0; m < MGroupTemp[k][l].Length; m++)
                            {
                                for (int a = 0; a < MGroupTemp[n].Length; a++)
                                {
                                    if (MGroupTemp[n][a].Contains(MGroupTemp[k][l][m]))
                                    {
                                        string[] tempArr = MGroupTemp[n][a].Clone() as string[];
                                        MGroupTemp[n][a] = MGroupTemp[n][a].Concat(MGroupTemp[k][l]).ToArray();
                                        MGroupTemp[k] = MGroupTemp[k].Where((arr, b) => b != l).ToArray();
                                    }
                                }
                            }

                        }
                    }
                }

                MGroup[i] = MGroupTemp[0].Concat(MGroupTemp[1].Concat(MGroupTemp[2]).ToArray()).ToArray();
            }
        }

        private List<List<string>> MatrixForm(int[][] matrix, int groupNumber)
        {
            int Max = 0;
            List<List<string>> MGroup = new List<List<string>>();
            bool[] checkAll = new bool[matrix.Length];
            do
            {
                List<string> temp = new List<string>();
                for (int i = 0; i < matrix.Length; i++)
                {
                    bool checkZero = false;
                    for(int j = 0; j < matrix[i].Length; j++)
                    {
                        if(matrix[i][j] == 1)
                        {
                            temp.Add(distinctGroups[groupNumber][i]);
                            temp.Add(distinctGroups[groupNumber][j]);
                            matrix[i][j] = -1;
                            matrix[j][i] = -1;
                            checkZero = true;
                            checkAll[i] = true;
                            checkAll[j] = true;
                            i = j;
                            break;
                        }
                    }
                    if (!checkZero)
                        break;
                }

                System.Diagnostics.Debug.Write(Max);

                foreach (int[] arr in matrix)
                    foreach (int m in arr)
                    {
                        if (Max < m)
                            Max = m;
                    }
                if (temp.Count > 0)
                    MGroup.Add(temp);
            }
            while (Max > 0);

            if(checkAll.Contains(false))
            {
                for(int i = 0; i < checkAll.Length; i++)
                {
                    if(!checkAll[i])
                    {
                        List<string> temp = new List<string>();
                        temp.Add(distinctGroups[groupNumber][i]);
                        MGroup.Add(temp);
                    }
                }
            }

            return MGroup;
        }

        /*private void RowCalc(int row, int column)
        {
            for(int i = 0; i <)
        }*/

        //-----???????????

        /*private int[][][] FirstOperation()
        {
            int[][][] firstRelation = new int[distinctGroups.Length][][];
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                firstRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    firstRelation[i][j] = new int[distinctGroups[i].Length];
                    bool first = true;
                    
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (j != distinctGroups[i].Length - 1 && distinctGroups[i][j + 1] == distinctGroups[i][k])
                        {
                            relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] = -1;
                        }
                        else if (j != 0 && distinctGroups[i][j - 1] == distinctGroups[i][k])
                        {
                            relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] = 1;
                        }
                    }
                }
            }
        }*/

        /*private int[][][] SecondOperation()
        {
            int[][][] secondRelation = new int[distinctGroups.Length][][];
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                secondRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    secondRelation[i][j] = new int[distinctGroups[i].Length];
                    bool second = true;

                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (j != k && relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] != 1)
                            second = false;
                    }

                    if(second)
                    {
                        for (int k = 0; k < distinctGroups[i].Length; k++)
                        {
                            secondRelation[i][j][k] = 1;
                            secondRelation[i][k][j] = 1;
                        }
                    }
                }
            }

            return secondRelation;
        }*/

        private int[][][] ThirdOperation()
        {
            int[][][] thirdRelation = new int[distinctGroups.Length][][];
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                thirdRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    thirdRelation[i][j] = new int[distinctGroups[i].Length];

                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == 2)
                        {
                            thirdRelation[i][j][k] = 1;
                            thirdRelation[i][k][j] = 1;
                        }
                    }
                }
            }
            return thirdRelation;
        }

        private int[][][] FourthOperation()
        {
            int[][][] fourthRelation = new int[distinctGroups.Length][][];

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                fourthRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    fourthRelation[i][j] = new int[distinctGroups[i].Length];
                }
            }

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == -1)
                        {
                            List<string> positionCheck = new List<string>();
                            positionCheck.Add(distinctGroups[i][j]);
                            FourthOperationRecursive(i, k, positionCheck);
                            if (positionCheck.Contains(distinctGroups[i][j]))
                            {
                                fourthRelation[i][k][j] = 1;
                                fourthRelation[i][j][k] = 1;
                                for (int l = 0; l < positionCheck.Count; l += 2)
                                {
                                    if (l != positionCheck.Count - 1)
                                    {
                                        fourthRelation[i][Array.IndexOf(distinctGroups[i], positionCheck[l])][Array.IndexOf(distinctGroups[i], positionCheck[l + 1])] = 1;
                                        fourthRelation[i][Array.IndexOf(distinctGroups[i], positionCheck[l + 1])][Array.IndexOf(distinctGroups[i], positionCheck[l])] = 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return fourthRelation;
        }

        private void FourthOperationRecursive(int i, int j, List<string> positionCheck)
        {
            for (int k = 0; k < distinctGroups[i].Length; k++)
            {
                if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == -1)
                {
                    if (positionCheck[0] != distinctGroups[i][k])
                    {
                        positionCheck.Add(distinctGroups[i][j]);
                        positionCheck.Add(distinctGroups[i][k]);
                        FourthOperationRecursive(i, k, positionCheck);
                    }
                    else
                    {
                        positionCheck.Add(distinctGroups[i][j]);
                    }
                }
            }
        }

        private int[][][] FifthOperation()
        {
            int[][][] fifthRelation = new int[distinctGroups.Length][][];

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                fifthRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    fifthRelation[i][j] = new int[distinctGroups[i].Length];
                }
            }

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == -1)
                        {
                            List<string> positionCheck = new List<string>();
                            positionCheck.Add(distinctGroups[i][j]);
                            FindLine(i, k, positionCheck);

                            for (int l = k; l < distinctGroups[i].Length; l++)
                            {
                                if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][l]] == -1)
                                {
                                    if (positionCheck.Contains(distinctGroups[i][l]))
                                    {
                                        int indexFound = positionCheck.IndexOf(distinctGroups[i][l]);
                                        fifthRelation[i][k][j] = 1;
                                        fifthRelation[i][j][k] = 1;
                                        for (int m = 0; m < indexFound; m += 2)
                                        {
                                            if (m != indexFound - 1) //??????
                                            {
                                                fifthRelation[i][Array.IndexOf(distinctGroups[i], positionCheck[m])][Array.IndexOf(distinctGroups[i], positionCheck[m + 1])] = 1;
                                                fifthRelation[i][Array.IndexOf(distinctGroups[i], positionCheck[m + 1])][Array.IndexOf(distinctGroups[i], positionCheck[m])] = 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return fifthRelation;
        }

        private void FindLine(int i, int j, List<string> positionCheck)
        {
            for (int k = 0; k < distinctGroups[i].Length; k++)
            {
                if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == -1)
                {
                    if (positionCheck[0] != distinctGroups[i][k])
                    {
                        positionCheck.Add(distinctGroups[i][j]);
                        positionCheck.Add(distinctGroups[i][k]);
                        FindLine(i, k, positionCheck);
                    }
                    else
                    {
                        positionCheck.Add(distinctGroups[i][j]);
                    }
                }
            }
        }
    }
}
