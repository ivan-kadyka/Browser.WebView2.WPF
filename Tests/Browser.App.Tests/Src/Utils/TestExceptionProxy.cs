using Browser.Abstractions.Exceptions;

namespace Browser.App.Tests.Utils;

internal class TestExceptionProxy
{
    private PageError? _pageError;
    private bool _activated;
    
    public void PrepareRaiseException(PageError? error = null)
    {
        _activated = true;
        _pageError = error;
    }   
    
    public void RaiseIfActivated()
    {
        if (_activated)
        {
            _activated = false;
            
            if (_pageError.HasValue)
            {
                throw new BrowserPageException(PageError.Reload, "Test reload page exception");
            }
            else
            {
                throw new NullReferenceException("Test null reference exception");
            }
        }
    }
}