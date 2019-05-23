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
    public partial class fNhanVien : Form
    {
        PhuongThucChung ptc = new PhuongThucChung();
        public fNhanVien()
        {
            InitializeComponent();
        }

        private void fNhanVien_Load(object sender, EventArgs e)
        {
            ptc.hienthidatagridview(dgvQuanTri, "select * from QuanTriHeThong");
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
            string strNgS = cbxThang.Text + "/" + cbxNgay.Text + "/" + cbxNam.Text;
            if (btnThem.Enabled == true)
            {
                
                string s = "insert into QuanTriHeThong(MaQT,HoTen,NgaySinh,SoDienThoai) Values ('" 
                    + txtMaQuanTri.Text + "','"
                    + txtTenQuanTri.Text + "','"
                    + strNgS+ "','"
                    + txtSoDienThoai.Text + "')";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvQuanTri, "select * from QuanTriHeThong");
                else return;
            }
            if (btnSua.Enabled == true)
            {
                string s = "update QuanTriHeThong set HoTen = '" 
                    + txtTenQuanTri.Text 
                    +"', NgaySinh = '"+ strNgS 
                    + "', SoDienThoai ='" + txtSoDienThoai.Text
                    + "' where MaQT = '" + txtMaQuanTri.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvQuanTri, "select * from QuanTriHeThong");
                else return;
            }
            if (btnXoa.Enabled == true)
            {
                if (MessageBox.Show("Xóa bản ghi này?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                string s = "delete QuanTriHeThong where MaQT = '" + txtMaQuanTri.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvQuanTri, "select * from QuanTriHeThong");
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
            string s = "select * from QuanTriHeThong where 1=1";
            if (!string.IsNullOrEmpty(txtTimKiemMa.Text))
                s += " and MaQT = '" + txtTimKiemMa.Text + "'";
            if (!string.IsNullOrEmpty(txtTimKiemTen.Text))
                s += " and HoTen like '%" + txtTimKiemTen.Text + "%'";
            ptc.hienthidatagridview(dgvQuanTri, s);
        }
        private void dgvQuanTri_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Parse(dgvQuanTri.CurrentRow.Cells[2].Value.ToString());
            cbxNgay.Text = dt.Day.ToString();
            cbxThang.Text = dt.Month.ToString();
            cbxNam.Text = dt.Year.ToString();
            txtMaQuanTri.Text = dgvQuanTri.CurrentRow.Cells[0].Value.ToString();
            txtTenQuanTri.Text = dgvQuanTri.CurrentRow.Cells[1].Value.ToString();
            txtSoDienThoai.Text = dgvQuanTri.CurrentRow.Cells[3].Value.ToString();
        }
        // hiện ô thông tin để nhập 
        void hien()
        {
            txtMaQuanTri.Enabled = true;
            txtTenQuanTri.Enabled = true;
            txtSoDienThoai.Enabled = true;
            cbxNgay.Enabled = true;
            cbxThang.Enabled = true;
            cbxNam.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
        // ẩn các ô thông tin và nút
        void an()
        {
            txtMaQuanTri.Enabled = false;
            txtTenQuanTri.Enabled = false;
            txtSoDienThoai.Enabled = false;
            cbxNgay.Enabled = false;
            cbxThang.Enabled = false;
            cbxNam.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        // xóa dữ liệu ở ổ thông tin
        void xoa()
        {
            txtMaQuanTri.Clear();
            txtTenQuanTri.Clear();
            txtSoDienThoai.Clear();
            cbxNgay.ResetText();
            cbxThang.ResetText();
            cbxNam.ResetText();
        }

        
    }
}
