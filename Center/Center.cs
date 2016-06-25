using ATrigger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace Core
{
    public class Center : ATrigger.ITriggerStatic
    {
        public static DockPanel Container;
        static DocumentManager mDocumentManager;
        static Option mOption;

        public static DocumentManager DocumentManager
        {
            get
            {
                if (mDocumentManager == null)
                    mDocumentManager = new DocumentManager();
                return mDocumentManager;
            }
        }
        public static Option Option
        {
            get
            {
                if (mOption == null)
                    mOption = new Option();
                return mOption;
            }
            set
            {
                mOption = value;
            }
        }

        [Emmiter((int)DataType.ApplicationExit)]
        public static Signal OnExit = new Signal();

        [Emmiter((int)DataType.OpenDocument)]
        public static ATrigger<string> CurrentOpenDoucment = new ATrigger<string>();

        [Emmiter((int)DataType.CloseDocument)]
        public static ATrigger<string> CurrentCloseDoucment = new ATrigger<string>();

        [Emmiter((int)DataType.View)]
        public static Signal View = new Signal();

        [Emmiter((int)DataType.ExtensionsLoaded)]
        public static Signal ExtensionsLoaded = new Signal();
    }
}
