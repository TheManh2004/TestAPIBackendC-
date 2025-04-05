namespace TestAPI.Models
{
    public class UploadedFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }

}
