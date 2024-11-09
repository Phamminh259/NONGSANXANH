using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NONGSANXANH.Model
{
    public class PhieuNhapModel : IModel
    {
        public int SoPhieu { get; set; }
        public string MaNCC { get; set; }
        public DateTime NgayNhap { get; set; }

        public PhieuNhapModel(int soPhieu, string maNCC, DateTime ngayNhap)
        {
            SoPhieu = soPhieu;
            MaNCC = maNCC;
            NgayNhap = ngayNhap;
        }
        public PhieuNhapModel(string maNCC, DateTime ngayNhap)
        {
            MaNCC = maNCC;
            NgayNhap = ngayNhap;
        }
    }
}
