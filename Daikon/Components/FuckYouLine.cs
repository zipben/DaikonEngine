using Daikon.Enums;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daikon.Components
{
    public class FuckYouLine : IStupidShape
    {
        private Orientation orientation { get; set; }

        private float _segmentSize;

        private static Dictionary<Orientation, int[,]> ShapeMap = new Dictionary<Orientation, int[,]>(){
            {
                Orientation.Standard, new int[1,4]
                {
                    { 1,  1,  1, 1}
                }
            },
            {
                Orientation.Inverted, new int[1,4]
                {
                    { 1,  1,  1, 1}
                }
            },
            {
                Orientation.RotateLeft, new int[4,2]
                {
                    { 1,  1},
                    { 1,  1},
                    { 1,  1},
                    { 1,  1}
                }
            },
            {
                Orientation.RotateRight, new int[4,2]
                {
                    { 1,  1},
                    { 1,  1},
                    { 1,  1},
                    { 1,  1}
                }
            }
        };

        private int[,] RotatedShapeMap;

        public FuckYouLine(float segmentSize)
        {
            _segmentSize = segmentSize;
            orientation = Orientation.Standard;
        }

        public RectangleShape[] GetShape(Vector2f origin)
        {
            return ConvertShapeMap(origin);
        }

        public void Rotate()
        {
            int nextOrientation = (int)orientation + 1;

            int maxCount = Enum.GetValues(typeof(Orientation)).Length;

            if (nextOrientation > maxCount)
                nextOrientation = 1;

            orientation = (Orientation)nextOrientation;
        }

        private RectangleShape[] ConvertShapeMap(Vector2f origin)
        {
            List<RectangleShape> retList = new List<RectangleShape>();

            int rowLength = ShapeMap[orientation].GetLength(0);
            int colLength = ShapeMap[orientation].GetLength(1);

            for (int rowIndex = 0; rowIndex < rowLength; rowIndex++)
            {
                for (int colIndex = 0; colIndex < colLength; colIndex++)
                {
                    if (ShapeMap[orientation][rowIndex, colIndex] == 1)
                    {
                        RectangleShape newShape = new RectangleShape();
                        newShape.FillColor = Color.Magenta;
                        newShape.OutlineColor = Color.Blue;
                        newShape.Size = new Vector2f(_segmentSize, _segmentSize);

                        newShape.Position = new Vector2f(origin.X + (_segmentSize * colIndex), origin.Y + (_segmentSize * rowIndex));

                        retList.Add(newShape);
                    }
                }
            }

            return retList.ToArray();
        }
    }
}
