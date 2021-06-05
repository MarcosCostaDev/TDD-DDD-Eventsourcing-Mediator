using Flunt.Notifications;
using Flunt.Validations;
using IntegrationTest.Core.Command;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IntegrationTest.Controllers
{
    [NotMapped]
    public abstract class CommonController : ControllerBase
    {


        protected IActionResult CommonResponse(CommandResult commandResult)
        {
            var response = new ResponseResult(commandResult);
            if (commandResult.IsValid)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        protected IActionResult CommonResponse(object value, Notifiable<Notification> notifiable = null)
        {
            var response = new ResponseResult(value, notifiable);

            if (response.Success)
            {
                if (value is AccessTokenResult)
                {
                    var res = value as AccessTokenResult;

                    return Ok(new
                    {
                        res.AccessToken,
                        res.RefreshToken,
                        res.ExpireIn,
                        res.TokenType
                    });
                }
                else
                {
                    return Ok(response);
                }
            }

            response.ClearObject();
            return BadRequest(response);

        }


    }

    public class ResponseResult
    {
        public ResponseResult(CommandResult commandResult)
        {
            Success = commandResult.Success;
            Notifications = commandResult.Notifications;
            if (commandResult.IsValid)
            {
                Object = commandResult.Object;
            }

        }
        public ResponseResult(object data)
        {

            if (data is Notifiable<Notification>)
            {
                var notifiable = data as Notifiable<Notification>;
                Success = notifiable.IsValid;
                Notifications = notifiable.Notifications;
            }

            Object = data;
        }

        public ResponseResult(object data, Notifiable<Notification> notifiable)
        {
            Object = data;
            Success = (notifiable?.IsValid).GetValueOrDefault(true);
            Notifications = notifiable?.Notifications;
        }

        public ResponseResult(object data, IEnumerable<Notifiable<Notification>> notifiables)
        {
            Object = data;
            Success = notifiables.SelectMany(p => p.Notifications).Any();
            Notifications = notifiables.SelectMany(p => p.Notifications);
        }

        public void ClearObject()
        {
            Object = null;
        }
        public bool Success { get; private set; }
        public object Object { get; private set; }

        public IEnumerable<Notification> Notifications { get; private set; }

    }



    public class AccessTokenResult : Notifiable<Notification>
    {
        public AccessTokenResult(string accessToken, string refreshToken, DateTime expireIn, string tokenType = "Bearer")
        {
            var contract = new Contract<Notification>();

            contract.IsNotNullOrEmpty(accessToken, "AccessToken", "Token was not generated.")
                .IsNotNullOrEmpty(accessToken, "RefreshToken", "RefreshToken was not generated.");

            AddNotifications(contract);
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpireIn = expireIn;
            TokenType = tokenType;
        }

        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime ExpireIn { get; private set; }
        public string TokenType { get; private set; }
    }
}

