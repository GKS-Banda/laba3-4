using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation4Test
    {
        private string[][] distinctGroups;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;
        private string[][][] MGroup;
        private List<List<string>>[] combinations;
        private bool noOperation = true;
        //Yaroslav
        bool outGood = false;

        public Calculation4Test(string[][] distinctGroups, Dictionary<string, Dictionary<string, int>>[] relationMatrix)
        {
            this.distinctGroups = distinctGroups;
            this.relationMatrix = relationMatrix;
        }

        public void StartCalculation(out string[][][] MGroup)
        {
            //GroupForm(ThirdOperation(), FourthOperation(), FifthOperation());

            CreateCombinations();
            do
            {
                ThirdOperation();
                FourthOperation();
                FifthOperation();
            }
            while (!noOperation);

            TransformList();

            MGroup = this.MGroup;
        }

        private void CreateCombinations()
        {
            combinations = new List<List<string>>[distinctGroups.Length];
            for(int i = 0; i < distinctGroups.Length; i++)
            {
                combinations[i] = new List<List<string>>();
                
                foreach(string s in distinctGroups[i])
                {
                    List<string> temp = new List<string>();
                    temp.Add(s);
                    combinations[i].Add(temp);
                }
                
            }
        }

        private void TransformList()
        {
            MGroup = new string[distinctGroups.Length][][];
            for(int i = 0; i < distinctGroups.Length; i++)
            {
                MGroup[i] = new string[combinations[i].Count][];
                for(int j = 0; j < combinations[i].Count; j++)
                {
                    MGroup[i][j] = new string[combinations[i][j].Count];
                    for(int k = 0; k < combinations[i][j].Count; k++)
                    {
                        MGroup[i][j][k] = combinations[i][j][k];
                    }
                }
            }
        }

        private void ThirdOperation()
        {

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                for (int j = 0; j < distinctGroups[i].Length; j++)
                {
                    for (int k = 0; k < distinctGroups[i].Length; k++)
                    {
                        if (relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] == 1 && relationMatrix[i][distinctGroups[i][k]][distinctGroups[i][j]] == 1)
                        {
                            relationMatrix[i][distinctGroups[i][j]][distinctGroups[i][k]] = 0;
                            relationMatrix[i][distinctGroups[i][k]][distinctGroups[i][j]] = 0;
                            for (int m = 0; m < combinations[i].Count; m++)
                            {
                                if (combinations[i][m].Contains(distinctGroups[i][j]))
                                {
                                    combinations[i][m].Add(distinctGroups[i][k]);
                                    break;
                                }
                            }
                            for (int m = 0; m < combinations[i].Count; m++)
                            {
                                if (combinations[i][m].Contains(distinctGroups[i][k]))
                                {
                                    combinations[i][m].Remove(distinctGroups[i][k]);
                                    if (combinations[i][m].Count == 0)
                                        combinations[i].Remove(combinations[i][m]);
                                    break;
                                }
                            }

                            noOperation = false;
                        }
                    }
                }
            }

            /*foreach (int[][] arr in thirdRelation)
            {
                foreach (int[] i in arr)
                {
                    foreach (int s in i)
                        System.Diagnostics.Debug.Write(s + " ");
                    System.Diagnostics.Debug.WriteLine("");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("-------------");
            }*/
        }

        //ЯРОСЛАВ
        private void FourthOperation()
        {

            int outNumber = 0;

            for (int i = 0; i < distinctGroups.Length; i++) //пробег по группам
            {
                for (int j = 0; j < distinctGroups[i].Length; j++) //пробег по колонкам
                {
                    for (int k = 0; k < distinctGroups[i].Length; k++) //пробег по рядкам
                    {
                        if (relationMatrix[i][distinctGroups[i][k]][distinctGroups[i][j]] == 1) //если 1 в матрице
                        {
                            List<string> temp = new List<string>(); //создаем лист
                            temp.Add(distinctGroups[i][j]); //добавляем в лист название колонки
                            List<string> first_column = new List<string>();
                            first_column.Add(distinctGroups[i][j]);
                            System.Diagnostics.Debug.Write("Temp_list1: ");
                            for (int o = 0; o < temp.Count; o++)
                            {
                                System.Diagnostics.Debug.Write(temp[o] + " ");
                            }
                            outGood = false;
                            FourthOperationRecursive(k, i, temp, first_column); //рекурсивная операция
                            System.Diagnostics.Debug.Write("Temp_list2: ");
                            for (int o = 0; o < temp.Count; o++)
                            {
                                System.Diagnostics.Debug.Write(temp[o] + " ");
                            }
                            //unknown
                            if (temp.Count >= 3)
                            {
                                for (int m = 1; m < temp.Count; m++)
                                {
                                    
                                    relationMatrix[i][temp[m]][temp[m - 1]] = 0;

                                    for (int q = 0; q < combinations[i].Count; q++)
                                    {
                                        if (combinations[i][q].Contains(temp[0]))
                                        {
                                            combinations[i][q].Add(temp[m]);
                                            break;
                                        }
                                    }
                                    for (int q = 0; q < combinations[i].Count; q++)
                                    {
                                        if (combinations[i][q].Contains(temp[m]))
                                        {
                                            combinations[i][q].Remove(temp[m]);
                                            if (combinations[i][q].Count == 0)
                                                combinations[i].Remove(combinations[i][q]);
                                            break;
                                        }
                                    }
                                    
                                }

                                noOperation = false;
                            }
                            //

                        }
                    }
                }
            }
        }

        private bool FourthOperationRecursive(int row, int groupNumber, List<string> positionCheck, List<string> first_column)
        {
            System.Diagnostics.Debug.WriteLine("Начальная колонка: " + positionCheck[0] + " ");
            System.Diagnostics.Debug.Write("First_List: ");
            for (int o = 0; o < first_column.Count; o++)
            {
                System.Diagnostics.Debug.Write(first_column[o] + " ");
            }
            bool checkAll = false;
            for (int i = 0; i < distinctGroups[groupNumber].Length; i++) //пробег по рядкам групп
            {
                if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][row]] == 1) //если 1 в матрице нашего столбца
                {
                    checkAll = true;
                    System.Diagnostics.Debug.WriteLine("Group" + (groupNumber + 1) + ": ");
                    System.Diagnostics.Debug.Write("List: ");
                    for (int o = 0; o < positionCheck.Count; o++)
                    {
                        System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                    }

                    if (first_column.Contains(distinctGroups[groupNumber][i])) //если найденный рядок такой же как и наша первая колонка
                    {
                        System.Diagnostics.Debug.WriteLine("Wo" + distinctGroups[groupNumber][i] + " ");
                        positionCheck.Add(distinctGroups[groupNumber][row]);
                        System.Diagnostics.Debug.Write(positionCheck[positionCheck.Count - 1] + " ");
                        System.Diagnostics.Debug.WriteLine("Out!");
                        outGood = true;
                        break;
                    }
                    else
                        positionCheck.Add(distinctGroups[groupNumber][row]);
                    if (!positionCheck.Contains(distinctGroups[groupNumber][i]))
                    {
                        FourthOperationRecursive(i, groupNumber, positionCheck, first_column);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("Opa! Out: " + outGood);
            if (outGood == false)
                positionCheck.Clear();
            return checkAll;
        }




        private void FifthOperation()
        {

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
                                    relationMatrix[i][temp[m]][temp[0]] = 0;

                                    checkAll = true;
                                    for (int p = 1; p <= m; p++)
                                    {
                                         relationMatrix[i][temp[p]][temp[p - 1]] = 0;

                                         for (int q = 0; q < combinations[i].Count; q++)
                                         {
                                             if (combinations[i][q].Contains(temp[0]))
                                             {
                                                 combinations[i][q].Add(temp[p]);
                                                 break;
                                             }
                                         }
                                         for (int q = 0; q < combinations[i].Count; q++)
                                         {
                                             if (combinations[i][q].Contains(temp[p]))
                                             {
                                                 combinations[i][q].Remove(temp[p]);
                                                 if (combinations[i][q].Count == 0)
                                                     combinations[i].Remove(combinations[i][q]);
                                                 break;
                                             }
                                         }

                                    }

                                    noOperation = false;

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
                    for (int l = 0; l < distinctGroups[i].Length; l++)
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
                    if (checkOne)
                    {
                        j--;
                    }
                    else
                    {
                        foreach (string s in reserve)
                            relationMatrix[i][s][distinctGroups[i][j]] = 1;
                        reserve.Clear();
                    }
                }
            }

            /*foreach (int[][] arr in fifthRelation)
            {
                foreach (int[] i in arr)
                {
                    foreach (int s in i)
                        System.Diagnostics.Debug.Write(s + " ");
                    System.Diagnostics.Debug.WriteLine("");
                }
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("-------------");
            }*/
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

                    bool exit = false;

                    for (int j = i + 1; j < distinctGroups[groupNumber].Length; j++)
                        if (relationMatrix[groupNumber][distinctGroups[groupNumber][j]][distinctGroups[groupNumber][column]] == 1)
                        {
                            exit = true;
                            break;
                        }

                    for (int j = 0; j < distinctGroups[groupNumber].Length; j++)
                        if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1 && j != column)
                        {
                            exit = true;
                            break;
                        }

                    if (exit)
                        break;

                    FindLine(i, groupNumber, positionCheck);
                    break;
                }
            }
        }
    }
}
