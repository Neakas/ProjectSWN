namespace UniverseGeneration.Range_Objects 
{
    class FormationSegment : Range
    {
        public int ownershipFlag { get; set; }
        public int parentID { get; set; }

        public FormationSegment(int ownership, double lower, double upper)
            : base(lower, upper)
        {
            this.ownershipFlag = ownership;
            this.parentID = ownership;
        }

        public FormationSegment(int parentID, int ownership, double lower, double upper)
            : base(lower, upper)
        {
            this.ownershipFlag = ownership;
            this.parentID = parentID;
        }

        public FormationSegment(int ownership, Range incoming)
            : base(incoming)
        {
            this.ownershipFlag = ownership;
            this.parentID = ownership;
        }

        public FormationSegment(FormationSegment s) : base (s.lowerBound, s.upperBound)
        {
            this.parentID = s.parentID;
            this.ownershipFlag = s.ownershipFlag;
        }
        private FormationSegment() { }
    }
}
