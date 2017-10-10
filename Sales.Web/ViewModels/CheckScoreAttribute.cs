using System.ComponentModel.DataAnnotations;

namespace Sales.Web.ViewModels
{
    public class CheckScoreAttribute : ValidationAttribute
    {
        private readonly decimal _minimumScore;

        public CheckScoreAttribute(double minimumScore) : base("Invalid score.")
        {
            _minimumScore = (decimal)minimumScore;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            decimal score = Score(value.ToString());
            if (score < _minimumScore)
                return new ValidationResult(string.Format("The name score must be greater than or equal to {0}, but was only {1}.",
                    _minimumScore, score));

            return ValidationResult.Success;
        }


        private decimal Score(string name)
        {
            if (string.IsNullOrEmpty(name))
                return 1m;

            name = name.Trim();

            int endingLength = -1;
            int startingLength = 0;
            while (startingLength > endingLength)
            {
                startingLength = name.Length;
                name = name.Replace("  ", " ");
                endingLength = name.Length;
            }

            string[] nameParts = name.Split(' ');

            int numberOfParts = nameParts.Length;
            int numberOfCharacters = name.Length - numberOfParts + 1;

            return (decimal)numberOfCharacters / (decimal)numberOfParts;
        }
    }
}
