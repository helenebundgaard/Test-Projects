using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using server;

namespace server_tests
{
    public class DieTests
    {
        private const int NumberOfRollsForRangeTest = 1000;
        private const int NumberOfRollsForDistributionTest = 60000;
        private const double Tolerance = 0.1;

        [Fact]
        public void Roll_ShouldAlwaysReturnValueWithinRange()
        {
            // Arrange
            var die = new Die();
            bool allWithinRange = true;

            // Act
            for (int i = 0; i < NumberOfRollsForRangeTest; i++)
            {
                int roll = die.Roll();
                if (roll < 1 || roll > 6)
                {
                    allWithinRange = false;
                    break;
                }
            }

            // Assert
            Assert.True(allWithinRange, "Die roll should always be between 1 and 6 inclusive.");
        }

        [Fact]
        public void Roll_ShouldReturnApproximatelyUniformDistribution()
        {
            // Arrange
            var die = new Die();
            int[] rollCounts = new int[6];

            // Act
            for (int i = 0; i < NumberOfRollsForDistributionTest; i++)
            {
                int roll = die.Roll();
                rollCounts[roll - 1]++;
            }

            // Assert
            double expectedCount = NumberOfRollsForDistributionTest / 6.0;
            for (int i = 0; i < 6; i++)
            {
                double actualCount = rollCounts[i];
                double deviation = Math.Abs(actualCount - expectedCount) / expectedCount;
                Assert.True(deviation < Tolerance, $"Die roll {i + 1} is not within {Tolerance * 100}% of the expected count.");
            }
        }
    }
}
