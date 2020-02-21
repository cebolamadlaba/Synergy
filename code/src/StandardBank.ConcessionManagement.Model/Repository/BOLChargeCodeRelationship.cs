namespace StandardBank.ConcessionManagement.Model.Repository
{
    public class BOLChargeCodeRelationship
    {
        public int pkBOLChargeCodeRelationshipId { get; set; }
        public int fkChargeCodeTypeId { get; set; }
        public int fkChargeCodeId { get; set; }
    }
}