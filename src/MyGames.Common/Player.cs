// Copyright (c) Stéphane ANDRE. All Right Reserved.
// See the LICENSE file in the project root for more information.

namespace MyGames.Common
{
    public abstract class Player : IPlayer
    {
        protected Player(string name, byte[]? image = null)
        {
            Name = name;
            Image = image;
        }

        public string Name { get; set; }

        public byte[]? Image { get; set; }

        public override string ToString() => Name;
    }
}
