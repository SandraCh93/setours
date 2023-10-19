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
    public partial class RegistroVenta : Form
    {
        private Form1 form1;
        public RegistroVenta(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        public void RegistroVenta_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT IDProducto, NombreProd FROM Producto", form1.cn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            comboBox1.DataSource = ds1.Tables[0];
            comboBox1.ValueMember = "IDProducto";
            comboBox1.DisplayMember = "NombreProd";

            SqlDataAdapter da2 = new SqlDataAdapter("SELECT IDCliente, NombreCliente FROM Cliente", form1.cn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);
            comboBox2.DataSource = ds2.Tables[0];
            comboBox2.ValueMember = "IDCliente";
            comboBox2.DisplayMember = "NombreCliente";

            SqlDataAdapter da3 = new SqlDataAdapter("SELECT IDTipoDoc, Descripcion FROM TipoDoc", form1.cn);
            DataSet ds3 = new DataSet();
            da3.Fill(ds3);
            comboBox4.DataSource = ds3.Tables[0];
            comboBox4.ValueMember = "IDTipoDoc";
            comboBox4.DisplayMember = "Descripcion";

            SqlDataAdapter da4 = new SqlDataAdapter("SELECT IDVendedor, NombreVendedor FROM Vendedor", form1.cn);
            DataSet ds4 = new DataSet();
            da4.Fill(ds4);
            comboBox3.DataSource = ds4.Tables[0];
            comboBox3.ValueMember = "IDVendedor";
            comboBox3.DisplayMember = "NombreVendedor";

            Consulta();
        }

        public void Consulta()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT P.NombreProd, CP.Cantidad, (CP.Cantidad*P.Precio) AS PrecioBruto, (CP.Cantidad*P.Precio)*0.18 AS IGV, ((CP.Cantidad*P.Precio)*1.18) AS TotalVenta FROM Comprobante CP JOIN Producto P ON CP.IDProducto = P.IDProducto GROUP BY CP.IDVenta, P.NombreProd, P.Precio, CP.Cantidad", form1.cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dab = new SqlDataAdapter("SELECT Precio " +
                "FROM Producto WHERE IDProducto = " + comboBox1.SelectedValue, form1.cn);
            DataSet dsb= new DataSet();
            dab.Fill(dsb);
            decimal precio = Convert.ToDecimal(dsb.Tables[0].Rows[0]["Precio"]);

            SqlDataAdapter dab1 = new SqlDataAdapter("SELECT IDSucursal " +
                "FROM Vendedor WHERE IDVendedor= " + comboBox3.SelectedValue, form1.cn);
            DataSet dsb1 = new DataSet();
            dab1.Fill(dsb1);
            int sucursal = Convert.ToInt32(dsb1.Tables[0].Rows[0]["IDSucursal"]);

            int producto = Convert.ToInt32(comboBox1.SelectedValue);
            int cantidad = Convert.ToInt32(textBox1.Text.Trim());
            int cliente = Convert.ToInt32(comboBox2.SelectedValue);
            int tipoDoc = Convert.ToInt32(comboBox4.SelectedValue);
            int vendedor = Convert.ToInt32(comboBox3.SelectedValue);
            int venta = 2;

            if (producto > 0)
            {
                SqlCommand cm = new SqlCommand();
                cm.Connection = form1.cn;

                cm.CommandText = "INSERT Comprobante(IDProducto, IDTipoDoc, IDCliente, IDVendedor, IDSucursal, Precio, Cantidad,IDVenta) VALUES(" +
                    producto + ", " +
                    tipoDoc + ", " +
                    cliente + ", " +
                    vendedor + ", " +
                    sucursal + ", " +
                    precio + ", " +
                    cantidad + ", " +
                    venta + ")";

                form1.cn.Open();
                cm.ExecuteNonQuery();
                form1.cn.Close();
                MessageBox.Show("Producto Agregado");
                foreach (Form form in Application.OpenForms)
                {
                    if(form.GetType() == typeof(RegistroVenta))
                    {
                        ((RegistroVenta)form).Consulta();
                        form.Activate();
                    }
                }
                

            }
            else
            {
                MessageBox.Show("Seleccione producto a agregar");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

        }
    }
}
