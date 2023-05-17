using GenerickiStackApp.Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerickiStackApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MojStack<int> mojStackInt= new MojStack<int>();
            for (int i = 5; i < 15; i++)
                mojStackInt.Push(i);

            int x1 = mojStackInt.Pop();
            int x2 = mojStackInt.Peek();

            foreach (var item in mojStackInt)
            {
                var x3 = item;
            }
            mojStackInt.Push(100);

            IEnumerator<int> iterator = mojStackInt.GetEnumerator();
            while (iterator.MoveNext())
            {
                int x4 = iterator.Current;
            }
        }
    }
}
