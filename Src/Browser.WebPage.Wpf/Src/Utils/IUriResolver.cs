using System;

namespace Browser.WebPage.Wpf.Utils;

public interface IUriResolver
{
    Uri ToUri(string address);
}