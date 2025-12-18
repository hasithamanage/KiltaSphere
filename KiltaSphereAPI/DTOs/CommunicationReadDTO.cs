namespace KiltaSphereAPI.DTOs
{
    public class CommunicationReadDTO
    {
        public int LogId { get; set; }
        public int MemberId { get; set; }
        public DateTime LogDate { get; set; }
        public string CommunicationType { get; set; } = string.Empty;
        public string SubjectSummary { get; set; } = string.Empty;
    }
}