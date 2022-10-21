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
            List<int> totalList = new List<int>();
            bool outer = false;
            while (outer == false)
            {
                bool status = false;
                while (status == false)
                {
                    try
                    {
                        Console.WriteLine("Hello, give me the Used Spaces of disks (max 50), as Integer numbers greater or equal to 1 and less or equal to 1000, separated with a comma");
                        string usedString = Console.ReadLine();
                        usedList = usedString.Split(",").Select(i => Int32.Parse(i)).ToList();
                        if (usedList.Count > 50) throw new Exception("Only 50 Hard disks allowed");
                        foreach (var item in usedList)
                        {
                            if (item < 1 || item > 1000) throw new Exception("Values must be greater or equal to 1 and less or equal to 1000");
                        }
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                    }
                }



                status = false;
                while (status == false)
                {

                    try
                    {
                        Console.WriteLine("Now give me the Total Spaces of disks (max 50), as Integer numbers greater or equal to 1 and less or equal to 1000, separated with a comma");
                        string totalString = Console.ReadLine();
                        totalList = totalString.Split(",").Select(i => Int32.Parse(i)).ToList();
                        if (totalList.Count > 50) throw new Exception("Only 50 Hard disks allowed");
                        foreach (var item in totalList)
                        {
                            if (item < 1 || item > 1000) throw new Exception("Values must be greater or equal to 1 and less or equal to 1000");
                        }
                        status = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                try
                {
                    if (usedList.Count != totalList.Count) throw new Exception("Used spaces do not match with Total spaces.Try again");
                    for(int i=0; i<=usedList.Count-1; i++)
                    {
                        if (usedList[i] > totalList[i]) throw new Exception("Used space cannot exceed Total space! Used Space = " + usedList[i] + " Total Space = " + totalList[i]+ ". Try again");
                    }
                    outer = true;

                }catch(Exception x)
                {
                    Console.WriteLine(x.Message);
                }
            }        

            DiskSpace ds = new DiskSpace();
            Console.WriteLine("-------------------------");
            Console.WriteLine("Disks with data = " + ds.minDrives(usedList, totalList));
            Console.WriteLine("-------------------------");


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

                return DisksWithData;
            }

           
        }
    }
}
