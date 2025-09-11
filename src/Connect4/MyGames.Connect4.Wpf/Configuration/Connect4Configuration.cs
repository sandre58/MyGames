// -----------------------------------------------------------------------
// <copyright file="Connect4Configuration.cs" company="Stéphane ANDRE">
// Copyright (c) Stéphane ANDRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.Options;

namespace MyGames.Connect4.Wpf.Configuration;

internal sealed class Connect4Configuration
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
