using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyGiayDep
{
    public partial class fXuatXu : Form
    {
        PhuongThucChung ptc = new PhuongThucChung();
        public fXuatXu()
        {
            InitializeComponent();
        }

        private void fXuatXu_Load(object sender, EventArgs e)
        {
            ptc.hienthidatagridview(dgvXuatXu, "select * from XuatXu");
            an();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            hien();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
            hien();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnThem.Enabled = false;
            hien();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == true)
            {
                string s = "insert into XuatXu(MaXX,MoTa) Values ('" + txtMaXuatXu.Text + "','" + txtMoTa.Text + "')";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvXuatXu, "select * from XuatXu");
                else return;
            }
            if (btnSua.Enabled == true)
            {
                string s = "update XuatXu set MoTa = '" + txtMoTa.Text + "' where MaXX = '" + txtMaXuatXu.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvXuatXu, "select * from XuatXu");
                else return;
            }
            if (btnXoa.Enabled == true)
            {
                if (MessageBox.Show("Xóa bản ghi này?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                string s = "delete XuatXu where MaXX = '" + txtMaXuatXu.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvXuatXu, "select * from XuatXu");
                else return;
            }
            an();
            xoa();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            an();
            xoa();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
        }

        private void txtTimKiemMa_TextChanged(object sender, EventArgs e)
        {
            string s = "select * from XuatXu where 1=1";
            if (!string.IsNullOrEmpty(txtTimKiemMa.Text))
                s += " and MaXX = '" + txtTimKiemMa.Text + "'";
            ptc.hienthidatagridview(dgvXuatXu, s);
        }

        private void dgvXuatXu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaXuatXu.Text = dgvXuatXu.CurrentRow.Cells[0].Value.ToString();
            txtMoTa.Text = dgvXuatXu.CurrentRow.Cells[1].Value.ToString();
        }

        // hiện ô thông tin để nhập 
        void hien()
        {
            txtMaXuatXu.Enabled = true;
            txtMoTa.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
        // ẩn các ô thông tin và nút
        void an()
        {
            txtMaXuatXu.Enabled = false;
            txtMoTa.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        // xóa dữ liệu ở ổ thông tin
        void xoa()
        {
            txtMaXuatXu.Clear();
            txtMoTa.Clear();
        }
    }
}
