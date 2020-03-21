using System;
using System.Collections.Generic;
using System.Reflection;
using LanguageExt;
using NCmdLiner.Resources;

namespace NCmdLiner
{
    internal static class InfoProvider
    {
        public sealed class EmbeddedResourceFileNamePostFix
        {
            public static readonly EmbeddedResourceFileNamePostFix LicenseFilePostfix = new EmbeddedResourceFileNamePostFix("license.xml");
            public static readonly EmbeddedResourceFileNamePostFix CreditFilePostfix = new EmbeddedResourceFileNamePostFix("credit.xml");

            private EmbeddedResourceFileNamePostFix(string value)
            {
                Value = value;
            }

            public string Value { get; private set; }
        }
        
        /// <summary>
        /// Get info from embedded resource 
        /// </summary>
        /// <typeparam name="T">Can be LicenseInfo or CreditInfo</typeparam>
        /// <param name="assembly"></param>
        /// <param name="embeddedResourceFileNamePostFix">EmbeddedResourceFileNamePostFix.LicenseFilePostfix or EmbeddedResourceFileNamePostFix.CreditFilePostfix</param>
        /// <returns></returns>
        public static List<T> GetEmbeddedInfo<T>(EmbeddedResourceFileNamePostFix embeddedResourceFileNamePostFix, Assembly assembly = null)
        {
            assembly ??= typeof(InfoProvider).GetAssembly();
            var assemblies = new List<Assembly> { assembly };
            var referencedAssemblies = assembly.GetReferencedAssemblies();
            foreach (var assemblyName in referencedAssemblies)
            {
                LoadAssembly(assemblyName)
                    .IfSome(a => assemblies.Add(a));
            }
            var licenses = new List<T>();
            foreach (var a in assemblies)
            {
                var resourceNames = a.GetManifestResourceNames();
                var embeddedResource = new EmbeddedResource();
                var resourceNameList = new List<string>(resourceNames.Length);
                resourceNameList.AddRange(resourceNames);
                resourceNameList.Sort();
                foreach (var resourceName in resourceNameList)
                {
                    if (resourceName.ToLower().EndsWith(embeddedResourceFileNamePostFix.Value))
                    {
                        using var resourceStream = embeddedResource.ExtractToStream(resourceName, a);
                        try
                        {
                            var licenseInfo = SerializerHelper<T>.DeSerialize(resourceStream);
                            licenses.Add(licenseInfo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Failed to deserialize embedded resource '{0}' in '{1}'. {2}",
                                resourceName, a.FullName, ex.Message);
                        }
                    }
                }
            }
            return licenses;
        }

        private static Try<Assembly> TryLoadAssembly(AssemblyName assemblyName) => () => Assembly.Load(assemblyName);

        private static Option<Assembly> LoadAssembly(AssemblyName assemblyName)
        {
            return TryLoadAssembly(assemblyName).Match(assembly => Option<Assembly>.Some(assembly), exception => Option<Assembly>.None);
        }
    }
}
