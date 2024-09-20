using Browser.Settings.Abstractions;
using Browser.Settings.Sections;

namespace Browser.Settings;

internal class BrowserSettings : IBrowserSettings
{
    public GeneralSettings General { get; set; }
}