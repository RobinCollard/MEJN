using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    public class Field
    {
        public Field Next { get; set; }
        public Field Previous { get; set; }
        public HomeField NextHome { get; set; }
        public Pawn MyPawn { get; set; }
        public bool IsLocked { get; set; }
        public Color MyColor { get; set; }

        public Field()
        {

        }
    }
}