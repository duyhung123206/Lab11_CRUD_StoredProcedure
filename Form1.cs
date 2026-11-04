using System;
using System.Windows.Forms;
using Lab11_CRUD_StoredProcedure.BUS;
using Lab11_CRUD_StoredProcedure.DTO;

namespace Lab11_CRUD_StoredProcedure
{
    public partial class Form1 : Form
    {
        private SinhVienBUS bus = new SinhVienBUS();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            try
            {
                dataGridView1.DataSource = bus.LayDanhSach();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SinhVienDTO sv = new SinhVienDTO
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    GioiTinh = chkGioiTinh.Checked,
                    MaKhoa = txtMaKhoa.Text.Trim(),
                    DiemTB = float.Parse(txtDiemTB.Text),
                    Email = txtEmail.Text.Trim()
                };

                bus.ThemSinhVien(sv);
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}