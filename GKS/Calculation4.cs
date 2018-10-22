﻿using System;
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
        //Yaroslav
        bool outGood = false;

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

            /*foreach(int[] arr in joinedMatrix)
            {
                foreach (int i in arr)
                    System.Diagnostics.Debug.Write(i + " ");
                System.Diagnostics.Debug.WriteLine("");
            }*/

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

            foreach (int[][] arr in thirdRelation)
            {
                foreach (int[] i in arr)
                {
                    foreach (int s in i)
                        System.Diagnostics.Debug.Write(s + " ");
                    System.Diagnostics.Debug.WriteLine("");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("-------------");
            }
            return thirdRelation;
        }

        /*private int[][][] FourthOperation()
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
                    List<string> temp = new List<string>();
                    temp.Add(distinctGroups[i][j]);
                    FourthOperationRecursive(j, i, temp);
                    if(temp.Count >= 3)
                    {
                        for(int m = 0; m < temp.Count; m++)
                        {
                            if (m != temp.Count - 1)
                            {
                                fourthRelation[i][Array.IndexOf(distinctGroups[i], temp[m])][Array.IndexOf(distinctGroups[i], temp[m + 1])] = 1;
                                fourthRelation[i][Array.IndexOf(distinctGroups[i], temp[m + 1])][Array.IndexOf(distinctGroups[i], temp[m])] = 1;
                            }
                        }
                    }
                }
            }
            foreach (int[][] arr in fourthRelation)
            {
                foreach (int[] i in arr)
                {
                    foreach (int s in i)
                        System.Diagnostics.Debug.Write(s + " ");
                    System.Diagnostics.Debug.WriteLine("");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("-------------");
            }
            return fourthRelation;
        }

        private bool FourthOperationRecursive(int column, int groupNumber, List<string> positionCheck)
        {
            bool deleteAll = false;
            bool checkAll = false;
            for (int i = 0; i < distinctGroups[groupNumber].Length; i++)
            {
                if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1)
                {
                    checkAll = true;
                    if (positionCheck.Contains(distinctGroups[groupNumber][i]))
                    {
                        if (!positionCheck[0].Equals(distinctGroups[groupNumber][i]))
                            deleteAll = true;
                        break;
                    }  
                    positionCheck.Add(distinctGroups[groupNumber][i]);
                    deleteAll = FourthOperationRecursive(i, groupNumber, positionCheck);
                }
            }

            if (deleteAll || !checkAll)
                positionCheck.Clear();

            return deleteAll || !checkAll;
        }*/

        //ЯРОСЛАВ
        private int[][][] FourthOperation()
        {
            
            int outNumber = 0;
            int[][][] fourthRelation = new int[distinctGroups.Length][][];
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                fourthRelation[i] = new int[distinctGroups[i].Length][];
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    fourthRelation[i][j] = new int[distinctGroups[i].Length];
                }
            }

            for (int i = 0; i < distinctGroups.Length; i++) //пробег по группам
            {
                for (int j = 0; j < distinctGroups[i].Length; j++) //пробег по колонкам
                {
                    for (int k = 0; k < distinctGroups[i].Length; k++) //пробег по рядкам
                    {
                        if(relationMatrix[i][distinctGroups[i][k]][distinctGroups[i][j]] == 1) //если 1 в матрице
                        {
                            List<string> temp = new List<string>(); //создаем лист
                            temp.Add(distinctGroups[i][j]); //добавляем в лист название колонки
                            List<string> first_column = new List<string>();
                            first_column.Add(distinctGroups[i][j]);
                            System.Diagnostics.Debug.Write("Temp_list1: ");
                            for(int o = 0; o < temp.Count; o++) {
                            System.Diagnostics.Debug.Write(temp[o] + " ");
                            }
                            outGood = false;
                            FourthOperationRecursive(k, i, temp, first_column); //рекурсивная операция
                            System.Diagnostics.Debug.Write("Temp_list2: ");
                            for(int o = 0; o < temp.Count; o++) {
                            System.Diagnostics.Debug.Write(temp[o] + " ");
                            }
                            //unknown
                            if(temp.Count >= 3)
                            {
                                for(int m = 0; m < temp.Count; m++)
                                {
                                    if (m != temp.Count - 1)
                                    {
                                        fourthRelation[i][Array.IndexOf(distinctGroups[i], temp[m])][Array.IndexOf(distinctGroups[i], temp[m + 1])] = 1;
                                        fourthRelation[i][Array.IndexOf(distinctGroups[i], temp[m + 1])][Array.IndexOf(distinctGroups[i], temp[m])] = 1;
                                    }
                                }
                            }
                            //

                        }
                    }
                }                       
            }

            return fourthRelation;
        }

        private bool FourthOperationRecursive(int row, int groupNumber, List<string> positionCheck, List<string> first_column)
        {
            System.Diagnostics.Debug.WriteLine("Начальная колонка: " + positionCheck[0] + " ");
            System.Diagnostics.Debug.Write("First_List: ");
                        for(int o = 0; o < first_column.Count; o++) {
                        System.Diagnostics.Debug.Write(first_column[o] + " ");
                        }
            bool checkAll = false;
            for(int i = 0; i < distinctGroups[groupNumber].Length; i++) //пробег по рядкам групп
            {
                if(relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][row]] == 1) //если 1 в матрице нашего столбца
                {
                    checkAll = true;
                        System.Diagnostics.Debug.WriteLine("Group" + (groupNumber+1) + ": ");
                        System.Diagnostics.Debug.Write("List: ");
                        for(int o = 0; o < positionCheck.Count; o++) {
                        System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                        }
                        
                    if (first_column.Contains(distinctGroups[groupNumber][i])) //если найденный рядок такой же как и наша первая колонка
                    {
                        System.Diagnostics.Debug.WriteLine("Wo" + distinctGroups[groupNumber][i] + " ");
                        positionCheck.Add(distinctGroups[groupNumber][row]);
                        System.Diagnostics.Debug.Write(positionCheck[positionCheck.Count-1] + " ");
                        System.Diagnostics.Debug.WriteLine("Out!");
                        outGood = true;
                        break;
                    }
                    else
                        positionCheck.Add(distinctGroups[groupNumber][row]);
                    if(!positionCheck.Contains(distinctGroups[groupNumber][i])) {
                        FourthOperationRecursive(i, groupNumber, positionCheck, first_column);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("Opa! Out: " + outGood);
            if(outGood == false)
                    positionCheck.Clear();
            return checkAll;
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
                List<string> reserve = new List<string>();
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    List<string> temp = new List<string>();
                    temp.Add(distinctGroups[i][j]);
                    FindLine(j, i, temp);
                    foreach (string s in reserve)
                        relationMatrix[i][s][distinctGroups[i][j]] = 1;
                    if (temp.Count >= 3)
                    {
                        for (int l = 0; l < temp.Count - 2; l++)
                        {
                            bool checkAll = false;
                            for (int m = temp.Count - 1; m > 1; m--)
                            {
                                if (relationMatrix[i][temp[m]][temp[0]] == 1)
                                {
                                    checkAll = true;
                                    for (int p = 0; p <= m; p++)
                                    {
                                        for(int o = 0; o <= m; o++)
                                        {
                                            if(o != p)
                                            {
                                                fifthRelation[i][Array.IndexOf(distinctGroups[i], temp[p])][Array.IndexOf(distinctGroups[i], temp[o])] = 1;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            if (!checkAll)
                                temp.RemoveAt(0);
                            else
                                break;
                        }
                    }
                    foreach (string s in reserve)
                        relationMatrix[i][s][distinctGroups[i][j]] = 0;
                    bool checkOne = false;
                    for(int l = 0; l < distinctGroups[i].Length; l++)
                    {
                        if (relationMatrix[i][distinctGroups[i][l]][distinctGroups[i][j]] == 1)
                        {
                            for (int m = l + 1; m < distinctGroups[i].Length; m++)
                            {
                                if (relationMatrix[i][distinctGroups[i][m]][distinctGroups[i][j]] == 1)
                                {
                                    relationMatrix[i][distinctGroups[i][l]][distinctGroups[i][j]] = 0;
                                    reserve.Add(distinctGroups[i][l]);
                                    checkOne = true;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    if(checkOne)
                    {
                        j--;
                    }
                    else
                    {
                        foreach(string s in reserve)
                            relationMatrix[i][s][distinctGroups[i][j]] = 1;
                        reserve.Clear();
                    }
                }
            }

            foreach (int[][] arr in fifthRelation)
            {
                foreach (int[] i in arr)
                {
                    foreach (int s in i)
                        System.Diagnostics.Debug.Write(s + " ");
                    System.Diagnostics.Debug.WriteLine("");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("-------------");
            }

            return fifthRelation;
        }

        private void FindLine(int column, int groupNumber, List<string> positionCheck)
        {
            for (int i = 0; i < distinctGroups[groupNumber].Length; i++)
            {
                if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1)
                {
                    if (positionCheck.Contains(distinctGroups[groupNumber][i]))
                        continue;
                    positionCheck.Add(distinctGroups[groupNumber][i]);
                    FindLine(i, groupNumber, positionCheck);
                    break;
                }
            }
        }
    }
}
