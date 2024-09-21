using Browser.Settings.Sections;

namespace Browser.Settings.Abstractions;

public interface IBrowserSettings
{
    GeneralSettings General { get; set; }
}