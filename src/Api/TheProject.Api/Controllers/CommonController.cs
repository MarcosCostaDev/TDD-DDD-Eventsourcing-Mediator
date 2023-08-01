using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheProject.Api.Controllers;

[NotMapped]
public abstract class CommonController : ControllerBase
{
    protected IActionResult CommonResponse(object value, IEnumerable<Notification> notifications = null)
    {
        var response = new ResponseResult(value, notifications);

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
    public ResponseResult(object data)
    {

        if (data is Notifiable<Notification>)
        {
            var notifiable = data as Notifiable<Notification>;
            Notifications = notifiable.Notifications;
        }

        Object = data;
    }

    public ResponseResult(object data, Notifiable<Notification> notifiable)
    {
        Object = data;
        Notifications = notifiable?.Notifications;
    }

    public ResponseResult(object data, IEnumerable<Notification> notifications)
    {
        Object = data;
        Notifications = notifications;
    }

    public void ClearObject()
    {
        Object = null;
    }
    public bool Success => !(Notifications?.Any()).GetValueOrDefault();
    public object Object { get; private set; }

    public IEnumerable<Notification> Notifications { get; private set; } = Enumerable.Empty<Notification>();

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

