namespace BlazorUserApp.Server.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "User Management API",
                    Version = "v1",
                    Description = "An API for managing users"
                });
            });
        }
    }
}
