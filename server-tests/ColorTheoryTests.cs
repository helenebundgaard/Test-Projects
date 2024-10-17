using System;
using Xunit;
using server;

namespace server_tests
{
    public class ColorTheoryTests
    {
        [Fact]
        public void TestAnalogousColors_Red()
        {
            string[] expected = { "orange", "purple" };
            string[] actual = ColorTheory.GetAnalogousColors("red");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAnalogousColors_Blue()
        {
            string[] expected = { "green", "purple" };
            string[] actual = ColorTheory.GetAnalogousColors("blue");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestComplementaryColor_Red()
        {
            string expected = "green";
            string actual = ColorTheory.GetComplementaryColor("red");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestComplementaryColor_Yellow()
        {
            string expected = "purple";
            string actual = ColorTheory.GetComplementaryColor("yellow");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddColors_RedAndYellow()
        {
            string expected = "orange";
            string actual = ColorTheory.AddColors("red", "yellow");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddColors_RedAndBlue()
        {
            string expected = "purple";
            string actual = ColorTheory.AddColors("red", "blue");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddColors_YellowAndBlue()
        {
            string expected = "green";
            string actual = ColorTheory.AddColors("yellow", "blue");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddColors_RedYellowBlue()
        {
            string expected = "brown";
            string actual = ColorTheory.AddColors("red", "yellow", "blue");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddColors_BlueOrange()
        {
            string expected = "brown";
            string actual = ColorTheory.AddColors("blue", "orange");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddColors_RedGreen()
        {
            string expected = "brown";
            string actual = ColorTheory.AddColors("red", "green");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestAddColors_YellowPurple()
        {
            string expected = "brown";
            string actual = ColorTheory.AddColors("yellow", "purple");
            Assert.Equal(expected, actual);
        }

    }
}

