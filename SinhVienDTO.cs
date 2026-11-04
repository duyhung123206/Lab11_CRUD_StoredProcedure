using System;

namespace Lab11_CRUD_StoredProcedure.DTO
{
    public class SinhVienDTO
    {
        public string MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string MaKhoa { get; set; }
        public float DiemTB { get; set; }
        public string Email { get; set; }
    }
}