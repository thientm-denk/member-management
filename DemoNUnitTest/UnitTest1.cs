using DataAccess.Repository;

namespace DemoNUnitTest
{
    public class Tests
    {
        MemberRepository memberRepository;

        [SetUp]
        public void Setup()
        {
            memberRepository = new MemberRepository();
        }

        [Test]
        public void TestLoginMethodLoginWithAdmin()
        {
            var actual = memberRepository.Login("admin@fstore.com", "admin@@");
            Assert.AreEqual("admin@fstore.com", actual.Email);
            Assert.AreEqual(1, actual.MemberID);
        }

        [Test]
        public void TestLoginMethodLoginWithWrongAdmin()
        {
            var actual = memberRepository.Login("admin@fstore.com", "ahihi");
            Assert.IsNull(actual);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [Ignore("Thang Dev hen ngay 23 sua code xong", Until = "2022-10-23 12:00:00")]
        public void TestSearchMemberMethod(int id)
        {
            var inumMember = memberRepository.SearchMember(id);
            var actual = inumMember.ToList()[0];
            Assert.That(actual.MemberID, Is.EqualTo(id));
        }




    }
}