using SiyinPractice.Shared.Core.Json;
using SiyinPractice.Web.Core.Filter;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ControllerExtension
    {
        public static void AddContelWorksControllers(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<UowActionFilter>();
                options.Filters.Add<ExceptionFilter>();
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            })
                   .AddJsonOptions(options =>
                   {
                       options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                       options.JsonSerializerOptions.Converters.Add(new DateTimeNullableConverter());
                       options.JsonSerializerOptions.Encoder = SystemTextJson.GetAdncDefaultEncoder();
                       //该值指示是否允许、不允许或跳过注释。
                       options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                       //dynamic与匿名类型序列化设置
                       options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                       //dynamic
                       options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                       //匿名类型
                       options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                   });

            services.AddFluentValidationAutoValidation(cfg =>
            {
                //Continue 验证失败，继续验证其他项
                ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
                //cfg.ValidatorOptions.DefaultClassLevelCascadeMode = FluentValidation.CascadeMode.Continue;
                // Optionally set validator factory if you have problems with scope resolve inside validators.
                // cfg.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //调整参数验证返回信息格式
                //关闭自动验证
                //options.SuppressModelStateInvalidFilter = true;
                //格式化验证信息
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var problemDetails = new ProblemDetails
                    {
                        Detail = context.ModelState.GetValidationSummary("<br>"),
                        Title = "参数错误",
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = "https://httpstatuses.com/400",
                        Instance = context.HttpContext.Request.Path
                    };

                    return new ObjectResult(problemDetails)
                    {
                        StatusCode = problemDetails.Status
                    };
                };
            });
        }
    }
}