using System;
using System.Data;
using Lab11_CRUD_StoredProcedure.DAL;
using Lab11_CRUD_StoredProcedure.DTO;

namespace Lab11_CRUD_StoredProcedure.BUS
{
    public class SinhVienBUS
    {
        private SinhVienDAL _dal = new SinhVienDAL();

        public DataTable LayDanhSach()
        {
            // Lọc chỉ lấy danh sách sinh viên đang hoạt động (IsActive = true)
            DataTable dt = _dal.LayDanhSach();
            DataTable dtActive = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row["IsActive"]))
                {
                    dtActive.ImportRow(row);
                }
            }
            return dtActive;
        }

        public void Add(SinhVienDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MaSV))
                throw new ArgumentException("Mã sinh viên không được rỗng."); 
            if (string.IsNullOrWhiteSpace(dto.HoTen))
                throw new ArgumentException("Họ tên không được rỗng.");
            if (dto.NgaySinh > DateTime.Today.AddYears(-15))
                throw new ArgumentException("Tuổi sinh viên chưa hợp lệ (phải từ 15 tuổi trở lên)."); 
            if (dto.DiemTB < 0 || dto.DiemTB > 10)
                throw new ArgumentException("Điểm phải thuộc [0,10]."); 

            _dal.Insert(dto); 
        }

        public void SuaSinhVien(SinhVienDTO sv)
        {
            if (string.IsNullOrWhiteSpace(sv.MaSV))
                throw new ArgumentException("Mã sinh viên không được rỗng.");
            if (sv.DiemTB < 0 || sv.DiemTB > 10)
                throw new ArgumentException("Điểm phải thuộc [0,10]."); 

            _dal.SuaSinhVien(sv);
        }

        public void XoaSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                throw new ArgumentException("Vui lòng chọn sinh viên cần xóa!");
            _dal.XoaSinhVien(maSV);
        }

        public DataTable TimKiem(string keyword, string maKhoa)
        {
            return _dal.TimKiem(keyword, maKhoa);
        }

        public void XoaKhoa(string maKhoa)
        {
            if (_dal.KiemTraKhoaCoSinhVien(maKhoa))
            {
                throw new Exception("Không thể xóa khoa này vì vẫn đang có sinh viên thuộc về khoa!"); 
            }
            // Logic xóa khoa nếu có bảng khoa riêng...
        }
    }
}