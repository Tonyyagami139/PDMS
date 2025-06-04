using Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Utility;

namespace Dal
{
    public class UserDal
    {
        public UserBll userBll { get; set; }

        public UserDal(string Setting)
        {
            userBll = new UserBll(Setting);
        }
        public bool Login(string userName, string password)
        {
            string pwd = userBll.Login(userName);
            if (string.IsNullOrEmpty(pwd)) return false;
            string md5Password = Md5Helper.GetMd5Hash(password);

            if (md5Password.ToUpper() == pwd.ToUpper())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetAccessLevel(string userName)
        {
            string level = userBll.GetAccessLevel(userName);
            int accessLevel = -1;
            int.TryParse(level, out accessLevel);
            return accessLevel;
        }

        public bool HaveAccess(string userName)
        {
            int accessLevel = GetAccessLevel(userName);
            if (accessLevel > 0)
            {
                if (accessLevel <= 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public bool HaveAccess(int level)
        {
            int accessLevel = level;
            if (accessLevel > 0)
            {
                if (accessLevel <= 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
