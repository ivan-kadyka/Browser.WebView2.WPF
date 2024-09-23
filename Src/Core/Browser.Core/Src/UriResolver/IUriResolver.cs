namespace Browser.Core.UriResolver;

public interface IUriResolver
{
    Uri GetUri(string address);
}