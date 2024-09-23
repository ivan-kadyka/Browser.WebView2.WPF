using Browser.Core.UriResolver;
using Browser.Settings.Abstractions;
using Browser.Settings.Sections;

public class BrowserUriResolverTests
{
    private readonly BrowserUriResolver _uriResolver;
    
    private const string HomeAddress = "https://duckduckgo.com";
    private const string SearchAddress = "https://duckduckgo.com/?q={0}";
    
    public BrowserUriResolverTests()
    {
        var browserSettingsMock = Substitute.For<IBrowserSettings>();
        var generalSettings = new GeneralSettings { SearchAddress = SearchAddress, HomeAddress = HomeAddress };
        
        browserSettingsMock.General.Returns(generalSettings);

        // Instantiate the resolver with the mock
        _uriResolver = new BrowserUriResolver(browserSettingsMock);
    }

    [Theory]
    [InlineData("duckduckgo.com", "https://duckduckgo.com/")]
    [InlineData("http://duckduckgo.com", "http://duckduckgo.com/")]
    [InlineData("https://duckduckgo.com", "https://duckduckgo.com/")]
    [InlineData("apple.pl", "https://apple.pl/")]
    public void GetUri_ValidDomain_ReturnsCorrectUri(string input, string expected)
    {
        // Act
        var result = _uriResolver.GetUri(input);

        // Assert
        Assert.Equal(expected, result.ToString());
    }

    [Theory]
    [InlineData("invalidaddress")]
    [InlineData("invalid address")]
    public void GetUri_InvalidAddress_FormatsAsSearchAddress(string input)
    {
        // Act
        var result = _uriResolver.GetUri(input);

        // Assert
        Assert.StartsWith("https://duckduckgo.com/?q=", result.ToString());
    }

    [Theory]
    [InlineData("duckduckgo com", "https://duckduckgo.com/?q=duckduckgo+com")]
    [InlineData("apple", "https://duckduckgo.com/?q=apple")]
    public void GetUri_AddressWithSpaces_FormatsCorrectly(string input, string expected)
    {
        // Act
        var result = _uriResolver.GetUri(input);

        // Assert
        Assert.Equal(expected, result.ToString());
    }

    [Fact]
    public void GetUri_WithoutScheme_AddsHttps()
    {
        // Arrange
        string address = "example.com";

        // Act
        var result = _uriResolver.GetUri(address);

        // Assert
        Assert.Equal("https://example.com/", result.ToString());
    }

    [Fact]
    public void GetUri_WithExistingScheme_KeepsOriginalScheme()
    {
        // Arrange
        string address = "http://example.com";

        // Act
        var result = _uriResolver.GetUri(address);

        // Assert
        Assert.Equal("http://example.com/", result.ToString());
    }
}
