using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.JSInterop;

namespace CompanionApp.Shared;

public partial class MainLayout : IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    private ElementReference _container;
    private IJSObjectReference? _jsModule;
    private bool _darkMode;


    private StandardLuminance _baseLayerLuminance = StandardLuminance.LightMode;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Shared/MainLayout.razor.js");
            GlobalState.SetContainer(_container);

            _darkMode = await _jsModule!.InvokeAsync<bool>("isDarkMode");
            if (_darkMode)
            {
                UpdateTheme();
                StateHasChanged();
            }
        }
    }

    public async void UpdateTheme()
    {
        _baseLayerLuminance = _darkMode ? StandardLuminance.DarkMode : StandardLuminance.LightMode;

        await BaseLayerLuminance.SetValueFor(_container, _baseLayerLuminance.GetLuminanceValue());

        GlobalState.SetLuminance(_baseLayerLuminance);
    }


    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            if (_jsModule is not null)
            {
                await _jsModule.DisposeAsync();
            }
        }
        catch (JSDisconnectedException)
        {
            // The JSRuntime side may routinely be gone already if the reason we're disposing is that
            // the client disconnected. This is not an error.
        }

        GC.SuppressFinalize(this);
    }
}