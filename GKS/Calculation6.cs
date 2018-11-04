using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation6
    {
        private string[][] mainArray;
        private string[] arrayToCount;
        private int[][] combinationsCount;
        private int bestReverseCount;
        private List<string[][]> bestResult;
        private string[][] currentResult;
        private string[][] finalResult;
        private bool firstEnter = true;

        public Calculation6(string[][] mainArray, string[][] finalResult)
        {
            this.mainArray = mainArray;
            this.finalResult = finalResult;
            //this.finalResult = new string[][] { new string[] { "C3"}, new string[] { "C1" }, new string[] { "T2", "C2" }, new string[] { "T3", "T4" }, new string[] { "T1" } };
        }

        public void StartCalculation(out List<string[][]> result)
        {
            MemoryCreate();
            RecursionCreate();

            result = bestResult;
        }

        private void MemoryCreate()
        {
            arrayToCount = new string[finalResult.Length];
            combinationsCount = new int[mainArray.Length][];
            for (int i = 0; i < mainArray.Length; i++)
                combinationsCount[i] = new int[mainArray[i].Length];
            currentResult = new string[finalResult.Length][];
            bestResult = new List<string[][]>();
        }

        private void RecursionCreate(params int[] used)
        {
            for(int i = 0; i < arrayToCount.Length; i++)
            {
                if (used.Contains(i))
                    continue;

                int[] currentCombination = new int[used.Length + 1];
                for (int j = 0; j < currentCombination.Length - 1; j++)
                    currentCombination[j] = used[j];
                currentCombination[currentCombination.Length - 1] = i;

                if (currentCombination.Length == finalResult.Length)
                    ReverseCount(currentCombination);
                else
                    RecursionCreate(currentCombination);
            }
        }

        private void ReverseCount(int[] positions)
        {
            for (int i = 0; i < finalResult.Length; i++)
                currentResult[i] = finalResult[positions[i]];
            for (int i = 0; i < finalResult.Length; i++)
            {
                if (currentResult[i].Length != 1)
                {
                    string s = "";
                    foreach (string str in currentResult[i])
                        s = string.Concat(s, str);
                    arrayToCount[i] = s;
                }
                else
                    arrayToCount[i] = currentResult[i][0];
            }

            for (int i = 0; i < mainArray.Length; i++)
            {
                for(int j = 0; j < mainArray[i].Length; j++)
                {
                    for(int k = 0; k < arrayToCount.Length; k++)
                    {
                        if(arrayToCount[k].Contains(mainArray[i][j]))
                        {
                            combinationsCount[i][j] = k;
                        }
                    }
                }
            }

            int reverseCount = 0;
            List<int[]> checkRepeat = new List<int[]>();

            for(int i = 0; i < combinationsCount.Length; i++)
            {
                for(int j = 0; j < combinationsCount[i].Length; j++)
                {
                    if(j != 0)
                    {
                        if (combinationsCount[i][j] < combinationsCount[i][j - 1])
                        {
                            bool check = true;
                            for(int m = 0; m < checkRepeat.Count; m++)
                            {
                                if(checkRepeat[m][0] == combinationsCount[i][j] && checkRepeat[m][1] == combinationsCount[i][j - 1])
                                {
                                    check = false;
                                    break;
                                }
                            }
                            if(check)
                            {
                                reverseCount++;
                                int[] temp = new int[2];
                                temp[0] = combinationsCount[i][j];
                                temp[1] = combinationsCount[i][j - 1];
                                checkRepeat.Add(temp);
                            }
                        }
                    }
                }
            }

            if(firstEnter)
            {
                bestReverseCount = reverseCount;
                bestResult.Add(currentResult.Clone() as string[][]);
                firstEnter = false;
            }
            else if(reverseCount < bestReverseCount)
            {
                bestReverseCount = reverseCount;
                bestResult.Clear();
                bestResult.Add(currentResult.Clone() as string[][]);
                System.Diagnostics.Debug.WriteLine("First: " + bestResult.Count);
            }
            else if(reverseCount == bestReverseCount)
            {
                bestResult.Add(currentResult.Clone() as string[][]);
                System.Diagnostics.Debug.WriteLine("Second: " + reverseCount);
            }
        }
    }
}
