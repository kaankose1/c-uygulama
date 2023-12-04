
using System.Data;
using System.Data.SqlClient;

namespace nwap3010
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlCommand cmdtedarik;

        string constr = "Data Source=.;Initial Catalog=NORTHWND;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(constr);

            con.Open();
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = $"insert into Products(ProductName,SupplierID,CategoryID,UnitPrice) " +
                $"values('{txturunad.Text.ToString()}',{cmbtedarik.SelectedValue},{cmbkategori.SelectedValue},{nupbirimfiyat.Value})";
            cmd.ExecuteNonQuery();
            con.Close();
            tazele();

        }


        private void tazele()
        {
            con = new SqlConnection(constr);
            con.Open();

            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from Products order by " +
                "ProductID desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(constr);
            con.Open();


            //Kategori bilgileri cmbkategori combosuna aktarýlýyor
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select CategoryID,CategoryName from Categories";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            cmbkategori.ValueMember = "CategoryID";
            cmbkategori.DisplayMember = "CategoryName";
            cmbkategori.DataSource = dt;


            //Tedarikçiler bilgileri cmbtedarik combosuna aktarýlýyor

            cmdtedarik = new SqlCommand();
            cmdtedarik.Connection = con;
            cmdtedarik.CommandText = "select SupplierID,CompanyName from Suppliers";
            cmdtedarik.ExecuteNonQuery();
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmdtedarik);
            da2.Fill(dt2);
            cmbtedarik.ValueMember = "SupplierID";
            cmbtedarik.DisplayMember = "CompanyName";
            cmbtedarik.DataSource = dt2;

            con.Close();





        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int productID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ProductID"].Value);

                con = new SqlConnection(constr);

                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"DELETE FROM Products WHERE ProductID = {productID}";
                cmd.ExecuteNonQuery();
                con.Close();

                tazele();
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediðiniz ürünü seçin.");
            }
        }

        private void btnguncel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                int productID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ProductID"].Value);

                con = new SqlConnection(constr);

                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"UPDATE Products SET ProductName = '{txturunad.Text}', " +
                                  $"SupplierID = {cmbtedarik.SelectedValue}, " +
                                  $"CategoryID = {cmbkategori.SelectedValue}, " +
                                  $"UnitPrice = {nupbirimfiyat.Value} " +
                                  $"WHERE ProductID = {productID}";
                cmd.ExecuteNonQuery();
                con.Close();

                tazele();
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediðiniz ürünü seçin.");
            }
        }

        private void btnbul_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txturunad.Text))
            {
                con = new SqlConnection(constr);
                con.Open();

                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"SELECT * FROM Products WHERE ProductName LIKE '%{txturunad.Text}%'";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir ürün adý girin.");
            }
        }
    }
}