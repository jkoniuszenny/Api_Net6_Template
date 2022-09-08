namespace Application.Exceptions
{
    public class ValidationModelException : Exception
    {
        private const string _message = "Błąd walidacji danych";

        public IReadOnlyList<string> Validations { get; set; }

        public ValidationModelException(IReadOnlyList<string> validations) : base(_message)
        {
            Validations = validations;
        }
    }
}
