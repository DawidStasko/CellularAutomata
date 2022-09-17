using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;
using FluentAssertions;
using WPFUserInterface.Common;
using Xunit;

namespace CellularAutomata.Tests.Common;

public class BoolToBlackWhiteConverterTests
{
    private BoolToBlackWhiteConverter _sut;

    public BoolToBlackWhiteConverterTests()
    {
        _sut = new BoolToBlackWhiteConverter();
    }


    [Fact]
    public void Convert_ShouldReturnColor_WhenValueIsTrue()
    {
        var value = true;

        object color = _sut.Convert(value, null, null, null);

        color.Should().BeOfType<SolidColorBrush>().And.NotBe(Brushes.White);
    }

    [Fact]
    public void Convert_ShouldReturnWhite_WhenValueIsFalse()
    {
        var value = false;

        object color = _sut.Convert(value, null, null, null);

        color.Should().BeOfType<SolidColorBrush>().And.Be(Brushes.White);
    }

    [Theory]
    [MemberData(nameof(BrushesData))]
    public void ConvertBack_ShouldReturnTrue_WhenValueIsColor(SolidColorBrush brush)
    {
        object boolean = _sut.ConvertBack(brush, null, null, null);

        boolean.Should().BeOfType<bool>().And.Be(true);
    }

    [Fact]
    public void ConvertBack_ShouldReturnFalse_WhenValueIsWhite()
    {
        var value = Brushes.White;
        
        object boolean = _sut.ConvertBack(value, null, null, null);

        boolean.Should().BeOfType<bool>().And.Be(false);
    }


    public static IEnumerable<object[]> BrushesData => new List<object[]>()
    {
        new object[]{Brushes.ForestGreen}, 
        new object[]{Brushes.Aqua}, 
        new object[]{Brushes.Black}
    };
}