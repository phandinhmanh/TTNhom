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
    public partial class fChungLoai : Form
    {
        PhuongThucChung ptc = new PhuongThucChung();
        public fChungLoai()
        {
            InitializeComponent();
        }
        private void fChungLoai_Load(object sender, EventArgs e)
        {
            ptc.hienthidatagridview(dgvChungLoai,"select * from ChungLoai");
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
                string s = "insert into ChungLoai(MaCL,MoTa) Values ('"+txtMaChungLoai.Text+"','"+txtMoTa.Text+"')";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvChungLoai, "select * from ChungLoai");
                else return;
            }
            if (btnSua.Enabled == true)
            {
                string s = "update ChungLoai set MoTa = '" + txtMoTa.Text + "' where MaCL = '" + txtMaChungLoai.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvChungLoai, "select * from ChungLoai");
                else return;
            }
            if (btnXoa.Enabled == true)
            {
                if (MessageBox.Show("Xóa bản ghi này?", "Chú ý", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                string s = "delete ChungLoai where MaCL = '" + txtMaChungLoai.Text + "'";
                if (ptc.thucthisql(s))
                    ptc.hienthidatagridview(dgvChungLoai, "select * from ChungLoai");
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
            string s = "select * from ChungLoai where 1=1";
            if (!string.IsNullOrEmpty(txtTimKiemMa.Text))
                s += " and MaCL = '" + txtTimKiemMa.Text + "'";
            ptc.hienthidatagridview(dgvChungLoai, s);
        }

        private void dgvChungLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaChungLoai.Text = dgvChungLoai.CurrentRow.Cells[0].Value.ToString();
            txtMoTa.Text = dgvChungLoai.CurrentRow.Cells[1].Value.ToString();
        }

        // hiện ô thông tin để nhập 
        void hien()
        {
            txtMaChungLoai.Enabled = true;
            txtMoTa.Enabled = true;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
        }
        // ẩn các ô thông tin và nút
        void an()
        {
            txtMaChungLoai.Enabled = false;
            txtMoTa.Enabled = false;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
        }
        // xóa dữ liệu ở ổ thông tin
        void xoa()
        {
            txtMaChungLoai.Clear();
            txtMoTa.Clear();
        }
    }
}
