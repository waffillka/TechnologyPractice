namespace Entities.RequestFeatures
{
    public class ContactParameters : RequestParameters
    {
        public int CountLettersMin { get; set; } = 0;
        public int CountLettersMax { get; set; } = int.MaxValue;

        public string SearchTerm { get; set; } = string.Empty;
    }
}
