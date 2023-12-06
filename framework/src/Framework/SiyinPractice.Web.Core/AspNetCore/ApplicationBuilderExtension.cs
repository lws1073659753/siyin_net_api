using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtension
    {
        public static void UseContelWorks(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseContelWorksApp();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomrSwagger(env);

            app.UseContelWorksSerilog();

            app.UseHttpsRedirection();

            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}