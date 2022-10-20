using BusinessObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public class MemberDAO
    {
        // Get Admin user 
        MemberObject GetDefaultMember()
        {
            MemberObject Default = null;
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
                string email = config["account:defaultAccount:email"];
                string password = config["account:defaultAccount:password"];
                Default = new MemberObject
                {
                    MemberID = 1,
                    Email = email,
                    Password = password,
                    City = "",
                    Country = "",
                    MemberName = "Admin"
                };
            }

            return Default;
        }

        // Tạo list member mẫu

        private static List<MemberObject> members = new List<MemberObject>
        {
            new MemberObject
            {
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
            }
        };
        private MemberDAO()
        {
            MemberObject DefaultAdmin = GetDefaultMember();
            if (DefaultAdmin != null)
            {
                members.Add(DefaultAdmin);
            }
        }

        // Using Singleton Pattern
        private static MemberDAO instance = null;
        private static object instanceLook = new object();

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<MemberObject> GetMembersList => members;

        public MemberObject Login(string Email, string Password)
        {
            return members.SingleOrDefault(mb => mb.Email.Equals(Email) && mb.Password.Equals(Password));
            
        }
        
        public MemberObject GetMember(int MemberId)
        {
            return members.SingleOrDefault(mb => mb.MemberID == MemberId);
        }
        public MemberObject GetMember(string MemberEmail)
        {
            return members.SingleOrDefault(mb => mb.Email.Equals(MemberEmail));
        }
        public void AddMember(MemberObject member)
        {
            if (member == null)
            {
                throw new Exception("Member is undefined!!");
            }

            if (GetMember(member.MemberID) == null && GetMember(member.Email) == null)
            {
                members.Add(member);
            } else
            {
                throw new Exception("Member is existed!!");
            }
        }
        public void Update(MemberObject member)
        {
            if (member == null)
            {
                throw new Exception("Member is undefined!!");
            }
            MemberObject mem = GetMember(member.MemberID);
            if (mem != null)
            {
                var index = members.IndexOf(mem);
                members[index] = member;
            } else
            {
                throw new Exception("Member does not exist!!");
            }
        }
        public void Delete(int MemberId)
        {
            MemberObject member = GetMember(MemberId);
            if (member != null)
            {
                members.Remove(member);
            } else
            {
                throw new Exception("Member does not exist!!");
            }
        }

        public IEnumerable<MemberObject> SearchMember(int id)
        {
            IEnumerable<MemberObject> searchResult = null;

            IEnumerable<MemberObject> memberSearch = from member in members
                               where member.MemberID == id
                               select member;
            searchResult = memberSearch;

            return searchResult;
        }
        public IEnumerable<MemberObject> SearchMember(string name)
        {
            IEnumerable<MemberObject> searchResult = null;

            var memberSearch = from member in members
                               where member.MemberName.ToLower().Contains(name.ToLower())
                               select member;
            searchResult = memberSearch;

            return searchResult;
        }

        public IEnumerable<MemberObject> FilterMemberByCountry(string country, IEnumerable<MemberObject> searchList)
        {
            IEnumerable<MemberObject> searchResult = null;

            var memberSearch = from member in searchList
                               where member.Country == country
                               select member;
            if (country.Equals("All"))
            {
                memberSearch = from member in searchList
                               select member;
            }
            searchResult = memberSearch;

            return searchResult;
        }

        public IEnumerable<MemberObject> FilterMemberByCity(string country, string city, IEnumerable<MemberObject> searchList)
        {
            IEnumerable<MemberObject> searchResult = null;

            var memberSearch = from member in searchList
                               where member.City == city
                               select member;
            if (city.Equals("All"))
            {
                memberSearch = from member in searchList
                               where member.Country == country
                               select member;
                if (country.Equals("All"))
                {
                    memberSearch = from member in searchList
                                   select member;
                }
            }
            searchResult = memberSearch;

            return searchResult;
        }
    }
}
