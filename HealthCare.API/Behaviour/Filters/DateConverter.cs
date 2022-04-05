namespace HealthCare.API.Behaviour.Filters
{
    using Newtonsoft.Json.Converters;

    public class DateConverter : IsoDateTimeConverter
    {
        public DateConverter() => 
            DateTimeFormat = Culture.DateTimeFormat.ShortDatePattern;
    }

}