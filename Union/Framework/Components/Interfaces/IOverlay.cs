using System;

namespace Union.Framework.Components.Interfaces
{
    public interface IOverlay
    {
        bool IsMatch(Exception e);

        void Close();
    }
}