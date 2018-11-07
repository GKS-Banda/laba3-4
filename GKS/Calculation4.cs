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
        //Yaroslav
        bool outGood = false;
        int count = 0;
        int first_column = 0;
        bool createList = true;
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

                /*bool more5 = false;
                foreach(List<string> lst in temp)
                {
                    if (lst.Count > 5)
                        more5 = true;
                }

                if (more5)
                    FullGroup(first[i], second[i], third[i]);

                temp = MForm(MatrixGlue(first[i], second[i], third[i]), i);

                more5 = false;

                foreach (List<string> lst in temp)
                {
                    if (lst.Count > 5)
                        more5 = true;
                }

                if (more5)
                    temp = FullGroup2(first[i], second[i], third[i], i);*/

                MGroup[i] = new string[temp.Count][];
                for (int j = 0; j < temp.Count; j++)
                    MGroup[i][j] = temp[j].Distinct().ToArray();
            }
        }

        private void FullGroup(int[][] first, int[][] second, int[][] third)
        {
            for(int i = 0; i < third.Length; i++)
            {
                if (third[i].Count(n => n == 1) > 5)
                {
                    for (int j = 0; j < third[i].Length; j++)
                        third[i][j] = 0;
                }
            }
            for (int i = 0; i < second.Length; i++)
            {
                if (second[i].Count(n => n == 1) > 5)
                {
                    for (int j = 0; j < second[i].Length; j++)
                        second[i][j] = 0;
                }
            }
        }

        private List<List<string>> FullGroup2(int[][] first, int[][] second, int[][] third, int groupNumber)
        {
            int[][] temp = new int[first.Length][];
            for(int i = 0; i < first.Length; i++)
            {
                temp[i] = new int[first[i].Length];
            }

            List<List<string>> tempLst = MForm(MatrixGlue(first, second, temp), groupNumber);
            bool more5 = false;
            foreach (List<string> lst in tempLst)
            {
                if (lst.Count > 5)
                    more5 = true;
            }
            if(!more5)
            {
                List<List<string>> tempResult = MForm(third.Clone() as int[][], groupNumber);
                foreach (List<string> lst in tempResult)
                {
                    foreach(List<string> lst2 in tempLst)
                    {
                        foreach(string s in lst2)
                        {
                            if (lst.Contains(s))
                                lst.Remove(s);
                        }
                    }
                    if (lst.Count == 0)
                        tempResult.Remove(lst);
                }
                if (tempResult.Count != 0)
                    foreach (List<string> lst in tempResult)
                        tempLst.Add(lst);

                return tempLst;
            }
            else
            {
                tempLst = MForm(MatrixGlue(first, temp, third), groupNumber);
                more5 = false;
                foreach (List<string> lst in tempLst)
                {
                    if (lst.Count > 5)
                        more5 = true;
                }
                if (!more5)
                {
                    List<List<string>> tempResult = MForm(second.Clone() as int[][], groupNumber);
                    foreach (List<string> lst in tempResult)
                    {
                        foreach (List<string> lst2 in tempLst)
                        {
                            foreach (string s in lst2)
                            {
                                if (lst.Contains(s))
                                    lst.Remove(s);
                            }
                        }
                        if (lst.Count == 0)
                            tempResult.Remove(lst);
                    }
                    if (tempResult.Count != 0)
                        foreach (List<string> lst in tempResult)
                            tempLst.Add(lst);

                    return tempLst;
                }
                else
                {
                    tempLst = MForm(first.Clone() as int[][], groupNumber);
                    List<List<string>> tempResult = MForm(second.Clone() as int[][], groupNumber);
                    foreach (List<string> lst in tempResult)
                    {
                        foreach (List<string> lst2 in tempLst)
                        {
                            foreach (string s in lst2)
                            {
                                if (lst.Contains(s))
                                    lst.Remove(s);
                            }
                        }
                        if (lst.Count == 0)
                            tempResult.Remove(lst);
                    }
                    if (tempResult.Count != 0)
                        foreach (List<string> lst in tempResult)
                            tempLst.Add(lst);

                    tempResult = MForm(third.Clone() as int[][], groupNumber);
                    foreach (List<string> lst in tempResult)
                    {
                        foreach (List<string> lst2 in tempLst)
                        {
                            foreach (string s in lst2)
                            {
                                if (lst.Contains(s))
                                    lst.Remove(s);
                            }
                        }
                        if (lst.Count == 0)
                            tempResult.Remove(lst);
                    }
                    if (tempResult.Count != 0)
                        foreach (List<string> lst in tempResult)
                            tempLst.Add(lst);

                    return tempLst;
                }
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
            
            //int outNumber = 0;
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
                            /*System.Diagnostics.Debug.Write("Temp_list1: ");
                            for(int o = 0; o < temp.Count; o++) {
                            System.Diagnostics.Debug.Write(temp[o] + " ");
                            }*/
                            System.Diagnostics.Debug.WriteLine("НАЧАЛО:");
                            outGood = false;
                            FourthOperationRecursive(k, i, temp, first_column); //рекурсивная операция
                            if(outGood == false) {
                                temp.Clear();
                            }
                            System.Diagnostics.Debug.Write("Out_list: ");
                            for(int o = 0; o < temp.Count; o++) {
                            System.Diagnostics.Debug.Write(temp[o] + " ");
                            }
                            System.Diagnostics.Debug.WriteLine("");
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
            int i = 0;
            System.Diagnostics.Debug.WriteLine("Начальная колонка: " + positionCheck[0] + " ");
            System.Diagnostics.Debug.Write("List: ");
                        for(int o = 0; o < positionCheck.Count; o++) {
                        System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                        }
            System.Diagnostics.Debug.WriteLine("");
            bool checkAll = false;
            for(i = 0; i < distinctGroups[groupNumber].Length; i++) //пробег по рядкам групп
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.Write(distinctGroups[groupNumber][i] + ", " + distinctGroups[groupNumber][row] + ": ");
                System.Diagnostics.Debug.WriteLine(relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][row]]);
                if(relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][row]] == 1) //если 1 в матрице нашего столбца
                {
                    //checkAll = true;
                    System.Diagnostics.Debug.WriteLine("Group" + (groupNumber+1) + ": ");
                    System.Diagnostics.Debug.WriteLine("FIND: " + distinctGroups[groupNumber][row] + " ");
                    System.Diagnostics.Debug.WriteLine("");
                        /*System.Diagnostics.Debug.Write("List: ");
                        for(int o = 0; o < positionCheck.Count; o++) {
                        System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                        }*/
                        
                    if (first_column.Contains(distinctGroups[groupNumber][i])) //если найденный рядок такой же как и наша первая колонка
                    {
                        System.Diagnostics.Debug.WriteLine("Wo: " + distinctGroups[groupNumber][i] + " ");
                        positionCheck.Add(distinctGroups[groupNumber][row]);
                        System.Diagnostics.Debug.Write(positionCheck[positionCheck.Count-1] + " ");
                        System.Diagnostics.Debug.WriteLine("Out!");
                        System.Diagnostics.Debug.WriteLine("");
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
            if(outGood == false && i == (distinctGroups[groupNumber].Length-1))
                    positionCheck.Clear();
            else if(outGood == false && i != (distinctGroups[groupNumber].Length-1))
                positionCheck.RemoveAt(positionCheck.Count-1);
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
                    count = 0;
                    outGood = false;
                    FindLine(j, i, temp);
                    System.Diagnostics.Debug.WriteLine("Exit:");
                    for(int o = 0; o < temp.Count; o++) {
                        System.Diagnostics.Debug.Write(temp[o] + " ");
                    }
                    System.Diagnostics.Debug.WriteLine("");
                    temp = new List<string>(temp.Distinct().ToArray());
                    CreateList(i, temp);
                    if(createList == false) 
                    {
                        temp.Clear();
                    }
                    System.Diagnostics.Debug.WriteLine("New Exit:");
                    for(int o = 0; o < temp.Count; o++) {
                        System.Diagnostics.Debug.Write(temp[o] + " ");
                    }
                    System.Diagnostics.Debug.WriteLine("");
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
            /*System.Diagnostics.Debug.WriteLine("Column: " + column + " ");
            System.Diagnostics.Debug.WriteLine("");
            for (int i = 0; i < distinctGroups[groupNumber].Length; i++)
            {
                System.Diagnostics.Debug.WriteLine(distinctGroups[groupNumber][i] + " " + distinctGroups[groupNumber][column] + " ");
                System.Diagnostics.Debug.WriteLine("");
                count++;
                bool exit = false;
                bool proverka = false;
                if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1)
                {
                    if(count == 1) {
                        if (relationMatrix[groupNumber][distinctGroups[groupNumber][column]][distinctGroups[groupNumber][i]] == 1)
                        {
                            exit = true;
                        }
                        if(!exit) {
                            for (int j = i + 1; j < distinctGroups[groupNumber].Length; j++)
                            if (relationMatrix[groupNumber][distinctGroups[groupNumber][j]][distinctGroups[groupNumber][column]] == 1 && positionCheck.Count != 1)
                            {
                                proverka = true;
                            }

                            positionCheck.Add(distinctGroups[groupNumber][i]);
                        }
                    }   
                    /*bool exit = false;
                    bool relation = false;
                    if (relationMatrix[groupNumber][distinctGroups[groupNumber][column]][distinctGroups[groupNumber][i]] == 1) {
                        relation = true;
                    }

                    for(int j = 0; j < distinctGroups[groupNumber].Length; j++) { 
                        if(relationMatrix[groupNumber][distinctGroups[groupNumber][j]][distinctGroups[groupNumber][column]] == 1 && !(relationMatrix[groupNumber][distinctGroups[groupNumber][column]][distinctGroups[groupNumber][j]] == 1) && positionCheck.Count != 1) {
                            exit = true;
                        }
                    }
                    for(int j = 0; j < distinctGroups[groupNumber].Length; j++) { 
                        if(relationMatrix[groupNumber][distinctGroups[groupNumber][column]][distinctGroups[groupNumber][j]] == 1 && !(relationMatrix[groupNumber][distinctGroups[groupNumber][column]][distinctGroups[groupNumber][j]] == 1) && positionCheck.Count != 1) {
                            exit = true;
                        }
                    }
                    if(exit)
                        break;
                    else
                        positionCheck.Add(distinctGroups[groupNumber][i]);
                    /*for (int j = i + 1; j < distinctGroups[groupNumber].Length; j++)
                        if (relationMatrix[groupNumber][distinctGroups[groupNumber][j]][distinctGroups[groupNumber][column]] == 1 && positionCheck.Count != 1)
                        {
                            exit++;
                            
                        }

                    for (int j = 0; j < distinctGroups[groupNumber].Length; j++)
                        if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1 && j != column && positionCheck.Count != 1)
                        {
                            exit++;
                        }

                    if (positionCheck.Contains(distinctGroups[groupNumber][i]) && exit >= 1 ) {
                        positionCheck.Add(distinctGroups[groupNumber][i]);
                        continue;
                    }
                    positionCheck.Add(distinctGroups[groupNumber][i]);

                    if (exit != 0)
                        break;




                        System.Diagnostics.Debug.WriteLine("List:");
                    for(int o = 0; o < positionCheck.Count; o++) {
                            System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                            }
                        System.Diagnostics.Debug.WriteLine("");
                    if(exit == false)
                        FindLine(i, groupNumber, positionCheck);
                    break;
                }
            }*/
            System.Diagnostics.Debug.WriteLine(distinctGroups[groupNumber][column]);
            int i = 0;
            for (i = 0; i < distinctGroups[groupNumber].Length; i++)
            {
                if(count == 0)
                {
                    first_column = column;
                    if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1)
                    {
                        count++;
                        positionCheck.Add(distinctGroups[groupNumber][i]);
                        FindLine(i, groupNumber, positionCheck);
                    }
                }
                else if(count == 1) {
                    if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1 && positionCheck.Contains(distinctGroups[groupNumber][i]) == false)
                    {
                        count++;
                        positionCheck.Add(distinctGroups[groupNumber][i]);
                        if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][first_column]] == 1) 
                        {
                            outGood = true;
                            break;
                        }
                            FindLine(i, groupNumber, positionCheck);
                    }
                }
                else {
                    if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1 && positionCheck.Contains(distinctGroups[groupNumber][i]) == false)
                    {
                        count++;
                        positionCheck.Add(distinctGroups[groupNumber][i]);
                        if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][first_column]] == 1) 
                        {
                            outGood = true;
                            break;
                        }
                            FindLine(i, groupNumber, positionCheck);
                    }
                }
            }

            if(outGood == false && i == (distinctGroups[groupNumber].Length-1))
            {
                positionCheck.Clear();
                count = 0;
            }
            else if(outGood == false && i != (distinctGroups[groupNumber].Length-1))
            {
                count--;
                positionCheck.RemoveAt(positionCheck.Count-1);
            }

            
            System.Diagnostics.Debug.WriteLine("-----" + outGood + "Count: " + count);
            for(int o = 0; o < positionCheck.Count; o++) 
            {
                System.Diagnostics.Debug.Write(positionCheck[o] + " ");
            }
            System.Diagnostics.Debug.WriteLine("------");
        }

        private void CreateList(int groupNumber, List<string> positionCheck)
        {
            createList = true;
            positionCheck = new List<string>(positionCheck.Distinct().ToArray());

            for (int k = 0; k < positionCheck.Count; k++)
            {
                if(createList == false) 
                {
                     break;
                }
                if(k > 0 && k < positionCheck.Count-1)
                {
                    for (int j = 0; j < distinctGroups[groupNumber].Length; j++)
                    {
                        if(createList == false) 
                        {
                            break;
                        }
                        if(distinctGroups[groupNumber][j] == positionCheck[k])
                        {
                            for(int l = 0; l < distinctGroups[groupNumber].Length; l++) 
                            {
                                if(createList == false) 
                                {
                                    break;
                                }
                                if(relationMatrix[groupNumber][distinctGroups[groupNumber][l]][distinctGroups[groupNumber][j]] == 1) 
                                {
                                    if(distinctGroups[groupNumber][l] == positionCheck[k-1] || distinctGroups[groupNumber][l] == positionCheck[k+1]) 
                                    {
                                        createList = true;
                                        System.Diagnostics.Debug.WriteLine("OPA1");
                                    }
                                    else
                                    {
                                        createList = false;
                                        System.Diagnostics.Debug.WriteLine("OPA2");
                                        break;
                                    }
                                }
                            }
                            if(createList) 
                            {
                                for(int l = 0; l < distinctGroups[groupNumber].Length; l++) 
                                { 
                                    if(relationMatrix[groupNumber][distinctGroups[groupNumber][j]][distinctGroups[groupNumber][l]] == 1) 
                                    {
                                        System.Diagnostics.Debug.WriteLine("OPA");
                                        if(positionCheck.Count == 3) {
                                            if(distinctGroups[groupNumber][l] == positionCheck[k+1]) {
                                                createList = false;
                                                System.Diagnostics.Debug.WriteLine("POPA3");
                                                break;
                                            }
                                        }
                                        /*if(distinctGroups[groupNumber][l] == positionCheck[k+1]) {
                                            if(distinctGroups[groupNumber][l] == positionCheck[k+1]) {
                                                createList = false;
                                                System.Diagnostics.Debug.WriteLine("POPA3");
                                                break;
                                            }
                                        }*/
                                        System.Diagnostics.Debug.WriteLine("POPA");
                                        if(distinctGroups[groupNumber][l] == positionCheck[k-1] || distinctGroups[groupNumber][l] == positionCheck[k+1]) 
                                        {
                                            createList = true;
                                            System.Diagnostics.Debug.WriteLine("POPA1");
                                        }
                                        else
                                        {
                                            createList = false;
                                            System.Diagnostics.Debug.WriteLine("POPA2");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
               }
            }
            if(positionCheck.Count < 3)
                createList = false;
            if(createList == false) 
            {
                for(int o = 0; o < positionCheck.Count; o++) 
                {
                    System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                }
                positionCheck.Clear();
                System.Diagnostics.Debug.WriteLine("DELETE");
                for(int o = 0; o < positionCheck.Count; o++) 
                {
                    System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                }
            }
        }
    }
}