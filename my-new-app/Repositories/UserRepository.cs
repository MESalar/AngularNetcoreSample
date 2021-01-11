using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace my_new_app.Repositories
{
    public class UserRepository
    {
        Models.FlightContext _db;
        IHttpContextAccessor _httpContex;



        public UserRepository(Models.FlightContext db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContex = httpContext;
        }
        public bool TokenIsValid(Guid token)
        {
            return _db.Tokens.Any(p => p.Token1 == token && p.ExpiredDate > DateTime.Now);
        }
        public int GetUserByToken(Guid token)
        {
            var tokenUser = _db.Tokens.FirstOrDefault(p => p.ExpiredDate > DateTime.Now && p.Token1 == token);
            if (tokenUser != null)
                return tokenUser.UserId;
            return 0;
        }

        public int GetCurrentUserId()
        {
            var token = new Guid(_httpContex.HttpContext.Request.Headers["csrf"]);
            var tokenUser = _db.Tokens.FirstOrDefault(p => p.ExpiredDate > DateTime.Now && p.Token1 == token);
            if (tokenUser != null)
                return tokenUser.UserId;
            return 0;
        }

        public bool Logout(Guid token)
        {
            var find = _db.Tokens.FirstOrDefault(p => p.Token1 == token);
            if (find != null)
                _db.Remove(find);
            _db.SaveChanges();
            return true;
        }

        public ViewModel.loginResponseVM Login(string nationalityCode, string password)
        {
            string hashed = hashPassword(password);
            Models.User user = _db.Users.FirstOrDefault(p => p.Password == hashed && p.NationalityCode == nationalityCode);
            if (user == null)
                return null;

            var token = new Models.Token() { CreateDate = DateTime.Now, UserId = user.Id, Token1 = Guid.NewGuid(), ExpiredDate = DateTime.Now.AddDays(10) };
            _db.Tokens.Add(token);
            _db.SaveChanges();
            return new ViewModel.loginResponseVM(){ token= token.Token1, DisplayName = user.FirstName+" "+user.LastName};
        }


        public int Register(string firstName, string lastName, string NationalityCode, string passWord)
        {
            string hashedPass = hashPassword(passWord);
            Models.User user = new Models.User() { FirstName = firstName, LastName = lastName, NationalityCode = NationalityCode, Password = hashedPass, CreateDate = DateTime.Now };
            _db.Users.Add(user);
            _db.SaveChanges();
            return user.Id;
        }

        String hashPassword(string pass)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(pass);
            var md5data = md5.ComputeHash(data);
            var hashedPassword = Encoding.ASCII.GetString(md5data);
            return hashedPassword;
        }

    }
}