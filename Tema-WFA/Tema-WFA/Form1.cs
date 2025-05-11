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
    public partial class Form1 : Form
    {
        internal Tema_WFA.ServiceReference1.WebService1SoapClient service = new Tema_WFA.ServiceReference1.WebService1SoapClient();
        public Form1()
        {
            InitializeComponent();
            
            List<string> product = new List<string>();
            product = service.GetProductIds();

            foreach (string item in product)
            {
                listBox_Items.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox_Items.SelectedItem != null)
            {
                Form2 form2 = new Form2(this, listBox_Items.Text);
                form2.Show();
            }
            else
            {
                MessageBox.Show("Select a Product to Update");
            }

        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (textBox_PID.Text != "" && textBox_Name.Text != "" && textBox_Price.Text != "" && textBox_QTY.Text != "")
            {
                List<string> x = new List<string>();
                x = service.GetProductIds();

                if (!x.Contains(textBox_PID.Text))
                {
                    service.AddProduct(textBox_PID.Text, textBox_Name.Text, textBox_Price.Text, textBox_QTY.Text);
                    MessageBox.Show("Product Added Successfully");

                    listBox_Items.Items.Clear();
                    x = service.GetProductIds();
                    foreach (string item in x)
                    {
                        listBox_Items.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("ID Already Existent");
                }
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (listBox_Items.SelectedItem != null)
            {
                string selectedItem = listBox_Items.SelectedItem.ToString();
                service.DeleteProduct(selectedItem);
                MessageBox.Show("Product Deleted Successfully");
                listBox_Items.Items.Clear();
                List<string> x = service.GetProductIds();
                foreach (string item in x)
                {
                    listBox_Items.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Select a Product to Delete");
            }
        }


    }
}
