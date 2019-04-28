using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Asp.Net.Core.Cognito.Users.Configuration
{
    /// <summary>
    ///    Read cognito setting 
    /// </summary>
    public class CognitoSettings
    {

        private static object lockObject = new object();
        /// <summary>
        ///    Get cognito configureation values from  {current dir}/config/cognito_config.json
        /// </summary>
        public static CognitoSettingValues cognitoSettingValues
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

            String baseDir = Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + "config" + Path.DirectorySeparatorChar;

            IConfiguration configuration;

            configuration = new ConfigurationBuilder()
                .SetBasePath(baseDir)
                .AddJsonFile("cognito_config.json", optional: true)
                .Build();

            _cognitoSettings = configuration.GetSection("Cognito").Get<CognitoSettingValues>();

        }

    }
}
