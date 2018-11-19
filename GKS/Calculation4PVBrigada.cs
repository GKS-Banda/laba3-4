using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS
{
    class Calculation4PVBrigada
    {
        /*private string[][] distinctGroups;
        private string[][] mainArray;
        private Dictionary<string, Dictionary<string, int>>[] relationMatrix;*/
        private string[][][] MGroup;
        private int numberOfFirstOperation = 0;
        private IDictionary<int, ISet<int>> groups;
        private IDictionary<int, string[]> allValues;
        private IList<IDictionary<string, ISet<string>>> matrixRelationships;
        private ISet<string> cycle;
        private const int maxElementsInModule = 5;

        public Calculation4PVBrigada(string[][] mainArray, string[][] distinctGroups, Dictionary<string, Dictionary<string, int>>[] relationMatrix)
        {
            bool[] checkAll = new bool[mainArray.Length];
            groups = new Dictionary<int, ISet<int>>();
            for (int k = 0; k < distinctGroups.Length; k++)
            {
                HashSet<int> temp = new HashSet<int>();
                for (int i = 0; i < mainArray.Length; i++)
                {
                    if (!checkAll[i])
                    {
                        bool addThis = true;
                        for (int j = 0; j < mainArray[i].Length; j++)
                        {
                            if (!distinctGroups[k].Contains(mainArray[i][j]))
                                addThis = false;
                        }

                        if (addThis)
                        {
                            checkAll[i] = true;
                            temp.Add(i);
                        }
                    }
                    
                }
                groups.Add(k, temp);
            }

            allValues = new Dictionary<int, string[]>();

            for(int i = 0; i < mainArray.Length; i++)
            {
                allValues.Add(i, mainArray[i].Clone() as string[]);
            }

            matrixRelationships = new List<IDictionary<string, ISet<string>>>();
        }

        public void StartCalculation(out string[][][] MGroup)
        {
            process();
            System.Diagnostics.Debug.WriteLine("cycle:");
            foreach (string s in cycle)
                System.Diagnostics.Debug.WriteLine(s);
            MGroup = this.MGroup;
        }

        /*public Lab3(Lab2 lab2)
        {
            groups = lab2.getGroups();
            allValues = lab2.getAllValues();
            matrixRelationships = new List<IDictionary<string, ISet<string>>>();
        }*/

        private void creatingMatrixR()
        {
            IDictionary<string, ISet<string>> tempMap;

            foreach(int numberGroup in groups.Keys)
            {
                tempMap = new Dictionary<string, ISet<string>>();
                foreach(int numberOfElement in (ISet<int>)groups[numberGroup]) //??????
                {
                    foreach(string value in allValues[numberOfElement])//////????
                    {
                        if (tempMap.ContainsKey(value))
                            tempMap.Remove(value);

                        tempMap.Add(value, addToMatrix(value, numberGroup)); //????
                    }
                }
                matrixRelationships.Add(tempMap);
            }
        }

        private ISet<string> addToMatrix(string value1, int groupNum)
        {
            ISet<string> tempSet = new HashSet<string>();
            string[] tempString;

            foreach(int numberGroup in groups.Keys)
            {
                foreach(int numberOfElement in (ISet<int>)groups[numberGroup]) //????
                {
                    tempString = allValues[numberOfElement]; //??????
                    for(int i = 0; i < tempString.Length; ++i)
                    {
                        if(tempString[i].Equals(value1) && (i != tempString.Length - 1)
                            && groupNum.Equals(numberGroup))
                        {
                            tempSet.Add(tempString[i + 1]);
                        }
                    }
                }
            }
            return tempSet;
        }

        private bool checkReversibleRelation(string value, int groupNumber)
        {
            IDictionary<string, ISet<string>> temp = matrixRelationships[groupNumber]; //????
            ISet<string> tempSet = temp[value]; //????
            cycle = new HashSet<string>();

            foreach(string element in tempSet)
            {
                if(temp[element].Contains(value)) //????
                {
                    cycle.Add(element);
                    cycle.Add(value);
                    /*System.Diagnostics.Debug.WriteLine("СheckReversibleRelation (cycle1): ");
                    foreach (string s in cycle)
                        System.Diagnostics.Debug.WriteLine(s);*/
                    return true;
                }
            }
            return false;
        }

        private bool checkOneOneRelation(string value, int groupNumber)
        {
            IDictionary<string, ISet<string>> temp = matrixRelationships[groupNumber]; //????
            int count = 0;

            foreach(string key in temp.Keys)
            {
                if(temp[key].Contains(value)) //????
                {
                    ++count;
                }
            }

            return temp[value] != null && temp[value].Count == 1 && count == 1; //????
        }

        private bool checkOneWayRelation(string value, int groupNumber)
        {
            IDictionary<string, ISet<string>> temp = matrixRelationships[groupNumber]; //????
            string tempValue = "";
            //string[] stringTemp = Arrays.copyOf(temp.get(value).toArray(), temp.get(value).size(), string[].class);
            string[] stringTemp = temp[value].ToArray().Clone() as string[];  //????
            //temp[value].ToArray().CopyTo(stringTemp, temp[value].Count); //???

            cycle = new HashSet<string>();

            for(int i = 0; i < stringTemp.Length; ++i)
            {
                if (checkOneOneRelation(stringTemp[i], groupNumber))
                {
                    cycle.Add(stringTemp[i]);
                    tempValue = stringTemp[i];
                    //stringTemp = Arrays.copyOf(temp.get(stringTemp[i]).toArray(), temp.get(value).size(), string[].clas);
                    //string s = stringTemp[i];
                    //stringTemp = new string[temp[s].Count]; //?????
                    //temp[s].ToArray().CopyTo(stringTemp, temp[value].Count); //????
                    System.Diagnostics.Debug.WriteLine("NumberOfFirstOperation: " + numberOfFirstOperation);
                    //numberOfFirstOperation = numberOfFirstOperation +1;
                    string[] tempString = new string[temp[value].Count];
                    int counter;
                    if (temp[value].Count > stringTemp.Length)
                        counter = stringTemp.Length;
                    else
                        counter = temp[value].Count;
                    for (int m = 0; m < counter; m++)
                    {
                        tempString[m] = stringTemp[m];
                    }
                    stringTemp = tempString;
                    //Array.Copy(temp[stringTemp[i]].ToArray(), stringTemp, temp[value].Count); //сколько 1 операции столько отнять
                    i = -1;
                    if (cycle.Count >= maxElementsInModule)
                    {
                        return false;
                    }
                }
                else if(temp.ContainsKey(tempValue) && temp[tempValue] != null) //????
                {
                    if(temp[value].Contains(stringTemp[i]) && temp[tempValue].Contains(stringTemp[i])) //????
                    {
                        cycle.Add(stringTemp[i]);
                        cycle.Add(value);
                        return cycle.Count < maxElementsInModule;
                    }
                }
            }
            return false;
        }

        private bool checkNRelation(string value, int groupNumber)
        {
            IDictionary<string, ISet<string>> temp = matrixRelationships[groupNumber]; //????
            cycle = new HashSet<string>();
            for (int i = 3; i <= maxElementsInModule; ++i)
            {
                if (checkNRHelp(value, i, groupNumber, value, 0))
                {
                    return true;
                }
            }
            return false;
        }

        private bool checkNRHelp(string elementOfRelation, int n, int groupNumber, string headElement, int counter)
        {
            IDictionary<string, ISet<string>> relations = matrixRelationships[groupNumber]; //????
            ISet<string> relationsOfElement = relations[elementOfRelation]; //????
            ++counter;
            if (counter == n)
            {
                foreach (string currentValue in relationsOfElement)
                {
                    if (currentValue.Equals(headElement))
                    {
                        cycle.Add(elementOfRelation);
                        return true;
                    }
                }
            }
            else
            {
                foreach (string currentValue in relationsOfElement)
                {
                    if (checkNRHelp(currentValue, n, groupNumber, headElement, counter))
                    {
                        cycle.Add(elementOfRelation);
                        return true;
                    }
                }
            }
            return false;
        }


        private bool modulesCreator()
        {
            for (int numberOfGroup = 0; numberOfGroup < matrixRelationships.Count; ++numberOfGroup)
            {
                //numberOfFirstOperation = 0;
                foreach (string currentElement in matrixRelationships[numberOfGroup].Keys) //????
                {
                    if (checkReversibleRelation(currentElement, numberOfGroup) && moveElements(numberOfGroup))
                    {
                        System.Diagnostics.Debug.WriteLine("First: (group " + (numberOfGroup+1) + ") :" );
                        foreach (string s in cycle)
                            System.Diagnostics.Debug.WriteLine(s);
                        numberOfFirstOperation += 1;
                        return true;
                    }
                    if (checkNRelation(currentElement, numberOfGroup) && moveElements(numberOfGroup))
                    {
                        System.Diagnostics.Debug.WriteLine("Second: (group " + (numberOfGroup+1) + ") :" );
                        foreach (string s in cycle)
                            System.Diagnostics.Debug.WriteLine(s);
                        return true;
                    }
                    if (checkOneWayRelation(currentElement, numberOfGroup) && moveElements(numberOfGroup))
                    {
                        System.Diagnostics.Debug.WriteLine("Third (group " + (numberOfGroup+1) + ") :" );
                        foreach (string s in cycle)
                            System.Diagnostics.Debug.WriteLine(s);
                        return true;
                    }
                }
            }
            return false;
        }


        private bool moveElements(int numberOfGroup)
        {
            IDictionary<string, ISet<string>> temp = matrixRelationships[numberOfGroup]; //????
            ISet<string> relation = new HashSet<string>();
            string newElement = string.Join("", cycle); //????
            if (newElement.Length > maxElementsInModule * 2)
            {
                return false;
            }

            foreach (string currentElement in temp.Keys)
            {
                if (cycle.Contains(currentElement))
                {
                    //relation.addall(temp[currentElement]); //????
                    foreach (string s in temp[currentElement]) //????
                        relation.Add(s);
                }
            }

            foreach (string element in cycle)
            {
                relation.Remove(element);
            }

            foreach (string removedElement in cycle)
            {
                temp.Remove(removedElement);
            }

            foreach (string currentElement in temp.Keys)
            {
                foreach (string rel in cycle)
                {
                    if (temp[currentElement].Contains(rel)) //????
                    {
                        temp[currentElement].Add(newElement);//????
                        temp[currentElement].Remove(rel);//????
                    }
                }
            }

            temp.Add(newElement, relation); //????
            return true;
        }

        public IList<IDictionary<string, ISet<string>>> getMatrixRelationships()
        {
            return matrixRelationships;
        }

        IDictionary<int, string[]> getAllValues()
        {
            return allValues;
        }

        public void process() //???? override
        {
            creatingMatrixR();
            while (modulesCreator()) ;
        }
    }
}
