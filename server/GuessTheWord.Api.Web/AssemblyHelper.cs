using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using GuessTheWord.Abstractions.Attributes;

namespace GuessTheWord.Api.Web
{
    /// <summary>
    /// Служебные методы для работы со сборками
    /// </summary>
    internal static class AssemblyHelper
    {
        /// <summary>
        /// Получить сборки для девсервера с типами для IoC
        /// </summary>
        /// <param name="path">Путь до папки сборок</param>
        /// <returns>Коллекция сборок</returns>
        public static IEnumerable<Assembly> InjectionAssemblies(string path)
        {
            return InjectionAssemblies(path, "*.dll");
        }

        /// <summary>
        /// Получить сборки с типами для IoC
        /// </summary>
        /// <param name="path">Путь до папки сборок</param>
        /// <param name="pattern">Шаблон для поиска по имени</param>
        /// <returns>Коллекция сборок</returns>
        private static IEnumerable<Assembly> InjectionAssemblies(string path, string pattern)
        {
            var di = new DirectoryInfo(path);
            var files = di.GetFiles(pattern, SearchOption.TopDirectoryOnly);
            var currentAssemblies = AssemblyLoadContext.Default.Assemblies;
            return files
                .Select(fi =>
                {
                    var fullName = fi.FullName;
                    var name = Path.GetFileNameWithoutExtension(fullName);
                    var loadedAssembly = currentAssemblies.FirstOrDefault(a => a.GetName().Name == name);
                    if (loadedAssembly != null)
                    {
                        return loadedAssembly;
                    }
                    return Assembly.LoadFile(fi.FullName);
                })
                .Where(a => a.GetCustomAttribute<GuessTheWordPluginAttribute>() != null);
        }
    }
}