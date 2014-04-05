using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    public class Pawn
    {
        public Field MyField { get; set; }
        public Color MyColor { get; set; }
        public int MyNumber { get; set; }
        public bool IsLocked { get; set; }

        public Pawn(Field myField, Color myColor, int myNumber)
        {
            this.MyField = myField;
            this.MyColor = myColor;
            this.MyNumber = myNumber;
        }
    }
}