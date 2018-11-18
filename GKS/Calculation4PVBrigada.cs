using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation4PVBrigada
    {
        private List<List<string>>[] distinctGroups;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;
        private string[][][] MGroup;
        private List<List<string>>[] combinations;
        private bool noOperation;
        //Yaroslav
        bool outGood = false;
        int count = 0;
        int first_column = 0;
        bool createList = true;

        public Calculation4PVBrigada(string[][] distinctGroups, Dictionary<string, Dictionary<string, int>>[] relationMatrix)
        {
            this.distinctGroups = new List<List<string>>[distinctGroups.Length];
            for(int i = 0; i < distinctGroups.Length; i++)
            {
                this.distinctGroups[i] = new List<List<string>>();
                foreach (string s in distinctGroups[i])
                {
                    List<string> temp = new List<string>();
                    temp.Add(s);
                    this.distinctGroups[i].Add(temp);
                }
            }
            this.relationMatrix = relationMatrix.Clone() as Dictionary<string, Dictionary<string, int>>[];
        }

        public void StartCalculation(out string[][][] MGroup)
        {
            CreateCombinations();
            do
            {
                noOperation = true;
                ThirdOperation();
                System.Diagnostics.Debug.WriteLine("ThirdOperation");
                JoinBlocks();
                System.Diagnostics.Debug.WriteLine("JoinBlocks");
                FourthOperation();
                System.Diagnostics.Debug.WriteLine("FourthOperation");
                JoinBlocks();
                System.Diagnostics.Debug.WriteLine("JoinBlocks");
                FifthOperation();
                System.Diagnostics.Debug.WriteLine("FifthOperation");
                JoinBlocks();
                System.Diagnostics.Debug.WriteLine("JoinBlocks");

                foreach (List<List<string>> a in combinations)
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
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                combinations[i] = new List<List<string>>();
                foreach(List<string> lst in distinctGroups[i])
                {
                    List<string> temp = new List<string>(lst);
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
                for (int j = 0; j < relationMatrix[i].Count; j++)
                {
                    string s1 = "";
                    foreach (string s in distinctGroups[i][j])
                        s1 = string.Concat(s1, s);

                    for (int k = 0; k < relationMatrix[i].Count; k++)
                    {
                        string s2 = "";
                        foreach (string s in distinctGroups[i][k])
                            s2 = string.Concat(s2, s);

                        if (relationMatrix[i][s1][s2] == 1 && relationMatrix[i][s2][s1] == 1)
                        {
                            relationMatrix[i][s1][s2] = 2;
                            for (int m = 0; m < combinations[i].Count; m++)
                            {
                                if (ContainAll(combinations[i][m], distinctGroups[i][j]))
                                {
                                    for (int q = 0; q < combinations[i].Count; q++)
                                    {
                                        if (ContainAll(combinations[i][q], distinctGroups[i][k]))
                                        {
                                            if (combinations[i][q].Count == 1)
                                            {
                                                foreach (string s in distinctGroups[i][k])
                                                {
                                                    combinations[i][m].Add(s);
                                                    combinations[i][q].Remove(s);
                                                }
                                                if (combinations[i][q].Count == 0)
                                                    combinations[i].Remove(combinations[i][q]);
                                            }
                                            else if (combinations[i][q].Count > 1)
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

        private void JoinBlocks()
        {
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                List<string[]> temp = new List<string[]>();
                List<int> tempInt = new List<int>();
                for (int j = 0; j < distinctGroups[i].Count; j++)
                {
                    string s1 = "";
                    foreach (string s in distinctGroups[i][j])
                        s1 = string.Concat(s1, s);

                    for (int k = 0; k < distinctGroups[i].Count; k++)
                    {
                        string s2 = "";
                        foreach (string s in distinctGroups[i][k])
                            s2 = string.Concat(s2, s);

                        if (relationMatrix[i][s1][s2] == 2)
                        {
                            string[] sTemp = distinctGroups[i][k].ToArray();
                            temp.Add(sTemp);
                            tempInt.Add(j);
                            tempInt.Add(k);
                            /*foreach (string s in distinctGroups[i][k])
                                distinctGroups[i][j].Add(s);

                            distinctGroups[i].RemoveAt(k);*/
                        }
                    }
                }

                for(int j = 0; j < temp.Count; j++)
                {
                    foreach (string s in temp[j])
                        distinctGroups[i][tempInt[j * 2]].Add(s);
                    distinctGroups[i].RemoveAt(tempInt[j * 2 + 1]);
                }

            }

            Dictionary<string, Dictionary<string, int>>[] tempRelation = new Dictionary<string, Dictionary<string, int>>[distinctGroups.Length];
            for (int i = 0; i < distinctGroups.Length; i++)
            {
                tempRelation[i] = new Dictionary<string, Dictionary<string, int>>();

                for (int j = 0; j < distinctGroups[i].Count; j++)
                {
                    Dictionary<string, int> temp = new Dictionary<string, int>();

                    string s1 = "";
                    foreach (string s in distinctGroups[i][j])
                        s1 = string.Concat(s1, s);

                    if (relationMatrix[i].ContainsKey(s1))
                    {
                        for (int k = 0; k < distinctGroups[i].Count; k++)
                        {
                            string s2 = "";
                            foreach (string s in distinctGroups[i][k])
                                s2 = string.Concat(s2, s);

                            if (string.Equals(s1, s2))
                                temp.Add(s2, 0);
                            else if(relationMatrix[i][s1].ContainsKey(s2))
                            {
                                temp.Add(s2, relationMatrix[i][s1][s2]);
                            }
                            else
                            {
                                bool onePut = false;
                                foreach (string s in distinctGroups[i][k])
                                {
                                    if (relationMatrix[i][s1][s] == 1)
                                        onePut = true;
                                }
                                if (onePut)
                                    temp.Add(s2, 1);
                                else
                                    temp.Add(s2, 0);
                            }
                        }
                    }
                    else
                    {
                        for (int k = 0; k < distinctGroups[i].Count; k++)
                        {
                            string s2 = "";
                            foreach (string s in distinctGroups[i][k])
                                s2 = string.Concat(s2, s);

                            if (string.Equals(s1, s2))
                                temp.Add(s2, 0);
                            else
                            {
                                bool onePut = false;
                                foreach (string s in distinctGroups[i][j])
                                {
                                    if (relationMatrix[i][s].ContainsKey(s2))
                                    {
                                        if(relationMatrix[i][s][s2] == 1)
                                            onePut = true;
                                    }
                                    else
                                    {
                                        bool onePut2 = false;
                                        foreach (string ss in distinctGroups[i][k])
                                        {
                                            if (relationMatrix[i][s][ss] == 1)
                                                onePut2 = true;
                                        }
                                        if (onePut2)
                                            onePut = true;
                                    }
                                }
                                if (onePut)
                                    temp.Add(s2, 1);
                                else
                                    temp.Add(s2, 0);
                            }
                        }
                    }

                    tempRelation[i].Add(s1, temp);
                }
            }
            relationMatrix = tempRelation;
        }

        private bool ContainAll(List<string> current, List<string> toCheck)
        {
            bool checkAll = true;
            foreach(string s in toCheck)
            {
                if (!current.Contains(s))
                    checkAll = false;
            }
            return checkAll;
        }
        //ЯРОСЛАВ


        private void FourthOperation()
        {

            for (int i = 0; i < distinctGroups.Length; i++) //пробег по группам
            {
                for (int j = 0; j < relationMatrix[i].Count; j++) //пробег по колонкам
                {
                    for (int k = 0; k < relationMatrix[i].Count; k++) //пробег по рядкам
                    {
                        string s1 = "";
                        foreach (string s in distinctGroups[i][j])
                            s1 = string.Concat(s1, s);

                        string s2 = "";
                        foreach (string s in distinctGroups[i][k])
                            s2 = string.Concat(s2, s);

                        if (relationMatrix[i][s1][s2] == 1) //если 1 в матрице
                        {
                            List<List<string>> temp = new List<List<string>>(); //создаем лист
                            temp.Add(distinctGroups[i][j]); //добавляем в лист название колонки
                            List<string> first_column = new List<string>();
                            first_column.Add(s1);

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
                                    string m1 = "";
                                    foreach (string s in temp[m])
                                        m1 = string.Concat(m1, s);

                                    string m2 = "";
                                    foreach (string s in temp[m - 1])
                                        m2 = string.Concat(m2, s);

                                    string m0 = "";
                                    foreach (string s in temp[0])
                                        m0 = string.Concat(m0, s);

                                    relationMatrix[i][m1][m2] = 2;

                                    for (int w = 0; w < combinations[i].Count; w++)
                                    {
                                        if (ContainAll(combinations[i][w], temp[0]))
                                        {
                                            for (int q = 0; q < combinations[i].Count; q++)
                                            {
                                                if (ContainAll(combinations[i][q], temp[m]))
                                                {
                                                    if (combinations[i][q].Count == 1)
                                                    {
                                                        foreach (string s in temp[m])
                                                        {
                                                            combinations[i][w].Add(s);
                                                            combinations[i][q].Remove(s);
                                                        }
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

        private bool FourthOperationRecursive(int row, int groupNumber, List<List<string>> positionCheck, List<string> first_column)
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
            for (i = 0; i < relationMatrix[groupNumber].Count; i++) //пробег по рядкам групп
            {
                /*System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.Write(distinctGroups[groupNumber][i] + ", " + distinctGroups[groupNumber][row] + ": ");
                System.Diagnostics.Debug.WriteLine(relationMatrix[groupNumber][distinctGroups[groupNumber][i]][distinctGroups[groupNumber][row]]);*/

                string s1 = "";
                foreach (string s in distinctGroups[groupNumber][i])
                    s1 = string.Concat(s1, s);

                string s2 = "";
                foreach (string s in distinctGroups[groupNumber][row])
                    s2 = string.Concat(s2, s);

                if (relationMatrix[groupNumber][s1][s2] == 1) //если 1 в матрице нашего столбца
                {
                    //checkAll = true;
                    /*System.Diagnostics.Debug.WriteLine("Group" + (groupNumber + 1) + ": ");
                    System.Diagnostics.Debug.WriteLine("FIND: " + distinctGroups[groupNumber][row] + " ");
                    System.Diagnostics.Debug.WriteLine("");
                    /*System.Diagnostics.Debug.Write("List: ");
                    for(int o = 0; o < positionCheck.Count; o++) {
                    System.Diagnostics.Debug.Write(positionCheck[o] + " ");
                    }*/

                    if (first_column.Contains(s1)) //если найденный рядок такой же как и наша первая колонка
                    {
                        //System.Diagnostics.Debug.WriteLine("Wo: " + distinctGroups[groupNumber][i] + " ");
                        positionCheck.Add(distinctGroups[groupNumber][row]);
                        /*System.Diagnostics.Debug.Write(positionCheck[positionCheck.Count - 1] + " ");
                        System.Diagnostics.Debug.WriteLine("Out!");
                        System.Diagnostics.Debug.WriteLine("");*/
                        outGood = true;
                        break;
                    }
                    else
                        positionCheck.Add(distinctGroups[groupNumber][row]);
                    if (!positionCheck.Contains(distinctGroups[groupNumber][i])) //!!!!
                    {
                        FourthOperationRecursive(i, groupNumber, positionCheck, first_column);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("Opa! Out: " + outGood);
            if (outGood == false && i == (relationMatrix[groupNumber].Count - 1))
                positionCheck.Clear();
            else if (outGood == false && i != (relationMatrix[groupNumber].Count - 1))
                positionCheck.RemoveAt(positionCheck.Count - 1);
            return checkAll;
        }




        private void FifthOperation()
        {

            for (int i = 0; i < distinctGroups.Length; i++)
            {
                List<string> reserve = new List<string>();
                for (int j = 0; j < relationMatrix[i].Count; j++)
                {
                    List<List<string>> temp = new List<List<string>>();
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
                    temp = new List<List<string>>(temp.Distinct().ToList()); //!!!
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

                    string s1 = "";
                    foreach (string s in distinctGroups[i][j])
                        s1 = string.Concat(s1, s);

                    foreach (string s in reserve)
                        relationMatrix[i][s][s1] = 1;
                    if (temp.Count >= 3)
                    {
                        for (int l = 0; l < temp.Count - 2; l++)
                        {
                            bool checkAll = false;
                            for (int m = temp.Count - 1; m > 1; m--)
                            {
                                string m1 = "";
                                foreach (string s in temp[m])
                                    m1 = string.Concat(m1, s);

                                string m2 = "";
                                foreach (string s in temp[m - 1])
                                    m2 = string.Concat(m2, s);

                                string m0 = "";
                                foreach (string s in temp[0])
                                    m0 = string.Concat(m0, s);

                                if (relationMatrix[i][m1][m0] == 1)
                                {
                                    relationMatrix[i][m1][m0] = 2;

                                    checkAll = true;
                                    for (int p = 1; p <= m; p++)
                                    {
                                        string p1 = "";
                                        foreach (string s in temp[p])
                                            p1 = string.Concat(p1, s);

                                        string p2 = "";
                                        foreach (string s in temp[p - 1])
                                            p2 = string.Concat(p2, s);

                                        relationMatrix[i][p1][p2] = 0;

                                        for (int w = 0; w < combinations[i].Count; w++)
                                        {
                                            if (ContainAll(combinations[i][w], temp[0]))
                                            {
                                                for (int q = 0; q < combinations[i].Count; q++)
                                                {
                                                    if (ContainAll(combinations[i][q], temp[p]))
                                                    {
                                                        if (combinations[i][q].Count == 1)
                                                        {
                                                            foreach (string s in temp[p])
                                                            {
                                                                combinations[i][w].Add(s);
                                                                combinations[i][q].Remove(s);
                                                            }
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
                        relationMatrix[i][s][s1] = 0;
                    bool checkOne = false;
                    for (int l = 0; l < relationMatrix[i].Count; l++)
                    {
                        string s2 = "";
                        foreach (string s in distinctGroups[i][l])
                            s2 = string.Concat(s2, s);

                        if (relationMatrix[i][s2][s1] == 1)
                        {
                            for (int m = l + 1; m < relationMatrix[i].Count; m++)
                            {
                                string s3 = "";
                                foreach (string s in distinctGroups[i][m])
                                    s3 = string.Concat(s3, s);

                                if (relationMatrix[i][s3][s1] == 1)
                                {
                                    relationMatrix[i][s2][s1] = 0;
                                    reserve.Add(s2);
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
                            relationMatrix[i][s][s1] = 1;
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

        private void FindLine(int column, int groupNumber, List<List<string>> positionCheck)
        {
            System.Diagnostics.Debug.WriteLine(distinctGroups[groupNumber][column]);
            int i = 0;
            for (i = 0; i < relationMatrix[groupNumber].Count; i++)
            {
                string s1 = "";
                foreach (string s in distinctGroups[groupNumber][i])
                    s1 = string.Concat(s1, s);

                string s2 = "";
                foreach (string s in distinctGroups[groupNumber][column])
                    s2 = string.Concat(s2, s);

                if (count == 0)
                {
                    first_column = column;
                    if (relationMatrix[groupNumber][s1][s2] == 1)
                    {
                        count++;
                        positionCheck.Add(distinctGroups[groupNumber][i]);
                        FindLine(i, groupNumber, positionCheck);
                    }
                }
                else if (count == 1)
                {
                    if (relationMatrix[groupNumber][s1][s2] == 1 && positionCheck.Contains(distinctGroups[groupNumber][i]) == false) //!!!!
                    {
                        count++;
                        positionCheck.Add(distinctGroups[groupNumber][i]);

                        string s3 = "";
                        foreach (string s in distinctGroups[groupNumber][first_column])
                            s3 = string.Concat(s3, s);

                        if (relationMatrix[groupNumber][s1][s3] == 1)
                        {
                            outGood = true;
                            break;
                        }
                        FindLine(i, groupNumber, positionCheck);
                    }
                }
                else
                {
                    if (relationMatrix[groupNumber][s1][s2] == 1 && positionCheck.Contains(distinctGroups[groupNumber][i]) == false)
                    {
                        count++;
                        positionCheck.Add(distinctGroups[groupNumber][i]);

                        string s3 = "";
                        foreach (string s in distinctGroups[groupNumber][first_column])
                            s3 = string.Concat(s3, s);

                        if (relationMatrix[groupNumber][s1][s3] == 1)
                        {
                            outGood = true;
                            break;
                        }
                        FindLine(i, groupNumber, positionCheck);
                    }
                }
            }

            if (outGood == false && i == (relationMatrix[groupNumber].Count - 1))
            {
                positionCheck.Clear();
                count = 0;
            }
            else if (outGood == false && i != (relationMatrix[groupNumber].Count - 1))
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

        private void CreateList(int groupNumber, List<List<string>> positionCheck)
        {
            createList = true;
            positionCheck = new List<List<string>>(positionCheck.Distinct().ToList());

            for (int k = 0; k < positionCheck.Count; k++)
            {
                if (createList == false)
                {
                    break;
                }
                if (k > 0 && k < positionCheck.Count - 1)
                {
                    for (int j = 0; j < relationMatrix[groupNumber].Count; j++)
                    {
                        if (createList == false)
                        {
                            break;
                        }
                        if (distinctGroups[groupNumber][j].Equals(positionCheck[k]))
                        {
                            string s1 = "";
                            foreach (string s in distinctGroups[groupNumber][j])
                                s1 = string.Concat(s1, s);

                            for (int l = 0; l < relationMatrix[groupNumber].Count; l++)
                            {
                                string s2 = "";
                                foreach (string s in distinctGroups[groupNumber][l])
                                    s2 = string.Concat(s2, s);

                                if (createList == false)
                                {
                                    break;
                                }
                                if (relationMatrix[groupNumber][s2][s1] == 1)
                                {
                                    if (distinctGroups[groupNumber][l].Equals(positionCheck[k - 1]) || distinctGroups[groupNumber][l].Equals(positionCheck[k + 1]))
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
                                for (int l = 0; l < relationMatrix[groupNumber].Count; l++)
                                {
                                    string s2 = "";
                                    foreach (string s in distinctGroups[groupNumber][l])
                                        s2 = string.Concat(s2, s);

                                    if (relationMatrix[groupNumber][s1][s2] == 1)
                                    {
                                        System.Diagnostics.Debug.WriteLine("OPA");
                                        if (positionCheck.Count == 3)
                                        {
                                            if (distinctGroups[groupNumber][l].Equals(positionCheck[k + 1]))
                                            {
                                                createList = false;
                                                System.Diagnostics.Debug.WriteLine("POPA3");
                                                break;
                                            }
                                        }
                                        System.Diagnostics.Debug.WriteLine("POPA");
                                        if (distinctGroups[groupNumber][l].Equals(positionCheck[k - 1]) || distinctGroups[groupNumber][l].Equals(positionCheck[k + 1]))
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
