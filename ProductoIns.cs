using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace setours
{
    public partial class ProductoIns : Form
    {
        private Form1 form1;
        private ProductosQry productosQry;
        public ProductoIns(Form1 form1, ProductosQry productosQry)
        {
            InitializeComponent();
            this.form1 = form1;
            this.productosQry = productosQry;
        }

        private void ProductoIns_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text.Trim();
            string tipo = textBox2.Text.Trim();
            int stock = Convert.ToInt32(textBox3.Text.Trim());
            decimal precio = Convert.ToDecimal(textBox4.Text.Trim());
            if (nombre.Length > 0)
            {
                SqlCommand cm = new SqlCommand();
                cm.Connection = form1.cn;
                cm.CommandText = "INSERT Producto VALUES(" +
                    "'" + nombre + "'," +
                    "'" + tipo + "'," +
                    " " + stock + " ," +
                    " " + precio + " )";
                form1.cn.Open();
                cm.ExecuteNonQuery();
                form1.cn.Close();
                MessageBox.Show("Producto Agregado");
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(ProductosQry))
                    {
                        ((ProductosQry)form).Consulta();
                        form.Activate();
                    }
                }
                this.Dispose();
               
            }
            else
            {
                MessageBox.Show("Ingrese nombre del producto");
            }
        }
    }
}
