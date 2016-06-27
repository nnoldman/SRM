using Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ExtensionLoader
    {
        ExtensionLoader() { }

        public static ExtensionLoader Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new ExtensionLoader();
                }
                return mInstance;
            }
        }
        static ExtensionLoader mInstance;

        public Dictionary<string, Type> Types
        {
            get
            {
                return mExtensionTypes;
            }
        }

        Dictionary<string, Type> mExtensionTypes = new Dictionary<string, Type>();

        void LoadInnerExtensions()
        {
            InstallExtensions(Assembly.GetCallingAssembly());
            //InstallExtensions(typeof(TextEditor).Assembly);
            //InstallExtensions(typeof(SolutionExplorer).Assembly);
        }
        void LoadExternExtensions()
        {
            if (Directory.Exists(Center.Option.ExtensionsPath))
            {
                var dlls = Directory.GetFiles(Center.Option.ExtensionsPath, "*.dll");

                if (dlls != null && dlls.Length > 0)
                {
                    for (int i = 0; i < dlls.Length; ++i)
                    {
                        var name = Path.GetFileNameWithoutExtension(dlls[i]);
                        if (!this.mExtensionTypes.ContainsKey(name))
                            LoadAnExternExtension(dlls[i]);
                    }
                }
            }
        }
        public void Load()
        {
            LoadInnerExtensions();
            LoadExternExtensions();

            Center.ExtensionsLoaded.Trigger();
        }

        void InstallExtensions(Assembly asm)
        {
            foreach (var tp in asm.DefinedTypes)
            {
                if (tp.BaseType == typeof(Extension))
                {
                    this.mExtensionTypes.Add(tp.Name, tp);
                }
            }

            ATrigger.DataCenter.InstallStaticTriggers(asm);
        }

        bool LoadAnExternExtension(string name)
        {
            FileInfo file = new FileInfo(name);
            try
            {
                Assembly asm = Assembly.LoadFile(file.FullName);
                if (asm != null)
                    InstallExtensions(asm);
            }
            catch (Exception exc)
            {
                Logger.error(exc.Message);
            }
            return false;
        }
    }
}
