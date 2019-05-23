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
    public partial class fSanPham : Form
    {
        PhuongThucChung ptc = new PhuongThucChung();
        public fSanPham()
        {
            InitializeComponent();
        }

        private void fSanPham_Load(object sender, EventArgs e)
        {
            ptc.hienthidatagridview(dgvSanPham, "select * from vSanPham");
            ptc.hienthicombobox(cbxQuanTri, "QuanTriHeThong", "HoTen", "MaQT");
            ptc.hienthicombobox(cbxChungLoai, "ChungLoai", "MoTa", "MaCL");
            ptc.hienthicombobox(cbxXuatXu, "XuatXu", "MoTa", "MaXX");
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
                string s = "insert into SanPham (MaSP,MaQT,TenSP,MaCL,MaXX,Gia) Values ('" 
                    + txtMaSanPham.Text + "','" 
                    + cbxQuanTri.SelectedValue.ToString() + "','" 
                    + txtTenSanPham.Text + "', '" 
                    + cbxChungLoai.SelectedValue.ToString() + "', '" 
                    + cbxXuatXu.SelectedValue.ToString() + "', "
                    +txtGia.Text+")";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvSanPham, "select * from vSanPham");
                else return;
            }
            if (btnSua.Enabled == true)
            {
                string s = "update SanPham set MaQT = '" + cbxQuanTri.SelectedValue.ToString() 
                    + "',TenSP = '" + txtTenSanPham.Text
                    + "',MaCL = '" + cbxChungLoai.SelectedValue.ToString()
                    + "',MaXX = '" + cbxXuatXu.SelectedValue.ToString() 
                    + "',Gia = " + txtGia.Text
                    + " where MaSP = '" + txtMaSanPham.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvSanPham, "select * from vSanPham");
                else return;
            }
            if (btnXoa.Enabled == true)
            {
                if (MessageBox.Show("Xóa bản ghi này?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                string s = "delete SanPham where MaSP = '" + txtMaSanPham.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvSanPham, "select * from vSanPham");
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
            string s = "select * from vSanPham where 1=1";
            if (!string.IsNullOrEmpty(txtTimKiemMa.Text))
                s += " and [Mã Sản Phẩm] = '" + txtTimKiemMa.Text + "'";
            //if (!string.IsNullOrEmpty(txttimten.Text))
            //    s += " and tentheloai like '%" + txttimten.Text + "%'";
            ptc.hienthidatagridview(dgvSanPham, s);
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSanPham.Text = dgvSanPham.CurrentRow.Cells[0].Value.ToString();
            txtTenSanPham.Text = dgvSanPham.CurrentRow.Cells[1].Value.ToString();
            txtGia.Text = dgvSanPham.CurrentRow.Cells[2].Value.ToString();
            cbxQuanTri.SelectedValue = dgvSanPham.CurrentRow.Cells[6].Value;
            cbxChungLoai.SelectedValue = dgvSanPham.CurrentRow.Cells[7].Value;
            cbxXuatXu.SelectedValue = dgvSanPham.CurrentRow.Cells[8].Value;
        }
        // hiện ô thông tin để nhập 
        void hien()
        {
            txtMaSanPham.Enabled = true;
            txtTenSanPham.Enabled = true;
            txtGia.Enabled = true;
            cbxQuanTri.Enabled = true;
            cbxChungLoai.Enabled = true;
            cbxXuatXu.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
        // ẩn các ô thông tin và nút
        void an()
        {
            txtMaSanPham.Enabled = false;
            txtTenSanPham.Enabled = false;
            txtGia.Enabled = false;
            cbxQuanTri.Enabled = false;
            cbxChungLoai.Enabled = false;
            cbxXuatXu.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        // xóa dữ liệu ở ổ thông tin
        void xoa()
        {
            txtTenSanPham.Clear();
            txtGia.Clear();
            txtMaSanPham.Clear();
        }
    }
}
