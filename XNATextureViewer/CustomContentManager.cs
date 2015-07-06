using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace XNATextureViewer
{
    public class CustomContentManager : ContentManager
    {
        public CustomContentManager(IServiceProvider services) : base(services) { }
        public CustomContentManager(IServiceProvider services, string rootDirectory) : base(services, rootDirectory) { }

        protected override Stream OpenStream(string assetName)
        {
            if (File.Exists(assetName))
            {
                return File.OpenRead(assetName);
            }
            return base.OpenStream(assetName);
        }
    }
}
