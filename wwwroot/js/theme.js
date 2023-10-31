import {
    baseLayerLuminance,
    StandardLuminance
} from "/_content/Microsoft.Fast.Components.FluentUI/js/web-components-v2.5.16.min.js";

const themeSettingDark = "Dark";
const themeSettingLight = "Light";

export function getSystemTheme() {
    let matched = window.matchMedia('(prefers-color-scheme: dark)').matches;

    if (matched) {
        return themeSettingDark;
    } else {
        return themeSettingLight;
    }
}

function setInitialBaseLayerLuminance() {
    let theme = getSystemTheme();

    if (theme === themeSettingDark) {
        baseLayerLuminance.withDefault(StandardLuminance.DarkMode);
    } else /* Light */ {
        baseLayerLuminance.withDefault(StandardLuminance.LightMode);
    }
}

setInitialBaseLayerLuminance();