using JPEngine.Entities;
using Microsoft.Xna.Framework;

namespace JPEngine.Components
{

    //Some infos : http://gamedev.stackexchange.com/questions/59301/xna-2d-camera-scrolling-why-use-matrix-transform


    public class CameraComponent : BaseComponent
    {
        public Vector2 Origin
        {
            get { return new Vector2(Engine.Window.Width/2f, Engine.Window.Height/2f); }
        }

        public Matrix TransformMatrix
        {
            get
            {
                //return Matrix.CreateTranslation(new Vector3(-Transform.Position.X, -Transform.Position.Y, 0)) *
                //       Matrix.CreateRotationZ(Transform.Rotation) *
                //       Matrix.CreateScale(new Vector3(Transform.Scale.X, Transform.Scale.Y, 1));
                return Matrix.CreateTranslation(new Vector3(-Transform.Position.X, -Transform.Position.Y, 0)) *
                       Matrix.CreateRotationZ(Transform.Rotation) *
                       Matrix.CreateScale(new Vector3(Transform.Scale.X, Transform.Scale.Y, 1)) *
                       Matrix.CreateTranslation(new Vector3(Origin, 0));
            }
        }

        public CameraComponent(Entity gameObject)
            :base(gameObject)
        {
        }

        //public void ClampToArea(int width, int height)
        //{
        //    //Pour ne pas sortir de la map (à droite ou en bas)
        //    if (Transform.Position.Y > height) //Hauteur de la map - largeur de l'écran
        //        Transform.Position.Y = height;
        //    if (Transform.Position.X > width)   //Largeur de la map - hauteur de l'écran
        //        Transform.Position.X = width;

        //    //Pour ne pas sortir de la map (en haut ou à gauche)
        //    if (Transform.Position.Y < 0)
        //        Transform.Position.Y = 0;
        //    if (Transform.Position.X < 0)
        //        Transform.Position.X = 0;
        //}

    }
}
