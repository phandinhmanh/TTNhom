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
    public partial class frptHoaDon : Form
    {
        PhuongThucChung ptc = new PhuongThucChung();
        public static string s = "";
        public frptHoaDon(string ma)
        {
            InitializeComponent();
            s = "select * from vBaoCaoHoaDon where [Mã Hóa Đơn] = '"+ma+"'";
        }

        private void frptHoaDon_Load(object sender, EventArgs e)
        {
            if (ptc.ketnoicsdl() == false)
                return;
            SqlDataAdapter da = new SqlDataAdapter(s, ptc.ketnoi);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rptHoaDon rpt = new rptHoaDon();
            rpt.SetDataSource(dt);

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
        }
    }
}
