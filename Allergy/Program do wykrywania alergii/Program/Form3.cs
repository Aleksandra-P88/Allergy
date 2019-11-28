using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;


namespace Program
{
    public partial class Form3 : Form
    {


        Form1 form1 = new Form1();

    

        private string myVal;
        private string myVal_1;

        public string MyVal
        {

            get { return myVal; }
            set { myVal = value; }
        }

        public string MyVal_1
        {

            get { return myVal_1; }
            set { myVal_1 = value; }
        }

        public Form3(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
          

        }

        public Tuple<string,string>changeback()
        {


            MyVal = textBox1.Text;
            MyVal_1 = textBox2.Text;


            return (Tuple.Create(MyVal, MyVal_1));
        }



        private void button1_Click(object sender, EventArgs e)
        {

            Tuple<string, string> changeback2=changeback();
            form1.textBox1.Text = changeback2.Item2;
            form1.textBox2.Text = changeback2.Item1;
            form1.change();
            form1.Main_Contour();
            

            this.Close();

        }
    }
}
