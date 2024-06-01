namespace Maxula_project.Services
{
    public class DateService
    {
        public DateOnly? StartValue { get; set; } = new DateOnly(2023, 6, 01);
        public DateOnly? EndValue { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public bool HasSearched { get; set; } = false;
    }

}
