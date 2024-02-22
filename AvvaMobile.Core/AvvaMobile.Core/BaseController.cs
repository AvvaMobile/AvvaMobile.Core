using AvvaMobile.Core.Business;
using AvvaMobile.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AvvaMobile.Core
{
    public class BaseController : Controller
    {
        public void Message(ServiceResult response)
        {
            if (response.Message.IsNullOrEmtpy()) return;

            if (response.Type == ServiceResultType.Success)
            {
                Success(response.Message);
            }
            else if (response.Type == ServiceResultType.Warning)
            {
                Warning(response.Message);
            }
            else if (response.Type == ServiceResultType.Error)
            {
                Error(response.Message);
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
            get { return int.Parse(User.Identities.FirstOrDefault(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))?.FindFirst(ClaimTypes.NameIdentifier).Value); }
        }

        public string CurrentUserName
        {
            get { return User.Identities.FirstOrDefault(u => u.IsAuthenticated && u.HasClaim(c => c.Type == ClaimTypes.Name))?.FindFirst(ClaimTypes.Name).Value; }
        }

        public bool HasRight(string right)
        {
            return User.Identities.First().FindAll(ClaimTypes.Role).FirstOrDefault(r => r.Value == right) != null;
        }
    }
}