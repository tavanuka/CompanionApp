using CompanionApp.Client.Enums;
using CompanionApp.Client.Extensions;

namespace CompanionApp.Client.Components;

public partial class CalculationSettings
{
    private void SetPreciseMargin(bool value)
    {
        Settings.SetPreciseMargin(value);
        State.SetMarginRight(0);
    }

    private void SetMeasurementType(MeasurementType value)
    {
        Settings.SetMeasurementType(value);
        // ShiftResultDecimalPoint();
    }

    // Unnecessary and not needed. We would have to change every single value. Leaving it in as a showcase.
    private void ShiftResultDecimalPoint()
    {
        switch (Settings.MeasurementType)
        {
            case MeasurementType.Millimeter:
                var mm = State.Interval.ShiftDecimalPoint(-1);
                var mmRound = State.RoundedInterval.ShiftDecimalPoint(-1);
                State.SetInterval(mm);
                State.SetRoundedInterval(mmRound);
                break;
            case MeasurementType.Centimeter:
                var cm = State.Interval.ShiftDecimalPoint(1);
                var cmRound = State.RoundedInterval.ShiftDecimalPoint(1);
                State.SetInterval(cm);
                State.SetRoundedInterval(cmRound);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}