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
                        if (k != 0)
                        {
                            relationMatrix[i][mainArray[group[i][j] - 1][k]][mainArray[group[i][j] - 1][k - 1]] = 1;
                        } 
                    }
                }
            }
        }

    }
}
