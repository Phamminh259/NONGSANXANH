using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NONGSANXANH.Model
{
    public class NHACUNGCAPModel : IModel
    {
        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string STK { get; set; }
        public string TenNH { get; set; }

        // Phương thức khởi tạo
        public NHACUNGCAPModel() { }

        public NHACUNGCAPModel(string maNCC, string tenNCC, string diaChi, string dienThoai, string stk, string tenNH)
        {
            MaNCC = maNCC;
            TenNCC = tenNCC;
            DiaChi = diaChi;
            DienThoai = dienThoai;
            STK = stk;
            TenNH = tenNH;
        }
    }
}
