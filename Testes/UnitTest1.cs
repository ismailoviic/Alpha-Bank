using System;
using Xunit;

namespace Alpha_Bank
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var x = 5;
            var result = x * x;
            Assert.Equal(25, result);

        }
    }
}
