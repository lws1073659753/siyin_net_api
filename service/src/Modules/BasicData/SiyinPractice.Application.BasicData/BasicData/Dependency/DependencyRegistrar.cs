using SiyinPractice.Framework.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace SiyinPractice.Application.BasicData.BasicData.Dependency
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(IServiceCollection services)
        {
            //services.AddSingleton<TimeCycleHelper>();
            //services.AddSingleton<DingDingTimeCycleHelper>();
            //services.AddHostedService<WeChatMsgReminderService>();
            //services.AddHostedService<DingDingMsgReminderService>();
        }
    }
}