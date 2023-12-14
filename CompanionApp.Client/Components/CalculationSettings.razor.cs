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
                var mm = State.DistanceBetweenHoles.ShiftDecimalPoint(-1);
                var mmRound = State.RoundedDistanceBetweenHoles.ShiftDecimalPoint(-1);
                State.SetDistanceBetweenHoles(mm);
                State.SetRoundedDistanceBetweenHoles(mmRound);
                break;
            case MeasurementType.Centimeter:
                var cm = State.DistanceBetweenHoles.ShiftDecimalPoint(1);
                var cmRound = State.RoundedDistanceBetweenHoles.ShiftDecimalPoint(1);
                State.SetDistanceBetweenHoles(cm);
                State.SetRoundedDistanceBetweenHoles(cmRound);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}