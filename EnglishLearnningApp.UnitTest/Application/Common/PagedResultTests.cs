using EnglishLearning.Application.Common;

namespace EnglishLearnningApp.UnitTest.Application.Common;

public class PagedResultTests
{
    [Fact]
    public void Create_ShouldCalculateTotalPagesCorrectly_ExactDivision()
    {
        var items = new List<string> { "a", "b", "c", "d", "e" };
        var result = PagedResult<string>.Create(items, 1, 5, 10);

        result.TotalPages.Should().Be(2);
        result.PageNumber.Should().Be(1);
        result.TotalRecords.Should().Be(10);
    }

    [Fact]
    public void Create_ShouldCalculateTotalPagesCorrectly_RemainingRecords()
    {
        var items = new List<string> { "a", "b", "c" };
        var result = PagedResult<string>.Create(items, 2, 5, 12);

        result.TotalPages.Should().Be(3);
    }

    [Fact]
    public void Create_WithSinglePage_ShouldReturnTotalPagesOne()
    {
        var items = new List<int> { 1, 2, 3 };
        var result = PagedResult<int>.Create(items, 1, 10, 3);
        result.TotalPages.Should().Be(1);
    }

    [Fact]
    public void Create_WithZeroRecords_ShouldReturnTotalPagesZero()
    {
        var items = new List<string>();
        var result = PagedResult<string>.Create(items, 1, 10, 0);
        result.TotalPages.Should().Be(0);
    }
}
