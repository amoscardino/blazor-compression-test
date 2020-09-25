namespace BlazorCompressionTest.Models
{
    public class CompressionResult
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public int Length { get => Data?.Length ?? 0; }
        public long Time { get; set; }
        public decimal PercentOfSource { get; set; }
    }
}