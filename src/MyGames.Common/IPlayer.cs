// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Common
{
    public interface IPlayer
    {
        string Name { get; set; }

        byte[]? Image { get; set; }
    }
}
