// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Options;

namespace MyGames.Connect4.Wpf.Configuration
{
    internal class Connect4Configuration
    {
        public Connect4Configuration() { }

        public Connect4Configuration(IOptions<Connect4Configuration> configuration)
        {
            MaxRecentFiles = configuration.Value.MaxRecentFiles;
            RecentFilesRegistry = configuration.Value.RecentFilesRegistry;
            TempDirectory = configuration.Value.TempDirectory;
            UserRegistry = configuration.Value.UserRegistry;
        }

        public int MaxRecentFiles { get; set; }

        public string RecentFilesRegistry { get; set; } = string.Empty;

        public string UserRegistry { get; set; } = string.Empty;

        public string TempDirectory { get; set; } = string.Empty;
    }
}
