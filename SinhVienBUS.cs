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
            return _dal.LayDanhSach();
        }

        // Phương thức Add kiểm tra nghiệp vụ chuẩn theo đề bài Lab
        public void Add(SinhVienDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MaSV))
            {
                throw new ArgumentException("Mã sinh viên không được rỗng."); 
            }
            if (string.IsNullOrWhiteSpace(dto.HoTen))
            {
                throw new ArgumentException("Họ tên không được rỗng."); 
            }
            if (dto.NgaySinh > DateTime.Today.AddYears(-15))
            {
                throw new ArgumentException("Tuổi sinh viên chưa hợp lệ."); 
            }
            if (dto.DiemTB < 0 || dto.DiemTB > 10)
            {
                throw new ArgumentException("Điểm phải thuộc [0,10]."); 
            }

            _dal.Insert(dto); 
        }

        public void SuaSinhVien(SinhVienDTO sv)
        {
            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                throw new ArgumentException("Mã sinh viên không được rỗng.");
            }
            if (sv.DiemTB < 0 || sv.DiemTB > 10)
            {
                throw new ArgumentException("Điểm phải thuộc [0,10]."); 
            }
            _dal.SuaSinhVien(sv);
        }

        public void XoaSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                throw new ArgumentException("Vui lòng chọn sinh viên cần xóa!");
            }
            _dal.XoaSinhVien(maSV);
        }
    }
}