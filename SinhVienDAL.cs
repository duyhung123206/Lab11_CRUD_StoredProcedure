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
            dt.Columns.Add("IsActive", typeof(bool)); // Phục vụ yêu cầu 9: Xóa mềm

            if (File.Exists(filePath))
            {
                DataSet ds = new DataSet();
                ds.ReadXml(filePath, XmlReadMode.ReadSchema);
                if (ds.Tables.Contains("SinhVien"))
                {
                    DataTable dtLoaded = ds.Tables["SinhVien"];
                    // Đảm bảo có cột IsActive nếu file cũ chưa có
                    if (!dtLoaded.Columns.Contains("IsActive"))
                    {
                        dtLoaded.Columns.Add("IsActive", typeof(bool));
                        foreach (DataRow r in dtLoaded.Rows) r["IsActive"] = true;
                    }
                    return dtLoaded;
                }
            }
            else
            {
                dt.Rows.Add("24001296", "Nguyễn Duy Hưng", new DateTime(2004, 1, 1), true, "CNTT", 8.5, "hung@gmail.com", true);
                dt.Rows.Add("24001297", "Nguyễn Văn Hiếu", new DateTime(2004, 5, 12), true, "CNTT", 8.0, "hieu@gmail.com", true);
                GhiFile(dt);
            }
            return dt;
        }

        private void GhiFile(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt.Copy());
            ds.WriteXml(filePath, XmlWriteMode.WriteSchema);
        }

        // Yêu cầu 8: Kiểm tra email duy nhất & Yêu cầu 3: Thêm
        public int Insert(SinhVienDTO dto)
        {
            DataTable dt = LayDanhSach();

            foreach (DataRow row in dt.Rows)
            {
                if (row["MaSV"].ToString() == dto.MaSV)
                {
                    throw new Exception("Lỗi nghiệp vụ: Mã sinh viên đã tồn tại trong hệ thống!");
                }
                if (!string.IsNullOrEmpty(dto.Email) && row["Email"].ToString().Equals(dto.Email, StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Lỗi nghiệp vụ: Email này đã được sử dụng bởi sinh viên khác!");
                }
            }

            dt.Rows.Add(dto.MaSV, dto.HoTen, dto.NgaySinh, dto.GioiTinh, dto.MaKhoa, dto.DiemTB, dto.Email, true);
            GhiFile(dt);
            return 1;
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
                throw new Exception("Lỗi nghiệp vụ: Không tìm thấy mã sinh viên cần sửa!");
            }

            GhiFile(dt);
        }

        // Yêu cầu 9: Xóa mềm sinh viên (chuyển IsActive thành false thay vì xóa vật lý)
        public void XoaSinhVien(string maSV)
        {
            DataTable dt = LayDanhSach();
            bool found = false;

            foreach (DataRow row in dt.Rows)
            {
                if (row["MaSV"].ToString() == maSV)
                {
                    row["IsActive"] = false; // Xóa mềm
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                throw new Exception("Lỗi nghiệp vụ: Không tìm thấy sinh viên cần xóa!");
            }

            GhiFile(dt);
        }

        // Yêu cầu 7: Tìm kiếm theo từ khóa (Mã hoặc Họ tên) và Khoa
        public DataTable TimKiem(string keyword, string maKhoa)
        {
            DataTable dt = LayDanhSach();
            DataTable dtResult = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row["IsActive"]) == false) continue; // Chỉ lấy sinh viên đang hoạt động

                bool matchKeyword = string.IsNullOrWhiteSpace(keyword) ||
                                    row["MaSV"].ToString().Contains(keyword) ||
                                    row["HoTen"].ToString().Contains(keyword);

                bool matchKhoa = string.IsNullOrWhiteSpace(maKhoa) ||
                                 row["MaKhoa"].ToString().Equals(maKhoa, StringComparison.OrdinalIgnoreCase);

                if (matchKeyword && matchKhoa)
                {
                    dtResult.ImportRow(row);
                }
            }
            return dtResult;
        }

        // Yêu cầu 6: Kiểm tra khoa có sinh viên hay không trước khi xóa khoa
        public bool KiemTraKhoaCoSinhVien(string maKhoa)
        {
            DataTable dt = LayDanhSach();
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row["IsActive"]) && row["MaKhoa"].ToString().Equals(maKhoa, StringComparison.OrdinalIgnoreCase))
                {
                    return true; // Khoa vẫn đang chứa sinh viên
                }
            }
            return false;
        }
    }
}