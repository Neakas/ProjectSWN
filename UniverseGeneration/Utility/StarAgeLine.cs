using System;

namespace UniverseGeneration.Utility
{
    /// <summary>
    ///     This class is used to track the current position of the star in it's growth sequence, as well as provide tracking
    ///     for dates.
    /// </summary>
    public class StarAgeLine : TimeLine
    {
        //flags

        /// <summary>
        ///     This flag is used to set or get the sequence for Main Sequence
        /// </summary>
        protected static readonly int AgMainlimit = 0;

        /// <summary>
        ///     This flag is used to set or get the sequence for Sub Giant branch
        /// </summary>
        protected static readonly int AgSublimit = 1;

        /// <summary>
        ///     This flag is used to set or get the sequence for the Asymptomic Giant Branch
        /// </summary>
        protected static readonly int AgGiantlimit = 2;

        /// <summary>
        ///     Flag to signify it's the mainbranch
        /// </summary>
        public static readonly int RetMainbranch = 20;

        /// <summary>
        ///     Flag to signify it's in the sub giant branch
        /// </summary>
        public static readonly int RetSubbranch = 21;

        /// <summary>
        ///     Flag to signify it's in the Asymptomic Giant Branch
        /// </summary>
        public static readonly int RetGiantbranch = 22;

        /// <summary>
        ///     Flag to signify it's a collasped star
        /// </summary>
        public static readonly int RetCollaspedstar = 23;

        /// <summary>
        ///     Error flag.
        /// </summary>
        public static readonly int RetError = -1;

        /// <summary>
        ///     Base Constructor
        /// </summary>
        public StarAgeLine()
        {
            InitList();
        }

        /// <summary>
        ///     Constructor assuming
        /// </summary>
        /// <param name="inLen"></param>
        public StarAgeLine( double[] inLen ) : base(inLen)
        {
        }

        /// <summary>
        ///     Copy constructor
        /// </summary>
        /// <param name="s">The StarAgeLine object being copied</param>
        public StarAgeLine( StarAgeLine s )
        {
            InitList();
            foreach (var d in s.Points)
            {
                Points.Add(d);
            }
        }

        /// <summary>
        ///     Gets the position within the Sub Giant Branch
        /// </summary>
        /// <param name="age">The age of the star</param>
        /// <returns>The position (0 - 1) within the branch. </returns>
        /// <exception cref="Exception">If the age is beyond the Sub Giant Branch, this function throws an exception</exception>
        public double CalcWithInSubLimit( double age )
        {
            if (age >= Points[AgSublimit]) //basic error checking.
            {
                throw new Exception("This star is beyond the Sub Giant Branch");
            }

            var pos = ( age - Points[AgMainlimit] ) / ( Points[AgSublimit] - Points[AgMainlimit] );

            return pos;
        }

        /// <summary>
        ///     Gets the position within the Asympotic Giant Branch
        /// </summary>
        /// <param name="age">The age of the Star</param>
        /// <returns>The position (0 - 1) within the branch.</returns>
        /// <exception cref="Exception">If the age is beyond the Asymptotic Giant Branch, this function throws an exception</exception>
        public double CalcWithInGiantLimit( double age )
        {
            if (age >= Points[AgGiantlimit]) //basic error checking.
            {
                throw new Exception("This star is beyond the Asymptotic Giant Branch");
            }

            var pos = ( age - Points[AgSublimit] ) / ( Points[AgGiantlimit] - Points[AgSublimit] );

            return pos;
        }

        /// <summary>
        ///     Returns the main sequence limit
        /// </summary>
        /// <returns>The main sequence limit</returns>
        public double GetMainLimit()
        {
            return Points[AgMainlimit];
        }

        /// <summary>
        ///     Returns the sub giant branch limit
        /// </summary>
        /// <returns>The sub giant branch limit</returns>
        public double GetSubLimit()
        {
            return Points[AgSublimit];
        }

        /// <summary>
        ///     Returns the Asymptotic Giant Branch Limit
        /// </summary>
        /// <returns>The Asymptotic Giant Branch Limit</returns>
        public double GetGiantLimit()
        {
            return Points[AgGiantlimit];
        }

        /// <summary>
        ///     A function to determine where you are in the age progression of a star
        /// </summary>
        /// <param name="currAge">The current age</param>
        /// <returns>Returns the flag for where you are</returns>
        public int FindCurrentAgeGroup( double currAge )
        {
            if (currAge < Points[AgMainlimit])
            {
                return RetMainbranch;
            }
            if (currAge < Points[AgSublimit])
            {
                return RetSubbranch;
            }
            if (currAge < Points[AgGiantlimit])
            {
                return RetGiantbranch;
            }
            if (currAge > Points[AgGiantlimit])
            {
                return RetCollaspedstar;
            }

            return RetError;
        }

        /// <summary>
        ///     This function adds the main sequence limit
        /// </summary>
        /// <param name="d">The limit of the main sequence</param>
        public void AddMainLimit( double d )
        {
            // if it's not added, add it.
            if (Points.Count > 1)
            {
                Points[AgMainlimit] = d;
            }
            else
            {
                Points.Add(d);
            }
        }

        /// <summary>
        ///     This function adds the Sub Giant Sequence Limit
        /// </summary>
        /// <param name="d">The limit of the Sub Giant Sequence</param>
        /// <exception cref="Exception">Throws an exception if the main sequence limit has not been set</exception>
        public void AddSubLimit( double d )
        {
            // if it's not added, add it. Throw an error if no one has set the main limit.
            if (Points.Count > 2)
            {
                Points[AgSublimit] = d + Points[AgMainlimit]; //add the main limit to this.
            }
            else if (Points.Count < 1)
            {
                throw new Exception("Main sequence limit has not been set.");
            }
            else
            {
                Points.Add(d + Points[AgMainlimit]);
            }
        }

        /// <summary>
        ///     This function adds the Asymptotic Giant Branch limit
        /// </summary>
        /// <param name="d">The limit of the Asymptotic Giant Branch</param>
        /// <exception cref="Exception">Throws an exception if the sublimit has not been set.</exception>
        public void AddGiantLimit( double d )
        {
            // if it's not added, add it. Throw an error if no one has set the sub limit.
            if (Points.Count > 3)
            {
                Points[AgGiantlimit] = d + Points[AgSublimit]; //add the sub limit to this.
            }
            else if (Points.Count < 2)
            {
                throw new Exception("Sublimit has not been set.");
            }
            else
            {
                Points.Add(d + Points[AgSublimit]);
            }
        }

        /// <summary>
        ///     This function returns the description of the flags.
        /// </summary>
        /// <param name="branch">The flag branch</param>
        /// <returns>The description</returns>
        public static string DescBranch( int branch )
        {
            if (branch == RetMainbranch)
            {
                return "Main Sequence";
            }
            if (branch == RetSubbranch)
            {
                return "Sub Giant Star";
            }
            if (branch == RetGiantbranch)
            {
                return "Asymptoic Giant Branch";
            }
            if (branch == RetCollaspedstar)
            {
                return "White Dwarf Branch";
            }

            return "ERROR";
        }

        /// <summary>
        ///     This function returns the age since collapse.
        /// </summary>
        /// <param name="age">The current age</param>
        /// <returns>The age from collapse</returns>
        /// <exception cref="Exception">Throws an error if you have not reached the collapse stage</exception>
        public double GetAgeFromCollapse( double age )
        {
            if (FindCurrentAgeGroup(age) != RetCollaspedstar)
            {
                throw new Exception("This star has not collapsed.");
            }

            return age - Points[AgGiantlimit];
        }
    }
}