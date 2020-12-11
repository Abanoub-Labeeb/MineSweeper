using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MineSweeper.PTL.MineSweeperLogic
{
    public class MineSweeperBL
    {
        public static string ParseMineSweeperInput(String input) {

            List<string> fields = new List<string>();
            string output = string.Empty;
            
            //check if input is null or empty return 
            if (String.IsNullOrEmpty(input)) {
                return output ;
            }

            
            
            //trim both ends from spaces
            fields = input.Trim().Split('\n').ToList();

            for (int i = 0; i < fields.Count; i++)
            {
                fields[i] = fields[i].Trim();
            }

            int MineFieldslineCounter = 0;
            int fieldsCount = 0;

            while (MineFieldslineCounter < fields.Count) {

                if (MineFieldslineCounter == 0 && String.IsNullOrEmpty(fields[MineFieldslineCounter])){
                    //check if input is null or empty return
                    return output;
                }else if (MineFieldslineCounter != 0 && String.IsNullOrEmpty(fields[MineFieldslineCounter])) {
                    //skip empty line between mine fields
                    MineFieldslineCounter++;
                }

                //read first line - > mine sweeper dimention line
                //read no. of rows = n  
                //read no. of columns = m
                
                int n = int.Parse(fields[MineFieldslineCounter][0].ToString());
                //fields[MineFieldslineCounter][2].ToString() is just a space
                int m = int.Parse(fields[MineFieldslineCounter][2].ToString());

                if (n == 0)
                {
                    //stop and return fieldsCount as you reached the end of input 00
                    return output;
                }


                fieldsCount++;

                //fill the process matrix from field lines
                char[,] processMatrix = new char[n,m];
                for (int i = 1; i <= n; i++)
                {
                    //read field input line
                    string fieldInputLine      = fields[MineFieldslineCounter + i];

                    //split it to characters and put it into the process matrix 
                    char[] fieldInputLineChars = fieldInputLine.ToCharArray();

                    //copy char array to matrix
                    for (int j = 0; j < m; j++)
                    {
                        if (fieldInputLineChars[j] == '.')
                        {
                            processMatrix[i - 1, j] = '0';
                        }
                        else {
                            processMatrix[i - 1, j] = '*';
                        }
                        
                    }
                }

                //process the matrix
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (processMatrix[i,j] == '*') {
                            //increment above
                            if (i - 1 >= 0) {
                                string curChar = Char.ToString(processMatrix[i - 1, j]);
                                if (curChar != "*") {
                                    int incValue = int.Parse(curChar) + 1;
                                    processMatrix[i - 1, j] = char.Parse(incValue.ToString());
                                } 
                            }
                            //increment down
                            if (i + 1 < n)
                            {
                                string curChar = Char.ToString(processMatrix[i + 1, j]);
                                if (curChar != "*")
                                {
                                    int incValue = int.Parse(curChar) + 1;
                                    processMatrix[i + 1, j] = char.Parse(incValue.ToString());
                                }
                            }
                            //increment right
                            if (j + 1 < m)
                            {
                                string curChar = Char.ToString(processMatrix[i, j + 1]);
                                if (curChar != "*")
                                {
                                    int incValue = int.Parse(curChar) + 1;
                                    processMatrix[i, j + 1] = char.Parse(incValue.ToString());
                                }
                            }
                            //increment left
                            if (j - 1 >= 0)
                            {
                                string curChar = Char.ToString(processMatrix[i, j - 1]);
                                if (curChar != "*")
                                {
                                    int incValue = int.Parse(curChar) + 1;
                                    processMatrix[i, j - 1] = char.Parse(incValue.ToString());
                                }
                            }

                            //increment above-right
                            if (i - 1 >= 0 && j + 1 < m)
                            {
                                string curChar = Char.ToString(processMatrix[i - 1, j + 1]);
                                if (curChar != "*")
                                {
                                    int incValue = int.Parse(curChar) + 1;
                                    processMatrix[i - 1, j + 1] = char.Parse(incValue.ToString());
                                }
                            }
                            //increment above-left
                            if (i - 1 >= 0 && j - 1 >= 0)
                            {
                                string curChar = Char.ToString(processMatrix[i - 1, j - 1]);
                                if (curChar != "*")
                                {
                                    int incValue = int.Parse(curChar) + 1;
                                    processMatrix[i - 1, j - 1] = char.Parse(incValue.ToString());
                                }
                            }
                            //increment down-right
                            if (i + 1 < n && j + 1 < m)
                            {
                                string curChar = Char.ToString(processMatrix[i + 1, j + 1]);
                                if (curChar != "*")
                                {
                                    int incValue = int.Parse(curChar) + 1;
                                    processMatrix[i + 1, j + 1] = char.Parse(incValue.ToString());
                                }
                            }
                            //increment down-left
                            if (i + 1 < n && j - 1 >= 0)
                            {
                                string curChar = Char.ToString(processMatrix[i + 1, j - 1]);
                                if (curChar != "*")
                                {
                                    int incValue = int.Parse(curChar) + 1;
                                    processMatrix[i + 1, j - 1] = char.Parse(incValue.ToString());
                                }
                            }
                        }
                    }
                }


                //Write the count and the array to the out string
                output = output + "Field #" + fieldsCount + ": \n";
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        output = output + processMatrix[i, j];
                    }
                    output = output + '\n';
                }
                output = output + '\n';
                MineFieldslineCounter = MineFieldslineCounter + n + 1;
            }


            return output;
        }
    }
}
