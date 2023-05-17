using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace GenerickiStackApp.Buisness
{
    public class MojStack<T> : IEnumerable<T>, IEnumerable 
    {
        #region Data
        private T[] items;
        private int vrh = -1;
        public const int PodrazumevaniPocetniKapacitet = 8;
        private const int PodrazumevanoUmnozavanjeKapaciteta = 2;
        #endregion

        #region Constructors
        public MojStack()
        {
            this.items = new T[MojStack<T>.PodrazumevaniPocetniKapacitet];
        }
        public MojStack(int pocetniKapacitet)
        {
            if (pocetniKapacitet < PodrazumevaniPocetniKapacitet)
                pocetniKapacitet = PodrazumevaniPocetniKapacitet;
            this.items=new T[pocetniKapacitet];
        }
        public MojStack(IEnumerable<T> items)
        {
            this.items = new T[items.Count() * 2];
            foreach (T item in items)
            {
                this.Push(item);
            }
            //Array.Copy(items.ToArray(), this.items, items.Count());
        }
        #endregion

        #region Properties
        public int Count => this.vrh + 1;
        #endregion

        #region Methods
        public void PovecajKapacitet(int umnozavanjeKapaciteta = PodrazumevanoUmnozavanjeKapaciteta)
        {
            if (umnozavanjeKapaciteta < PodrazumevanoUmnozavanjeKapaciteta)
                umnozavanjeKapaciteta = PodrazumevanoUmnozavanjeKapaciteta;
            T[] itemsNew = new T[this.items.Length * umnozavanjeKapaciteta];
            //for (int i = 0; i < this.items.Length; i++)
            //{
            //    itemsNew[i] = this.items[i];
            //}
            Array.Copy(this.items,itemsNew, this.items.Length);
            this.items = itemsNew;
        }
        public void Push(T item)
        {
            vrh++;
            if (vrh == items.Length)
                this.PovecajKapacitet();
            this.items[vrh] = item;
        }
        public T Pop()
        {
            if (vrh < 0)
                throw new InvalidOperationException();
            return this.items[vrh--];
            //vrh--;
            //return this.items[vrh + 1];
        }
        public T Peek()
        {
            if (vrh < 0)
                throw new InvalidOperationException();
            return this.items[vrh];
        }
        public void Clear() => this.vrh = - 1;
        #endregion

        #region Implementation
        public IEnumerator<T> GetEnumerator() => new EnumeratorZaMojStack<T>(this.items, this.vrh);
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        #endregion
    }

    public class EnumeratorZaMojStack<T> : IEnumerator<T>, IEnumerator
    {
        #region Data
        private T[] items;
        private int vrh;
        private int indeks;
        #endregion

        #region Constructor
        public EnumeratorZaMojStack(T[] items, int vrh)
        {
            this.items = items;
            this.vrh = vrh;
            this.indeks = vrh + 1;
        }
        #endregion

        #region Implementacija generickog interfejsa
        public T Current
        {
            get
            {
                if (this.indeks < 0 || this.indeks > vrh)
                    throw new InvalidOperationException();
                return this.items[this.indeks];
            }
            //ako se zakomentarise set, onda bi objekat klasi MojStack bio samo readonly
            set
            {
                if (this.indeks < 0 || this.indeks > vrh)
                    throw new InvalidOperationException();
                this.items[this.indeks] = value;
            }
        }
        public bool MoveNext()
        {
            this.indeks--;
            return this.indeks >= 0;
        }
        public void Reset() => this.indeks = vrh + 1;
        public void Dispose() { }
        //movenext i reset su zajadnicki za oba
        #endregion

        #region Implementacija negenerickog interfejsa
        object IEnumerator.Current => this.Current;
        #endregion
    }
}
