﻿namespace HiwelairchawCekaywebeale;

class TestHost : IDisposable
{
    public TestHost(string host, WebApplication app)
    {
        Host = host;
        App = app;
    }

    public string Host { get; }
    public WebApplication App { get; }

    public void Dispose()
    {
        App.DisposeAsync();
    }
}