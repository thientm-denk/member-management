using System;
using System.Collections.Generic;
using DataValidation;

namespace BusinessObject
{
    public class MemberObject
    {
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        private string email;
        public string Email
        {
            get { 
                return email; 
            }
            set {
                if (Validation.IsEmail(value))
                {
                    email = value;
                } else
                {
                    throw new Exception("Wrong format for Email!!");
                }
                
            }
        }


    }

    public class MemberComparer : IEqualityComparer<MemberObject>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(MemberObject x, MemberObject y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the member' properties are equal.
            return x.MemberID == y.MemberID && x.MemberName == y.MemberName 
                && x.Email==y.Email && x.Password == y.Password && x.City == y.City && x.Country == y.Country;
        }

        // If Equals() returns true for a pair of objects
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(MemberObject member)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(member, null)) return 0;

            int hashMemberName = member.MemberName == null ? 0 : member.MemberName.GetHashCode();
            int hashMemberEmail = member.Email == null ? 0 : member.Email.GetHashCode();
            int hashMemberPassword = member.Password == null ? 0 : member.Password.GetHashCode();
            int hashMemberCity = member.City == null ? 0 : member.City.GetHashCode();
            int hashMemberCountry = member.Country == null ? 0 : member.Country.GetHashCode();

            int hashMemberID = member.MemberID.GetHashCode();

            //Calculate the hash code for the product.
            return hashMemberID ^ hashMemberName ^ hashMemberEmail ^ hashMemberPassword
                ^ hashMemberCity ^ hashMemberCountry;
        }
    }
}
