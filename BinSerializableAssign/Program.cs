using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace BinSerializableAssign
{

    [Serializable]
    class BinarySer : IDeserializationCallback
    {
        public DateTime todaysdate, YrOfBirth;
       
        [NonSerialized]
        public int age;
        public BinarySer()
        {

        }
        public BinarySer(DateTime yearbirth)
        {
            YrOfBirth = yearbirth;
            todaysdate = DateTime.Now;
            
        }
        public void OnDeserialization(object sender)
        {
            
            age = (int)(todaysdate - YrOfBirth).TotalDays / 365;
           
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Day: ");
            int day = int.Parse(Console.ReadLine());
            Console.Write("Enter Month: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Enter Year: ");
            int year = int.Parse(Console.ReadLine());

            
            DateTime inputDate = new DateTime(year, month, day);
            BinarySer bs = new BinarySer(inputDate);
            Console.WriteLine("Todays Date is:" + bs.todaysdate);

            FileStream fs = new FileStream(@"BinaryEg.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter brf = new BinaryFormatter();
            brf.Serialize(fs, bs);

            //deserialize
            fs.Seek(0, SeekOrigin.Begin);
            BinarySer result = (BinarySer)brf.Deserialize(fs);

            Console.WriteLine("---------------------------");
            Console.WriteLine("Age in Years:" + result.age);
            Console.ReadLine();
        }
    }
}
