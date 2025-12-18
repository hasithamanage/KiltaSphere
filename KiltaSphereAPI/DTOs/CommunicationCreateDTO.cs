namespace KiltaSphereAPI.DTOs
{
    public class CommunicationCreateDTO
    {
        public int MemberId { get; set; }
        public string CommunicationType { get; set; } = string.Empty;
        public string SubjectSummary { get; set; } = string.Empty;
    }
}