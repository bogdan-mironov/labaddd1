using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace labaddd1
{
    class Element
    { 
        public int integer;         
        public Element next;  
    };
    class StackW
    {
        Element top = null;
        public int count = 0;
        public StackW()
        {
            top = null;
        }
        public void Push(int value)
        {
            Element n = new Element();
            {
                n.integer = value;
                n.next = top;
            }
            top = n;
            count++;
        }
        public int Pop()
        {
            if (top != null)
            {
                int poppedOut = top.integer;
                top = top.next;
                count--;
                return poppedOut;
            }
            return 0;
        }
        
    };
    class QueueW
    {
        Element head;
        Element tail;
        public int count = 0;
        public void Enqueue(int value)
        {
            Element n = new Element();
            n.integer = value;
            n.next = null;
            if (tail != null) tail.next = n;
            else head = n;
            tail = n;
            count++;
        }
        public int Dequeue()
        {
            if (head != null)
            {
                int deleted = head.integer;
                head = head.next;
                count--;
                if (head == null) tail = null;
                return deleted;
            }
            return 0;
        }
        public QueueW()
        {
            head = null;
            tail = head;
        }
    };
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "info.csv";
            FileStream file = new FileStream(filename, FileMode.OpenOrCreate);
            file.Close();
            QueueW queue = new QueueW();
            StackW stack = new StackW();
            int tmp = 0;
            string writetime, readtime;
            Console.WriteLine("Queue");
            StreamWriter streamWriter = new StreamWriter(filename);
            streamWriter.WriteLine("Queue: \n data;Write ;Read");
            Stopwatch sw = new Stopwatch();
            for (int j = 1; j <= 5; j++)
            {
                sw.Start();
                for (int i = 0; i < 2000000 * j; i++) queue.Enqueue(i);
                sw.Stop(); writetime = sw.ElapsedMilliseconds.ToString();
                sw.Restart();
                for (int i = 0; i < queue.count;) tmp = queue.Dequeue();  
                sw.Stop(); readtime = sw.ElapsedMilliseconds.ToString();
                sw.Reset();
                Console.WriteLine($"{j * 2000000} чисел записано за {writetime}мс, прочитано за {readtime}мс");
                streamWriter.WriteLine(j * 2000000 + ";" + writetime + ";" + readtime);
            }
            streamWriter.WriteLine("stack: \n");
            Console.WriteLine("Stack ");
            for (int j = 1; j <= 5; j++)
            {
                sw.Start();
                for (int i = 0; i < 2000000 * j; i++) stack.Push(i);  
                sw.Stop(); writetime = sw.ElapsedMilliseconds.ToString();
                sw.Restart();
                for (int i = 0; i < stack.count;) tmp = stack.Pop();
                sw.Stop(); readtime = sw.ElapsedMilliseconds.ToString();
                sw.Reset();
                Console.WriteLine($"{j * 2000000} чисел записано за {writetime}мс, прочитано за {readtime}мс");
                streamWriter.WriteLine(j * 2000000 + ";" + writetime + ";" + readtime);
            }
            streamWriter.Close();
            Console.WriteLine("Stop");
            Console.ReadKey();

        }
    }
}
