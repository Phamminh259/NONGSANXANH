using NONGSANXANH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NONGSANXANH.Model;
using NONGSANXANH.Controller;

namespace NONGSANXANH.View
{
    public interface IView
    {

        public void SetDataToText(Object item);
        IModel GetDataFromText();
        public void clearDataFromText();
    }
}
