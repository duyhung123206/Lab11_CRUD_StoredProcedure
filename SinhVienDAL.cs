using System;
using System.Data;
using System.IO;
using Lab11_CRUD_StoredProcedure.DTO;

namespace Lab11_CRUD_StoredProcedure.DAL
{
    public class SinhVienDAL
    {
        private string filePath = "DataSinhVien.xml";

        public DataTable LayDanhSach()
        {
            DataTable dt = new DataTable("SinhVien");
            dt.Columns.Add("MaSV", typeof(string));
            dt.Columns.Add("HoTen", typeof(string));
            dt.Columns.Add("NgaySinh", typeof(DateTime));
            dt.Columns.Add("GioiTinh", typeof(bool));
            dt.Columns.Add("MaKhoa", typeof(string));
            dt.Columns.Add("DiemTB", typeof(float));
            dt.Columns.Add("Email", typeof(string));

            if (File.Exists(filePath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(filePath, XmlReadMode.ReadSchema);
                if (ds.Tables.Contains("SinhVien"))
                {
                    return ds.Tables["SinhVien"];
                }
            }
            else
            {
                // Dữ liệu mẫu khởi tạo lần đầu tiên
                dt.Rows.Add("24001296", "Nguyễn Duy Hưng", new DateTime(2004, 1, 1), true, "CNTT", 8.5, "hung@gmail.com");
                dt.Rows.Add("24001297", "Nguyễn Văn Hiếu", new DateTime(2004, 5, 12), true, "CNTT", 8.0, "hieu@gmail.com");
                GhiFile(dt);
            }
            return dt;
        }

        private void GhiFile(DataTable dt)
        {
            DataSet ds = new DataSet();
            // Sử dụng dt.Copy() để tạo bản sao độc lập, tránh xung đột DataSet
            ds.Tables.Add(dt.Copy());
            ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
        }

        public void ThemSinhVien(SinhVienDTO sv)
        {
            DataTable dt = LayDanhSach();

            foreach (DataRow row in dt.Rows)
            {
                if (row["MaSV"].ToString() == sv.MaSV)
                {
                    throw new Exception("Mã sinh viên đã tồn tại trong hệ thống!");
                }
            }

            dt.Rows.Add(sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.MaKhoa, sv.DiemTB, sv.Email);
            GhiFile(dt);
        }

        public void SuaSinhVien(SinhVienDTO sv)
        {
            DataTable dt = LayDanhSach();
            bool found = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row["MaSV"].ToString() == sv.MaSV)
                {
                    row["HoTen"] = sv.HoTen;
                    row["NgaySinh"] = sv.NgaySinh;
                    row["GioiTinh"] = sv.GioiTinh;
                    row["MaKhoa"] = sv.MaKhoa;
                    row["DiemTB"] = sv.DiemTB;
                    row["Email"] = sv.Email;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception("Không tìm thấy mã sinh viên cần sửa!");
            }

            GhiFile(dt);
        }

        public void XoaSinhVien(string maSV)
        {
            DataTable dt = LayDanhSach();
            DataRow rowToDelete = null;

            foreach (DataRow row in dt.Rows)
            {
                if (row["MaSV"].ToString() == maSV)
                {
                    rowToDelete = row;
                    break;
                }
            }

            if (rowToDelete == null)
            {
                throw new Exception("Không tìm thấy sinh viên cần xóa!");
            }

            dt.Rows.Remove(rowToDelete);
            GhiFile(dt);
        }
    }
}