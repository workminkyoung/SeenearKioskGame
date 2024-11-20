using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : SingletonBehaviour<DataManager>
{
    protected override void Init()
    {
        
    }
    

    /// <summary>
    /// get sprite from sprite sheet from resources folder, match with excel order
    /// </summary>
    /// <param name="brand">brand name in resource folder</param>
    /// <param name="type">sprite sheet name</param>
    /// <param name="index">index of sheet item, match with excel index</param>
    /// <returns>sprite</returns>
    public Sprite LoadItemSprite_spriteSheet(string brand, string type, int index)
    {
        string path = Path.Combine("SpriteSheet", brand, type);
        Sprite[] textures = Resources.LoadAll<Sprite>(path);
        if (textures.Length <= 0)
            return null;
        else
            return textures[index];
    }

    /// <summary>
    /// get texture2D from sprite sheet from resources folder, match with excel order
    /// </summary>
    /// <param name="brand">brand name in resource folder</param>
    /// <param name="type">sprite sheet name</param>
    /// <param name="index">index of sheet item, match with excel index</param>
    /// <returns>texture2D</returns>
    public Texture2D LoadItemTexture2D_spriteSheet(string brand, string type, int index)
    {
        string path = Path.Combine("SpriteSheet", brand, type);
        Sprite[] textures = Resources.LoadAll<Sprite>(path);
        if (textures.Length <= 0)
            return null;
        else
        {
            Sprite sprite = textures[index];
            Texture2D texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] colors = textures[index].texture.GetPixels((int)sprite.textureRect.x,
                                             (int)sprite.textureRect.y,
                                             (int)sprite.textureRect.width,
                                             (int)sprite.textureRect.height);
            texture.SetPixels(colors);
            texture.Apply();
            return texture;
        }
    }
}

