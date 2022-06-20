using CalculatorApp;

namespace CalculatorAppTest
{
    public class Tests
    {
        Calculator c = null;
        [SetUp]
        public void Setup()
        {
            c = new Calculator();
        }

        [Test]
        public void SubShouldSubstract()
        {
            //Act
            var res = c.Sub(1, 1);

            //Assert
            Assert.AreEqual(0, res, 0, "Expected result to be 0");
        }

        [Test]
        public void DivShouldDivide()
        {
            //Act 
            var res = c.Div(1, 1);
            //Assert
            Assert.AreEqual(1, res);
        }

        [Test]
        public void DivShouldThrowOnDivByZero()
        {
            //Act
            var lambda = () => c.Div(1, 0);

            //Assert
            Assert.Throws<InvalidOperationException>(() => lambda());
        }

    }
}