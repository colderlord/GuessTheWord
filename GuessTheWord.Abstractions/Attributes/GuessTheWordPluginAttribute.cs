using System;

namespace GuessTheWord.Abstractions.Attributes
{
    /// <summary>
    /// Атрибут, отмечающий сборку
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class GuessTheWordPluginAttribute : Attribute
    {
    }
}