using Flunt.Notifications;
using Flunt.Validations;
using IntegrationTest.Infra.UnitOfWork;
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
        private IUnitOfWork _unitOfWork;

        protected CommonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        protected IActionResult CommonResponse(object value, Notification notification)
        {
            var contract = new Contract<Notification>();
            contract.AddNotification(notification);
            return CommonResponse(value, contract);
        }

        protected IActionResult CommonResponse(object value, Notifiable<Notification> notifiable = null)
        {
            var response = new ResponseResult(value);
            if (notifiable != null)
            {
                response.AddNotifications(notifiable);
            }

            if (value is AccessTokenResult)
            {
                if (response.IsValid)
                {
                    var res = value as AccessTokenResult;
                    _unitOfWork.Commit();
                    return Ok(new
                    {
                        res.AccessToken,
                        res.RefreshToken,
                        res.ExpireIn,
                        res.TokenType
                    });
                }
            }
            else
            {
                if (response.IsValid)
                {
                    _unitOfWork.Commit();
                    return Ok(response);
                }
            }

            _unitOfWork.RollBack();
            response.ClearObject();
            return BadRequest(response);

        }


    }

    public class ResponseResult : Notifiable<Notification>
    {

        public ResponseResult(object data)
        {

            if (data is Notifiable<Notification>)
            {
                AddNotifications(data as Notifiable<Notification>);
            }

            Object = data;
        }

        public ResponseResult(object data, Notifiable<Notification> notifiable)
        {
            Object = data;
            AddNotifications(notifiable);
        }

        public ResponseResult(object data, IEnumerable<Notifiable<Notification>> notifications)
        {
            Object = data;
            notifications.ToList().ForEach(n => AddNotifications(n));
        }

        public void ClearObject()
        {
            Object = null;
        }
        public bool Success => IsValid;
        public object Object { get; private set; }

    }

    public class ResponseResult<TEntityResult> : Notifiable<Notification>
        where TEntityResult : Notifiable<Notification>
    {
        public ResponseResult(TEntityResult data)
        {

            AddNotifications(data);

            Object = data;
        }

        public ResponseResult(TEntityResult data, Notifiable<Notification> notifiable)
        {
            Object = data;
            AddNotifications(data);
            AddNotifications(notifiable);
        }

        public ResponseResult(TEntityResult data, IEnumerable<Notification> notifications)
        {
            Object = data;
            notifications.ToList().ForEach(n => AddNotification(n));
        }

        public void ClearObject()
        {
            Object = null;
        }

        public bool Success => IsValid;
        public TEntityResult Object { get; private set; }
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

