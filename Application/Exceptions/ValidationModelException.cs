using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ValidationModelException : Exception
    {
        private const string _message = "Błąd walidacji danych";

        public IReadOnlyList<string> Validations { get; set;  }

        public ValidationModelException(IReadOnlyList<string> validations) : base(_message)
        {
            Validations = validations;
        }
    }
}
