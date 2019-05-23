using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGiayDep
{
    class PhuongThucChung
    {
        public SqlConnection ketnoi = new SqlConnection();
        public string diachiketnoi = @"Data Source=QUOC;Initial Catalog=QUANLYGIAYDEP;Integrated Security=True;";
        public bool ketnoicsdl()
        {
            try
            {
                if (ketnoi.State == ConnectionState.Open)
                    ketnoi.Close();
                ketnoi.ConnectionString = diachiketnoi;
                ketnoi.Open();
            }
            catch
            {
                MessageBox.Show("Kết nối với cơ sở dữ liệu bị lỗi");
                return false;
            }
            return true;
        }
        public void hienthidatagridview(DataGridView dgv, string s)
        {

            if (!ketnoicsdl())
                return;

            SqlDataAdapter da = new SqlDataAdapter(s, ketnoi);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv.DataSource = dt;
            dgv.AutoResizeColumns();
            dgv.ReadOnly = true;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < dgv.Columns.Count; i++)
                dgv.Columns[i].Width += 30;
            dt.Dispose();
            da.Dispose();
        }
        public bool thucthisql(string s)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(s, ketnoi);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                MessageBox.Show("Thành công.");
                return true;
            }
            catch { 
                MessageBox.Show("Thực thi bị lỗi, vui lòng kiểm tra lại dữ liệu.");
                return false;
            }
        }
        public void hienthicombobox(ComboBox cbx, string bang, string ten, string ma)
        {

            if (!ketnoicsdl())
                return;

            SqlDataAdapter da = new SqlDataAdapter("Select * from " + bang, ketnoi);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbx.DataSource = dt;
            cbx.DisplayMember = ten;
            cbx.ValueMember = ma;


            AutoCompleteStringCollection AutoItem = new AutoCompleteStringCollection();
            foreach (DataRow rw in dt.Rows)
            {
                AutoItem.Add(rw[ten].ToString());
            }
            cbx.AutoCompleteMode = AutoCompleteMode.Suggest;
            cbx.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbx.AutoCompleteCustomSource = AutoItem;

        }
    }
}
