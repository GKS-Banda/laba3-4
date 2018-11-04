using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation5
    {
        private string[][][] MGroup;
        private List<string[]> MList;

        public Calculation5(string[][][] MGroup)
        {
            this.MGroup = MGroup;
            /*this.MGroup = new string[][][] { new string[][] { new string[] { "T1"}, new string[] { "T1", "T2", "T3" }, new string[] { "T2", "C2" }, new string[] { "T2" } },
                                             new string[][] { new string[] { "T3"}, new string[] { "T3", "T4" }, new string[] { "C3" } },
                                             new string[][] { new string[] { "T2"}, new string[] { "T3" }, new string[] { "C1" }}
                                            };*/
        }

        public void StartCalculation(out string[][] finalGroups)
        {
            SortAll();
            OneElement();
            NotOneElement();

            finalGroups = MList.ToArray();
        }

        private void SortAll()
        {
            MList = new List<string[]>();
            foreach(string[][] str in MGroup)
            {
                foreach(string[] s in str)
                {
                    MList.Add(s.Clone() as string[]);
                }
            }

            int maxLenght = 0;
            foreach (string[] s in MList)
                if (s.Length > maxLenght)
                    maxLenght = s.Length;

            int currentChange = 0;
            for(int i = 2; i <= maxLenght; i++)
            {
                for(int j = 0; j < MList.Count - currentChange; j++)
                {
                    if(MList[j].Length == i)
                    {
                        string[] s = MList[j];
                        MList.RemoveAt(j);
                        MList.Add(s);
                        j--;
                        currentChange++;
                    }
                }
            }

            foreach(string[] str in MList)
            {
                foreach(string s in str)
                {
                    System.Diagnostics.Debug.Write(s + " ");
                }
                System.Diagnostics.Debug.WriteLine(";");
            }
        }

        private void OneElement()
        {
            for(int i = 0; i < MList.Count; i++)
            {
                if (MList[i].Length == 1)
                {
                    for (int j = i + 1; j < MList.Count; j++)
                    {
                        if (MList[j].Contains(MList[i][0]))
                        {
                            MList.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
                else
                    break;
            }
        }

        private void NotOneElement()
        {
            for (int i = 0; i < MList.Count; i++)
            {
                if (MList[i].Length == 1)
                {
                    continue;
                }
                else
                {
                    for(int j = 0; j < MList[i].Length; j++)
                    {
                        for(int k = i + 1; k < MList.Count; k++)
                        {
                            if(MList[k].Contains(MList[i][j]))
                            {
                                if (MList[k].Length == 1)
                                {
                                    MList.RemoveAt(k);
                                    k--;
                                    continue;
                                }
                                MList[k] = MList[k].Where((arr, m) => m != Array.IndexOf(MList[k], MList[i][j])).ToArray();
                            }
                        }
                    }
                }
            }
        }
        
    }
}
