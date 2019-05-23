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
    public partial class fHoaDonSanPham : Form
    {
        PhuongThucChung ptc = new PhuongThucChung();
        public static string maHD ="";
        public fHoaDonSanPham(string ma)
        {
            InitializeComponent();
            maHD = ma;
        }

        private void fHoaDonSanPham_Load(object sender, EventArgs e)
        {
            ptc.hienthidatagridview(dgvHoaDonSanPham, "select * from vHoaDonSanPham where [Mã Hóa Đơn] ='" + maHD + "'");
            ptc.hienthicombobox(cbxSanPham, "SanPham", "TenSP", "MaSP");
            lblMaHoaDon.Text = maHD;
            an();
            tongtien();
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
            //cbxSanPham.Enabled = false;
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
                string s = "insert into HD_SP(MaHD,MaSP,SoLuong,Gia) Values ('"
                    + maHD + "','" 
                    + cbxSanPham.SelectedValue.ToString() + "','"
                    + txtSoLuong.Text + "','"
                    + txtGia.Text + "')";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvHoaDonSanPham, "select * from vHoaDonSanPham where [Mã Hóa Đơn] ='" + maHD + "'");
                else return;
            }
            if (btnSua.Enabled == true)
            {
                string s = "update HD_SP set MaSP = '" 
                    + cbxSanPham.SelectedValue.ToString() 
                    + "',SoLuong = '" + txtSoLuong.Text
                    + "',Gia = '" + txtGia.Text + "' where MaHD = '" + maHD + "' and MaSP = '" + dgvHoaDonSanPham.CurrentRow.Cells[1].Value.ToString() + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvHoaDonSanPham, "select * from vHoaDonSanPham where [Mã Hóa Đơn] ='" + maHD + "'");
                else return;
            }
            if (btnXoa.Enabled == true)
            {
                if (MessageBox.Show("Xóa bản ghi này?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                string s = "delete HD_SP where MaHD = '" + maHD + "' and MaSP = '" + cbxSanPham.SelectedValue.ToString() + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvHoaDonSanPham, "select * from vHoaDonSanPham where [Mã Hóa Đơn] ='" + maHD + "'");
                else return;
            }
            an();
            xoa();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            tongtien();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            an();
            xoa();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
        }

        private void txtTimKiemMa_TextChanged(object sender, EventArgs e)
        {
            string s = "select * from vHoaDonSanPham where 1=1";
            if (!string.IsNullOrEmpty(txtTimKiemMa.Text))
                s += " and [Mã Sản Phẩm] = '" + txtTimKiemMa.Text + "'";
            //if (!string.IsNullOrEmpty(txttimten.Text))
            //    s += " and tentheloai like '%" + txttimten.Text + "%'";

            ptc.hienthidatagridview(dgvHoaDonSanPham, s);
        }
        private void dgvHoaDonSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cbxSanPham.SelectedValue = dgvHoaDonSanPham.CurrentRow.Cells[1].Value;
            txtSoLuong.Text = dgvHoaDonSanPham.CurrentRow.Cells[3].Value.ToString();
            txtGia.Text = dgvHoaDonSanPham.CurrentRow.Cells[4].Value.ToString();
        }
        // hiện ô thông tin để nhập 
        void hien()
        {
            txtGia.Enabled = true;
            txtSoLuong.Enabled = true;
            txtSoLuong.Enabled = true;
            cbxSanPham.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
        // ẩn các ô thông tin và nút
        void an()
        {
            txtSoLuong.Enabled = false;
            txtGia.Enabled = false;
            txtSoLuong.Enabled = false;
            cbxSanPham.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        // xóa dữ liệu ở ổ thông tin
        void xoa()
        {
            txtSoLuong.Clear();
            txtGia.Clear();
        }
        // tính tổng tiền
        void tongtien()
        {
            float tongTien = 0;
            for (int i = 0; i < dgvHoaDonSanPham.Rows.Count; i++)
            {
                tongTien += float.Parse(dgvHoaDonSanPham.Rows[i].Cells["Tổng Tiền"].Value.ToString());
            }
            lblTongTien.Text = tongTien.ToString();
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            frptHoaDon f = new frptHoaDon(maHD);
            f.Show();
        }

    }
}
