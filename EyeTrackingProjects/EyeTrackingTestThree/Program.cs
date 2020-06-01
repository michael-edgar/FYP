using System;
using System.Drawing;
using System.Windows.Forms;
using Tobii.Interaction;

namespace EyeTrackingTestThree
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            var host = new Host();
            var gazePointDataStream = host.Streams.CreateGazePointDataStream();
            var eyePositionStream = host.Streams.CreateEyePositionStream();
            var headPoseStream = host.Streams.CreateHeadPoseStream();

            // https://github.com/tobiitech/core-sdk-docs/blob/master/samples/Streams/Interaction_Streams_103/Program.cs
            /*int i = 0;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Enter)
                    {
                        Environment.Exit(0);
                    }
                }

                if (i < 10)
                {
                    //headPoseStream.Next += OnNextHeadPose;
                    gazePointDataStream.Next += OnNextGaze;
                    i += 1;
                }

            }*/

            // Wouldn't accurately capture head direction/movements
            /*headPoseStream.HeadPose((double timestamp, Vector3 headPosition, Vector3 headDirection) =>
            {
                Console.WriteLine("Head Position: {0} Head Direction: {1} Timestamp: {2}", headPosition, headDirection, timestamp);
                
                if (headDirection.Y.CompareTo(Double.NaN) != 0)
                {
                    //Console.WriteLine("Head Position: {0} Head Direction: {1} Timestamp: {2}", headPosition, headDirection, timestamp);
                    double moveX = headDirection.X * 10;
                    double moveY = headDirection.Y * 10;
                    int newX = Cursor.Position.X + (int)moveX;
                    int newY = Cursor.Position.Y + (int)moveY;
                    Cursor.Position = new Point(newX, newY);
                    //Cursor.Position = new Point((int)head_direction.X, (int)head_direction.Y);
                }
            });*/

            /*eyePositionStream.EyePosition((EyePositionData eyePositionData) =>
            {

                Console.WriteLine("Left Eye: {0}", eyePositionData.LeftEye);
                Console.WriteLine("Right Eye: {0}", eyePositionData.RightEye);

                var eyeAverageX = (eyePositionData.LeftEye.X + eyePositionData.RightEye.X) / 2;
                var eyeAverageY = (eyePositionData.LeftEye.Y + eyePositionData.RightEye.Y) / 2;

                eyeAverageX += Cursor.Position.X;
                eyeAverageY += Cursor.Position.Y;

                Cursor.Position = new Point((int)eyeAverageX, (int)eyeAverageY);
                Console.WriteLine("Cursor Position X: {0} Y: {1}", Cursor.Position.X, Cursor.Position.Y);
                
            });*/
            
            gazePointDataStream.GazePoint((gazePointX, gazePointY, timestamp) =>
            {
                // https://docs.microsoft.com/en-us/dotnet/api/system.math.abs?view=netcore-3.1
                double gazeDifferenceX = Math.Abs(Cursor.Position.X - gazePointX);
                double gazeDifferenceY = Math.Abs(Cursor.Position.Y - gazePointY);
                
                //Ensures smoother movements
                if (gazeDifferenceX >= 50 || gazeDifferenceY >= 50)
                {
                    Console.WriteLine("X: {0}  Y: {1}", gazePointX, gazePointY);
                    //double adjustedX = gazePointX - 90;
                    //double adjustedY = gazePointY - 90;
                    Cursor.Position = new Point((int)gazePointX, (int)gazePointY);
                }
            });

            Console.ReadLine();

            // https://developer.tobii.com/community/forums/topic/bug-tobii-interaction-gazepointdatastream-gazepoint-sometimes-has-no-effect/
            // gazePointDataStream.GazePoint((gazePointX, gazePointY, _) => { Environment.Exit(0); });
            // eye_position_stream.EyePosition((Tobii.Interaction.EyePositionData eye_position_data) => { });
            // head_pose_stream.HeadPose((double unused, Tobii.Interaction.Vector3 head_position, Tobii.Interaction.Vector3 head_direction) => { });

        }

        // https://github.com/tobiitech/core-sdk-docs/blob/master/samples/Streams/Interaction_Streams_103/Program.cs
        /*private static void OnNextHeadPose(object sender, StreamData<HeadPoseData> headPose)
        {
            Console.WriteLine($"Head pose timestamp  : {headPose.Data.Timestamp}");
            Console.WriteLine($"Has head position    : {headPose.Data.HasHeadPosition}");
            Console.WriteLine($"Has rotation  (X,Y,Z): ({headPose.Data.HasRotation.HasRotationX},{headPose.Data.HasRotation.HasRotationY},{headPose.Data.HasRotation.HasRotationZ})");
            Console.WriteLine($"Head position (X,Y,Z): ({headPose.Data.HeadPosition.X},{headPose.Data.HeadPosition.Y},{headPose.Data.HeadPosition.Z})");
            Console.WriteLine($"Head rotation (X,Y,Z): ({headPose.Data.HeadRotation.X},{headPose.Data.HeadRotation.Y},{headPose.Data.HeadRotation.Z})");
            Console.WriteLine("-----------------------------------------------------------------");
        }*/

        /*private static void OnNextGaze(object sender, StreamData<GazePointData> gaze)
        {
            Console.WriteLine($"Gaze timestamp: {gaze.Data.Timestamp}");
            Console.WriteLine($"Gaze engine timestamp: {gaze.Data.EngineTimestamp}");
            Console.WriteLine($"Gaze X Position: {gaze.Data.X}");
            Console.WriteLine($"Gaze Y Position: {gaze.Data.Y}");
            
            Cursor.Position = new Point((int)gaze.Data.X, (int)gaze.Data.Y);
        }*/
    }
}
