using Microsoft.AspNetCore.Authentication;

namespace SiyinPractice.Web.Core.Authentication.Bearer;

public class BearerSchemeOptions : AuthenticationSchemeOptions
{
    public BearerSchemeOptions()
    {
        Events = new BearerEvents();
    }

    public new BearerEvents Events
    {
        get { return (BearerEvents)base.Events; }
        set { base.Events = value; }
    }
}