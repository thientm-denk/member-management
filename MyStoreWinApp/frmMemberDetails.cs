using BusinessObject;
using DataAccess.Repository;
using DataValidation;
using System;
using System.Windows.Forms;

namespace MyStoreWinApp
{
    public partial class frmMemberDetails : Form
    {
        public bool InsertOrUpdate { get; set; }
        public IMemberRepository memberRepository { get; set; }
        public MemberObject memberInfo { get; set; }
        public bool isAdmin { get; set; } // true/false
        public frmMemberDetails()
        {
            InitializeComponent();
        }
        private void frmMemberDetails_Load(object sender, EventArgs e)
        {
            if (InsertOrUpdate) // Insert
            {
                btnAdd.Visible = true;
                btnLogout.Visible = true;
                btnUpdate.Visible = false;
                btnLogout.Visible = false;
            } else
            {
                btnAdd.Visible = false;
                if (!isAdmin)
                {
                    btnLogout.Visible = true;
                }
                btnUpdate.Visible = true;
                txtMemberID.Enabled = false;

                txtMemberID.Text = memberInfo.MemberID.ToString() ;
                txtMemberName.Text = memberInfo.MemberName;
                txtEmail.Text = memberInfo.Email;
                txtPassword.Text = memberInfo.Password;
                txtConfirm.Text = memberInfo.Password;
                txtCity.Text = memberInfo.City;
                txtCountry.Text = memberInfo.Country;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validation.IsEmail(txtEmail.Text))
                {
                    throw new Exception("Wrong Email!");
                }
                if (!txtPassword.Text.Equals(txtConfirm.Text))
                {
                    throw new Exception("Confirm does not match with Password!!!");
                }

                MemberObject member = new MemberObject
                {
                    MemberID = memberInfo.MemberID,
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
                memberRepository.UpdateMember(member);
                MessageBox.Show("Update successfully!!", "Update member", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMemberName.Text = member.MemberName;
                txtEmail.Text = member.Email;
                txtPassword.Text = member.Password;
                txtConfirm.Text = member.Password;
                txtCity.Text = member.City;
                txtCountry.Text = member.Country;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update member", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin { };
            frmLogin.Closed += (s, args) => this.Close();
            this.Hide();
            frmLogin.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Validation.IsEmail(txtEmail.Text))
                {
                    throw new Exception("Wrong Email!");
                }
                if (!txtPassword.Text.Equals(txtConfirm.Text))
                {
                    throw new Exception("Confirm does not match with Password!!!");
                }

                MemberObject member = new MemberObject
                {
                    MemberID = int.Parse(txtMemberID.Text),
                    MemberName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text
                };
                memberRepository.AddMember(member);
                MessageBox.Show("Add successfully!!", "Add member", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add member", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
