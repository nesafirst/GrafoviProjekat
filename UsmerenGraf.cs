using System;
using System.Collections.Generic;
using System.Text;

namespace Grafovi
{
    class UsmerenGraf:Graf
    {
       
        public UsmerenGraf():base()
        {
            
        }

        public UsmerenGraf(int brojCvorova)
        {
            this.brojCvorova = brojCvorova;
            CvoroviSusedi = new Dictionary<int, List<Tuple<int, double>>>();
            for(int i=1;i<=brojCvorova;i++)
            {
                CvoroviSusedi.Add(i,new List<Tuple<int,double>>());
            }
        }

        public UsmerenGraf(int brojCvorova,Dictionary<int,List<Tuple<int,double>>> CvoroviSusedi):base(brojCvorova,CvoroviSusedi)
        {
            
        }

        public UsmerenGraf(UsmerenGraf g)
        {
            this.brojCvorova = g.brojCvorova;
            this.CvoroviSusedi = g.CvoroviSusedi;
            
        }

        public UsmerenGraf(int brojCvorova, int[,] matrica):base(brojCvorova,matrica)
        {
            for(int i=0;i<brojCvorova;i++)
            {
                for(int j=0;j<brojCvorova;j++)
                {
                    if (matrica[i,j]==1)
                    { 
                        CvoroviSusedi[i + 1].Add(new Tuple<int, double>(j + 1, 1));
                    }
                }
            }
        }



        public override void ispisiGrane()
        {
            for (int i=1;i<=brojCvorova;i++)
            {
                foreach(Tuple<int,double> t in CvoroviSusedi[i])
                {
                    Console.WriteLine($"({i},{t.Item1}) težina:{t.Item2} ");
                }
            }
        }

        public int ulazniStepen(int i)
        {
            int s = 0;
            for(int j=1;j<=brojCvorova;j++)
            {
                if(j!=i)
                {
                    foreach(Tuple<int,double> t in CvoroviSusedi[j] )
                    {
                        if (t.Item1 == i)
                            s++;
                    }
                }
            }
            return s;
        }

        public int izlazniStepen(int i)
        {
            return CvoroviSusedi[i].Count;
        }

        public double EdmondsKarp(int s,int t, Dictionary<int,Dictionary<int,double>> capacity,out Dictionary<int,Dictionary<int,double>>flow)
        {
          

            flow = new Dictionary<int, Dictionary<int, double>>();
            
            foreach(int i in capacity.Keys)
            {
                flow.Add(i, new Dictionary<int, double>());
            }
            

            foreach(int i in capacity.Keys)
            {
                
                foreach (Tuple<int, double> tuple in CvoroviSusedi[i])
                {
                    flow[i].Add(tuple.Item1, 0);
                    flow[tuple.Item1].Add(i, capacity[i][tuple.Item1]);
                }

            }

            double f = 0;
            int[] p;

            while (true)
            {
                double fr = BFSFlow(s,t,capacity,flow,out p);
                if (fr == 0) break;

                f += fr;
                int c = t;

                while(c!=s)
                {
                    int parent =p[c];
                    flow[parent][c] += fr;
                    flow[c][parent] -= fr;
                    
                    c = parent;
                }
               

            }
            return f;
                

        }

        protected double BFSFlow(int s, int t, Dictionary<int, Dictionary<int, double>> capacity,Dictionary<int,Dictionary<int,double>>flow,out int[]p)
        {
            p = new int[brojCvorova+1];
            for (int i = 1; i <= brojCvorova; i++)
                p[i] = 0;
            p[s] = -1;

            double[] d = new double[brojCvorova+1];
            d[s] = double.MaxValue;
            Queue<int> que = new Queue<int>();
            que.Enqueue(s);

            while(que.Count!=0)
            {
                int i = que.Dequeue();
                foreach(int j in capacity[i].Keys )
                {
                    
                    if(p[j]==0 && capacity[i][j]>flow[i][j])
                    {
                        p[j] = i;
                        if (d[i] < capacity[i][j] - flow[i][j]) d[j] = d[i];
                        else d[j] = capacity[i][j] - flow[i][j];
                        if (j != t) que.Enqueue(j);
                        else return d[t];
                    }
                }
            }
           
            return d[t];
        }



    }
}
