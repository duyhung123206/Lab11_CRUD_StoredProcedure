using System;
using System.Data;
using Lab11_CRUD_StoredProcedure.DAL;
using Lab11_CRUD_StoredProcedure.DTO;

namespace Lab11_CRUD_StoredProcedure.BUS
{
    public class SinhVienBUS
    {
        private SinhVienDAL dal = new SinhVienDAL();

        public DataTable LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        public void ThemSinhVien(SinhVienDTO sv)
        {
            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                throw new Exception("Mã sinh viên không được để trống!");
            }
            if (string.IsNullOrWhiteSpace(sv.HoTen))
            {
                throw new Exception("Họ tên sinh viên không được để trống!");
            }

            dal.ThemSinhVien(sv);
        }

        public void SuaSinhVien(SinhVienDTO sv)
        {
            if (string.IsNullOrWhiteSpace(sv.MaSV))
            {
                throw new Exception("Mã sinh viên không được để trống!");
            }
            dal.SuaSinhVien(sv);
        }

        public void XoaSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                throw new Exception("Vui lòng chọn sinh viên cần xóa!");
            }
            dal.XoaSinhVien(maSV);
        }
    }
}