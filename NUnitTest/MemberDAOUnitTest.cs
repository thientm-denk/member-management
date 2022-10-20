using BusinessObject;
using DataAccess.Repository;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTest
{
    public class MemberDAOUnitTest
    {
        //Để chạy test: chọn Test trên thanh taskbar, chọn run test
        // phím tắt chạy test: Ctrl R A

        // hàm nạy chạy đầu tiên khi vào test
        [SetUp]
        public void Setup()
        {
            memberRepository = new MemberRepository();
        }

        #region Demo Data
        MemberRepository memberRepository;
        public static List<MemberObject> members = new List<MemberObject>
            {
                new MemberObject{
                    MemberID = 2,
                    MemberName = "TranThien",
                    Email = "denk@gmail.com",
                    Password = "123456",
                    City = "RahGia",
                    Country = "KienGiang"
                },
                new MemberObject
                {
                    MemberID = 3,
                    MemberName = "Minh Dang",
                    Email = "DangDang@gmail.com",
                    Password = "123456",
                    City = "Dak lak",
                    Country = "Dak lak"
                },
                new MemberObject
                {
                    MemberID = 4,
                    MemberName = "Thanh BB",
                    Email = "Bobobo@psg.vn",
                    Password = "123456",
                    City = "Nauy",
                    Country = "AmericanThor"
                },
                new MemberObject
                {
                    MemberID = 5,
                    MemberName = "Lam Dang",
                    Email = "CR7@gmail.com",
                    Password = "123456",
                    City = "Wifub",
                    Country = "Tokyo"
                },
                new MemberObject
                {
                    MemberID = 6,
                    MemberName = "xnxxxRapid Dogge",
                    Email = "Hacker@gmail.com",
                    Password = "123456",
                    City = "Quan 9",
                    Country = "Ho Chi Minh"
                },
                new MemberObject
                {
                    MemberID = 1,
                    Email = "admin@fstore.com",
                    Password = "admin@@",
                    City = "",
                    Country = "",
                    MemberName = "Admin"
                }
            };
        #endregion

        

        #region Demo Basic Guild
        /*
        // một Test case cơ bản
        [Test]
        public void TestDemo()
        {
            // so sánh 2 giá trị cơ bản
            Assert.AreEqual(1, 1);

        }

        // TestCase với parameter truyền vào -> thử được nhiều data cũng lúc | Method cũng phải có parameter | số lượng parameter trên dưới phải giống nhau
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void TestCaseDemo(int a, int b)
        {
            // so sánh 2 giá trị 
            Assert.AreEqual(a, b);
        }

        // TestCase với parameter truyền vào đi kèm Expected luôn -> thử được nhiều data cùng lúc | Method cũng phải return giá trị
        //không cần Assert
        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 2)]

        public int TestCaseWithExeptedDemo(int a)
        {
            return a;
        }

        // Test xem có quăng lỗi ra không
        [Test]
        public void ExpectionDemo()
        {
            // thay method quăng lỗi thành method tương ứng
            // Assert.Throws<System.Exception>(() => MethodQuangLoi());
        }


        // Cơ bản nhiêu đó thôi. Viết code test từng method trong MemberDAO đi anh em =))
        // Tham khảo thêm tại https://docs.nunit.org/articles/nunit/writing-tests/attributes.html
        */

        #endregion

        #region Test Case
        // Vô code:
        // TEST METHOD GetDefaultMember()
        [Test]
        [Order(0)]
        public void LoginUnitTest()
        {
            // Trả về Tài khoảng trong file // appsettings
            // user "email": "admin@fstore.com",
            // "password": "admin@@"
            // Tra ve tai khoan admin nay neu dung
            var actual = memberRepository.Login("admin@fstore.com", "admin@@");
            var expected = new MemberObject
            {
                MemberID = 1,
                Email = "admin@fstore.com",
                Password = "admin@@",
                City = "",
                Country = "",
                MemberName = "Admin"
            };
            Assert.IsTrue(CompareTwoMemberObject(expected, actual));

            // sai tra ve null
            actual = memberRepository.Login("admin@fstore.com", "Ahihi");
            Assert.IsTrue(actual == null);


        }

        [Test]
        [Order(1)]
        public void ShowMemberListUnitTest()
        {
            //exception: show all members
            var actual = memberRepository.GetMembersList();
            var expected = members;            
            Assert.IsTrue(expected.SequenceEqual(actual, new MemberComparer()));
        }

        [Test]
        [Order(2)]
        [TestCase(1)]
        [TestCase(2)]
        public void SearchMemberByIdUnitTest(int id)
        {
            //exception: show all member by MemberID
            var actual = memberRepository.SearchMember(id);
            var expected = from member1 in members
                           where member1.MemberID == id
                           select member1;
            Assert.IsTrue(expected.SequenceEqual(actual,new MemberComparer()));
        }

        [Test]
        [Order(3)]
        [TestCase(-1)]
        public void SearchMemberByIdGivenWrongArgumentThrowsException(int id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => memberRepository.SearchMember(id));
        }
        #endregion

        #region Support Method
        public bool CompareTwoMemberObject(MemberObject A, MemberObject B)
        {
            if (A.MemberID != B.MemberID) return false;
            if (A.Email != B.Email) return false;
            if (A.Password != B.Password) return false;
            if (A.Country != B.Country) return false;
            if (A.City != B.City) return false;
            if (A.MemberName != B.MemberName) return false;
            return true;
        }
        #endregion

    }
}