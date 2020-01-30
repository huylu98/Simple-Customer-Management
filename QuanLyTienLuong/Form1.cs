using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTienLuong
{
    public partial class frmQuanLyTienLuong : Form
    {
        static float luongNV = 150000;
        static float luongQL = 200000;
        public frmQuanLyTienLuong()
        {
            InitializeComponent();
        }

        class NhanVien
        {
            private string maNV { get; set; }
            private string hoTen { get; set; }
            private string chucVu { get; set; }
            private int soNgayCong { get; set; }
            private float tongTien { get; set; }

            public NhanVien(string maNV, string hoTen, string chucVu, int soNgayCong, float tongTien)
            {
                this.maNV = maNV;
                this.hoTen = hoTen;
                this.chucVu = chucVu;
                this.soNgayCong = soNgayCong;
                this.tongTien = tongTien;
            }

            public NhanVien()
            {
                this.maNV = "";
                this.hoTen = "";
                this.chucVu = "";
                this.soNgayCong = 1;
                this.tongTien = 0;
            }

        }

        private void frmQuanLyTienLuong_Load(object sender, EventArgs e)
        {
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtNgayCong.Clear();
            txtTien.Clear();
            txtMaSV.Focus();
            btnLuu.Enabled = true;
            
        }

        private void rdoCN_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void rdoQL_CheckedChanged(object sender, EventArgs e)
        {
            calculate();
        }

        private void calculate()
        {
            if (!txtNgayCong.Text.Equals(""))
            {
                if (rdoCN.Checked == true)
                {
                    txtTien.Text = (int.Parse(txtNgayCong.Text) * luongNV).ToString("#,###");
                }
                if (rdoQL.Checked == true)
                {
                    txtTien.Text = (int.Parse(txtNgayCong.Text) * luongQL).ToString("#,###");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaSV.Text.Trim() == "" || txtHoTen.Text.Trim() == "" || txtNgayCong.Text.Trim()=="")
            {
                MessageBox.Show("Chưa nhâp đủ thông tin", "Thông báo");
            }
            else
            {
                if (rdoCN.Checked)
                {
                    string[] row = { txtMaSV.Text, txtHoTen.Text, rdoCN.Text, txtNgayCong.Text, txtTien.Text };
                    dgvDanhSach.Rows.Add(row);

                }
                if (rdoQL.Checked)
                {
                    string[] row = { txtMaSV.Text, txtHoTen.Text, rdoQL.Text, txtNgayCong.Text, txtTien.Text };
                    dgvDanhSach.Rows.Add(row);
                }

                btnLuu.Enabled = false;

            }
        }

        private void txtNgayCong_TextChanged(object sender, EventArgs e)
        {
            short num;
            if (Int16.TryParse(txtNgayCong.Text, out num))
            {
                if (num >= 0 && num <=365)
                {
                    calculate();
                }
                else if (num > 365)
                {
                    txtNgayCong.TextChanged -= txtNgayCong_TextChanged;
                    MessageBox.Show("Số ngày vượt quá 365 ngày");
                    txtNgayCong.Text = "";
                    txtNgayCong.TextChanged += txtNgayCong_TextChanged;
                }
                else
                {
                    txtNgayCong.TextChanged -= txtNgayCong_TextChanged;
                    MessageBox.Show("Số ngày không hợp lệ");
                    txtNgayCong.Text = "";
                    txtNgayCong.TextChanged += txtNgayCong_TextChanged;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            dgvDanhSach.Rows.RemoveAt(dgvDanhSach.SelectedRows[0].Index);
        }


        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                DataGridViewRow row = dgvDanhSach.Rows[index];
                txtMaSV.Text = row.Cells["MSNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtNgayCong.Text = row.Cells["NgayCong"].Value.ToString();
                txtTien.Text = row.Cells["SoTien"].Value.ToString();
                if (row.Cells["ChucVu"].Value.Equals(rdoQL.Text))
                {
                    rdoQL.Checked = true; 
                }
                else
                {
                    rdoCN.Checked = true;
                }
                if (row.Cells["ChucVu"].Value.ToString().Equals(rdoCN.Text))
                {
                    rdoCN.Checked = true;
                }
                else
                {
                    rdoQL.Checked = true;
                }
            }
        }
    }
}
