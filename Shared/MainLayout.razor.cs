using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.JSInterop;

namespace CompanionApp.Shared;

public partial class MainLayout : IAsyncDisposable
{
    private StandardLuminance _baseLayerLuminance = StandardLuminance.LightMode;

    private ElementReference _container;

    private bool _darkMode;
    private IJSObjectReference? _jsModule;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            if (_jsModule is not null) await _jsModule.DisposeAsync();
        }
        catch (JSDisconnectedException)
        {
            // The JSRuntime side may routinely be gone already if the reason we're disposing is that
            // the client disconnected. This is not an error.
        }

        GC.SuppressFinalize(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _jsModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./Shared/MainLayout.razor.js");
            GlobalState.SetContainer(_container);

            _darkMode = await _jsModule!.InvokeAsync<bool>("isDarkMode");
            _baseLayerLuminance = _darkMode ? StandardLuminance.DarkMode : StandardLuminance.LightMode;
            GlobalState.SetLuminance(_baseLayerLuminance);
            StateHasChanged();
        }
    }

    public async void UpdateTheme()
    {
        _baseLayerLuminance = _darkMode ? StandardLuminance.DarkMode : StandardLuminance.LightMode;
        await BaseLayerLuminance.WithDefault(_baseLayerLuminance.GetLuminanceValue());
        GlobalState.SetLuminance(_baseLayerLuminance);
    }
}