﻿using Browser.Abstractions;
using Browser.Abstractions.Page;

namespace Browser.Core.Commands;

public class CreateBrowserPageCommand : CommandBase<IPageCreateOptions>
{
    private readonly IBrowser _browser;

    public CreateBrowserPageCommand(IBrowser browser)
    {
        _browser = browser;
    }
    
    protected override void OnExecute(IPageCreateOptions? options)
    {
        _browser.CreatePage(options);
    }
}


    