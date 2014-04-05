using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using MensErgerJeNiet.View;
using MensErgerJeNiet.Model;

namespace MensErgerJeNiet.Controller
{
    public class Board
    {
        public BaseField OriginBaseField { get; set; }
        public Field Origin { get; set; }
        public Color CurrentColor { get; set; }
        public BoardView MyView { get; set; }
        public GameController GameControl { get; set; }
        public Player OriginPlayer { get; set; }
        public Player CurrentTurn { get; set; }
        public int AmountOfPlayers {get; set;}

        public Board(int amountOfPlayers)
        {
            this.AmountOfPlayers = amountOfPlayers;
            StartNewGame(amountOfPlayers);
            GameControl = new GameController(this);
        }

        public void StartNewGame(int amountOfPlayers)
        {
            
                string pathString = "";
                string[] fileStrings = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory() + @"\..\..\..\MEJN-Levels", "*.mejn");
                foreach (string s in fileStrings)
                {
                    if (amountOfPlayers == 4)
                    {
                        if (s.Contains("std.mejn"))
                        {
                            OriginPlayer = new Player(Color.Yellow);
                            Player currentPlayer = OriginPlayer;
                            currentPlayer.Next = new Player(Color.Green);
                            currentPlayer = currentPlayer.Next;
                            currentPlayer.Next = new Player(Color.Red);
                            currentPlayer = currentPlayer.Next;
                            currentPlayer.Next = new Player(Color.Blue);
                            currentPlayer = currentPlayer.Next;
                            currentPlayer.Next = OriginPlayer;
                        
                            pathString = s;
                            buildLevel(pathString);
                            break;
                        }
                    }
                    if (amountOfPlayers == 3)
                    {
                        if (s.Contains("std3.mejn"))
                        {
                            OriginPlayer = new Player(Color.Yellow);
                            Player currentPlayer = OriginPlayer;
                            currentPlayer.Next = new Player(Color.Green);
                            currentPlayer = currentPlayer.Next;
                            currentPlayer.Next = new Player(Color.Red);
                            currentPlayer = currentPlayer.Next;
                            currentPlayer.Next = OriginPlayer;
                        
                            pathString = s;
                            buildLevel(pathString);
                            break;
                        }
                    }
                    if (amountOfPlayers == 2)
                    {
                        if (s.Contains("std2.mejn"))
                        {
                            OriginPlayer = new Player(Color.Yellow);
                            Player currentPlayer = OriginPlayer;
                            OriginPlayer.Next = new Player(Color.Red);
                            currentPlayer = currentPlayer.Next;
                            currentPlayer.Next = OriginPlayer;
                        
                            pathString = s;
                            buildLevel(pathString);
                            break;
                        }
                    }
                    
                }
                CurrentTurn = OriginPlayer;                
        }

        public void buildLevel(string pathString)
        {
            System.IO.StreamReader myFile =
            new System.IO.StreamReader(pathString);
            string myString = myFile.ReadToEnd();
            myFile.Close();

            string[] lines = Regex.Split(myString, "\r\n");

            BuildBaseFields(lines);
            BuildBoardFields(lines);
            MyView = new BoardView(this);
            MyView.Show();

        }

        public void BuildBaseFields(string[] lines)
        {
            Field currentField = null;
            Field previousField = null;
            Player currentPlayer;
            int numberY = 1, numberG = 1, numberR = 1, numberB = 1;
            for (int y = 0; y < 1; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (OriginBaseField != null)
                    {
                        previousField = currentField;
                    }
                    switch (lines[y][x])
                    {
                        case 'Y': currentField = new BaseField(Color.Yellow, numberY);
                            currentField.MyPawn = new Pawn(currentField, Color.Yellow, numberY);
                            if (GetPlayerByColor(Color.Yellow) != null)
                            {
                                currentPlayer = GetPlayerByColor(Color.Yellow);
                                currentPlayer.AddBase((BaseField)currentField);
                                currentPlayer.AddPawn(currentField.MyPawn);
                            }
                            numberY++;
                            break;
                        case 'G': currentField = new BaseField(Color.Green, numberG);
                            currentField.MyPawn = new Pawn(currentField, Color.Green, numberG);
                            if (GetPlayerByColor(Color.Green) != null)
                            {
                                currentPlayer = GetPlayerByColor(Color.Green);
                                currentPlayer.AddBase((BaseField)currentField);
                                currentPlayer.AddPawn(currentField.MyPawn);
                            }
                            numberG++;
                            break;
                        case 'R': currentField = new BaseField(Color.Red,numberR);
                            currentField.MyPawn = new Pawn(currentField, Color.Red, numberR);
                            if (GetPlayerByColor(Color.Red) != null)
                            {
                                currentPlayer = GetPlayerByColor(Color.Red);
                                currentPlayer.AddBase((BaseField)currentField);
                                currentPlayer.AddPawn(currentField.MyPawn);
                            }
                            numberR++;
                            break;
                        case 'B': currentField = new BaseField(Color.Blue,numberB);
                            currentField.MyPawn = new Pawn(currentField, Color.Blue, numberB);
                            if (GetPlayerByColor(Color.Blue) != null)
                            {
                                currentPlayer = GetPlayerByColor(Color.Blue);
                                currentPlayer.AddBase((BaseField)currentField);
                                currentPlayer.AddPawn(currentField.MyPawn);
                            }
                            numberB++;
                            break;
                        case '5': currentField = new BaseField(Color.Yellow,numberY);
                            if(GetPlayerByColor(Color.Yellow) != null)
                            {
                                currentPlayer = GetPlayerByColor(Color.Yellow);
                                currentPlayer.AddBase((BaseField) currentField);
                            }
                            numberY++;
                            break;
                        case '6': currentField = new BaseField(Color.Green,numberG);
                            if(GetPlayerByColor(Color.Green) != null)
                            {
                                currentPlayer = GetPlayerByColor(Color.Green);
                                currentPlayer.AddBase((BaseField) currentField);
                            }
                            numberG++;
                            break;
                        case '7': currentField = new BaseField(Color.Red,numberR);
                            if(GetPlayerByColor(Color.Red) != null)
                            {
                                currentPlayer = GetPlayerByColor(Color.Red);
                                currentPlayer.AddBase((BaseField) currentField);
                            }
                            numberR++;
                            break;
                        case '8': currentField = new BaseField(Color.Blue,numberB);
                            if(GetPlayerByColor(Color.Blue) != null)
                            {
                                currentPlayer = GetPlayerByColor(Color.Blue);
                                currentPlayer.AddBase((BaseField) currentField);
                            }
                            numberB++;
                            break;
                        default:
                            break;
                    }
                    if (x == 0 && y == 0) { OriginBaseField = (BaseField)currentField; }
                    if (x > 0) { currentField.Previous = previousField; previousField.Next = currentField; }

                }
            }
        }

        public void BuildBoardFields(string[] lines)
        {
            Field currentField = null;
            Field previousField = null;
            Field continueOn = null;
            Player currentPlayer;
            for (int y = 1; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (Origin != null)
                    {
                        previousField = currentField;
                    }
                    switch (y)
                    {
                        case 1: CurrentColor = Color.Yellow; break;
                        case 2: CurrentColor = Color.Green; break;
                        case 3: CurrentColor = Color.Red; break;
                        case 4: CurrentColor = Color.Blue; break;
                    }
                    if (x == 0)
                    {
                        currentField = new StartField(CurrentColor);
                        if (y == 1) { Origin = currentField; }
                        if (y > 1) { previousField = continueOn; previousField.Next = currentField; currentField.Previous = previousField; }
                        if (GetPlayerByColor(CurrentColor) != null)
                        {
                            currentPlayer = GetPlayerByColor(CurrentColor);
                            currentPlayer.MyStart = (StartField)currentField;
                        }

                    }
                    if (x > 0 && x < 9)
                    {
                        currentField = new Field();
                        currentField.Previous = previousField;
                        previousField.Next = currentField;
                    }
                    if (x == 9)
                    {
                        currentField = new EndField();
                        currentField.Previous = previousField;
                        previousField.Next = currentField;
                        continueOn = currentField;
                    }
                    if (x > 9)
                    {
                        Color previousColour = CurrentColor;
                        if (previousColour == Color.Yellow) CurrentColor = Color.Green;
                        if (previousColour == Color.Green) CurrentColor = Color.Red;
                        if (previousColour == Color.Red) CurrentColor = Color.Blue;
                        if (previousColour == Color.Blue) CurrentColor = Color.Yellow;
                        currentField = new HomeField(CurrentColor);
                        currentField.Previous = previousField;
                        previousField.NextHome = (HomeField)currentField;
                        if (GetPlayerByColor(CurrentColor) != null)
                        {
                            currentPlayer = GetPlayerByColor(CurrentColor);
                            currentPlayer.AddHome((HomeField)currentField);
                        }
                        CurrentColor = previousColour;
                    }
                    if (lines[y][x] != 'o')
                    {
                        BaseField current = OriginBaseField;
                        switch(lines[y][x])
                        {
                            case 'y': CurrentColor = Color.Yellow; break;
                            case 'g': CurrentColor = Color.Green; break;
                            case 'b': CurrentColor = Color.Blue; break;
                            case 'r': CurrentColor = Color.Red; break;
                            default: break;
                        }
                        int amount = 0;
                        int baseNr = 0;
                        while (current.MyColor == CurrentColor)
                        {
                            if (current.MyPawn != null)
                            {
                                amount++;
                            }
                            current = (BaseField)current.Next;
                        }
                        current = OriginBaseField;
                        while (current.Next != null)
                        {
                            if (current.MyColor == CurrentColor && current.MyPawn == null)
                            {
                                baseNr = current.MyNumber;
                                break;
                            }
                            current = (BaseField)current.Next;
                        }
                        if (GetPlayerByColor(CurrentColor) != null)
                        {
                            currentPlayer = GetPlayerByColor(CurrentColor);
                            currentField.MyPawn = new Pawn(current, CurrentColor, currentPlayer.GetNonUsedNumber());
                            currentField.MyPawn.MyField = currentField;
                            if(currentField.GetType() == typeof(HomeField))
                            {
                                currentField.IsLocked = true;
                                currentField.MyPawn.IsLocked = true;
                            }
                            currentPlayer.AddPawn(currentField.MyPawn);
                        }
                    }
                }
            }
            Origin.Previous = continueOn;
            continueOn.Next = Origin;
        }

        public Player GetPlayerByColor(Color color)
        {
            int max = 0;
            Player current = OriginPlayer;
            while(max < 4)
            {
                if (current.MyColor == color)
                {
                    return current;
                }
                current = current.Next;
                max++;
            }
            return null;
        }
    }
}
