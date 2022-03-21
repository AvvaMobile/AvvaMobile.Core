using AvvaMobile.Core.Business;
using AvvaMobile.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AvvaMobile.Core
{
    public class BaseController : Controller
    {
        public void Message(ServiceResponse response)
        {
            if (response.Message.IsNotNull())
            {
                if (response.IsSuccess)
                {
                    Success(response.Message);
                }
                else
                {
                    Error(response.Message);
                }
            }
        }

        public void Success(string message)
        {
            TempData["SucessMessage"] = message;
        }

        public void Warning(string message)
        {
            TempData["WarningMessage"] = message;
        }

        public void Error(string message)
        {
            TempData["ErrorMessage"] = message;
        }

        public int CurrentUserID
        {
            get
            {
                return int.Parse(User.Identities.First(u => u.IsAuthenticated)?.FindFirst("ID")?.Value ?? "0");
            }
        }

        public bool HasRight(string right)
        {
            return User.Identities.First().FindAll(ClaimTypes.Role).FirstOrDefault(r => r.Value == right) != null;
        }
    }
}