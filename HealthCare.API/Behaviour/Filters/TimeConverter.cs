namespace HealthCare.API.Behaviour.Filters
{
    using Newtonsoft.Json.Converters;

    public class TimeConverter: IsoDateTimeConverter
    {
        public TimeConverter() =>
            DateTimeFormat = Culture.DateTimeFormat.ShortTimePattern;
    }
}
