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

namespace QuanLyGiayDep
{
    public partial class frptSanPham : Form
    {
        PhuongThucChung ptc = new PhuongThucChung();
        public frptSanPham()
        {
            InitializeComponent();
        }

        private void frptSanPham_Load(object sender, EventArgs e)
        {
            if (ptc.ketnoicsdl() == false)
                return;
            SqlDataAdapter da = new SqlDataAdapter("Select * from vBaoCaoSanPham", ptc.ketnoi);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rptSanPham rpt = new rptSanPham();
            rpt.SetDataSource(dt);

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
