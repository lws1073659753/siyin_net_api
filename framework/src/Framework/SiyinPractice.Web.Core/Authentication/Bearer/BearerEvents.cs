namespace SiyinPractice.Web.Core.Authentication.Bearer;

public class BearerEvents
{
    public Func<BearerTokenValidatedContext, Task> OnTokenValidated { get; set; }
}