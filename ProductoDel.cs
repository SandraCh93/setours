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
    public partial class ProductoDel : Form
    {
        private Form1 form1;
        private ProductosQry productosQry;

        public ProductoDel(Form1 form1, ProductosQry productosQry)
        {
            InitializeComponent();
            this.form1 = form1;
            this.productosQry = productosQry;
        }

        private void ProductoDel_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT IDProducto, NombreProd FROM Producto", form1.cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "IDProducto";
            comboBox1.DisplayMember = "NombreProd";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != -1)
            {
                SqlCommand cm = new SqlCommand();
                cm.Connection = form1.cn;
                cm.CommandText = "DELETE FROM Producto WHERE IDProducto = " + comboBox1.SelectedValue;
                form1.cn.Open();
                cm.ExecuteNonQuery();
                form1.cn.Close();
                MessageBox.Show("Producto Eliminado");
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
                MessageBox.Show("Seleccione producto a eliminar");
            }
        }
    }
}
