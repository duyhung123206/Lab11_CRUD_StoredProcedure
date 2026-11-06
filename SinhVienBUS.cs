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

        public DataTable GetByDepartment(string maKhoa)
        {
            DataTable dt = _dal.LayDanhSach();
            DataTable dtResult = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row["IsActive"]) && row["MaKhoa"].ToString().Equals(maKhoa, StringComparison.OrdinalIgnoreCase))
                {
                    dtResult.ImportRow(row);
                }
            }
            return dtResult;
        }

        // Thay đổi tên '_dal.Them(sv)' bên dưới thành tên hàm thực tế trong SinhVienDAL.cs của bạn (vd: Insert, ThemMoi,...)
        public void Add(SinhVienDTO sv)
        {
            _dal.Insert(sv); // Hoặc _dal.ThemMoi(sv); tùy theo DAL của bạn
        }

        public void SuaSinhVien(SinhVienDTO sv)
        {
            _dal.SuaSinhVien(sv);
        }

        public void XoaSinhVien(string maSV)
        {
            _dal.XoaSinhVien(maSV);
        }

        public DataTable TimKiem(string keyword)
        {
            return _dal.TimKiem(keyword, "");
        }

        public DataTable TimKiem(string keyword, string maKhoa)
        {
            return _dal.TimKiem(keyword, maKhoa);
        }
    }
}