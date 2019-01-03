using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAndEvents
{
    //public delegate int WorkPerformedHandler(object sender, WorkPerformedEventArgs e);
    // public delegate int WorkPerformedHandler(hours, WorkType);


    class Program
    {
        static void Main(string[] args)
        {
            //WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
            //WorkPerformedHandler del2 = new WorkPerformedHandler(WorkPerformed2);
            //WorkPerformedHandler del3 = new WorkPerformedHandler(WorkPerformed3);

            //del1 += del2 + del3;

            //int finalHours = del1(10, WorkType.GenerateReports);
            //Console.WriteLine(finalHours);

            var worker = new Worker();
            worker.WorkPerformed += new EventHandler<WorkPerformedEventArgs>(Worker_WorkPerformed);
            worker.WorkCompleted += new EventHandler(Worker_WorkCompleted);
            worker.DoWork(8, Worker.WorkType.GenerateReports);

            Console.ReadLine();
        }

        static void Worker_WorkPerformed(object sender, WorkPerformedEventArgs e)
        {
            Console.WriteLine($"Hours worked: {e.Hours}; Work Type: {e.WorkType}");
        }

        static void Worker_WorkCompleted(object sender, EventArgs e)
        {
            Console.WriteLine("Worker is done.");
        }

        //        static void DoWork(WorkPerformedHandler del)
        //        {
        //            del(5, WorkType.Golf);
        //        }

        //        static int WorkPerformed1(int hours, WorkType workType)
        //        {
        //            Console.WriteLine("WorkPerformed1 called " + hours.ToString());
        //            return hours + 1;
        //        }

        //        static int WorkPerformed2(int hours, WorkType workType)
        //        {
        //            Console.WriteLine("WorkPerformed2 called " + hours.ToString());
        //            return hours + 2;
        //        }

        //        static int WorkPerformed3(int hours, WorkType workType)
        //        {
        //            Console.WriteLine("WorkPerformed3 called " + hours.ToString());
        //            return hours + 3;
        //        }
        //    }
    }
}
