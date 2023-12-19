using CompanionApp.Client.Components;
using CompanionApp.Client.Enums;
using CompanionApp.Client.Services;
using CompanionApp.Client.States;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

namespace CompanionApp.Client.Pages;

public partial class Calculator
{
    [Inject] public IDialogService DialogService { get; set; } = default!;
    [Inject] public CalculationSettingsStateContainer Settings { get; set; } = default!;
    [Inject] public CalculationStateContainer State { get; set; } = default!;
    [Inject] public IHoleCalculationService Service { get; set; } = default!;
    [Inject] public IToastService ToastService { get; set; } = default!;

    public void Dispose()
    {
        Settings.OnStateChange -= StateHasChanged;
        GC.SuppressFinalize(this);
    }

    protected override void OnInitialized()
    {
        Settings.OnStateChange += StateHasChanged;
        base.OnInitialized();
    }

    private void Calculate()
    {
        if (State.Length <= 0D)
        {
            ToastService.ShowWarning("Please enter a length greater than 0.");
            return;
        }

        var offset = Service.CalculateOffset(State.Length,
            State.NumberOfHoles,
            marginLeft: Settings.PreciseMargin ? State.MarginLeft : State.Margin,
            marginRight: Settings.PreciseMargin ? State.MarginRight : State.Margin);
        
        State.SetOffset(offset);

        var roundedInterval = Service.CalculateDistance(
            offset,
            State.NumberOfHoles,
            Settings.PreciseMargin ? State.MarginLeft : State.Margin,
            Settings.PreciseMargin ? State.MarginRight : State.Margin);
        State.SetRoundedInterval(roundedInterval);

        var interval = Service.CalculateDistance(State.Length,
            State.NumberOfHoles, Settings.PreciseMargin ? State.MarginLeft : State.Margin,
            Settings.PreciseMargin ? State.MarginRight : State.Margin);
        State.SetInterval(interval);

        var holes = Service.GenerateHoles(State.RoundedInterval, State.Interval,
            State.NumberOfHoles, State.Offset, Settings.PreciseMargin ? State.MarginLeft : State.Margin);
        State.SetHoles(holes);

        State.SetShowResultCard(true);
    }

    private async Task OpenCalculationSettings()
    {
        DialogParameters parameters = new()
        {
            Title = "Calculation settings",
            PrimaryAction = "Ok",
            SecondaryAction = null,
            Alignment = HorizontalAlignment.Right,
            ShowDismiss = true
        };
        var dialog = await DialogService.ShowPanelAsync<CalculationSettings>(parameters);
        _ = await dialog.Result;
    }
}