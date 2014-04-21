using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Managers
{
    class TexturesManager
    {
        private Dictionary<string, Texture2D> _textures;
        private ContentManager _content;



        public TexturesManager(ContentManager content)
        {
            _content = content;
        }
        
        public bool AddTexture(string name, string key = "")
        {
            throw new NotImplementedException();
        }

        public bool AddTexture(string name, Texture2D texture)
        {
            bool added = false;
            if(!_textures.ContainsKey(name))
                _textures.Add(name, texture);

            return added;
        }

        public bool RemoveTexture(string name)
        {
            return _textures.Remove(name);
        }


        public void LoadContent()
        {
            throw new NotImplementedException();
        }
       


    }
}
