using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace УМФ
{
    class task2
    {
        public List<String> param_names;
        public List<double> param_values;

        public task2(){
            param_names = new List<string>();
            param_values = new List<double>();

            param_names.Add("t0 сверху");
            param_names.Add("t0 снизу");
            param_names.Add("t0 слева");
            param_names.Add("t0 справа");

            param_values.Add(100);
            param_values.Add(0);
            param_values.Add(75);
            param_values.Add(50);
        }

        public double calc(double pt1, double pt2, double pt3, double pt4){
            return ((pt1 + pt2 + pt3 + pt4)/4);
        }
    }
}
