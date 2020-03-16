using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateTesting
{
    delegate void mydelegate();
    delegate void myotherdelegate(int count);
    delegate int mystillanother(int first, int second);
    delegate void myfinaldelegate();

    class Hello
    {
        public void sayHello()
        {
            Console.WriteLine("Hello There");
        }
        public void screamHello(int a)
        {
            Console.WriteLine($"HELLO {a} times");
        }
    }   // this class is used to study delegates only

    class Goodbye
    {
        public string Name { get; set; }
        public void sayGoodbye()
        {
            Console.WriteLine($"Goodbye {Name}");
        }
        public void screamGoodbye(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"GOODBYE {Name.ToUpper()}");
            }
        }
    }  // this class is used to study delegates only

    class Ouch
    {
        public /*specifically not an event*/ mydelegate Phase1;
        public /*specifically not an event*/ mystillanother Phase2;
        public event  mystillanother Phase3;
        public event mydelegate Phase4; 
        public string Victim { get; set; }
        public string Perp { get; set; }
        public void sayOuch()
        {
            Console.WriteLine($"{Perp} is looking at {Victim}");
        }
        public void screamOuch(int count)
        {
            
            Console.WriteLine("Entering ScreamOuch");
            // Phase1 - encountered only one time BEFORE entering the loop
            //if (Phase1 != null) Phase1();
            Phase1?.Invoke();
            for (int i = 1; i<= count; i++)
            {
                Phase2?.Invoke(i,count);
                // Phase2 - encountered count times, once before each WriteLine
                Console.WriteLine($"{Perp} is looking at {Victim} time {i} of {count}");
                Phase3?.Invoke(i, count);
                // Phase3 - encountered count times, once after  each WriteLine
            }
            Phase4?.Invoke();
            // Phase4 - encountered only one time AFTER finishing all iterations of the loop
            Console.WriteLine("Exiting screamOuch");
            
        }

       
    }  // this class is the publisher of the 4 phase events

    // the difference between phase1, phase2 and phase3, phase4
    // is that the first two are NOT events (delegates only)
    // and that the other two are events
    //  events are different ONLY because they are less visible in the
    // program class.

    class Program
    {
        static void XXXXX()
        {
            Console.WriteLine("In Main Again, at phase 1");
        }

        static int Phase2(int f, int s)
        {
            Console.WriteLine($"In Main Again, at phase 2 f = {f} s = {s}");
            return f * s;
        }

        static void Main(string[] args)
        {
          // declaration
            Ouch o;
           
         // initialize
            o = new Ouch();
          
            
            

                // loading of data
            o.Perp = "Daniel";
            o.Victim = "Lydia";
            o.Phase1 += XXXXX;
            o.Phase2 += Phase2;
            o.Phase3 += O_Phase3;
            o.Phase4 += O_Phase4;

           // o.Phase2(1000, 30000);
            // usage
            Console.WriteLine("In Main: Getting Ready to call ScreamOuch");
            o.screamOuch(5);
            Console.WriteLine("Back In Main: just called ScreamOuch");

            //o.Phase3(-3, 30);
            // destruction

            

            
        }

        private static void O_Phase4()
        {
            Console.WriteLine("In Main Again, at phase 4");
        }

        private static int O_Phase3(int first, int second)
        {
            Console.WriteLine($"In Main Again, at phase 3 f = {first} s = {second}");
            return first * second;
        }
    }   // this class is the subscriber of the 4 phase events
}
