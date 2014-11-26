using System.Collections.Generic;
using JPEngine.Events;

namespace JPEngine.Managers
{
    public class CameraManager : Manager, ICameraManager
    {
        private readonly List<ICamera> _cameras = new List<ICamera>();
        private readonly Dictionary<string, ICamera> _taggedCameras = new Dictionary<string, ICamera>();

        public ICamera Current { get; private set; }

        internal CameraManager()
        {
        }

        protected override void InitializeCore()
        {
            _cameras.Clear();
            _taggedCameras.Clear();
        }

        #region Camera Handling

        public bool SetCurrent(string tag)
        {
            if (_taggedCameras.ContainsKey(tag))
                SetCurrent(_taggedCameras[tag]);

            return false;
        }

        public bool SetCurrent(ICamera camera)
        {
            if (!_cameras.Contains(camera))
                AddCamera(camera);

            Current = camera;

            return true;
        }

        public void AddCamera(ICamera camera)
        {
            _cameras.Add(camera);

            if (!string.IsNullOrEmpty(camera.Tag))
            {
                camera.TagChanged += CameraTagChanged;
                _taggedCameras.Add(camera.Tag, camera);
            }
        }

        public bool ContainsCamera(string tag)
        {
            return _taggedCameras.ContainsKey(tag);
        }

        public bool ContainsCamera(ICamera cam)
        {
            return _cameras.Contains(cam);
        }

        public ICamera GetCamera(string tag)
        {
            if (_taggedCameras.ContainsKey(tag))
                return _taggedCameras[tag];

            return null;
        }

        public bool RemoveCamera(string tag)
        {
            if (_taggedCameras.ContainsKey(tag))
            {
                ICamera cam = _taggedCameras[tag];
                if (_taggedCameras.Remove(tag))
                {
                    cam.TagChanged -= CameraTagChanged;
                    return RemoveCamera(cam);
                }
            }
            return false;
        }

        public bool RemoveCamera(ICamera camera)
        {
            return _taggedCameras.ContainsKey(camera.Tag)
                ? RemoveCamera(camera.Tag)
                : _cameras.Remove(camera);
        }

        #endregion

        #region EventHandlers

        private void CameraTagChanged(object sender, ValueChangedEventArgs<string> e)
        {
            if (_taggedCameras.ContainsKey(e.OldValue))
            {
                ICamera cam = _taggedCameras[e.OldValue];
                _taggedCameras.Remove(e.OldValue);
                _taggedCameras.Add(e.NewValue, cam);
            }
        }

        #endregion
    }
}