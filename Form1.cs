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
    public partial class Form1 : Form
    {
        public SqlConnection cn;
        public ProductosQry productosQry;
        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection("Data Source=(local);Initial Catalog=SETOURS;Integrated Security=SSPI;");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listaDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if(form.GetType() == typeof(ProductosQry))
                {
                    form.Activate();
                    return;
                }
            }
            productosQry = new ProductosQry(this);
            productosQry.MdiParent = this;
            productosQry.Show();
        }

        private void agregarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductoIns productoIns = new ProductoIns(this, productosQry);
            productoIns.MdiParent = this;
            productoIns.Show();
        }

        private void modificarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductoUpd productoUpd = new ProductoUpd(this, productosQry);
            productoUpd.MdiParent = this;
            productoUpd.Show();
        }

        private void eliminarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductoDel productoDel = new ProductoDel(this, productosQry);
            productoDel.MdiParent = this;
            productoDel.Show();
        }

        private void realizarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(RegistroVenta))
                {
                    form.Activate();
                    return;
                }
            }
            RegistroVenta registroVenta = new RegistroVenta(this);
            registroVenta.MdiParent = this;
            registroVenta.Show();
        }
    }
}
