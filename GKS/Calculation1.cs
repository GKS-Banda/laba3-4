using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation1
    {
        private string[][] mainArray;
        private string[] Kno;
        private int[][] mainMatrix;
        private int[][] outMatrix;
        private int matrixSize;
        private int[][] group;

        public void MainCalculation(string[] input, out int[][] matrix, out int[][] groupOut, out string[] KnoOutput, out string[][] mainArray)
        {
            StringConvert(input);
            MatrixCreate();
            GroupForm();
            matrix = outMatrix;
            groupOut = group;
            KnoOutput = Kno;
            mainArray = this.mainArray;
            /*//КУРСАЧ_Ярослав_Землянко
            mainArray = new string[] []{ new string[] {"T1", "C2", "T3", "C3", "T4", "F1", "F2"},
                        new string[] {"T2", "T5", "T4", "F1", "T1", "C2", "T3"},
                        new string[] {"T5", "T4", "T1", "C2", "T3"},
                        new string[] {"T5", "T1", "C2", "T3"},
                        new string[] {"T4", "F1", "F2", "T1", "C2", "T3"},
                        new string[] {"T5", "C1", "T1", "C2", "T3"},
                        new string[] {"T1", "C2", "T3", "T5", "T4", "F2"},
                        new string[] {"T2", "C2", "F2", "T3", "T4", "F1", "P2"},
                        new string[] {"T3", "T4", "T2", "C2", "F2", "P2"},
                        new string[] {"T3", "T4", "F3", "C1", "T2", "C2", "F2"},
                        new string[] {"T4", "T5", "F3", "C1", "T2", "C2", "F2"},
                        new string[] {"T5", "T2", "C2", "F2", "F3", "T5"},
                        new string[] {"T5", "T2", "C2", "F2", "F1", "P2"},
                        new string[] {"T2", "C2", "F2", "P2"}};*/
        }

        private void StringConvert(string[] input)
        {
            matrixSize = input.Length;

            mainArray = new string[matrixSize][];

            for (int i = 0; i < matrixSize; i++)
            {
                int j = 0;
                for (int k = 0; k < input[i].Length;)
                {
                    string s = input[i].Substring(k, input[i].Length - k);
                    int position = s.IndexOfAny("0123456789".ToCharArray());
                    while (s.Length > position + 1 && char.IsDigit(s[position + 1])) { position++; }

                    k += position + 1;
                    j++;
                }
                mainArray[i] = new string[j];
            }

            int t = 0;
            foreach (string[] s in mainArray)
                t += s.Length;

            string[] temp = new string[t];
            t = 0;

            for (int i = 0; i < matrixSize; i++)
            {
                int j = 0;
                for (int k = 0; k < input[i].Length;)
                {
                    string s = input[i].Substring(k, input[i].Length - k);
                    int position = s.IndexOfAny("0123456789".ToCharArray());
                    while (s.Length > position + 1 && char.IsDigit(s[position + 1])) { position++; }
                    mainArray[i][j] = s.Substring(0, position + 1);

                    temp[t] = s.Substring(0, position + 1);
                    t++;

                    j++;
                    k += position + 1;
                }
            }

            Kno = temp.Distinct().ToArray();
        }

        private void MatrixCreate()
        {
            mainMatrix = new int[matrixSize][];
            for (int i = 0; i < matrixSize; i++)
                mainMatrix[i] = new int[i + 1];

            for(int i = 0; i < matrixSize; i++)
            {
                for(int j = 0; j < matrixSize; j++)
                {
                    if (i > j)
                    {
                        foreach (string knoStatus in Kno)
                        {
                            if (mainArray[i].Contains(knoStatus) == mainArray[j].Contains(knoStatus))
                                mainMatrix[i][j]++;
                        }
                    }
                    else if (i == j)
                        mainMatrix[i][j] = 0;
                }
            }

            outMatrix = mainMatrix.Clone() as int[][];
            for (int i = 0; i < matrixSize; i++)
                outMatrix[i] = mainMatrix[i].Clone() as int[];
        }

        private void GroupForm()
        {
            List<List<int>> checkList = new List<List<int>>();

            for (int i = 0; i < matrixSize; i++)
            {
                int Max = -1;
                foreach (int[] intArray in mainMatrix)
                    foreach (int iElement in intArray)
                    {
                        if (iElement > Max)
                            Max = iElement;
                    }

                if (Max == -1)
                    break;

                List<int> groupList = new List<int>();
                checkList.Add(groupList);
                for (int k = 0; k < matrixSize; k++)
                {
                    if (mainMatrix[k].Contains(Max))
                    {
                        MaxCheck(k, Array.IndexOf(mainMatrix[k], Max), Max, checkList);

                        break;
                    }
                }
            }

            group = new int[checkList.Count][];
            for (int i = 0; i < checkList.Count; i++)
                group[i] = checkList[i].Distinct().ToArray();

            for(int i = 0; i < group.Length; i++)
            {
                for (int j = 0; j < group[i].Length; j++)
                    group[i][j]++;
            }
        }

        private void MaxCheck(int rowNumber, int columnNumber, int Max, List<List<int>> checkList)
        {
            if(!checkList[checkList.Count - 1].Contains(rowNumber))
                checkList[checkList.Count - 1].Add(rowNumber);
            if (!checkList[checkList.Count - 1].Contains(columnNumber))
                checkList[checkList.Count - 1].Add(columnNumber);

            int[] allIndex = mainMatrix[rowNumber].Select((b, index) => Equals(b, Max) ? index : -1).Where(index => index != -1).ToArray();
            foreach (int m in allIndex)
            {
                if (!checkList[checkList.Count - 1].Contains(m))
                {
                    checkList[checkList.Count - 1].Add(m);
                    MaxCheck(rowNumber, m, Max, checkList);
                }
            }

            for (int i = 0; i < mainMatrix[rowNumber].Length; i++)
                mainMatrix[rowNumber][i] = -1;

            for (int i = 0; i < mainMatrix[columnNumber].Length; i++)
                mainMatrix[columnNumber][i] = -1;

            allIndex = null;

            for(int i = columnNumber; i < matrixSize; i++)
            {
                if (mainMatrix[i][columnNumber] == Max && !checkList[checkList.Count - 1].Contains(i))
                {
                    checkList[checkList.Count - 1].Add(i);
                    MaxCheck(i, columnNumber, Max, checkList);
                }
            }

            for (int i = columnNumber; i < matrixSize; i++)
                mainMatrix[i][columnNumber] = -1;

            for (int i = rowNumber; i < matrixSize; i++)
            {
                if (mainMatrix[i][rowNumber] == Max && !checkList[checkList.Count - 1].Contains(i))
                {
                    checkList[checkList.Count - 1].Add(i);
                    MaxCheck(i, rowNumber, Max, checkList);
                }
            }

            for (int i = rowNumber; i < matrixSize; i++)
                mainMatrix[i][rowNumber] = -1;

        }
    }
}
