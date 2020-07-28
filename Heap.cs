using System;
using System.Collections.Generic;
using System.Text;

namespace Grafovi
{
    public class Heap
    {
        private List<double> tezine = new List<double>();
        private List<int> cvorovi = new List< int>();
        private int size = 0;

        private Dictionary<int, int> indeksi;
        

        public Heap()
        {
            tezine = new List<double>();
            cvorovi = new List<int>();
            indeksi = new Dictionary<int, int>();
        }

        public Heap(int n)
        {
            size = n;
            tezine = new List< double>(n);
            cvorovi = new List<int>(n);
            indeksi = new Dictionary<int, int>(n);
        }

        public  KeyValuePair<int, double> Min()
        {
            if(size>0)
                return new KeyValuePair<int, double> (cvorovi[0],tezine[0]);
            throw new IndexOutOfRangeException();
        }

        public KeyValuePair<int, double> Max()
        {
            if(size>0)
                return new KeyValuePair<int,double>(cvorovi[size-1],tezine[size - 1]);
            throw new IndexOutOfRangeException();
        }

        public bool Contains(int i)
        {
            return (indeksi.ContainsKey(i));
        }

        public void Add(int cvor,double tezina)
        {
            size++;
            tezine.Add(tezina);
            cvorovi.Add(cvor);
            indeksi.Add(cvor, size - 1);
            CalculateUp(cvorovi[size-1]);
        }

        public void Remove()
        {
            if(size>0)
            {
                indeksi.Remove(cvorovi[0]);
                tezine[0] = tezine[size - 1];
                cvorovi[0]=cvorovi [size-1];
                indeksi[cvorovi[size - 1]] = 0;
                size--;
                CalculateDown(cvorovi[0]);
            }
        }

        private void CalculateDown(int cvor)
        {
            int i = indeksi[cvor];

            while(HasLeftChild(i))
            {
                int indexToSwap = i;

                if (LeftChild(i).Value < tezine[i])
                    indexToSwap = 2 * i + 1;

                if (HasRightChild(i))
                {
                    if (RightChild(i).Value < tezine[indexToSwap])
                        indexToSwap = i * 2 + 2;
                }

                if (indexToSwap != i)
                {
                    Swap(i, indexToSwap);
                    i = indexToSwap;
                }
                else return;
                
            }
          
        }

        private void CalculateUp(int cvor)
        {
            int i = indeksi[cvor];
            while(HasParent(i) && tezine[i]<GetParent(i).Value)
            {
                Swap( i, (i-1)/2);
                i = (i - 1) / 2;
            }
        }

        private  void Swap(int  i,int j)
        {
           
            int s = indeksi[cvorovi[i]];
            indeksi[cvorovi[i]] = indeksi[cvorovi[j]];
            indeksi[cvorovi[j]] = s;
         
            int t = cvorovi[i];
            double p=tezine[i];
            cvorovi[i] = cvorovi[j];
            tezine[i] = tezine[j];
            cvorovi[j] = t;
            tezine[j] = p;
            
        }



        public bool HasParent(int i)
        {
            return i != 0;
        }

        public KeyValuePair<int,double> GetParent(int i)
        {
            if (HasParent(i))
                return new KeyValuePair<int, double>(cvorovi[(i - 1) / 2],tezine[(i-1)/2]);
            throw new IndexOutOfRangeException();
        }

        public bool HasLeftChild(int i)
        {
            return size > i * 2 + 1;
        }

        public bool HasRightChild(int i)
        {
            return size > 2 * (i + 1);
        }

        public KeyValuePair<int,double> LeftChild(int i)
        {
            if(HasLeftChild(i))
                return new KeyValuePair<int,double>(cvorovi[i * 2 + 1],tezine[i*2+1]);
            throw new IndexOutOfRangeException();
        }

        public KeyValuePair<int,double> RightChild(int i)
        {
            if (HasRightChild(i))
                return new KeyValuePair<int,double>(cvorovi[i * 2 + 2],tezine[i*2+2]);
            throw new IndexOutOfRangeException();
        }

        public double weight(int cvor)
        {
        
            return tezine[indeksi[cvor]];
        }

        public void setWeight(int cvor,double w)
        {
            int i = indeksi[cvor];
            if (w < tezine[i])
            {
                tezine[i] = w;
                CalculateUp(cvor);
                return;
            }
            else if (w > tezine[i])
            {
                tezine[i] = w;
                CalculateDown(cvor);
                return;
            }
            else if (w == tezine[i])
                return;
           
        }

        public void ispisi()
        {
            for(int i=0;i<size;i++)
            {
                Console.WriteLine(cvorovi[i] + " " + tezine[i]);
            }
        }

        public void ispisiIndekse()
        {
            foreach(int i in indeksi.Keys)
            {
                Console.WriteLine(i + " " + indeksi[i]);
            }
        }
    }
}
