using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MensErgerJeNiet.Controller;
using MensErgerJeNiet.Model;

namespace MensErgerJeNiet.View
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Window
    {
        //System.diagnostics.output.writeline 
        private ImageSource aRed, aGreen, aYellow, aBlue, bRed, bGreen, bYellow, bBlue,
            cRed, cGreen, cYellow, cBlue, dRed, dGreen, dYellow, dBlue,
            srcField, BaseRed, BaseGreen, BaseYellow, BaseBlue,
            pawnRed1, pawnRed2, pawnRed3, pawnRed4, pawnGreen1, pawnGreen2, pawnGreen3, pawnGreen4,
            pawnYellow1, pawnYellow2, pawnYellow3, pawnYellow4, pawnBlue1, pawnBlue2, pawnBlue3, pawnBlue4,
            startRed, startGreen, startYellow, startBlue;
        private Board myBoard;
        private int nRows = 11;
        private int nCols = 11;
        private int cellSize = 50;
        private Point startPoint = new Point(0, 0);
        public Label Dice { get; set; }

        public BoardView(Board myBoard)
        {
            InitializeComponent();
            this.myBoard = myBoard;

            LoadPictures();

            for (int i = 0; i < nCols; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(cellSize);
                FieldsGrid.ColumnDefinitions.Add(col);
            }

            for (int i = 0; i < nRows; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(cellSize);
                FieldsGrid.RowDefinitions.Add(row);
            }

            UpdateView();

            Dice = new Label();
            Dice.FontSize = 35;
            Dice.Height = 50;
            Dice.Width = 50;
            Dice.HorizontalAlignment = HorizontalAlignment.Center;
            Dice.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(Dice, 5);
            Grid.SetRow(Dice, 5);
            FieldsGrid.Children.Add(Dice);
        }

        public void UpdateView()
        {
            DrawBaseFiels();
            DrawBoard();
        }

        public void DrawBoard()
        {
            Field current = (Field) myBoard.Origin;
            int index = 0;
            int indextotal = 0;
            Point direction = new Point(1, -1);
            while (indextotal < 40)
            {
                Image currentImg = new Image();
                if (indextotal % 10 == 0) { index++; }
                switch (index)
                {
                    case 1: if(indextotal%10==0) {startPoint.X = 0; startPoint.Y = 4; direction.X = 1; direction.Y = -1;}
                        if (current.MyPawn != null) { currentImg.Source = SetPawnImage(current.MyPawn); }
                        else if (current.GetType() == typeof(Field)) { currentImg.Source = srcField; }
                        else if (current.GetType() == typeof(EndField)) { DrawHomeFields(startPoint, current.NextHome); currentImg.Source = srcField; }
                        else if (current.GetType() == typeof(StartField)) { currentImg.Source = startYellow; }
                            DrawPlayField(direction, startPoint, currentImg, indextotal,index);
                        break;
                    case 2: if(indextotal%10==0) {startPoint.X = 6; startPoint.Y = 0; direction.X = 1; direction.Y = 1;}
                        if (current.MyPawn != null) { currentImg.Source = SetPawnImage(current.MyPawn); }
                        else if (current.GetType() == typeof(Field)) { currentImg.Source = srcField; }
                        else if (current.GetType() == typeof(EndField)) { DrawHomeFields(startPoint, current.NextHome); currentImg.Source = srcField; }
                        else if (current.GetType() == typeof(StartField)) { currentImg.Source = startGreen; }
                            DrawPlayField(direction, startPoint, currentImg, indextotal,index);
                        break;
                    case 3:  if(indextotal%10==0) {startPoint.X = 10; startPoint.Y = 6; direction.X = -1; direction.Y = 1;}
                        if (current.MyPawn != null) { currentImg.Source = SetPawnImage(current.MyPawn); }
                        else if (current.GetType() == typeof(Field)) { currentImg.Source = srcField; }
                        else if (current.GetType() == typeof(EndField)) { DrawHomeFields(startPoint, current.NextHome); currentImg.Source = srcField; }
                        else if (current.GetType() == typeof(StartField)) { currentImg.Source = startRed; }
                            DrawPlayField(direction, startPoint, currentImg, indextotal,index);
                        break;
                    case 4: if (indextotal % 10 == 0) { startPoint.X = 4; startPoint.Y = 10; direction.X = -1; direction.Y = -1; }
                        if (current.MyPawn != null) { currentImg.Source = SetPawnImage(current.MyPawn); }
                        else if (current.GetType() == typeof(Field)) { currentImg.Source = srcField; }
                        else if (current.GetType() == typeof(EndField)) { DrawHomeFields(startPoint, current.NextHome); currentImg.Source = srcField; }
                        else if (current.GetType() == typeof(StartField)) { currentImg.Source = startBlue; }
                            DrawPlayField(direction, startPoint, currentImg,indextotal,index);
                        break;
                    default: break;
                }
                current = current.Next;
                indextotal++;
            }
        }

        public void DrawBaseFiels()
        {
            BaseField current = myBoard.OriginBaseField;
            Color currentColor = Color.Yellow;
            int index = 0;
            int indextotal = 0;
            while (current !=null)
            {
                Image currentImg = new Image();
                if (current.MyColor != currentColor) { index++; }
                switch (index)
                {
                    case 0: currentColor = Color.Yellow; startPoint.X = 0; startPoint.Y = 0;
                        if (current.MyPawn != null) { currentImg.Source = SetPawnImage(current.MyPawn); } else { currentImg.Source = BaseYellow; }
                        DrawBaseFieldsSquare(currentImg, indextotal);
                        break;
                    case 1: currentColor = Color.Green; startPoint.X = 9; startPoint.Y = 0;
                        if (current.MyPawn != null) { currentImg.Source = SetPawnImage(current.MyPawn); } else { currentImg.Source = BaseGreen; }
                        DrawBaseFieldsSquare(currentImg, indextotal);
                        break;
                    case 2: currentColor = Color.Red; startPoint.X = 9; startPoint.Y = 9;
                        if (current.MyPawn != null) { currentImg.Source = SetPawnImage(current.MyPawn); } else { currentImg.Source = BaseRed; }
                        DrawBaseFieldsSquare(currentImg, indextotal);
                        break;
                    case 3: currentColor = Color.Blue; startPoint.X = 0; startPoint.Y = 9;
                        if (current.MyPawn != null) { currentImg.Source = SetPawnImage(current.MyPawn); } else { currentImg.Source = BaseBlue; }
                        DrawBaseFieldsSquare(currentImg, indextotal);
                        break;
                    default: break;
                }
                current = (BaseField) current.Next;
                indextotal++;
            }
        }

        public void DrawBaseFieldsSquare(Image currentImg, int indextotal)
        {
            switch (indextotal % 4)
            {
                case 1: startPoint.Y += 1; break;
                case 2: startPoint.Y += 1; startPoint.X += 1; break;
                case 3: startPoint.X += 1; break;
            }
            currentImg.SetValue(Grid.ColumnProperty, (int)startPoint.X);
            currentImg.SetValue(Grid.RowProperty, (int)startPoint.Y);

            FieldsGrid.Children.Add(currentImg);
            
        }

        public void DrawPlayField(Point direction, Point startPoint, Image currentImg, int indextotal, int index)
        {
            currentImg.SetValue(Grid.ColumnProperty, (int)startPoint.X);
            currentImg.SetValue(Grid.RowProperty, (int)startPoint.Y);

            FieldsGrid.Children.Add(currentImg);
     
            if (index % 2 == 1)
            {
                if (indextotal % 10 < 4)
                {
                    this.startPoint.X = (this.startPoint.X + (1 * direction.X));
                }
                if (indextotal % 10 > 3 && indextotal % 10 < 8)
                {
                    this.startPoint.Y = (this.startPoint.Y + (1 * direction.Y));
                }
                if (indextotal % 10 > 7 && indextotal % 10 < 10)
                {
                    this.startPoint.X = (this.startPoint.X + (1 * direction.X));
                }
            }
            else
            {
                if (indextotal % 10 < 4)
                {
                    this.startPoint.Y = (this.startPoint.Y + (1 * direction.Y));
                }
                if (indextotal % 10 > 3 && indextotal % 10 < 8)
                {
                    this.startPoint.X = (this.startPoint.X + (1 * direction.X));
                }
                if (indextotal % 10 > 7 && indextotal % 10 < 10)
                {
                    this.startPoint.Y = (this.startPoint.Y + (1 * direction.Y));
                }
            }
        }

        public void DrawHomeFields(Point startPoint, Field current)
        {
            Point endFieldPoint = new Point(startPoint.X, startPoint.Y);
            int index = 0;

            while (current != null)
            {
                Image img = new Image();
                switch (current.MyColor)
                {
                    case Color.Green:
                        switch(index)
                        {
                            case 0: startPoint.Y += 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = aGreen; } break;
                            case 1: startPoint.Y += 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = bGreen; } break;
                            case 2: startPoint.Y += 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = cGreen; } break;
                            case 3: startPoint.Y += 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = dGreen; } break;
                        }
                        img.SetValue(Grid.ColumnProperty, (int)startPoint.X);
                        img.SetValue(Grid.RowProperty, (int)startPoint.Y);
                        FieldsGrid.Children.Add(img);
                        break;
                    case Color.Red:
                        switch(index)
                        {
                            case 0: startPoint.X -= 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = aRed; } break;
                            case 1: startPoint.X -= 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = bRed; } break;
                            case 2: startPoint.X -= 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = cRed; } break;
                            case 3: startPoint.X -= 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = dRed; } break;
                        }
                        img.SetValue(Grid.ColumnProperty, (int)startPoint.X);
                        img.SetValue(Grid.RowProperty, (int)startPoint.Y);
                        FieldsGrid.Children.Add(img);
                        break;
                    case Color.Blue:
                        switch(index)
                        {
                            case 0: startPoint.Y -= 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = aBlue; } break;
                            case 1: startPoint.Y -= 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = bBlue; } break;
                            case 2: startPoint.Y -= 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = cBlue; } break;
                            case 3: startPoint.Y -= 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = dBlue; } break;
                        }
                        img.SetValue(Grid.ColumnProperty, (int)startPoint.X);
                        img.SetValue(Grid.RowProperty, (int)startPoint.Y);
                        FieldsGrid.Children.Add(img);
                        break;
                    case Color.Yellow:
                        switch(index)
                        {
                            case 0: startPoint.X += 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = aYellow; } break;
                            case 1: startPoint.X += 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = bYellow; } break;
                            case 2: startPoint.X += 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = cYellow; } break;
                            case 3: startPoint.X += 1; if (current.MyPawn != null) { img.Source = SetPawnImage(current.MyPawn); } else { img.Source = dYellow; } break;
                        }
                        img.SetValue(Grid.ColumnProperty, (int)startPoint.X);
                        img.SetValue(Grid.RowProperty, (int)startPoint.Y);
                        FieldsGrid.Children.Add(img);
                        break;
                }
                index++;
                current = current.NextHome;
            }

            startPoint = endFieldPoint;
        }

        public ImageSource SetPawnImage(Pawn myPawn)
        {
            ImageSource img = null;
            switch (myPawn.MyColor)
            {
                case Color.Red:
                    switch(myPawn.MyNumber)
                    {
                        case 1: img = pawnRed1; break;
                        case 2: img = pawnRed2; break;
                        case 3: img = pawnRed3; break;
                        case 4: img = pawnRed4; break;
                        default: break;
                    }
                    break;
                case Color.Yellow:
                    switch (myPawn.MyNumber)
                    {
                        case 1: img = pawnYellow1; break;
                        case 2: img = pawnYellow2; break;
                        case 3: img = pawnYellow3; break;
                        case 4: img = pawnYellow4; break;
                        default: break;
                    }
                    break;
                case Color.Blue:
                    switch (myPawn.MyNumber)
                    {
                        case 1: img = pawnBlue1; break;
                        case 2: img = pawnBlue2; break;
                        case 3: img = pawnBlue3; break;
                        case 4: img = pawnBlue4; break;
                        default: break;
                    }
                    break;
                case Color.Green:
                    switch (myPawn.MyNumber)
                    {
                        case 1: img = pawnGreen1; break;
                        case 2: img = pawnGreen2; break;
                        case 3: img = pawnGreen3; break;
                        case 4: img = pawnGreen4; break;
                        default: break;
                    }
                    break;
                default: break;
            }
            return img;
        }

        private void BoardWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (myBoard.GameControl.WaitForSpaceInput)
            {
                if (e.Key.Equals(Key.Space))
                {
                    myBoard.GameControl.PlayTurn(0);
                    
                }
            }
            else if(myBoard.GameControl.SpaceToRethrow)
            {
                myBoard.GameControl.PlayTurn(0);
            }
             if(myBoard.GameControl.WaitForNumberInput)
             {
                if(e.Key.Equals(Key.D1) || e.Key.Equals(Key.NumPad1))
                {
                    myBoard.GameControl.PlayTurn(1);
                }
                else if(e.Key.Equals(Key.D2) || e.Key.Equals(Key.NumPad2))
                {
                    myBoard.GameControl.PlayTurn(2);
                }
                else if(e.Key.Equals(Key.D3) || e.Key.Equals(Key.NumPad3))
                {
                    myBoard.GameControl.PlayTurn(3);
                }
                else if(e.Key.Equals(Key.D4) || e.Key.Equals(Key.NumPad4))
                {
                    myBoard.GameControl.PlayTurn(4);
                }
             }
        }

        public void UpdateDice()
        {
            Player current = myBoard.OriginPlayer;
            BrushConverter bc = new BrushConverter();
            Brush brush;
            while (current.Next != null)
            {
                if (current.Equals(myBoard.CurrentTurn))
                {
                    if (current.MyColor == Color.Blue) { brush = (Brush)bc.ConvertFrom("#00A2E8"); Dice.Background = brush; }
                    if (current.MyColor == Color.Red) { brush = (Brush)bc.ConvertFrom("#ED1C24"); Dice.Background = brush; }
                    if (current.MyColor == Color.Yellow) { brush = (Brush)bc.ConvertFrom("#FFF200"); Dice.Background = brush; }
                    if (current.MyColor == Color.Green) { brush = (Brush)bc.ConvertFrom("#22B14C"); Dice.Background = brush; }
                    break;
                }
                current = current.Next;
            }
        }

        public void ShowWinMessage(Color myColor)
        {
            System.Windows.MessageBox.Show("GAME OVER! \n Player " + myColor + " heeft gewonnen!");
            this.Close();
        }
        private void LoadPictures()
        {
            aRed = new BitmapImage(new Uri("pack://application:,,,/images/aRed.png"));
            aGreen = new BitmapImage(new Uri("pack://application:,,,/images/aGreen.png"));
            aYellow = new BitmapImage(new Uri("pack://application:,,,/images/aYellow.png"));
            aBlue = new BitmapImage(new Uri("pack://application:,,,/images/aBlue.png"));

            bRed = new BitmapImage(new Uri("pack://application:,,,/images/bRed.png"));
            bGreen = new BitmapImage(new Uri("pack://application:,,,/images/bGreen.png"));
            bYellow = new BitmapImage(new Uri("pack://application:,,,/images/bYellow.png"));
            bBlue = new BitmapImage(new Uri("pack://application:,,,/images/bBlue.png"));

            cRed = new BitmapImage(new Uri("pack://application:,,,/images/cRed.png"));
            cGreen = new BitmapImage(new Uri("pack://application:,,,/images/cGreen.png"));
            cYellow = new BitmapImage(new Uri("pack://application:,,,/images/cYellow.png"));
            cBlue = new BitmapImage(new Uri("pack://application:,,,/images/cBlue.png"));

            dRed = new BitmapImage(new Uri("pack://application:,,,/images/dRed.png"));
            dGreen = new BitmapImage(new Uri("pack://application:,,,/images/dGreen.png"));
            dYellow = new BitmapImage(new Uri("pack://application:,,,/images/dYellow.png"));
            dBlue = new BitmapImage(new Uri("pack://application:,,,/images/dBlue.png"));

            BaseRed = new BitmapImage(new Uri("pack://application:,,,/images/BaseRed.png"));
            BaseGreen = new BitmapImage(new Uri("pack://application:,,,/images/BaseGreen.png"));
            BaseYellow = new BitmapImage(new Uri("pack://application:,,,/images/BaseYellow.png"));
            BaseBlue = new BitmapImage(new Uri("pack://application:,,,/images/BaseBlue.png"));

            pawnRed1 = new BitmapImage(new Uri("pack://application:,,,/images/pawnRed1.png"));
            pawnRed2 = new BitmapImage(new Uri("pack://application:,,,/images/pawnRed2.png"));
            pawnRed3 = new BitmapImage(new Uri("pack://application:,,,/images/pawnRed3.png"));
            pawnRed4 = new BitmapImage(new Uri("pack://application:,,,/images/pawnRed4.png"));
            
            pawnGreen1 = new BitmapImage(new Uri("pack://application:,,,/images/pawnGreen1.png"));
            pawnGreen2 = new BitmapImage(new Uri("pack://application:,,,/images/pawnGreen2.png"));
            pawnGreen3 = new BitmapImage(new Uri("pack://application:,,,/images/pawnGreen3.png"));
            pawnGreen4 = new BitmapImage(new Uri("pack://application:,,,/images/pawnGreen4.png"));
            
            pawnYellow1 = new BitmapImage(new Uri("pack://application:,,,/images/pawnYellow1.png"));
            pawnYellow2 = new BitmapImage(new Uri("pack://application:,,,/images/pawnYellow2.png"));
            pawnYellow3 = new BitmapImage(new Uri("pack://application:,,,/images/pawnYellow3.png"));
            pawnYellow4 = new BitmapImage(new Uri("pack://application:,,,/images/pawnYellow4.png"));

            pawnBlue1 = new BitmapImage(new Uri("pack://application:,,,/images/pawnBlue1.png"));
            pawnBlue2 = new BitmapImage(new Uri("pack://application:,,,/images/pawnBlue2.png"));
            pawnBlue3 = new BitmapImage(new Uri("pack://application:,,,/images/pawnBlue3.png"));
            pawnBlue4 = new BitmapImage(new Uri("pack://application:,,,/images/pawnBlue4.png"));

            startRed = new BitmapImage(new Uri("pack://application:,,,/images/startRed.png"));
            startGreen = new BitmapImage(new Uri("pack://application:,,,/images/startGreen.png"));
            startYellow = new BitmapImage(new Uri("pack://application:,,,/images/startYellow.png"));
            startBlue = new BitmapImage(new Uri("pack://application:,,,/images/startBlue.png"));

            srcField = new BitmapImage(new Uri("pack://application:,,,/images/Field.png"));
        }
    }
}

