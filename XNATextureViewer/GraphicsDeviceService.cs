using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Windows.Forms;

namespace XNATextureViewer
{
    public class GraphicsDeviceService : IGraphicsDeviceService
    {
        static GraphicsDeviceService instance;
        static int refCount;

        GraphicsDevice graphicsDevice;

        // Store the current device settings.
        PresentationParameters parameters;

        public GraphicsDevice GraphicsDevice { get { return graphicsDevice; } }

        // IGraphicsDeviceService events.
        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

        GraphicsDeviceService(IntPtr windowHandle, int width, int height)
        {
            parameters = new PresentationParameters();
            parameters.BackBufferWidth = Math.Max(width, 1);
            parameters.BackBufferHeight = Math.Max(height, 1);
            parameters.DepthStencilFormat = DepthFormat.Depth24;
            parameters.DeviceWindowHandle = windowHandle;
            parameters.PresentationInterval = PresentInterval.Immediate;
            parameters.IsFullScreen = false;

            graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, parameters);
        }

        public static GraphicsDeviceService AddRef(IntPtr windowHandle, int width, int height)
        {
            if (Interlocked.Increment(ref refCount) == 1)
            {
                instance = new GraphicsDeviceService(windowHandle, width, height);
            }
            return instance;
        }

        public void Release(bool disposing)
        {
            if (Interlocked.Decrement(ref refCount) == 0)
            {
                if (disposing)
                {
                    if (DeviceDisposing != null)
                        DeviceDisposing(this, EventArgs.Empty);
                    graphicsDevice.Dispose();
                }
                graphicsDevice = null;
            }
        }

        public void ResetDevice(int width, int height)
        {
            if (DeviceResetting != null)
                DeviceResetting(this, EventArgs.Empty);

            parameters.BackBufferWidth = Math.Max(parameters.BackBufferWidth, width);
            parameters.BackBufferHeight = Math.Max(parameters.BackBufferHeight, height);

            graphicsDevice.Reset(parameters);

            if (DeviceReset != null)
                DeviceReset(this, EventArgs.Empty);
        }
    }
}
