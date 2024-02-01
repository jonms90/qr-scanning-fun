namespace QRScanningFun.Application.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(1,1,2)]
        [InlineData(13,1,14)]
        [InlineData(11,11,22)]
        [InlineData(15,12,27)]
        [InlineData(19,19,38)]
        [InlineData(10,0,10)]
        public void Test1(int x, int y, int expectedResult)
        {
            Assert.Equal(expectedResult, new Class1().Sum(x,y));
        }
    }
}