﻿using System;
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
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if(txtMatKhau.Text != "admin")
            {
                MessageBox.Show("Sai khẩu");
                return;
            }
            fHoaDon f = new fHoaDon();
            f.Show();
            this.Hide();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fDangNhap_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnDangNhap;
        }

    }
}
