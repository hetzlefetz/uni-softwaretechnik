using Moq;
using Verlag;

namespace VarlagTest
{
    public class Tests
    {
        readonly string EmptyBook = "";
        readonly string NullBook = null;
        readonly string Book1000 = new string('*', 1000);
        readonly string BookOnlyPics = "PicturePicturePicture";
        readonly string BookWithPicsAndText = "asdfPicturexyzPictureazbPictureqrs";


        CalcHonorar CalcHonorar = null;

        [SetUp]
        public void Setup()
        {
            CalcHonorar = new CalcHonorar();
        }

        [Test]
        public void ShouldReportZeroEarningsOnEmptyOrNull()
        {
            Assert.AreEqual(0, CalcHonorar.calc(EmptyBook));
            Assert.AreEqual(0, CalcHonorar.calc(NullBook));
            Assert.AreNotEqual(0, CalcHonorar.calc(Book1000));
            Assert.AreNotEqual(0, CalcHonorar.calc(BookOnlyPics));
            Assert.AreNotEqual(0, CalcHonorar.calc(BookWithPicsAndText));
        }


        [Test]
        public void ShouldCountPicsCorrectly()
        {
            Assert.AreEqual(0, CalcHonorar.countPics(Book1000));
            Assert.AreEqual(0, CalcHonorar.countPics(EmptyBook));
            Assert.AreEqual(0, CalcHonorar.countPics(NullBook));
            Assert.AreEqual(3, CalcHonorar.countPics(BookOnlyPics));
            Assert.AreEqual(3, CalcHonorar.countPics(BookWithPicsAndText));
        }

        [Test]
        public void ShouldCountTextCorrectly()
        {
            Assert.AreEqual(1000, CalcHonorar.countText(Book1000));
            Assert.AreEqual(0, CalcHonorar.countText(EmptyBook));
            Assert.AreEqual(0, CalcHonorar.countText(NullBook));
            Assert.AreEqual(0, CalcHonorar.countText(BookOnlyPics));
            Assert.AreEqual(13, CalcHonorar.countText(BookWithPicsAndText));
        }

        [Test]
        public void ShouldReportCorrectEarningsOnNonEmpty()
        {
            Assert.AreEqual(100, CalcHonorar.calc(Book1000));
            Assert.AreEqual(3, CalcHonorar.calc(BookOnlyPics));
            Assert.AreEqual(4.3M, CalcHonorar.calc(BookWithPicsAndText));
            Assert.AreEqual(0, CalcHonorar.calc(EmptyBook));
            Assert.AreEqual(0, CalcHonorar.calc(NullBook));
        }

        [Test]
        public void ShouldReportCorrectEarningsOnNonEmptyWithPricingService()
        {
            var pc = new Mock<IPricingService>();
            pc.Setup(x => x.GetPicturePrice()).Returns(10).Verifiable();
            pc.Setup(x => x.GetCharPrice()).Returns(1).Verifiable();

            var nc = new CalcHonorar(pc.Object);

            Assert.AreEqual(1000, nc.calc(Book1000));
            Assert.AreEqual(30, nc.calc(BookOnlyPics));
            Assert.AreEqual(43M, nc.calc(BookWithPicsAndText));
            Assert.AreEqual(0, nc.calc(EmptyBook));
            Assert.AreEqual(0, nc.calc(NullBook));

            pc.VerifyAll();
        }


    }
}