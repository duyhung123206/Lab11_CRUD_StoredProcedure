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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells["MaSV"].Value != null)
                {
                    txtMaSV.Text = row.Cells["MaSV"].Value.ToString();
                    txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["NgaySinh"].Value);
                    chkGioiTinh.Checked = Convert.ToBoolean(row.Cells["GioiTinh"].Value);
                    txtMaKhoa.Text = row.Cells["MaKhoa"].Value.ToString();
                    txtDiemTB.Text = row.Cells["DiemTB"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SinhVienDTO dto = new SinhVienDTO
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value.Date,
                    GioiTinh = chkGioiTinh.Checked,
                    MaKhoa = txtMaKhoa.Text.Trim(),
                    DiemTB = float.Parse(string.IsNullOrEmpty(txtDiemTB.Text) ? "0" : txtDiemTB.Text),
                    Email = txtEmail.Text.Trim()
                };

                bus.Add(dto);
                LoadDataToGrid();
                MessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SinhVienDTO sv = new SinhVienDTO
                {
                    MaSV = txtMaSV.Text.Trim(),
                    HoTen = txtHoTen.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value.Date,
                    GioiTinh = chkGioiTinh.Checked,
                    MaKhoa = txtMaKhoa.Text.Trim(),
                    DiemTB = float.Parse(string.IsNullOrEmpty(txtDiemTB.Text) ? "0" : txtDiemTB.Text),
                    Email = txtEmail.Text.Trim()
                };

                bus.SuaSinhVien(sv);
                LoadDataToGrid();
                MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = txtMaSV.Text.Trim();
                DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên {maSV} không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    bus.XoaSinhVien(maSV);
                    LoadDataToGrid();
                    MessageBox.Show("Xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}