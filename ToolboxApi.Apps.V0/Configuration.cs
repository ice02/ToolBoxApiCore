using System;
using System.Collections.Generic;
using System.Text;

namespace ToolboxApi.Apps.V0
{
    public static class Configuration
    {
        public const string MetadataDocumentUri = "http://localhost:58200/odata/";

        public const bool UseDataServiceCollection = true;

        public const string NamespacePrefix = "ConsoleAppClient";

        public const string TargetLanguage = "CSharp";

        public const bool EnableNamingAlias = true;

        public const bool IgnoreUnexpectedElementsAndAttributes = true;
    }
}
