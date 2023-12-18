/*
 *  by @gontzalve
 *  July 9th, 2016
 *  Updated: November 18th, 2017
 */

using UnityEngine;
using System.Collections;
using DG.Tweening;
using PowerTools;

public static class ExtensionsGameObject
{
    public static void Activate(this GameObject go)
    {
        go.SetActive(true);
    }

    public static void Deactivate(this GameObject go)
    {
        go.SetActive(false);
    }

    // TRANSFORM ======================================================

        // SETTING GLOBAL POSITIONS ======================================================

        public static void SetPositionXY(this GameObject go, float newX, float newY)
        {
            go.transform.SetPositionXY(newX, newY);
        }

        public static void SetPositionXY(this GameObject go, Vector2 newPos)
        {
            go.transform.SetPositionXY(newPos);
        }

        public static void SetPositionX(this GameObject go, float newX)
        {
            go.transform.SetPositionX(newX);
        }

        public static void SetPositionY(this GameObject go, float newY)
        {
            go.transform.SetPositionY(newY);
        }

        public static void SetPositionZ(this GameObject go, float newZ)
        {
            go.transform.SetPositionZ(newZ);
        }

        // SETTING LOCAL POSITIONS ======================================================

        public static void SetLocalPositionXY(this GameObject go, float newX, float newY)
        {
            go.transform.SetLocalPositionXY(newX, newY);
        }

        public static void SetLocalPositionX(this GameObject go, float newX)
        {
            go.transform.SetLocalPositionX(newX);
        }

        public static void SetLocalPositionY(this GameObject go, float newY)
        {
            go.transform.SetLocalPositionY(newY);
        }

        public static void SetLocalPositionZ(this GameObject go, float newZ)
        {
            go.transform.SetLocalPositionZ(newZ);
        }

        // GETTING GLOBAL POSITIONS ======================================================
    
        public static Vector2 GetPositionXY(this GameObject go)
        {
            return go.transform.GetPositionXY();
        }

        public static float GetPositionX(this GameObject go)
        {
            return go.transform.GetPositionX();
        }

        public static float GetPositionY(this GameObject go)
        {
            return go.transform.GetPositionY();
        }

        public static float GetPositionZ(this GameObject go)
        {
            return go.transform.GetPositionZ();
        }

        // GETTING LOCAL POSITIONS ======================================================

        public static Vector2 GetLocalPositionXY(this GameObject go)
        {
            return go.transform.GetLocalPositionXY();
        }

        public static float GetLocalPositionX(this GameObject go)
        {
            return go.transform.GetLocalPositionX();
        }

        public static float GetLocalPositionY(this GameObject go)
        {
            return go.transform.GetLocalPositionY();
        }

        public static float GetLocalPositionZ(this GameObject go)
        {
            return go.transform.GetLocalPositionZ();
        }

        // ADDING OFFSETS TO POSITION ======================================================

        public static void AddPositionXY(this GameObject go, float offsetX, float offsetY)
        {
            go.transform.AddPositionXY(offsetX, offsetY);
        }

        public static void AddPositionX(this GameObject go, float offsetX)
        {
            go.transform.AddPositionX(offsetX);
        }

        public static void AddPositionY(this GameObject go, float offsetY)
        {
            go.transform.AddPositionY(offsetY);
        }

        public static void AddLocalPositionXY(this GameObject go, float offsetX, float offsetY)
        {
            go.transform.AddLocalPositionXY(offsetX, offsetY);
        }

        public static void AddLocalPositionX(this GameObject go, float offsetX)
        {
            go.transform.AddLocalPositionX(offsetX);
        }

        public static void AddLocalPositionY(this GameObject go, float offsetY)
        {
            go.transform.AddLocalPositionY(offsetY);
        }
    
        // MOVING POSITION TO TARGET ======================================================

        public static void SetPositionToMousePosition(this GameObject go)
        {
            go.transform.SetPositionToMousePosition();
        }

        public static void SetPositionToGameObject(this GameObject go, GameObject target)
        {
            go.transform.SetPositionToGameObject(target);
        }

        // SETTING SCALE ======================================================

        public static void SetScaleXY(this GameObject go, float scale)
        {
            go.transform.SetScaleXY(scale);
        }

        public static void SetScaleXY(this GameObject go, float scaleX, float scaleY)
        {
            go.transform.SetScaleXY(scaleX, scaleY);
        }

        public static void SetScaleX(this GameObject go, float scaleX)
        {
            go.transform.SetScaleX(scaleX);
        }

        public static void SetScaleY(this GameObject go, float scaleY)
        {
            go.transform.SetScaleY(scaleY);
        }

        public static void FlipScaleXToPositive(this GameObject go)
        {
            go.transform.FlipScaleXToPositive();
        }

        public static void FlipScaleXToNegative(this GameObject go)
        {
            go.transform.FlipScaleXToNegative();
        }

        public static void FlipScaleXHorizontally(this GameObject go)
        {
            go.transform.FlipScaleXHorizontally();
        }

        // GETTING SCALE ======================================================

        public static Vector2 GetScaleXY(this GameObject go)
        {
            return go.transform.GetScaleXY();
        }

        public static float GetScaleX(this GameObject go)
        {
            return go.transform.GetScaleX();
        }

        public static float GetScaleY(this GameObject go)
        {
            return go.transform.GetScaleY();
        }

        // SETTING & GETTING ROTATIONS ======================================================

        public static void SetRotation(this GameObject go, Vector3 newEulerAngles)
        {
            go.transform.SetRotation(newEulerAngles);
        }

        public static void SetRotationZ(this GameObject go, float newAngle)
        {
            go.transform.SetRotationZ(newAngle);
        }

        /// <summary>
        /// Rotates a GameObject clockwise.
        /// </summary>
        /// <param name="angles">
        /// Amount of angles subtracted to the current angle.
        /// Remember: Negative rotation is clockwise. Positive is counterclockwise.
        /// </param>

        public static void RotateClockwise(this GameObject go, float angles)
        {
            go.transform.RotateClockwise(angles);
        }

        /// <summary>
        /// Rotates a GameObject counterclockwise.
        /// </summary>
        /// <param name="angles">
        /// Amount of angles added to the current angle.
        /// Remember: Negative rotation is clockwise. Positive is counterclockwise.
        /// </param>

        public static void RotateCounterClockwise(this GameObject go, float angles)
        {
            go.transform.RotateCounterClockwise(angles);
        }

        public static float GetRotationZ(this GameObject go)
        {
            return go.transform.GetRotationZ();
        }

        // GETTING CHILDREN GAMEOBJECTS ======================================================

        public static GameObject GetChildByName(this GameObject go, string name)
        {
            return go.transform.GetChildByName(name);
        }

        public static GameObject GetChildByTag(this GameObject go, string tag)
        {
            return go.transform.GetChildByTag(tag);
        }

    
    // RIGIDBODY 2D ======================================================

    public static void SetVelocity(this GameObject go, float velX, float velY)
    {
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = new Vector2(velX, velY);
        }
    }

    public static void SetVelocityX(this GameObject go, float velX)
    {
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = new Vector2(velX, rb.velocity.y);
        }
    }

    public static void SetVelocityY(this GameObject go, float velY)
    {
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, velY);
        }
    }

    public static void SetGravityScale(this GameObject go, float g)
    {
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = g;
        }
    }

    // SPRITE RENDERER ======================================================

    public static void ShowSprite(this GameObject go)
    {
        SpriteRenderer rnd = go.GetComponent<SpriteRenderer>();

        if (rnd != null)
        {
            rnd.enabled = true;
        }
    }

    public static void HideSprite(this GameObject go)
    {
        SpriteRenderer rnd = go.GetComponent<SpriteRenderer>();

        if (rnd != null)
        {
            rnd.enabled = false;
        }
    }

    public static void FadeIn(this GameObject go, float time = 0.2f)
    {
        SpriteRenderer rnd = go.GetComponent<SpriteRenderer>();

        if (rnd != null)
        {
            rnd.DOFade(1, time);
        }
    }

    public static void FadeOut(this GameObject go, float time = 0.2f)
    {
        SpriteRenderer rnd = go.GetComponent<SpriteRenderer>();

        if (rnd != null)
        {
            rnd.DOFade(0, time);
        }
    }

    public static void SetSortingOrder(this GameObject go, int order)
    {
        SpriteRenderer rnd = go.GetComponent<SpriteRenderer>();

        if (rnd != null)
        {
            rnd.sortingOrder = order;
        }
    }

    public static void SetSprite(this GameObject go, Sprite spr)
    { 
        SpriteRenderer rnd = go.GetComponent<SpriteRenderer>();

        if (rnd != null)
        {
            rnd.sprite = spr;
        }
    }

    public static void SetColor(this GameObject go, Color c)
    { 
        SpriteRenderer rnd = go.GetComponent<SpriteRenderer>();

        if (rnd != null)
        {
            rnd.color = c;
        }
    }

    public static void SetColor(this GameObject go, int r, int g, int b, int a = 255)
    { 
        SpriteRenderer rnd = go.GetComponent<SpriteRenderer>();

        if (rnd != null)
        {
            rnd.color = new Color(r / 255f, g / 255f, b / 255f, a / 255f);
        }
    }

    // SPRITE ANIM / POWERTOOLS ======================================================

    public static void PlayAnim(this GameObject go, AnimationClip anim)
    { 
        SpriteAnim compAnim = go.GetComponent<SpriteAnim>();

        if (compAnim != null)
        {
            compAnim.Play(anim);
        }
    }
}