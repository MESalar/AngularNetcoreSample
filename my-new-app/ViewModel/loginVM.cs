using System;

namespace my_new_app.ViewModel
{
    public class loginVM
    {
        public string NationalityCode { get; set; }
        public string Password { get; set; }
    }
    public class loginResponseVM
    {
        public Guid token { get; set; }
        public string DisplayName { get; set;}
    }

}