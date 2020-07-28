using Grafovi;
using System;
using System.Collections.Generic;
using System.IO;


namespace Grafovi
{
    class Program
    {
       

        static void Main(string[] args)
        {
            /*
             * Test za g6 algoritam, u oba smera

            StreamReader s = new StreamReader("g6.txt");

            NeusmerenGraf g = new NeusmerenGraf(s.ReadLine());
            Console.WriteLine(g.formatiraj("g6"));

            NeusmerenGraf g1 = new NeusmerenGraf(s.ReadLine(), "g6");
            g1.ispisiGrane();

            */
            


            /*
             * Test za dfs

            StreamReader s = new StreamReader("ulazdfs.txt");
            int brCvorova = Convert.ToInt32(s.ReadLine());
            int brGrana = Convert.ToInt32(s.ReadLine());
            Dictionary<int, List<Tuple<int, double>>> lista = new Dictionary<int, List<Tuple<int, double>>>();

            for(int i=1;i<=brCvorova;i++)
                lista.Add(i, new List<Tuple<int, double>>());

            for(int i=1;i<brGrana;i++)
            {
                string[] cvorovi = s.ReadLine().Split(' ');
                int c1 = Convert.ToInt32(cvorovi[0]);
                int c2 = Convert.ToInt32(cvorovi[1]);
                lista[c1].Add(new Tuple<int, double>(c2,1));
            }
            UsmerenGraf g = new UsmerenGraf(brCvorova,lista);
            g.DFS(1);

            */

            /*
            * Test za Belman-Ford algoritam

            StreamReader s = new StreamReader("ulazbelmanford.txt");
            int brCvorova = Convert.ToInt32(s.ReadLine());
            int brGrana = Convert.ToInt32(s.ReadLine());
            Dictionary<int, List<Tuple<int, double>>> lista = new Dictionary<int, List<Tuple<int, double>>>();
            for (int i = 1; i <= brCvorova; i++)
                lista.Add(i, new List<Tuple<int, double>>());

            for(int i=1;i<=brGrana;i++)
            {
                string[] niz = s.ReadLine().Split(' ');
                int c1 = Convert.ToInt32(niz[0]);
                int c2 = Convert.ToInt32(niz[1]);
                double t = Convert.ToDouble(niz[2]);
                lista[c1].Add(new Tuple<int, double>(c2, t));
            }

            UsmerenGraf g = new UsmerenGraf(brCvorova, lista);
            Dictionary<int, Tuple<int, double>> d = g.BelmanFord(1);
            for(int i=1;i<=brCvorova;i++)
            {
                Console.WriteLine("cvor "+i+ "predak "+d[i].Item1+"udaljenost "+d[i].Item2);
            }

           */

          
            /*
             * Test za Primov algoritam
             * 
            StreamReader s = new StreamReader("ulazprimov.txt");
            int brCvorova = Convert.ToInt32(s.ReadLine());
            int brGrana = Convert.ToInt32(s.ReadLine());
            Dictionary<int, List<Tuple<int, double>>> lista = new Dictionary<int, List<Tuple<int, double>>>();
            for (int i = 1; i <= brCvorova; i++)
                lista.Add(i, new List<Tuple<int, double>>());
            for(int i=1;i<=brGrana;i++)
            {
                string[]niz=s.ReadLine().Split(' ');
                int c1 = Convert.ToInt32(niz[0]);
                int c2 = Convert.ToInt32(niz[1]);
                double tezina = Convert.ToDouble(niz[2]);
                lista[c1].Add(new Tuple<int, double>(c2,tezina));
                lista[c2].Add(new Tuple<int, double>(c1, tezina));
            }

            NeusmerenGraf g = new NeusmerenGraf(brCvorova, lista);
            NeusmerenGraf stablo = g.Primov();
            stablo.ispisiGrane();

            */


            /*
             *       Test za flow
             * 
            StreamReader s = new StreamReader("ulazflow.txt");
            Dictionary<int, List<Tuple<int, double>>> listaSusedstva = new Dictionary<int, List<Tuple<int, double>>>();
            Dictionary<int, Dictionary<int, double>> kapacitetGrana = new Dictionary<int, Dictionary<int, double>>();
            int brojCvorova = Convert.ToInt16(s.ReadLine());
            int brojGrana = Convert.ToInt16(s.ReadLine());
            for(int i =1;i<=brojCvorova;i++)
            {
                listaSusedstva.Add(i, new List<Tuple<int, double>>());
                kapacitetGrana.Add(i, new Dictionary<int, double>());
            }
            for(int i=1;i<=brojGrana;i++)
            {
                string[] words = s.ReadLine().Split(' ');
                int cvor1 = Convert.ToInt16(words[0]);
                int cvor2 = Convert.ToInt16(words[1]);
                double k = Convert.ToInt16(words[2]);
                listaSusedstva[cvor1].Add(new Tuple<int,double>(cvor2, 1));
                kapacitetGrana[cvor1].Add(cvor2, k);
                kapacitetGrana[cvor2].Add(cvor1, k);
            }
            UsmerenGraf g = new UsmerenGraf(brojCvorova, listaSusedstva);
            int izvor = Convert.ToInt16(s.ReadLine());
            int kraj = Convert.ToInt16(s.ReadLine());
            Dictionary<int, Dictionary<int, double>> flow;
            double f=g.EdmondsKarp(izvor, kraj, kapacitetGrana,out flow);
            Console.WriteLine(f);
            
             */


            /* 
             * Test za matching
             * 
            StreamReader s1 = new StreamReader("ulazmatching.txt");
            Dictionary<int, List<Tuple<int, double>>> l = new Dictionary<int, List<Tuple<int, double>>>();
            List<int> L = new List<int>();
            List<int> R = new List<int>();
            int brCvorova = Convert.ToInt32(s1.ReadLine());
            string[] ld = s1.ReadLine().Split(' ');
            int brlevo = Convert.ToInt32(ld[0]);
            int brdesno = Convert.ToInt32(ld[1]);
            for(int i=1;i<=brlevo;i++)
            {
                int c = Convert.ToInt32(s1.ReadLine());
                L.Add(c);
                l.Add(c, new List<Tuple<int, double>>());
            }
            for(int i=1;i<=brdesno;i++)
            {
                int c = Convert.ToInt32(s1.ReadLine());
                R.Add(c);
                l.Add(c, new List<Tuple<int, double>>());
            }
            int brveza = Convert.ToInt32(s1.ReadLine());
            for(int i=1;i<=brveza;i++)
            {
                string[] grane = s1.ReadLine().Split(' ');
                int c1 = Convert.ToInt32(grane[0]);
                int c2 = Convert.ToInt32(grane[1]);
                l[c1].Add(new Tuple<int, double>(c2, 1));
                l[c2].Add(new Tuple<int, double>(c1, 1));
            }
            BipartitniGraf b = new BipartitniGraf(brCvorova,l,L,R);
            Dictionary<int, int> m = b.Matching();
            foreach (int i in m.Keys)
                Console.WriteLine(i + " " + m[i]);
            */
        }
    }
}
