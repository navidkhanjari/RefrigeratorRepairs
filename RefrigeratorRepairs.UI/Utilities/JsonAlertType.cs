using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefrigeratorRepairs.UI.Utilities
{
    public class JsonAlertType
    {
        #region (Fields)
        public String Message { get; set; }
        public AlertType Type { get; set; }
        #endregion

        #region (Errors)
        public static JsonAlertType Error()
        {
            return new JsonAlertType()
            {
                Type = AlertType.Error,
                Message = "عملیات با خطا مواجه شد",
            };
        }
        public static JsonAlertType Error(String Message)
        {
            return new JsonAlertType()
            {
                Type = AlertType.Error,
                Message = Message,
            };
        }
        #endregion

        #region (Succsess)
        public static JsonAlertType Success()
        {
            return new JsonAlertType()
            {
                Type = AlertType.Success,
                Message = "عملیات با موفقیت انجام شد",
            };
        }
        public static JsonAlertType Success(String Message)
        {
            return new JsonAlertType()
            {
                Type = AlertType.Success,
                Message = Message,
            };
        }
        #endregion
    }

    #region (Enum)
    public enum AlertType
    {
        Error = 10,
        Success = 200
    }
    #endregion
}
