using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Lab7
{

    class CollectionType<T> : Func<T>
    {
        
        List<int> list = new List<int>(10);
        public void Push() 
        {
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }

        }
        public void Pop()
        {
            Console.WriteLine("Введите элемент для удаления");
            int pos = Convert.ToInt32(Console.ReadLine());
            if (pos < 10)
            {
                for (int i = 0; i < pos; i++)
                {
                    Console.Write(list[i]);
                }
                for (int i = pos + 1; i < 10; i++)
                {
                    Console.Write(list[i]);
                }
                Console.WriteLine();
            }
            else
            {
                throw new IndexOutOfRangeException("введено неккоректное значение");
            }
        }
        public void Check()
        {
            Predicate<int> Find = (int x) => (x > 0&&x<10);
            Console.WriteLine($"Элемент есть в списке?:{Find(2)}");
        }
     
    }
    interface Func<T>
    {
        void Push();
        void Check();
        
    }
    class Messenger<T> where T : Message
    {
        public void SendMessage(T message)
        {
            Console.WriteLine($"Отправляется сообщение: {message.Text}");
        }
    }

    class Message
    {
        public string Text { get; } // текст сообщения
        public Message(string text)
        {
            Text = text;
        }
    }
    public class Bench
    {
        public static Bench bench = new Bench();
        public string NameOfElement { get; set; }
        int SizeOfElement { get; set; }
        string MaterialOfElement { get; set; }
        public int Cost { get; set; }
        public Bench()
        {
            NameOfElement = "Bench";
            SizeOfElement = 23;
            MaterialOfElement = "wood";
            Cost = 100;

        }
        public override string ToString()
        {
            return $"Тип обьекта:{bench.GetType()};Значения обькта:{bench.NameOfElement},{bench.SizeOfElement},{bench.MaterialOfElement}";

        }

    }
    class Collection<T, Bnch>
    {

    }
    class Program
    {
        public static async Task Main(string[] args)
        {
            CollectionType<int> obj1 = new CollectionType<int>();
            CollectionType<int> obj2 = new CollectionType<int>();
            CollectionType<int> obj3 = new CollectionType<int>();
            CollectionType<int>[] mas = { obj1, obj2, obj3 };
            obj1.Push();
            obj1.Check();
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<CollectionType<int>>(fs, mas[2]);
                Console.WriteLine("Data has been saved to file");
            }

            // чтение данных
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                CollectionType<int>? obj = await JsonSerializer.DeserializeAsync<CollectionType<int>>(fs);
                
            }
            try
            {
                obj1.Pop();
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка:{ex.Message}");
            }
            finally
            {
                Console.WriteLine("Программа завершена");
            }
        }


    }
}


