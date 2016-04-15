namespace UniverseGeneration.Range_Objects
{
    internal class FormationSegment : Range
    {
        public FormationSegment( int ownership, double lower, double upper ) : base(lower, upper)
        {
            OwnershipFlag = ownership;
            ParentId = ownership;
        }

        public FormationSegment( int parentId, int ownership, double lower, double upper ) : base(lower, upper)
        {
            OwnershipFlag = ownership;
            ParentId = parentId;
        }

        public FormationSegment( int ownership, Range incoming ) : base(incoming)
        {
            OwnershipFlag = ownership;
            ParentId = ownership;
        }

        public FormationSegment( FormationSegment s ) : base(s.LowerBound, s.UpperBound)
        {
            ParentId = s.ParentId;
            OwnershipFlag = s.OwnershipFlag;
        }

        public int OwnershipFlag { get; set; }
        public int ParentId { get; set; }
    }
}