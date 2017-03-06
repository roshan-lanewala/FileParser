using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System;

namespace MobileDen.CodeChallenge.FileParsing
{
    public enum ParserType
    {
        FileTypeA,
        FileTypeB
    }

    public abstract class GenericFacotry<TEntity>
    {
        public abstract TEntity GetObject(string type);
    }

    public class ParserFactory : GenericFacotry<BaseParser>
    {
        Dictionary<string, BaseParser> parsers = new Dictionary<string, BaseParser>();

        /// <summary>
        /// Initialize a ParserFactory by going through all types in the assembly
        /// and creating a dictionary for all available parsers
        /// </summary>
        public ParserFactory(IFileSystem fileSystem)
        {
            // get the executing assembly and available types using System.Reflection
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            parsers = types
                .Where(t => t.IsSubclassOf(typeof(BaseParser)) && !t.IsAbstract)
                .Select(s => Activator.CreateInstance(s, fileSystem) as BaseParser)
                .ToDictionary(k => k.ToString(), v => v);
        }

        public override BaseParser GetObject(string type)
        {
            return parsers[type];
        }
    }
}
