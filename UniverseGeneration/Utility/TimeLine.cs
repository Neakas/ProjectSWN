using System;
using System.Collections.Generic;

namespace UniverseGeneration.Utility
{
    /// <summary>
    ///     A base class for something with multiple segments (i.e a timeline)
    /// </summary>
    public class TimeLine
    {
        /// <summary>
        ///     Internal array to track points.
        /// </summary>
        protected List<double> Points;

        /// <summary>
        ///     A constructor assuming a list of existing points.
        /// </summary>
        /// <param name="inLen">The list of existing points</param>
        public TimeLine( IList<double> inLen )
        {
            InitList();
            foreach (var t in inLen)
            {
                Points.Add(t);
            }
        }

        /// <summary>
        ///     Base constructor
        /// </summary>
        public TimeLine()
        {
            InitList();
        }

        /// <summary>
        ///     Copy constructor
        /// </summary>
        /// <param name="t">The object to be copied</param>
        public TimeLine( TimeLine t )
        {
            InitList();
            foreach (var d in t.Points)
            {
                Points.Add(d);
            }
        }

        /// <summary>
        ///     A function used to initiate the <see cref="Points" /> object
        /// </summary>
        public void InitList()
        {
            Points = new List<double>();
        }

        /// <summary>
        ///     Used to get a count of the number of points
        /// </summary>
        /// <returns>The number of points in the internal array</returns>
        public int Count()
        {
            return Points.Count;
        }

        /// <summary>
        ///     Gets the distance from the first point and the last point
        /// </summary>
        /// <returns>The distance</returns>
        public double GetMaxLength()
        {
            var max = Points[Points.Count - 1];
            var min = Points[0];

            var total = Math.Abs(Math.Abs(max) - Math.Abs(min));

            return total;
        }

        /// <summary>
        ///     Adds a point to the line
        /// </summary>
        /// <param name="d">The point to be added to the line</param>
        /// <remarks>This automatically sorts it, so that the list will always have points correctly placed</remarks>
        public void AddToLine( double d )
        {
            Points.Add(d);
            Points.Sort();
        }
    }
}