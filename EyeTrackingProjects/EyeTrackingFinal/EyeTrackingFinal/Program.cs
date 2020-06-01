using System;
using System.Drawing;
using System.Windows.Forms;
using Tobii.Interaction;
using Tobii.Interaction.Client;

namespace EyeTrackingFinal
{
    // Found from EyeTrackingTestThree Program.cs
    public static class Program
    {
        private const int MiddleX = 750;
        private const int MiddleY = 410;
        private const int SmoothingValue = 200;
        
        public static void Main(string[] args)
        {
            var host = new Host();
            var gazePointDataStream = host.Streams.CreateGazePointDataStream();

            gazePointDataStream.GazePoint((gazePointX, gazePointY, timestamp) =>
            {
                double centerDistanceX = Math.Abs(MouseOperations.GetCursorPosition().X - MiddleX);
                double centerDistanceY = Math.Abs(MouseOperations.GetCursorPosition().Y - MiddleY);

                //Ensures smoother movements
                if (Math.Abs(centerDistanceX) >= SmoothingValue || Math.Abs(centerDistanceY) >= SmoothingValue)
                {
                    MouseOperations.SetCursorPosition(MiddleX, MiddleY);
                }

                Console.WriteLine($"Gaze X Position: {gazePointX} \t Gaze Y Position: {gazePointY}");

                double dX = MouseOperations.GetCursorPosition().X - gazePointX;
                double dY = MouseOperations.GetCursorPosition().Y - gazePointY;

                if (Math.Abs(dX) > SmoothingValue && Math.Abs(dY) > SmoothingValue)
                {
                    if (dX > 0)
                    {
                        if (dY > 0)
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.Move, -1, -1);
                        }
                        else if (dY < 0)
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.Move, -1, 1);
                        }
                    }
                    else
                    {
                        if (dY > 0)
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.Move, 1, -1);
                        }
                        else if (dY < 0)
                        {
                            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.Move, 1, 1);
                        }
                    }
                }
                else if (Math.Abs(dX) > SmoothingValue)
                {
                    if (dX > 0)
                    {
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.Move, -1, 0);
                    }
                    else
                    {
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.Move, 1, 0);
                    }
                }
                else if (Math.Abs(dY) > SmoothingValue)
                {
                    if (dY > 0)
                    {
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.Move, 0, -1);
                    }
                    else
                    {
                        MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.Move, 0, 1);
                    }
                }

                Console.WriteLine($"New Cursor Position: {MouseOperations.GetCursorPosition()}");
            });

            Console.ReadLine();
        }
    }
}