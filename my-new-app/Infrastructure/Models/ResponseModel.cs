namespace my_new_app.Infrastructure.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            Status = 1;
        }
        public int TotalRow { get; set; }

        public T Data  // حاوی شیء اصلی دیتا
        {
            get; set;
        }

        public System.Exception Exception // حاوی شیء استثنای رخ داده در حین اجرای دستورات
        {
            get; set;
        }

        public string Message // حاوی پیغام نتیجه اجرای دستور
        {
            get; set;
        }

        public int Status // حاوی پیغام نتیجه اجرای دستور
        {
            get; set;
        }
        public void SetError(string message)
        {
            Status = 2;
            Message = message;
        }
        public void Success(T data)
        {
            Status = 1;
            Data = data;
        }
    }
}