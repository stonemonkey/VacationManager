namespace VacationManager.Services.Model
{
    public class VacationDays
    {
        public long Id { get; set; }

        public long Employee { get; set; }

        public int Year { get; set; }

        public int TotalNumber { get; set; }

        public int Taken { get; set; }

        public int Left { get; set; }

        public int Paid { get; set; }
    }
}