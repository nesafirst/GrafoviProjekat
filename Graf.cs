using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafovi
{
    abstract class Graf
    {
        protected Dictionary<int, List<Tuple<int,double>>> CvoroviSusedi;   //liste susedstva za svaki cvor- int je sused, double tezina grane
        protected int brojCvorova;
        protected int[,] matrica;  //matrica za predstavljanje grafa, koristi se opciono

        

        public int BrojCvorova
        {
            get
            {
                return brojCvorova;
            }
            set
            {
                brojCvorova = value;
            }
        }


        public Graf()
        {
            brojCvorova = 0;
            CvoroviSusedi = new Dictionary<int, List<Tuple<int,double>>>();
           
        }

        public Graf(int brojCvorova, int[,] matrica)
        {
            this.brojCvorova = brojCvorova;
            this.matrica = matrica;
            CvoroviSusedi = new Dictionary<int, List<Tuple<int, double>>>();
            for (int i = 1; i <= brojCvorova; i++)
            {
                CvoroviSusedi[i] = new List<Tuple<int, double>>();
            }
        }

        public Graf(int brojCvorova, Dictionary<int, List<Tuple<int, double>>> CvoroviSusedi)
        {
            this.brojCvorova = brojCvorova;
            this.CvoroviSusedi = CvoroviSusedi;
        }

       

        public int[,] Matrica
        {
            get
            {
                if (matrica.LongLength > 0)
                    return matrica;
                else return new int[1, 1];
            }
        }

        

        public void ispisiMatricu()
        {
            for (int i = 0; i < brojCvorova; i++)
            {
                for (int j = 0; j < brojCvorova; j++)
                {
                    Console.Write(matrica[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public abstract void ispisiGrane();
       

     

        public void DFS(int i)
        {
            Stack<int> stek = new Stack<int>();
            bool[] visited = new bool[brojCvorova];
            stek.Push(i);
            visited[i - 1] = true;
            while (stek.Count != 0)
            {
                int k = stek.Pop();
                Console.Write(k + " ");
                foreach (Tuple<int,double> p in CvoroviSusedi[k])
                {
                    if (!visited[p.Item1-1])
                    {

                        stek.Push(p.Item1);
                        visited[p.Item1-1] = true;
                    }
                }
            }
        }

        public void BFS(int i)
        {
            Queue<int> red = new Queue<int>();
            bool[] visited = new bool[brojCvorova];
            red.Enqueue(i);
            visited[i - 1] = true;
            while (red.Count > 0)
            {
                int k = red.Dequeue();
                Console.Write(k + " ");
                foreach (Tuple<int,double> p in CvoroviSusedi[k])
                {
                    if (!visited[p.Item1-1])
                    {
                        red.Enqueue(p.Item1);
                        visited[p.Item1-1] = true;
                    }
                }

            }
        }

        
        //funkcija vraca Dictionary ciji je kljuc i-ti cvor, a vrednost uredjeni par ciji su elementi udaljenost od cvora a i predak u najkracem putu
        public Dictionary<int,Tuple<int,double>> BelmanFord(int a) 
        {
            Dictionary<int,Tuple<int, double>> Cvorovi = new Dictionary<int,Tuple<int, double>>();
            for(int i=1;i<=brojCvorova;i++)
            {
                Cvorovi.Add(i, new Tuple<int,double>(0, double.MaxValue));
            }
            Cvorovi[a] = new Tuple<int, double>(0, 0);
            for(int v=1;v<brojCvorova;v++)
            {
                for(int i =1;i<=brojCvorova;i++)
                {
                    foreach(Tuple<int,double> t in CvoroviSusedi[i])
                    {
                        if(t.Item2+Cvorovi[i].Item2<Cvorovi[t.Item1].Item2)
                        {
                            Cvorovi[t.Item1] = new Tuple<int, double>(i, t.Item2 + Cvorovi[i].Item2);
                        }
                    }
                }
            }

            for(int i=1;i<brojCvorova;i++)
            {
                foreach(Tuple<int,double> t in CvoroviSusedi[i])
                {
                    if(t.Item2+Cvorovi[i].Item2<Cvorovi[t.Item1].Item2)
                    {
                      throw new NegativanCiklusException();
                    }
                }
            }

            return Cvorovi;
        }

    }
}
