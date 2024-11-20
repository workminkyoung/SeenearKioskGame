using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FreeDraw
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]

    public class Drawable : MonoBehaviour
    {
        // PEN COLOUR
        public static Color Pen_Color = Color.black;
        public static int Pen_Width = 3;


        public delegate void Brush_Function(Vector2 world_position);
        public Brush_Function current_brush;

        public LayerMask Drawing_Layers;

        public bool Reset_Canvas_On_Play = true;
        public Color Reset_Colour = new Color(0, 0, 0, 0); 

        public static Drawable drawable;
        Sprite drawable_sprite;
        Texture2D drawable_texture;

        Vector2 previous_drag_position;
        Color[] clean_colours_array;
        Color transparent;
        Color32[] cur_colors;
        bool mouse_was_previously_held_down = false;
        bool no_drawing_on_current_drag = false;


        public void BrushTemplate(Vector2 world_position)
        {
            Vector2 pixel_pos = WorldToPixelCoordinates(world_position);

            cur_colors = drawable_texture.GetPixels32();
            if (previous_drag_position == Vector2.zero)
            {
                MarkPixelsToColour(pixel_pos, Pen_Width, Pen_Color);
            }
            else
            {
                ColourBetween(previous_drag_position, pixel_pos, Pen_Width, Pen_Color);
            }

            ApplyMarkedPixelChanges();
            
            previous_drag_position = pixel_pos;
        }


        public void PenBrush(Vector2 world_point)
        {
            Vector2 pixel_pos = WorldToPixelCoordinates(world_point);

            cur_colors = drawable_texture.GetPixels32();

            if (previous_drag_position == Vector2.zero)
            {
                MarkPixelsToColour(pixel_pos, Pen_Width, Pen_Color);
            }
            else
            {
                ColourBetween(previous_drag_position, pixel_pos, Pen_Width, Pen_Color);
            }
            ApplyMarkedPixelChanges();

            //Debug.Log("Dimensions: " + pixelWidth + "," + pixelHeight + ". Units to pixels: " + unitsToPixels + ". Pixel pos: " + pixel_pos);
            previous_drag_position = pixel_pos;
        }


        public void SetPenBrush()
        {
            current_brush = PenBrush;
        }

        void Update()
        {

            if(Input.GetKeyDown(KeyCode.Space))
            {
                ResetCanvas();
            }

            bool input_held_down = Input.GetMouseButton(0) || Input.touchCount > 0;

            if (input_held_down && !no_drawing_on_current_drag)
            {
                Vector2 input_position;

                if (Input.touchCount > 0)
                {
                    // 터치 입력일 때
                    Touch touch = Input.GetTouch(0);
                    input_position = Camera.main.ScreenToWorldPoint(touch.position);
                }
                else
                {
                    // 마우스 입력일 때
                    input_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }

                Collider2D hit = Physics2D.OverlapPoint(input_position, Drawing_Layers.value);
                if (hit != null && hit.transform != null)
                {
                    current_brush(input_position);
                }
                else
                {
                    previous_drag_position = Vector2.zero;
                    if (!mouse_was_previously_held_down)
                    {
                        no_drawing_on_current_drag = true;
                    }
                }
            }
            else if (!input_held_down)
            {
                previous_drag_position = Vector2.zero;
                no_drawing_on_current_drag = false;
            }

            mouse_was_previously_held_down = input_held_down;
        }


        public void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
        {
            float distance = Vector2.Distance(start_point, end_point);
            Vector2 direction = (start_point - end_point).normalized;

            Vector2 cur_position = start_point;

            float lerp_steps = 1 / distance;

            for (float lerp = 0; lerp <= 1; lerp += lerp_steps)
            {
                cur_position = Vector2.Lerp(start_point, end_point, lerp);
                MarkPixelsToColour(cur_position, width, color);
            }
        }





        public void MarkPixelsToColour(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
        {
            int center_x = (int)center_pixel.x;
            int center_y = (int)center_pixel.y;

            for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
            {
                if (x >= (int)drawable_sprite.rect.width || x < 0)
                    continue;

                for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
                {
                    MarkPixelToChange(x, y, color_of_pen);
                }
            }
        }
        public void MarkPixelToChange(int x, int y, Color color)
        {
            int array_pos = y * (int)drawable_sprite.rect.width + x;

            // Check if this is a valid position
            if (array_pos > cur_colors.Length || array_pos < 0)
                return;

            cur_colors[array_pos] = color;
        }
        public void ApplyMarkedPixelChanges()
        {
            drawable_texture.SetPixels32(cur_colors);
            drawable_texture.Apply();
        }

        public void ColourPixels(Vector2 center_pixel, int pen_thickness, Color color_of_pen)
        {
            int center_x = (int)center_pixel.x;
            int center_y = (int)center_pixel.y;

            for (int x = center_x - pen_thickness; x <= center_x + pen_thickness; x++)
            {
                for (int y = center_y - pen_thickness; y <= center_y + pen_thickness; y++)
                {
                    drawable_texture.SetPixel(x, y, color_of_pen);
                }
            }

            drawable_texture.Apply();
        }


        public Vector2 WorldToPixelCoordinates(Vector2 world_position)
        {
            Vector3 local_pos = transform.InverseTransformPoint(world_position);

            float pixelWidth = drawable_sprite.rect.width;
            float pixelHeight = drawable_sprite.rect.height;
            float unitsToPixels = pixelWidth / drawable_sprite.bounds.size.x * transform.localScale.x;

            float centered_x = local_pos.x * unitsToPixels + pixelWidth / 2;
            float centered_y = local_pos.y * unitsToPixels + pixelHeight / 2;

            Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

            return pixel_pos;
        }

        public void ResetCanvas()
        {
            drawable_texture.SetPixels(clean_colours_array);
            drawable_texture.Apply();
        }


        
        void Awake()
        {
            drawable = this;
            current_brush = PenBrush;

            drawable_sprite = this.GetComponent<SpriteRenderer>().sprite;
            drawable_texture = drawable_sprite.texture;

            clean_colours_array = new Color[(int)drawable_sprite.rect.width * (int)drawable_sprite.rect.height];
            for (int x = 0; x < clean_colours_array.Length; x++)
                clean_colours_array[x] = Reset_Colour;

            if (Reset_Canvas_On_Play)
                ResetCanvas();
        }
    }
}