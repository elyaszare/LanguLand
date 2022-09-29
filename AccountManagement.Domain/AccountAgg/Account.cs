using System.Collections.Generic;
using _0_Framework;
using BlogManagement.Domain.ArticleAgg;
using CourseManagement.Domain.CourseAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : EntityBase
    {
        public string Fullname { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public string ProfilePhoto { get; private set; }
        public string ActiveCode { get; private set; }
        public bool IsActive { get; private set; }
        public List<Article> Articles { get; set; }
        public List<Course> TeacherCourses { get; set; }
        public List<Course> StudentCourses { get; set; }

        public Account(string fullname, string username, string email, string mobile, string password,
            string profilePhoto, string activeCode)
        {
            Fullname = fullname;
            Username = username;
            Email = email;
            Mobile = mobile;
            Password = password;
            ProfilePhoto = profilePhoto;
            ActiveCode = activeCode;
            IsActive = false;
        }

        public void Edit(string fullname, string username, string email, string mobile,
            string profilePhoto, string activeCode)
        {
            Fullname = fullname;
            Username = username;
            Email = email;
            Mobile = mobile;

            if (!string.IsNullOrWhiteSpace(profilePhoto))
                ProfilePhoto = profilePhoto;

            ActiveCode = activeCode;
            IsActive = false;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void Active()
        {
            IsActive = true;
        }
    }
}
