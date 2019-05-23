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
    public partial class fHoaDon : Form
    {
        PhuongThucChung ptc = new PhuongThucChung();
        public fHoaDon()
        {
            InitializeComponent();
        }

        private void fHoaDon_Load(object sender, EventArgs e)
        {
            ptc.hienthidatagridview(dgvHoaDon,"select * from vHoaDon");
            ptc.hienthicombobox(cbxQuanTri, "QuanTriHeThong", "HoTen", "MaQT");
            an();
        }
        private void btnHuy_Click(object sender, EventArgs e)
        {
            an();
            xoa();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(btnThem.Enabled == true)
            {
                string s = "insert into HoaDon(MaHD,MaQT,NgayLap) Values ('"+txtMaHoaDon.Text+"','"+cbxQuanTri.SelectedValue.ToString()+"','"+txtNgayLap.Text+"')";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvHoaDon, "select * from vHoaDon");
                else return;
            }
            if (btnSua.Enabled == true)
            {
                string s = "update HoaDon set MaQT = '" + cbxQuanTri.SelectedValue.ToString() + "',NgayLap = '" + txtNgayLap.Text + "' where MaHD = '" + txtMaHoaDon.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvHoaDon, "select * from vHoaDon");
                else return;
            }
            if (btnXoa.Enabled == true)
            {
                if (MessageBox.Show("Xóa bản ghi này?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                string s = "delete HoaDon where MaHD = '" + txtMaHoaDon.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvHoaDon, "select * from vHoaDon");
                else return;
            }
            an();
            xoa();
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            txtNgayLap.Text = DateTime.Now.ToString();
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
        // hiện ô thông tin để nhập 
        void hien()
        {
            txtMaHoaDon.Enabled = true;
            txtNgayLap.Enabled = true;
            cbxQuanTri.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
        // ẩn các ô thông tin và nút
        void an()
        {
            txtMaHoaDon.Enabled = false;
            txtNgayLap.Enabled = false;
            cbxQuanTri.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        // xóa dữ liệu ở ổ thông tin
        void xoa()
        {
            txtNgayLap.Clear();
            txtMaHoaDon.Clear();
        }
        // ấn vào datagridview đổ thong tin vào các ô thông tin
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHoaDon.Text = dgvHoaDon.CurrentRow.Cells[0].Value.ToString();
            cbxQuanTri.SelectedValue = dgvHoaDon.CurrentRow.Cells[3].Value;
            txtNgayLap.Text = dgvHoaDon.CurrentRow.Cells[2].Value.ToString();
        }
        // tìm kiếm mỗi khi nhập vào ô tìm kiếm
        private void txtTimKiemMa_TextChanged(object sender, EventArgs e)
        {
            string s = "select * from vHoaDon where 1=1";
            if (!string.IsNullOrEmpty(txtTimKiemMa.Text))
                s += " and [Mã Hóa Đơn] = '" + txtTimKiemMa.Text+"'";
            //if (!string.IsNullOrEmpty(txttimten.Text))
            //    s += " and tentheloai like '%" + txttimten.Text + "%'";

            ptc.hienthidatagridview(dgvHoaDon, s);
        }

        private void chủngLoạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fChungLoai f = new fChungLoai();
            f.ShowDialog();
        }

        private void xuấtXứToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fXuatXu f = new fXuatXu();
            f.ShowDialog();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fSanPham f = new fSanPham();
            f.ShowDialog();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fNhanVien f = new fNhanVien();
            f.ShowDialog();
        }

        private void btnChiTietHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHoaDon.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn");
                return;
            }
            fHoaDonSanPham f = new fHoaDonSanPham(txtMaHoaDon.Text);
            f.ShowDialog();
        }

        private void fHoaDon_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void danhSáchSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frptSanPham f = new frptSanPham();
            f.Show();
        }

    }
}
