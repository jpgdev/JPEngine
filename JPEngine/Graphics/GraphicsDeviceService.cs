using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#region Using Statements

#endregion

// The IGraphicsDeviceService interface requires a DeviceCreated event, but we
// always just create the device inside our constructor, so we have no place to
// raise that event. The C# compiler warns us that the event is never used, but
// we don't care so we just disable this warning.
#pragma warning disable 67

namespace JPEngine.Graphics
{
    /// <summary>
    /// Helper class responsible for creating and managing the GraphicsDevice.
    /// All GraphicsDeviceControl instances share the same GraphicsDeviceService,
    /// so even though there can be many controls, there will only ever be a single
    /// underlying GraphicsDevice. This implements the standard IGraphicsDeviceService
    /// interface, which provides notification events for when the device is reset
    /// or disposed.
    /// </summary>
    public class GraphicsDeviceService : IGraphicsDeviceService
    {
        #region Fields

        private Game _game; //HACK : Temp fix for the GraphicsDevice ctor that crashes if there is no Game instance
		private GraphicsDeviceManager _graphicsDeviceManager;

        // Singleton device service instance.
		private static GraphicsDeviceService _instance;// = new GraphicsDeviceService();


        // Keep track of how many controls are sharing the singletonInstance.
        private static int _referenceCount;

        private GraphicsDevice _graphicsDevice;

        #endregion

        /// <summary>
        /// Gets the current graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return _graphicsDevice; }
        }

        // IGraphicsDeviceService events.
        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;

		/// <summary>
		/// Constructor is private, because this is a singleton class:
		/// client controls should use the public AddRef method instead.
		/// </summary>
		protected GraphicsDeviceService (IntPtr windowHandle, int width, int height)
		{ 		
			PresentationParameters _presentationParams = new PresentationParameters
			{
				DeviceWindowHandle = windowHandle,
				BackBufferWidth = Math.Max(width, 1),
				BackBufferHeight = Math.Max(height, 1)
			};
			//parameters.BackBufferFormat = SurfaceFormat.Color;

			//HACK : Need a Game to be created to be able to create a GraphicsDevice
			_game = new Game();

            // HACK : There is a side effect in the GraphicsDeviceManager ctor. It adds itself as a IGraphicsDeviceManager & IGraphicsDeviceService. 
            // This is also why it does not correctly work, since when we try to create
            // Anything with the GraphicsDevice, it will try to use the GraphicsDevice from the 
            // GraphiceDeviceManager created here, not the GraphicsDeviceService (this class).
            _graphicsDeviceManager = new GraphicsDeviceManager(_game);           

			_graphicsDevice = new GraphicsDevice(
				GraphicsAdapter.DefaultAdapter,
				GraphicsProfile.Reach,
				_presentationParams);	

			// Currently useless, since it is impossible to bind to this before the object is created
            //if (DeviceCreated != null)
			//    DeviceCreated(this, EventArgs.Empty);
		}


        /// <summary>
        /// Gets a reference to the singleton instance.
        /// </summary>
        public static GraphicsDeviceService AddRef(IntPtr windowHandle,
                                                   int width, int height)
        {
            // Increment the "how many controls sharing the device" reference count.
            if (Interlocked.Increment(ref _referenceCount) == 1)
            {
                // If this is the first control to start using the
                // device, we must create the singleton instance.
				_instance = new GraphicsDeviceService(windowHandle, width, height);
                //_singletonInstance = new GraphicsDeviceService(windowHandle,
                                                            //  width, height);
            }

			return _instance;
        }


        /// <summary>
        /// Releases a reference to the singleton instance.
        /// </summary>
        public void Release(bool disposing)
        {
            // Decrement the "how many controls sharing the device" reference count.
            if (Interlocked.Decrement(ref _referenceCount) == 0)
            {
                // If this is the last control to finish using the
                // device, we should dispose the singleton instance.
                if (disposing)
                {
                    if (DeviceDisposing != null)
                        DeviceDisposing(this, EventArgs.Empty);

                    _graphicsDevice.Dispose();
                    _game.Dispose();
                }

                _graphicsDevice = null;
            }
        }

        /// <summary>
        /// Resets the graphics device to whichever is bigger out of the specified
        /// resolution or its current size. This behavior means the device will
        /// demand-grow to the largest of all its GraphicsDeviceControl clients.
        /// </summary>
        public void ResetDevice(int width, int height)
        {
            //if (DeviceResetting != null)
            //    DeviceResetting(this, EventArgs.Empty);

            //parameters.BackBufferWidth = Math.Max(parameters.BackBufferWidth, width);
            //parameters.BackBufferHeight = Math.Max(parameters.BackBufferHeight, height);

            //graphicsDevice.Reset(parameters);

            //if (DeviceReset != null)
            //    DeviceReset(this, EventArgs.Empty);
        }
    }
}
