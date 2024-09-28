namespace Browser.Core.Tests;

public class PageIdTests
{
    [Fact]
    public void PageId_New_ShouldReturnUniqueId()
    {
        // Act
        var pageId1 = PageId.New();
        var pageId2 = PageId.New();

        // Assert
        Assert.NotEqual(pageId1, pageId2); // New IDs should be unique
        Assert.NotEqual(pageId1.Value, pageId2.Value); // Value property should also be unique
    }

    [Fact]
    public void PageId_Constructor_ShouldSetValueProperty()
    {
        // Arrange
        var idValue = Guid.NewGuid().ToString();

        // Act
        var pageId = new PageId(idValue);

        // Assert
        Assert.Equal(idValue, pageId.Value);
    }

    [Fact]
    public void PageId_Constructor_ShouldThrowArgumentException_WhenValueIsEmpty()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new PageId(string.Empty));
        Assert.Equal("ID value cannot be empty (Parameter 'value')", exception.Message);
    }

    [Fact]
    public void PageId_Equals_ShouldReturnTrue_ForSameValueAndType()
    {
        // Arrange
        var idValue = Guid.NewGuid().ToString();
        var pageId1 = new PageId(idValue);
        var pageId2 = new PageId(idValue);

        // Act
        var result = pageId1.Equals(pageId2);

        // Assert
        Assert.True(result);
        Assert.True(pageId1 == pageId2);
        Assert.False(pageId1 != pageId2);
    }

    [Fact]
    public void PageId_Equals_ShouldReturnFalse_ForDifferentValues()
    {
        // Arrange
        var pageId1 = new PageId(Guid.NewGuid().ToString());
        var pageId2 = new PageId(Guid.NewGuid().ToString());

        // Act
        var result = pageId1.Equals(pageId2);

        // Assert
        Assert.False(result);
        Assert.False(pageId1 == pageId2);
        Assert.True(pageId1 != pageId2);
    }

    [Fact]
    public void PageId_ToString_ShouldReturnValue()
    {
        // Arrange
        var idValue = Guid.NewGuid().ToString();
        var pageId = new PageId(idValue);

        // Act
        var result = pageId.ToString();

        // Assert
        Assert.Equal(idValue, result);
    }

    [Fact]
    public void PageId_GetHashCode_ShouldReturnSameHashCode_ForSameValueAndType()
    {
        // Arrange
        var idValue = Guid.NewGuid().ToString();
        var pageId1 = new PageId(idValue);
        var pageId2 = new PageId(idValue);

        // Act
        var hash1 = pageId1.GetHashCode();
        var hash2 = pageId2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void PageId_GetHashCode_ShouldReturnDifferentHashCode_ForDifferentValues()
    {
        // Arrange
        var pageId1 = new PageId(Guid.NewGuid().ToString());
        var pageId2 = new PageId(Guid.NewGuid().ToString());

        // Act
        var hash1 = pageId1.GetHashCode();
        var hash2 = pageId2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }
}
