﻿using Swashbuckle.AspNetCore.Swagger;

namespace ToolBoxApiCore.Utils
{
    /// <summary>
    /// Swagger Configuration
    /// </summary>
    public class SwaggerSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SwaggerSettings"/> class.
        /// </summary>
        public SwaggerSettings()
        {
            Name = "API Whiteapp";
            Info = new Info
            {
                Title = "API Whiteapp",
                Description = "API whiteapp with versions",
                TermsOfService = ""
            };
        }

        /// <summary>
        /// Gets or sets document Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets swagger Info.
        /// </summary>
        public Info Info { get; set; }

        /// <summary>
        /// Gets or sets RoutePrefix.
        /// </summary>
        public string RoutePrefix { get; set; }

        /// <summary>
        /// Gets Route Prefix with tailing slash.
        /// </summary>
        public string RoutePrefixWithSlash =>
            string.IsNullOrWhiteSpace(RoutePrefix)
                ? string.Empty
                : RoutePrefix + "/";
    }
}
