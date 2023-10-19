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
    public partial class ProductoUpd : Form
    {
        private Form1 form1;
        private ProductosQry productosQry;
        public ProductoUpd(Form1 form1, ProductosQry productosQry)
        {
            InitializeComponent();
            this.form1 = form1;
            this.productosQry = productosQry;
        }

        private void ProductoUpd_Load(object sender, EventArgs e)
        {
           
            SqlDataAdapter da = new SqlDataAdapter("SELECT IDProducto, NombreProd FROM Producto", form1.cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "IDProducto";
            comboBox1.DisplayMember = "NombreProd";

            MostrarDatos();
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MostrarDatos();
        }

        private void MostrarDatos()
        {
            if(comboBox1.SelectedIndex != -1)
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * " +
                    "FROM Producto WHERE IDProducto = " + comboBox1.SelectedValue, form1.cn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow row = ds.Tables[0].Select().ElementAt(0);
                textBox1.Text = row["NombreProd"].ToString();
                textBox2.Text = row["TipoProd"].ToString();
                textBox3.Text = row["Stock"].ToString();
                textBox4.Text = row["Precio"].ToString();

            }
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
                cm.CommandText = "UPDATE Producto SET " +
                    "NombreProd = '" + nombre + "'," +
                    "TipoProd = '" + tipo + "'," +
                    "Stock = " + stock + "," + 
                    "Precio = " + precio + " " + 
                    "WHERE IDProducto = " + comboBox1.SelectedValue;
                form1.cn.Open();
                cm.ExecuteNonQuery();
                form1.cn.Close();
                MessageBox.Show("Producto Actualizado");
                foreach (Form form in Application.OpenForms)
                {
                    if(form.GetType() == typeof(ProductosQry))
                    {
                        ((ProductosQry)form).Consulta();
                        form.Activate();
                    }
                }

                this.Dispose();
                    
            }
            else
            {
                MessageBox.Show("Seleccione producto para actualizar datos");
            }
        }

        
    }
}
