namespace HealthCare.Exceptions
{
    using System;

    public class InvalidViewLocationException : Exception
    {
        private readonly string _name;
        private readonly string _searchedLocations;

        public InvalidViewLocationException(string name, string searchedLocations)
        {
            _name = name;
            _searchedLocations = searchedLocations;
        }

        public string StatusCode => "InvalidViewLocation";

        public override string Message => $"Unable to find view '{_name}'. The following locations were searched: {_searchedLocations}";
    }
}