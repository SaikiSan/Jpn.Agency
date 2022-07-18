using System.Text.RegularExpressions;

namespace JPN.Core.DomainObjects
{
    public class Phone
    {
        public const int NumerMaxLength = 14;
        public string Number { get; set; }

        protected Phone() {}

        public Phone(string number)
        {
            //if (!Validation(number)) throw new DomainException("Numero de telefone inválido");
            Number = number;
        }

        private bool Validation(string number)
        {
            var regexNumber = new Regex(@"(@”^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$”)");
            return regexNumber.IsMatch(number);
        }
    }
}
