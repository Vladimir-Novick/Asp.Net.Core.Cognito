using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Asp.Net.Core.Cognito.Users.Configuration
{
    /// <summary>
    ///    Read cognito setting 
    /// </summary>
    public class CognitoSettings
    {
        public static void AddJsonFile(String configFile)
        {
            jsonConfigFile = configFile;
        }

        private static string jsonConfigFile { get; set; } = "config/cognito_config.json";

        /// <summary>
        ///   Setup ASP.NET service
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
          
            services.AddAuthentication("Bearer")
              .AddJwtBearer(options =>
              {
                  options.Audience = CognitoSettings.Values.UserPoolClientId;
                  options.Authority = $"https://cognito-idp.{CognitoSettings.Values.Region}.amazonaws.com/{CognitoSettings.Values.UserPoolId}";
              });



        }

        private static object lockObject = new object();
        /// <summary>
        ///    Get cognito configureation values from  {current dir}/config/cognito_config.json
        /// </summary>
        public static CognitoSettingValues Values
        { get
            {
                lock (lockObject)
                {
                    if (_cognitoSettings == null)
                    {
                        getConfiguration();
                    }
                }
                return _cognitoSettings;
            }
        }

        private static CognitoSettingValues _cognitoSettings = null;

        private static void getConfiguration()
        {

            String baseDir = Directory.GetCurrentDirectory();

            IConfiguration configuration;

            configuration = new ConfigurationBuilder()
                .SetBasePath(baseDir)
                .AddJsonFile(baseDir + jsonConfigFile, optional: true)
                .Build();

            _cognitoSettings = configuration.GetSection("AWS").Get<CognitoSettingValues>();

        }

    }
}
