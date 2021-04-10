using System;
using FluentAssertions;
using Xunit;

namespace RPGAAS
{
    public interface IMap
    {
        string[,] MoveCoordinateToCoordinate(Tuple<int, int> coord1, Tuple<int, int> coord2);
    }


    public class NavigationTest
    {
        [Fact]
        public void When_ITellCharacterMoveNorth_Then_MapReflectsChange()
        {
            //arrange
            //Given map of
            string[,] map =
            {
                {"0", "0", "0"},
                {"0", "0", "0"},
                {"0", "1", "0"}
            };
            
            //act

            string[,] expectedMap = {
                {"0", "0", "0"},
                {"0", "0", "0"},
                {"0", "1", "0"}
            };
            
            //assert
            expectedMap.Should().BeEquivalentTo(map);
        }
    }
}
