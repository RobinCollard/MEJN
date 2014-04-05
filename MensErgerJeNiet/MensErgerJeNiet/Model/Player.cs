using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensErgerJeNiet.Model
{
    public class Player
    {
        public Color MyColor;
        public Player Next { get; set; }
        public Pawn[] MyPawns { get; set; }
        public BaseField[] MyBases { get; set; }
        public HomeField[] MyHomes { get; set; }
        public StartField MyStart { get; set; }

        public Player(Color myColor)
        {
            this.MyColor = myColor;
            MyPawns = new Pawn[4];
            MyBases = new BaseField[4];
            MyHomes = new HomeField[4];
        }

        public void AddPawn(Pawn pawn)
        {
            MyPawns[pawn.MyNumber - 1] = pawn;
        }

        public void AddBase(BaseField basef)
        {
            for (int i = 0; i < 4; i++)
            {
                if (MyBases[i] == null)
                {
                    MyBases[i] = basef;
                    break;
                }
            }
        }

        public void AddHome(HomeField homef)
        {
            for (int i = 0; i < 4; i++)
            {
                if (MyHomes[i] == null)
                {
                    MyHomes[i] = homef;
                    break;
                }
            }
        }

        public bool FullBase()
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.MyBases[i].MyPawn == null && !MyPawns[i].IsLocked)
                {
                    return false;
                }
            }
            return true;
        }

        public BaseField GetBaseByNumber(int number)
        {
            BaseField bf = null;
            for (int i = 0; i < 4; i++)
            {
                if (MyBases[i].MyNumber == number)
                {
                    bf = MyBases[i];
                    break;
                }
            }
            return bf;
        }

        public Pawn GetPawnByNumber(int number)
        {
            Pawn pawn = null;
            for (int i = 0; i < 4; i++)
            {
                if (MyPawns[i].MyNumber == number)
                {
                    pawn = MyPawns[i];
                    break;
                }
            }
            return pawn;
        }

        public int GetNonUsedNumber()
        {
            int nonUsed = 0;
            for (int i = 0; i < 4; i++)
            {
                if (MyPawns[i] == null)
                {
                    nonUsed = i + 1;
                }
            }
            return nonUsed;
        }
    }
}
