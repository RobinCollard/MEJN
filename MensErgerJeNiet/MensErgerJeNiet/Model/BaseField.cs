using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    public class BaseField : Field
    {
        public int MyNumber { get; set; }

        public BaseField(Color color, int number)
        {
            this.MyColor = color;
            this.MyNumber = number;
        }
    }
}