using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation3
    {
        private int[][] group;
        private string[][] distinctGroups;
        private string[][] mainArray;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;

        public Calculation3(int[][] group, string[][] mainArray)
        {
            this.group = group;
            this.mainArray = mainArray;
        }

        public void StartCalculation(out Dictionary<string, Dictionary<string, int>>[] relationMatrix, out string[][] distinctGroups)
        {
            JoinGroup();
            FormRelationMatrix();

            relationMatrix = this.relationMatrix;
            distinctGroups = this.distinctGroups;
        }

        private void JoinGroup()
        {
            string[] selectGroup = new string[group.Length];
            for (int i = 0; i < group.Length; i++)
            {
                for (int j = 0; j < group[i].Length; j++)
                {
                    for (int k = 0; k < mainArray[group[i][j] - 1].Length; k++)
                    {
                        selectGroup[i] += mainArray[group[i][j] - 1][k];
                        selectGroup[i] += " ";
                    }
                }
            }
            distinctGroups = new string[group.Length][];
            for (int i = 0; i < group.Length; i++)
            {
                distinctGroups[i] = selectGroup[i].Split().Distinct().ToArray();
                distinctGroups[i] = distinctGroups[i].Where((arr, j) => j != distinctGroups[i].Length - 1).ToArray();
            }
        }

        private void FormRelationMatrix()
        {
            relationMatrix = new Dictionary<string, Dictionary<string, int>>[group.Length];
            for(int i = 0; i < distinctGroups.Length; i++)
            {
                relationMatrix[i] = new Dictionary<string, Dictionary<string, int>>();
                for(int j = 0; j < distinctGroups[i].Length; j++)
                {
                    Dictionary<string, int> temp = new Dictionary<string, int>();
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        temp.Add(distinctGroups[i][k], 0);
                    }
                    relationMatrix[i].Add(distinctGroups[i][j], temp);
                }
            }

            for (int i = 0; i < group.Length; i++)
            {
                for (int j = 0; j < group[i].Length; j++)
                {
                    for (int k = 0; k < mainArray[group[i][j] - 1].Length; k++)
                    {
                        if (k != mainArray[group[i][j] - 1].Length - 1)
                        {
                            if (relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k + 1]] == 1)
                                relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k + 1]] = 2;
                            else if(relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k + 1]] != 2)
                                relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k + 1]] = -1;
                        }
                        if (k != 0)
                        {
                            if (relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k - 1]] == -1)
                                relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k - 1]] = 2;
                            else if(relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k - 1]] != 2)
                                relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k - 1]] = 1;
                        } 
                    }
                }
            }
        }

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

        private int[][][] SecondOperation()
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

                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == 1)
                            if(relationMatrix[i][distinctGroups[i][k]][distinctGroups[i][j]] == 1)
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

                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == -1)
                        {
                            List<string> positionCheck = new List<string>();
                            positionCheck.Add(distinctGroups[i][j]);
                            FourthOperationRecursive(i, k, positionCheck);
                            if(positionCheck.Contains(distinctGroups[i][j]))
                            {
                                fourthRelation[i][k][j] = 1;
                                fourthRelation[i][j][k] = 1;
                                for (int l = 0; l < positionCheck.Count; l += 2)
                                {
                                    fourthRelation[i][Array.IndexOf(distinctGroups[i], positionCheck[l])][Array.IndexOf(distinctGroups[i], positionCheck[l + 1])] = 1;
                                    fourthRelation[i][Array.IndexOf(distinctGroups[i], positionCheck[l + 1])][Array.IndexOf(distinctGroups[i], positionCheck[l])] = 1;
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
            for(int k = 0; k < distinctGroups[i].Length; k++)
            {
                if(relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == -1)
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

                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == -1)
                        {
                            List<string> positionCheck = new List<string>();
                            positionCheck.Add(distinctGroups[i][j]);
                            FindLine(i, k, positionCheck);

                            for(int l = k; l < distinctGroups[i].Length; l++)
                            {
                                if(relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][l]] == -1)
                                {
                                    if(positionCheck.Contains(distinctGroups[i][l]))
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
