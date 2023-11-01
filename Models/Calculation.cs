using FluentValidation;

namespace CompanionApp.Models;

public class Calculation
{
    public int NumberOfPoint { get; set; }
    public int Dimension { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
    
}

public class CalculationValidator : AbstractValidator<Calculation>
{
    public CalculationValidator()
    {
        RuleFor(x => x.NumberOfPoint).GreaterThanOrEqualTo(1);
        RuleFor(x => x.Dimension).GreaterThanOrEqualTo(1);
        RuleFor(x => x.MarginLeft).LessThanOrEqualTo(x => x.Dimension - x.MarginRight).WithMessage("Left margin is too big.");
        RuleFor(x => x.MarginRight).LessThanOrEqualTo(x => x.Dimension - x.MarginLeft).WithMessage("Right margin is too big.");
        
    }
}