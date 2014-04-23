using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPEngine.Managers
{

    /*
     * Logic / Ideas:
     * 
     *  ADDED TEXTURE = texture added to the dict of name/path
     *  LOADED TEXTURE = texture added to the dict of name/Texture2D
     *   
     * - A user adds a texture (string name)
     * 
     *              - Data Structures:
     *                      - private Dictionary <string, string> _paths;
     *                      - private Dictionary <string, Texture2D> _textures;
     *                      
     *              - Attributes:
     *                      - private ContentManager _content;
     * 
     *              - Property : 
     *                      - public string[] LoadedTextures; //Returns all the loaded texture names
     *                      - public string[] AddedTextures;  //Returns all the added texture names
     *                      - public int NbTexturesLoaded;
     *                      - public int NbTexturesAdded;
     * 
     *              - Methods:
     *                      - public bool AddTexture(name, path, forceLoad = false);
     *                      - public bool RemoveTexture(name);                        
     *                      - public bool LoadTexture(name);
     *                      - public bool LoadTextures(params names);
     *                      - public bool UnloadTexture(name);
     *                      - public bool UnloadTextures(params name);
     *                      - public bool IsTextureLoaded(name);    //Check if the Texture2D is loaded
     *                      - public bool IsTextureAdded(name);     //Check if the path is in the path dictionary
     * 
     */

    public class TextureManager : ResourceManager<Texture2D>
    {

#region Attributes
            
#endregion

#region Properties

#endregion

#region Constructors

        internal TextureManager(ContentManager content)
            : base(content)
        {          
        }
        
#endregion


#region Methods


#endregion


    }
}
