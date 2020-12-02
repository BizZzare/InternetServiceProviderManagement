using System;
using System.Collections.Generic;

namespace InternetServiceProviderManagement.UI
{
    public class ConsoleSelector
    {
        private readonly int _xCoodinate;
        private readonly int _yCoodinate;

        private readonly ConsoleColor _foreGround;
        private readonly ConsoleColor _backGround;

        public List<string> Choises { get; set; }

        #region Constructors

        public ConsoleSelector()
        {
            Choises = new List<string>();

            _foreGround = ConsoleColor.Black;
            _backGround = ConsoleColor.White;

            _xCoodinate = 0;
            _yCoodinate = 0;
        }

        public ConsoleSelector(List<string> choises, int xCoordinate, int yCoordinate)
        {
            Choises = choises;

            _xCoodinate = xCoordinate;
            _yCoodinate = yCoordinate;

            _foreGround = ConsoleColor.Black;
            _backGround = ConsoleColor.White;

        }

        public ConsoleSelector(List<string> choises, int xCoordinate, int yCoordinate, ConsoleColor foreGround, ConsoleColor backGround)
        {
            Choises = choises;

            _xCoodinate = xCoordinate;
            _yCoodinate = yCoordinate;

            _foreGround = foreGround;
            _backGround = backGround;

        }

        #endregion

        public int GetChosenOptionIndex()
        {
            try
            {
                int selectedItem = 0;

                while (true)
                {

                    for (int i = 0; i < Choises.Count; i++)
                    {
                        Console.SetCursorPosition(_xCoodinate, _yCoodinate + i);

                        if (selectedItem == i)
                        {
                            Console.ForegroundColor = _foreGround;
                            Console.BackgroundColor = _backGround;
                        }
                        else
                        {
                            Console.ResetColor();
                        }

                        Console.Write(Choises[i]);
                        Console.ResetColor();
                    }

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            {
                                selectedItem = selectedItem > 0
                                    ? selectedItem - 1
                                    : Choises.Count - 1;

                                //if (selectedItem > 0)
                                //    selectedItem--;
                                //else
                                //    selectedItem = Choises.Count - 1;

                            }
                            break;

                        case ConsoleKey.DownArrow:
                            {
                                selectedItem = selectedItem < Choises.Count - 1
                                    ? selectedItem + 1
                                    : selectedItem = 0;

                                //if (selectedItem < Choises.Count - 1)
                                //    selectedItem++;
                                //else
                                //    selectedItem = 0;

                            }
                            break;

                        case ConsoleKey.Enter:
                            {
                                return selectedItem;
                            }
                    }
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Error");
                throw new Exception();
            }
        }
    }
}
