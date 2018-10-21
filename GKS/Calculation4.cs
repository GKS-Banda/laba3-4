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
                List<List<string>> temp = MForm(MatrixGlue(first[i], second[i], third[i]), i);
                MGroup[i] = new string[temp.Count][];
                for (int j = 0; j < temp.Count; j++)
                    MGroup[i][j] = temp[j].Distinct().ToArray();
            }
        }

        private int[][] MatrixGlue(int[][] first, int[][] second, int[][] third)
        {
            int[][] joinedMatrix = new int[first.Length][];
            for(int i = 0; i < first.Length; i++)
            {
                joinedMatrix[i] = new int[first[i].Length];
                for(int j = 0; j < first[i].Length; j++)
                {
                    if (first[i][j] == 1)
                        joinedMatrix[i][j] = 1;
                    else if (second[i][j] == 1)
                        joinedMatrix[i][j] = 1;
                    else if (third[i][j] == 1)
                        joinedMatrix[i][j] = 1;
                }
            }

            foreach(int[] arr in joinedMatrix)
            {
                foreach (int i in arr)
                    System.Diagnostics.Debug.Write(i + " ");
                System.Diagnostics.Debug.WriteLine("");
            }

            return joinedMatrix;
        }

        private List<List<string>> MForm(int[][] matrix, int groupNumber)
        {
            List<List<string>> temp = new List<List<string>>();
            for(int i = 0; i < matrix.Length; i++)
            {
                List<string> newTemp = CheckCoord(matrix, i, groupNumber);
                if (newTemp.Count != 0)
                    temp.Add(newTemp);
            }

            return temp;
        }

        private List<string> CheckCoord(int[][]matrix, int row, int groupNumber)
        {
            bool containOne = false;
            List<string> temp = new List<string>();
            if (matrix[row].Max() == 1)
            {
                for (int i = 0; i < matrix[row].Length; i++)
                {
                    if (matrix[row][i] == 1)
                    {
                        containOne = true;
                        matrix[row][i] = -1;
                        matrix[i][row] = -1;
                        temp.Add(distinctGroups[groupNumber][row]);
                        temp.Add(distinctGroups[groupNumber][i]);
                        foreach (string s in CheckCoord(matrix, i, groupNumber))
                            temp.Add(s);
                    }
                }
            }
            else if(matrix[row].Max() == 0)
            {
                containOne = true;
                temp.Add(distinctGroups[groupNumber][row]);
            }
            if(containOne)
            {
                for (int i = 0; i < matrix[row].Length; i++)
                {
                    matrix[row][i] = -1;
                }
            }

            return temp;
        }

        private int[][][] ThirdOperation()
        {
            int[][][] thirdRelation = new int[distinctGroups.Length][][];

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                thirdRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    thirdRelation[i][j] = new int[distinctGroups[i].Length];
                }
            }

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == 1 && relationMatrix[i][distinctGroups[i][k]][distinctGroups[i][j]] == 1)
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
                        if(relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == 1)
                        {
                            List<string> temp = new List<string>();
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
