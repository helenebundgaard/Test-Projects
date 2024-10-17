using Xunit;
using Moq;
using System.Net.Sockets;
using System.IO;
using server;

namespace server_tests
{
    public class ClientHandlerTests
    {
        private readonly Mock<Calculator> _mockCalculator;
        private readonly Mock<Die> _mockDie;
        private readonly Mock<WeatherRequestHandler> _mockWeatherRequestHandler;
        private readonly Mock<ColorTheory> _mockColorTheory;

        public ClientHandlerTests()
        {
            _mockCalculator = new Mock<Calculator>();
            _mockDie = new Mock<Die>();
            _mockWeatherRequestHandler = new Mock<WeatherRequestHandler>();
            _mockColorTheory = new Mock<ColorTheory>();
        }

        [Fact]
        public void TestHandleRequestDice()
        {
            _mockDie.Setup(d => d.Roll()).Returns(4);
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, _mockColorTheory.Object);

            string response = handler.HandleRequest("dice");

            Assert.Equal("4", response);
        }

        [Theory]
        [InlineData("add 1 2", 3)]
        [InlineData("subtract 5 3", 2)]
        [InlineData("multiply 2 3", 6)]
        [InlineData("divide 6 2", 3)]
        public void TestHandleRequest_Calculator(string request, float expected)
        {
            switch (request.Split(' ')[0])
            {
                case "add":
                    _mockCalculator.Setup(c => c.Add(1, 2)).Returns(3);
                    break;
                case "subtract":
                    _mockCalculator.Setup(c => c.Subtract(5, 3)).Returns(2);
                    break;
                case "multiply":
                    _mockCalculator.Setup(c => c.Multiply(2, 3)).Returns(6);
                    break;
                case "divide":
                    _mockCalculator.Setup(c => c.Divide(6, 2)).Returns(3);
                    break;
            }

            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, _mockColorTheory.Object);

            string response = handler.HandleRequest(request);

            Assert.Equal(expected.ToString(), response);
        }

        [Fact]
        public void TestHandleRequest_DivideByZero()
        {
            _mockCalculator.Setup(c => c.Divide(6, 0)).Throws(new DivideByZeroException("Attempted to divide by zero."));
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, _mockColorTheory.Object);

            string response = handler.HandleRequest("divide 6 0");

            Assert.Equal("Attempted to divide by zero.", response);
        }

        [Fact]
        public void TestHandleRequest_Weather()
        {
            _mockWeatherRequestHandler.Setup(w => w.HandleWeatherRequest("weather Seattle")).Returns("Rainy");
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, _mockColorTheory.Object);

            string response = handler.HandleRequest("weather Seattle");

            Assert.Equal("Rainy", response);
        }

        [Fact]
        public void TestHandleRequest_Analogous()
        {
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, new ColorTheory());

            string response = handler.HandleRequest("analogous red");

            Assert.Equal("orange, purple", response);
        }

        [Fact]
        public void TestHandleRequest_Complementary()
        {
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, new ColorTheory());

            string response = handler.HandleRequest("complementary red");

            Assert.Equal("green", response);
        }

        [Fact]
        public void TestHandleRequest_AddColors()
        {
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, new ColorTheory());

            string response = handler.HandleRequest("addcolors red yellow");

            Assert.Equal("orange", response);
        }

        [Fact]
        public void TestHandleRequest_UnknownCommand()
        {
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, _mockColorTheory.Object);

            string response = handler.HandleRequest("unknowncommand");

            Assert.Equal("Unknown request", response);
        }

        [Fact]
        public void TestHandleRequest_InvalidRequest()
        {
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, _mockColorTheory.Object);

            string response = handler.HandleRequest("");

            Assert.Equal("Invalid request", response);
        }

        [Theory]
        [InlineData("add 1")]
        [InlineData("subtract 2")]
        [InlineData("multiply 2")]
        [InlineData("divide 2")]
        [InlineData("analogous")]
        [InlineData("complementary")]
        [InlineData("addcolors")]
        public void TestHandleRequest_InvalidParameters(string request)
        {
            var handler = new ClientHandler(_mockCalculator.Object, _mockDie.Object, _mockWeatherRequestHandler.Object, _mockColorTheory.Object);

            string response = handler.HandleRequest(request);

            Assert.Equal("Invalid parameters", response);
        }
        [Fact]
        public void TestHandleRequest_Dice()
        {
            var mockCalc = new Mock<Calculator>();
            var mockDie = new Mock<Die>();
            mockDie.Setup(die => die.Roll()).Returns(5);
            var mockWeatherRequestHandler = new Mock<WeatherRequestHandler>();
            var mockColorTheory = new Mock<ColorTheory>();

            var handler = new ClientHandler(mockCalc.Object, mockDie.Object, mockWeatherRequestHandler.Object, mockColorTheory.Object);

            string response = handler.HandleRequest("dice");

            Assert.Equal("5", response);
        }
    }
}
