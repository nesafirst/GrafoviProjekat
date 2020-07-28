using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grafovi
{
    class NeusmerenGraf: Graf
    {
        protected string kod; // string koji predstavlja gornji trougaoni deo matrice - koristi se kod g6 algoritma
       

        public string Kod
        {
            get
            {
                return kod;
            }
        }

        public NeusmerenGraf(NeusmerenGraf g)
        {
            this.brojCvorova = g.BrojCvorova;
            this.CvoroviSusedi = new Dictionary<int, List<Tuple<int, double>>>(g.CvoroviSusedi);
        }

        public NeusmerenGraf():base()
        {
           
        }

        public NeusmerenGraf(int brojCvorova,Dictionary<int,List<Tuple<int,double>>> CvoroviSusedi):base(brojCvorova,CvoroviSusedi)
        {
            
        }


        public NeusmerenGraf Primov()
        {
            Dictionary<int, List<Tuple<int, double>>> ListaSusedstva = new Dictionary<int, List<Tuple<int, double>>>(brojCvorova);
            Heap ostaliCvorovi = new Heap();
            Dictionary<int, int> prethodni = new Dictionary<int, int>(brojCvorova);
            prethodni.Add(1, 1);

            for (int j = 2; j <= brojCvorova; j++)
            {
                ostaliCvorovi.Add(j, double.MaxValue);
                prethodni.Add(j, 0);
            }
            ListaSusedstva.Add(1, new List<Tuple<int, double>>());

            int noviCvor = 0;
            int i = 0;
            int pocetniCvor = 0;
            while (ListaSusedstva.Count < brojCvorova)
            {
                if (noviCvor != 0)
                {
                    i = noviCvor;
                }
                else
                {
                    i = 1;
                }

                foreach (Tuple<int, double> t in CvoroviSusedi[i])
                {
                    if (!ListaSusedstva.ContainsKey(t.Item1))
                    {
                        if (t.Item2 < ostaliCvorovi.weight(t.Item1))
                        {
                            ostaliCvorovi.setWeight(t.Item1, t.Item2);
                            prethodni[t.Item1] = i;
                        }
                    }
                }

                noviCvor = ostaliCvorovi.Min().Key;
                pocetniCvor = prethodni[noviCvor];
                ListaSusedstva[pocetniCvor].Add(new Tuple<int, double>(noviCvor, ostaliCvorovi.Min().Value));
                ListaSusedstva.Add(noviCvor, new List<Tuple<int, double>>());
                ListaSusedstva[noviCvor].Add(new Tuple<int, double>(pocetniCvor, ostaliCvorovi.Min().Value));
                ostaliCvorovi.Remove();
            }
            return new NeusmerenGraf(brojCvorova, ListaSusedstva); 
        }


        public NeusmerenGraf(int brojCvorova,int[,]matrica):base(brojCvorova,matrica)
        {
            for (int j = 1; j < brojCvorova; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    if (matrica[i, j] == 1)
                    {
                        CvoroviSusedi[i + 1].Add(new Tuple<int, double>(j + 1, 1));
                        CvoroviSusedi[j + 1].Add(new Tuple<int, double>(i + 1, 1));
                    }
                }
            }
 
        }


        public NeusmerenGraf(string kod)
        {
            this.kod = kod;
            for(int i=1;i<=kod.Length;i++)
            {
                if(i*(i-1)/2==kod.Length)
                {
                    brojCvorova = i;
                    break;
                }
            }
            GrafOdKoda(kod);
        }

        public NeusmerenGraf(string s, string format)
        {
            if(format=="g6" || format=="G6")
            {
                string t = "";
                for(int i=0;i<s.Length;i++)
                {
                    t+=Convert.ToInt16(s[i])+" ";
                }
                t = t.Remove(t.Length-1);
                konstruisiG6(t);
            }
            if(format=="s6" || format=="S6")
            {
                konstruisiS6(s);
            }
        }

        private void konstruisiG6(string s)
        {
            if(s[0]!='~')
            {
                string[] niz = s.Split(' ');
                brojCvorova = Convert.ToInt16(niz[0]) - 63;

                kod = RInverz(niz, 1, niz.Length - 1);
                kod = kod.Remove(brojCvorova * (brojCvorova - 1) / 2);

                GrafOdKoda(kod);
            }
            if (s[0]=='~' && s[1]!='~')
            {
                string[] niz = s.Split(' ');
                string bin = RInverz(niz, 1, 3);
                brojCvorova = Convert.ToInt32(bin, 2);

                kod = RInverz(niz, 4, niz.Length - 1);
                kod = kod.Remove(brojCvorova * (brojCvorova - 1) / 2);

                GrafOdKoda(kod);
             
            }
            if (s[0]=='~' && s[1]=='~')
            {
                string[] niz = s.Split(' ');
                string bin = RInverz(niz, 2, 7);
                brojCvorova = Convert.ToInt32(bin, 2);

                kod = RInverz(niz, 8, niz.Length - 1);
                kod = kod.Remove(brojCvorova * (brojCvorova - 1) / 2);

                GrafOdKoda(kod);
            }
        }
       
        private void GrafOdKoda(string kod)
        {
            matrica = new int[brojCvorova, brojCvorova];
            int e = -1;
            for (int j = 1; j < brojCvorova; j++)
            {
                for (int i = 0; i < j; i++)
                {
                    e++;
                    matrica[i, j] = kod[e] - '0';
                    matrica[j, i] = matrica[i, j];
                }

            }

            CvoroviSusedi = new Dictionary<int, List<Tuple<int,double>>>();
           

            for(int i=1;i<=brojCvorova;i++)
            {
                CvoroviSusedi[i] = new List<Tuple<int, double>>();
            }

            for(int j=1;j<brojCvorova;j++)
            {
                for(int i=0;i<j;i++)
                {
                    if(matrica[i,j]==1)
                    {
                        CvoroviSusedi[i + 1].Add(new Tuple<int, double>(j + 1, 1));
                        CvoroviSusedi[j + 1].Add(new Tuple<int, double>(i + 1, 1));
                    }
                }
            }
        }

        public override void  ispisiGrane()
        {
            for(int i=1;i<=brojCvorova;i++)
            {
                foreach(Tuple<int,double> t in CvoroviSusedi[i])
                {
                    if(i<=t.Item1)
                    {
                        Console.WriteLine($"({i},{t.Item1}) težina:{t.Item2}");
                    }
                }
            } 
        }


        private string R(string x)
        {
            int br = x.Length / 6;
            for(int i=1;i<br;i++)
            {
                x = x.Insert(i * 7 - 1, " ");
            }
            string r = "";
            string[] niz = x.Split(' ');
            for(int i=0;i<niz.Length;i++)
            {
                r += (char)(Convert.ToInt32(niz[i],2)+63);
            }
            return r;
        }

        private string RInverz(string[] niz,int poc,int kraj)
        {
            string bin = "";
            for (int i = poc; i<=kraj; i++)
            {
                int broj = Convert.ToInt16(niz[i]) - 63;
                string bin1 = Convert.ToString(broj, 2);
                int dopuna = (6 - (bin1.Length % 6)) % 6;
                for (int u = 0; u < dopuna; u++)
                    bin1 = "0" + bin1;
                bin += bin1;
            }
            return bin;
        }

        private void konstruisiS6(string s)
        {
            
        }


        public string formatiraj(string format)
        {
            if(format=="g6")
            {
                return g6();
            }
            return "";
        }

        private string s6()
        {
            return "";
        }

        protected string g6()
        {
            string Rx;
            string b = kod;
            int p = 6 - (b.Length % 6);
            for (int i = 0; i < p; i++)
            {
                b += "0";
            }
            Rx = R(b);

            string N = "";
            string Rn = "";

            string bigEnd = Convert.ToString(brojCvorova, 2);
            if (brojCvorova >= 0 && brojCvorova <= 62)
                N += (char)(brojCvorova + 63);
            if (brojCvorova >= 63 && brojCvorova <= 258047)
            {
                int dopuna = 18 - bigEnd.Length % 18;
                for (int i = 0; i < dopuna; i++)
                {
                    bigEnd = "0" + bigEnd;
                }
                Rn = R(bigEnd);
                N += "~" + Rn;
            }
            if (brojCvorova >= 258048 && brojCvorova <= Int32.MaxValue)
            {
                int dopuna = 36 - (bigEnd.Length % 36);
                for (int i = 0; i < dopuna; i++)
                {
                    bigEnd = "0" + bigEnd;
                }
                Rn = R(bigEnd);
                N += "~~" + Rn;
            }
            if ((brojCvorova >= 63 && brojCvorova <= 258047) || (brojCvorova >= 258048 && brojCvorova <= Int32.MaxValue))
            {
                string[] niz1 = N.Split(' ');
                for (int i = 0; i < niz1.Length; i++)
                    N += (char)(Convert.ToInt32(niz1[i]));
            }

            return N + Rx;
        }

        public int stepen(int i)
        {
            return CvoroviSusedi[i].Count;
        }
        
    }

}
