using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation2
    {
        private string[][] mainArray;
        private string[][] distinctGroups;
        private int[][] group;
        private int[] groupPosition;

        public Calculation2(int[][] group, string[][] mainArray)
        {
            this.group = group;
            this.mainArray = mainArray;
        }

        public void StartCalculation(out string[][] distinctGroups, out int[][] newGroups, out int[] groupPosition)
        {
            JoinGroup();
            GroupRecombination();

            distinctGroups = this.distinctGroups;
            newGroups = group;
            groupPosition = this.groupPosition;
        }

        private void GroupRecombination()
        {
            Dictionary<int, bool> groupsCheck = new Dictionary<int, bool>();
            for (int i = 1; i <= mainArray.Length; i++)
                groupsCheck.Add(i, false);

            for(int i = 0; i < group.Length; i++)
            {
                for (int j = 1; j <= mainArray.Length; j++)
                {
                    if (!group[i].Contains(j) && !groupsCheck[j])
                    {
                        bool checkAll = true;
                        foreach(string s in mainArray[j - 1])
                        {
                            if (!distinctGroups[i].Contains(s))
                                checkAll = false;
                        }
                        if(checkAll)
                        {
                            int[] temp = group[i];
                            group[i] = new int[temp.Length + 1];
                            for (int m = 0; m < temp.Length; m++)
                            {
                                group[i][m] = temp[m];
                            }
                            group[i][temp.Length] = j;

                            int arrayIndex = -1;
                            for(int l = 0; l < group.Length; l++)
                            {
                                if (group[l].Contains(j))
                                    arrayIndex = l;
                            }

                            group[arrayIndex] = group[arrayIndex].Where(item => item != j).ToArray();
                            
                            if(group[arrayIndex].Length < 1)
                            {
                                group = group.Where((arr, l) => l != arrayIndex).ToArray();
                            }
                            groupsCheck[j] = true;
                        }
                    }
                    else if(group[i].Contains(j))
                        groupsCheck[j] = true;

                }
            }
            for (int i = 0; i < group.Length; i++)
                group[i] = group[i].Distinct().ToArray();
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
                distinctGroups[i] = selectGroup[i].Split();
                distinctGroups[i] = distinctGroups[i].Distinct().ToArray();
            }

            groupPosition = new int[group.Length];
            for (int i = 0; i < groupPosition.Length; i++)
                groupPosition[i] = i + 1;
            for (int p = 0; p <= distinctGroups.Length - 2; p++)
            {
                for (int i = 0; i <= distinctGroups.Length - 2; i++)
                {
                    bool check = false;
                    if(distinctGroups[i].Length == distinctGroups[i + 1].Length)
                    {
                        if (SameGroups(distinctGroups[i], distinctGroups[i + 1]))
                        {
                            check = true;
                        }
                    }
                    if (check || distinctGroups[i].Length < distinctGroups[i + 1].Length)
                    {
                        string[] t1 = distinctGroups[i + 1];
                        distinctGroups[i + 1] = distinctGroups[i];
                        distinctGroups[i] = t1;

                        int[] t2 = group[i + 1];
                        group[i + 1] = group[i];
                        group[i] = t2;

                        int t3 = groupPosition[i + 1];
                        groupPosition[i + 1] = groupPosition[i];
                        groupPosition[i] = t3;
                    }
                }
            }
        }

        private bool SameGroups(string[] first, string[] second)
        {
            string[] select;
            select = first;
            int[] count = new int[2];
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < mainArray.Length; i++)
                {
                    bool checkAll = true;
                    foreach (string s in mainArray[i])
                    {
                        if (!select.Contains(s))
                            checkAll = false;
                    }
                    if (checkAll)
                        count[j]++;
                }
                select = second;
            }

            if (count[0] < count[1])
                return true;
            else
                return false;

        }
    }
}
