using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tema_WFA
{
    public partial class Form2 : Form
    {
        internal Form1 form1;
        internal string id;

        public Form2(Form1 form1, string id)
        {
            InitializeComponent();
            this.form1 = form1;
            this.id = id;

            
            List<string> x = new List<string>();
            x = form1.service.GetProductInfo(id);

            
            textBox1.Text = x[0]; // Name
            textBox2.Text = x[1]; // Price
            textBox3.Text = x[2]; // Quantity
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                form1.service.UpdateProduct(id, textBox1.Text, textBox2.Text, textBox3.Text);
                MessageBox.Show("Product Updated Successfully");
            }
            else
            {
                MessageBox.Show("Please fill all fields");
            }
        }
    }
}