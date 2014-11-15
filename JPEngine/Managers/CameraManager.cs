using System.Collections.Generic;
using JPEngine.Components;
using JPEngine.Events;

namespace JPEngine.Managers
{
    public class CameraManager : Manager
    {
        private readonly List<CameraComponent> _cameras = new List<CameraComponent>();
        private readonly Dictionary<string, CameraComponent> _taggedCameras = new Dictionary<string, CameraComponent>();
        private CameraComponent _currentCamera;

        internal CameraManager()
        {
        }

        public CameraComponent Current
        {
            get { return _currentCamera; }
        }

        protected override bool InitializeCore()
        {
            _cameras.Clear();
            _taggedCameras.Clear();

            return base.InitializeCore();
        }

        public bool SetCurrent(string tag)
        {
            if (_taggedCameras.ContainsKey(tag))
                SetCurrent(_taggedCameras[tag]);

            return false;
        }

        public bool SetCurrent(CameraComponent camera)
        {
            if (!_cameras.Contains(camera))
                AddCamera(camera);

            _currentCamera = camera;

            return true;
        }

        public void AddCamera(CameraComponent camera)
        {
            _cameras.Add(camera);

            if (!string.IsNullOrEmpty(camera.GameObject.Tag))
            {
                camera.GameObject.TagChanged += CameraTagChanged;
                _taggedCameras.Add(camera.GameObject.Tag, camera);
            }
        }

        public bool ContainsCamera(string tag)
        {
            return _taggedCameras.ContainsKey(tag);
        }

        public bool ContainsCamera(CameraComponent cam)
        {
            return _cameras.Contains(cam);
        }

        public CameraComponent GetCamera(string tag)
        {
            if (_taggedCameras.ContainsKey(tag))
                return _taggedCameras[tag];

            return null;
        }

        private bool RemoveCamera(string tag)
        {
            if (_taggedCameras.ContainsKey(tag))
            {
                CameraComponent cam = _taggedCameras[tag];
                if (_taggedCameras.Remove(tag))
                {
                    cam.GameObject.TagChanged -= CameraTagChanged;
                    return RemoveCamera(cam);
                }
            }
            return false;
        }

        private bool RemoveCamera(CameraComponent camera)
        {
            return _taggedCameras.ContainsKey(camera.GameObject.Tag)
                ? RemoveCamera(camera.GameObject.Tag)
                : _cameras.Remove(camera);
        }

        #region EventHandlers

        private void CameraTagChanged(object sender, ValueChangedEventArgs<string> e)
        {
            if (_taggedCameras.ContainsKey(e.OldValue))
            {
                CameraComponent cam = _taggedCameras[e.OldValue];
                _taggedCameras.Remove(e.OldValue);
                _taggedCameras.Add(e.NewValue, cam);
            }
        }

        #endregion
    }
}