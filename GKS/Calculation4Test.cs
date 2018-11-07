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
        private bool noOperation;
        //Yaroslav
        bool outGood = false;
        int count = 0;
        int first_column = 0;
        bool createList = true;

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
                noOperation = true;
                ThirdOperation();
                FourthOperation();
                FifthOperation();

                foreach(List<List<string>> a in combinations)
                {
                    System.Diagnostics.Debug.WriteLine("List:---------------");
                    foreach(List<string> b in a)
                    {
                        System.Diagnostics.Debug.WriteLine("One List:---------------");
                        foreach(string c in b)
                            System.Diagnostics.Debug.WriteLine(c);
                    }
                }
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
                                    for (int q = 0; q < combinations[i].Count; q++)
                                    {
                                        if (combinations[i][q].Contains(distinctGroups[i][k]))
                                        {
                                            if (combinations[i][q].Count == 1)
                                            {
                                                combinations[i][m].Add(distinctGroups[i][k]);
                                                combinations[i][q].Remove(distinctGroups[i][k]);
                                                if (combinations[i][q].Count == 0)
                                                    combinations[i].Remove(combinations[i][q]);
                                            }
                                            else if(combinations[i][q].Count > 1)
                                            {
                                                foreach (string s in combinations[i][q])
                                                    combinations[i][m].Add(s);
                                                combinations[i].Remove(combinations[i][q]);
                                            }
                                            break;
                                        }
                                    }

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
                            /*System.Diagnostics.Debug.Write("Temp_list1: ");
                            for(int o = 0; o < temp.Count; o++) {
                            System.Diagnostics.Debug.Write(temp[o] + " ");
                            }*/
                            System.Diagnostics.Debug.WriteLine("НАЧАЛО:");
                            outGood = false;
                            FourthOperationRecursive(k, i, temp, first_column); //рекурсивная операция
                            if (outGood == false)
                            {
                                temp.Clear();
                            }
                            System.Diagnostics.Debug.Write("Out_list: ");
                            for (int o = 0; o < temp.Count; o++)
                            {
                                System.Diagnostics.Debug.Write(temp[o] + " ");
                            }
                            System.Diagnostics.Debug.WriteLine("");
                            //unknown
                            if (temp.Count >= 3)
                            {
                                for (int m = 1; m < temp.Count; m++)
                                {

                                    relationMatrix[i][temp[m]][temp[m - 1]] = 0;

                                    for (int w = 0; w < combinations[i].Count; w++)
                                    {
                                        if (combinations[i][w].Contains(temp[0]))
                                        {
                                            for (int q = 0; q < combinations[i].Count; q++)
                                            {
                                                if (combinations[i][q].Contains(temp[m]))
                                                {
                                                    if (combinations[i][q].Count == 1)
                                                    {
                                                        combinations[i][w].Add(temp[m]);
                                                        combinations[i][q].Remove(temp[m]);
                                                        if (combinations[i][q].Count == 0)
                                                            combinations[i].Remove(combinations[i][q]);
                                                    }
                                                    else if (combinations[i][q].Count > 1)
                                                    {
                                                        foreach (string s in combinations[i][q])
                                                            combinations[i][w].Add(s);
                                                        combinations[i].Remove(combinations[i][q]);
                                                    }
                                                    break;
                                                }
                                            }

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
            int i = 0;
            System.Diagnostics.Debug.WriteLine("Начальная колонка: " + positionCheck[0] + " ");
            System.Diagnostics.Debug.Write("List: ");
            for (int o = 0; o < positionCheck.Count; o++)
            {
                System.Diagnostics.Debug.Write(positionCheck[o] + " ");
            }
            System.Diagnostics.Debug.WriteLine("");
            bool checkAll = false;
            for (i = 0; i < distinctGroups[groupNumber].Length; i++) //пробег по рядкам групп
            {
                System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.Write(distinctGroups[groupNumber][i] + ", " + distinctGroups[groupNumber][row] + ": ");
                System.Diagnostics.Debug.WriteLine(relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][row]]);
                if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][row]] == 1) //если 1 в матрице нашего столбца
                {
                    //checkAll = true;
                    System.Diagnostics.Debug.WriteLine("Group" + (groupNumber + 1) + ": ");
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
                        System.Diagnostics.Debug.Write(positionCheck[positionCheck.Count - 1] + " ");
                        System.Diagnostics.Debug.WriteLine("Out!");
                        System.Diagnostics.Debug.WriteLine("");
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
            if (outGood == false && i == (distinctGroups[groupNumber].Length - 1))
                positionCheck.Clear();
            else if (outGood == false && i != (distinctGroups[groupNumber].Length - 1))
                positionCheck.RemoveAt(positionCheck.Count - 1);
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
                    count = 0;
                    outGood = false;
                    FindLine(j, i, temp);
                    System.Diagnostics.Debug.WriteLine("Exit:");
                    for (int o = 0; o < temp.Count; o++)
                    {
                        System.Diagnostics.Debug.Write(temp[o] + " ");
                    }
                    System.Diagnostics.Debug.WriteLine("");
                    temp = new List<string>(temp.Distinct().ToArray());
                    CreateList(i, temp);
                    if (createList == false)
                    {
                        temp.Clear();
                    }
                    System.Diagnostics.Debug.WriteLine("New Exit:");
                    for (int o = 0; o < temp.Count; o++)
                    {
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
                                    relationMatrix[i][temp[m]][temp[0]] = 0;

                                    checkAll = true;
                                    for (int p = 1; p <= m; p++)
                                    {
                                        relationMatrix[i][temp[p]][temp[p - 1]] = 0;

                                        for (int w = 0; w < combinations[i].Count; w++)
                                        {
                                            if (combinations[i][w].Contains(temp[0]))
                                            {
                                                for (int q = 0; q < combinations[i].Count; q++)
                                                {
                                                    if (combinations[i][q].Contains(temp[p]))
                                                    {
                                                        if (combinations[i][q].Count == 1)
                                                        {
                                                            combinations[i][w].Add(temp[p]);
                                                            combinations[i][q].Remove(temp[p]);
                                                            if (combinations[i][q].Count == 0)
                                                                combinations[i].Remove(combinations[i][q]);
                                                        }
                                                        else if (combinations[i][q].Count > 1)
                                                        {
                                                            foreach (string s in combinations[i][q])
                                                                combinations[i][w].Add(s);
                                                            combinations[i].Remove(combinations[i][q]);
                                                        }
                                                        break;
                                                    }
                                                }

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
                if (count == 0)
                {
                    first_column = column;
                    if (relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][column]] == 1)
                    {
                        count++;
                        positionCheck.Add(distinctGroups[groupNumber][i]);
                        FindLine(i, groupNumber, positionCheck);
                    }
                }
                else if (count == 1)
                {
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
                else
                {
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

            if (outGood == false && i == (distinctGroups[groupNumber].Length - 1))
            {
                positionCheck.Clear();
                count = 0;
            }
            else if (outGood == false && i != (distinctGroups[groupNumber].Length - 1))
            {
                count--;
                positionCheck.RemoveAt(positionCheck.Count - 1);
            }


            System.Diagnostics.Debug.WriteLine("-----" + outGood + "Count: " + count);
            for (int o = 0; o < positionCheck.Count; o++)
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
                if (createList == false)
                {
                    break;
                }
                if (k > 0 && k < positionCheck.Count - 1)
                {
                    for (int j = 0; j < distinctGroups[groupNumber].Length; j++)
                    {
                        if (createList == false)
                        {
                            break;
                        }
                        if (distinctGroups[groupNumber][j] == positionCheck[k])
                        {
                            for (int l = 0; l < distinctGroups[groupNumber].Length; l++)
                            {
                                if (createList == false)
                                {
                                    break;
                                }
                                if (relationMatrix[groupNumber][distinctGroups[groupNumber][l]][distinctGroups[groupNumber][j]] == 1)
                                {
                                    if (distinctGroups[groupNumber][l] == positionCheck[k - 1] || distinctGroups[groupNumber][l] == positionCheck[k + 1])
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
                            if (createList)
                            {
                                for (int l = 0; l < distinctGroups[groupNumber].Length; l++)
                                {
                                    if (relationMatrix[groupNumber][distinctGroups[groupNumber][j]][distinctGroups[groupNumber][l]] == 1)
                                    {
                                        System.Diagnostics.Debug.WriteLine("OPA");
                                        if (positionCheck.Count == 3)
                                        {
                                            if (distinctGroups[groupNumber][l] == positionCheck[k + 1])
                                            {
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
                                        if (distinctGroups[groupNumber][l] == positionCheck[k - 1] || distinctGroups[groupNumber][l] == positionCheck[k + 1])
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
            if (positionCheck.Count < 3)
                createList = false;
            if (createList == false)
            {
                for (int o = 0; o < positionCheck.Count; o++)
                {
                    System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                }
                positionCheck.Clear();
                System.Diagnostics.Debug.WriteLine("DELETE");
                for (int o = 0; o < positionCheck.Count; o++)
                {
                    System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                }
            }
        }
    }
}
