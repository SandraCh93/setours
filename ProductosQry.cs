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
    public partial class ProductosQry : Form
    {
        private Form1 form1;
        public ProductosQry(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        private void ProductosQry_Load(object sender, EventArgs e)
        {
            Consulta();
        }

        public void Consulta()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Producto", form1.cn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    }
}
