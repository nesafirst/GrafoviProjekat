using System;
using System.Collections.Generic;
using System.Text;

namespace Grafovi
{
    class BipartitniGraf:NeusmerenGraf
    {
        private List<int> L, R;

        public BipartitniGraf():base()
        {
            L = new List<int>();
            R = new List<int>();
        }

        public BipartitniGraf(int brojCvorova, Dictionary<int, List<Tuple<int, double>>> CvoroviSusedi) : base(brojCvorova, CvoroviSusedi)
        {
            L = new List<int>();
            R = new List<int>();
        }

        public BipartitniGraf(int brojCvorova, Dictionary<int, List<Tuple<int, double>>> CvoroviSusedi, List<int> L,List<int> R) : base(brojCvorova, CvoroviSusedi)
        {
            this.L = L;
            this.R = R;
        }

        public Dictionary<int,int>Matching()
        {
            //prvo pravimo usemeren graf sa izvornim i krajnjim cvorom, pa pustamo Edmonds-Karpov algoritam

             
            Dictionary<int,int> res = new Dictionary<int,int>();    //rezultujuce uparivanje

            Dictionary<int, List<Tuple<int, double>>> listaSusedstva = new Dictionary<int,List< Tuple<int, double>>>(); //potrebno za kreiranje grafa

            listaSusedstva.Add(1, new List<Tuple<int, double>>());
            foreach(int i in L)
            {
                listaSusedstva[1].Add(new Tuple<int,double>(i+1, 1));
                listaSusedstva.Add(i+1, new List<Tuple<int, double>>());
                foreach(Tuple<int,double> t in CvoroviSusedi[i])
                {
                    listaSusedstva[i+1].Add(new Tuple<int,double>(t.Item1+1, 1));
                }
            }
            foreach(int i in R)
            {
                listaSusedstva.Add(i+1, new List<Tuple<int, double>>());
                listaSusedstva[i+1].Add(new Tuple<int, double>(brojCvorova + 2, 1));
            }
            listaSusedstva.Add(brojCvorova + 2, new List<Tuple<int, double>>());
            UsmerenGraf g = new UsmerenGraf(brojCvorova+2,listaSusedstva);
            
            
            
            //graf je napravljen, sada dodeljujemo granama kapacitete i zovemo Edmonds-Karp

            Dictionary<int, Dictionary<int, double>> listaKapaciteta = new Dictionary<int, Dictionary<int, double>>();
            for(int i=1;i<=brojCvorova+2;i++)
            {
                listaKapaciteta.Add(i, new Dictionary<int, double>());
            }
            foreach(int i in listaSusedstva.Keys)
            {  
                foreach (Tuple<int, double> t in listaSusedstva[i])
                {
                    listaKapaciteta[i].Add(t.Item1, 1);
                    listaKapaciteta[t.Item1].Add(i, 1);
                }
            }

            
            
            

            Dictionary<int, Dictionary<int, double>> m = new Dictionary<int, Dictionary<int, double>>();
            double flow= g.EdmondsKarp(1, brojCvorova + 2, listaKapaciteta,out m);
            foreach(int i in L)
            {
                foreach (Tuple<int,double>t in CvoroviSusedi[i])
                    if (m[i+1][t.Item1+1] == 1) res.Add(i, t.Item1);     //za svaki cvor i, bice samo jedan ovakav cvor j
            }

            return res;

            
        }

       




    }
}
