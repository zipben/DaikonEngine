using Daikon.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Daikon.Factories
{
    public class StupidShapeFactory
    {
        public IStupidShape GetNextStupidShape()
        {
            StumpyTee tee = new StumpyTee(50);

            return tee;
        }
    }
}
