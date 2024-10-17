using server;
using System.Net.Sockets;
using System.Net;
using Moq;

namespace server_tests
{

    public class CalculatorTests
    {
        private readonly Calculator _calculator;

        public CalculatorTests()
        {
            _calculator = new Calculator();
        }

        [Fact]
        public void Add_ReturnsCorrectSum()
        {
            // Arrange
            float x = 2;
            float y = 3;
            float expected = 5;

            // Act
            float result = _calculator.Add(x, y);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Subtract_ReturnsCorrectDifference()
        {
            // Arrange
            float x = 5;
            float y = 3;
            float expected = 2;

            // Act
            float result = _calculator.Subtract(x, y);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Multiply_ReturnsCorrectProduct()
        {
            // Arrange
            float x = 2;
            float y = 3;
            float expected = 6;

            // Act
            float result = _calculator.Multiply(x, y);

            // Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Divide_ReturnsCorrectQuotient()
        {
            // Arrange
            float x = 6;
            float y = 3;
            float expected = 2;

            // Act
            float result = _calculator.Divide(x, y);

            // Assert
            Assert.Equal(expected, result);
        }
        [Fact]
        public void Divide_ByZero_ShouldThrowDivideByZeroException()
        {
            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
        }
    }
}