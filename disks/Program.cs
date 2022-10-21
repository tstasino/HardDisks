using System;
using System.Collections.Generic;
using System.Linq;

namespace disks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> usedList = new List<int>();
            Console.WriteLine("Hello, give me the used spaces separating them with a comma");
            string usedString = Console.ReadLine();
            usedList = usedString.Split(",").Select(i => Int32.Parse(i)).ToList();

            List<int> totalList = new List<int>();

            Console.WriteLine("Now give me the total spaces ");
            string totalString = Console.ReadLine();
            totalList = totalString.Split(",").Select(i => Int32.Parse(i)).ToList();

            DiskSpace ds = new DiskSpace();
            Console.WriteLine("Disks with data = " + ds.minDrives(usedList, totalList));

        }

        public class DiskSpace
        {
            public int minDrives(List<int> used, List<int> total)
            {
                int DisksWithData = used.Count;

                //bubblesort lists
                var n = used.Count;
                for (int i=0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        if (used[j]-total[j] > used[j + 1]- total[j+1])
                        {
                            var tempUsedVar = used[j];
                            used[j] = used[j + 1];
                            used[j + 1] = tempUsedVar;

                            var tempTotalVar = total[j];
                            total[j] = total[j + 1];
                            total[j + 1] = tempTotalVar;
                        }
                    }

                }
                
                for (int i = used.Count-1; i >= 0; i--)
                {
                    for(int j = 0; j<i; j++)
                    {
                        if(used[i] <= total[j] - used[j])
                        {
                            var tmpdiff = total[j] - used[j];
                            used[j] = used[j] + used[i];
                            used[i] = 0;
                        }
                        else
                        {
                            if (total[j] > used[j])
                            {
                                var tmpDiff = total[j] - used[j];
                                used[j] = total[j];
                                used[i] = used[i] - tmpDiff;
                            }
                            
                        }
                    }
                }

                foreach (var num in used)
                {
                    if (num == 0) DisksWithData--;                    
                }

                foreach (var num in total)
                {
                    Console.Write(num + ",");
                }

                return DisksWithData;
            }

           
        }
    }
}
