using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonCrawler.Code.Input
{
    internal class InputProvider
    {
        private static GraphicsDevice _graphicsDevice;

        #region Keyboard

        private static Dictionary<Keys, List<Action>> _keyDownActionMap = new Dictionary<Keys, List<Action>>();
        private static Dictionary<Keys, List<Action>> _keyUpActionMap = new Dictionary<Keys, List<Action>>();

        /// <summary>
        /// Register an action to a key in a map
        /// </summary>
        /// <param name="key">The key the action should be registered to</param>
        /// <param name="action">The action to perfom on key activation</param>
        /// <param name="keyMap">The map to assign this to (i.e. key up or key down map)</param>
        private static void RegisterActionToKeyMap(Keys key, Action action, Dictionary<Keys, List<Action>> keyMap)
        {
            // If the key doesnt exist, add it
            if (!keyMap.ContainsKey(key))
            {
                keyMap.Add(key, new List<Action>());
            }

            keyMap[key].Add(action);
        }

        /// <summary>
        /// Deregister an action from a key in a map
        /// </summary>
        /// <param name="key">The key the action is registered to</param>
        /// <param name="action">The action to remove from the key</param>
        /// <param name="keyMap">The key map to remove from (i.e. key up or key down map)</param>
        private static void DeregisterActionFromKeyMap(Keys key, Action action, Dictionary<Keys, List<Action>> keyMap)
        {
            if (!keyMap.ContainsKey(key))
            {
                keyMap[key].Remove(action);
            }

            // If there are no actions left attached to this key we should remove it from the map
            if (keyMap[key].Count == 0) keyMap.Remove(key);
        }

        public static void RegisterActionToKeyDown(Keys key, Action action) => RegisterActionToKeyMap(key, action, _keyDownActionMap);
        public static void DeregisterActionFromKeyDown(Keys key, Action action) => DeregisterActionFromKeyMap(key, action, _keyDownActionMap);
        public static void RegisterActionToKeyUp(Keys key, Action action) => RegisterActionToKeyMap(key, action, _keyUpActionMap);
        public static void DeregisterActionToKeyUp(Keys key, Action action) => DeregisterActionFromKeyMap(key, action, _keyUpActionMap);

        public static bool IsKeyDown(Keys key) => Keyboard.GetState().IsKeyDown(key);

        #region Checks

        private static void CheckKeyboard()
        {
            // Key Down
            for (int i = 0; i < _keyDownActionMap.Count; i++)
            {
                Keys currKey = _keyDownActionMap.ElementAt(i).Key;

                if (Keyboard.GetState().IsKeyDown(currKey))
                {
                    List<Action> actions = _keyDownActionMap[currKey];
                    for (int j = 0; j < actions.Count; j++)
                    {
                        actions[j]?.Invoke();
                    }
                }
            }

            //Key Up
            for (int i = 0; i < _keyUpActionMap.Count; i++)
            {
                Keys currKey = _keyUpActionMap.ElementAt(i).Key;

                if (Keyboard.GetState().IsKeyUp(currKey))
                {
                    List<Action> actions = _keyUpActionMap[currKey];
                    for (int j = 0; j < actions.Count; j++)
                    {
                        actions[j]?.Invoke();
                    }
                }
            }
        }
        #endregion
        #endregion

        #region Mouse

        #region Scroll
        public delegate void MouseScrollHandler(int value);
        public static MouseScrollHandler OnMouseScroll;
        private static int _lastScrollWheelValue = 0;
        private static int _currentScrollWheelValue = 0;
        #endregion

        #region Mouse Move
        public delegate void MouseMoveHandler(Vector2 value);
        public static MouseMoveHandler OnMouseMove;
        private static Vector2 _lastMousePosition = Vector2.Zero;
        private static Vector2 _currentMousePosition = Vector2.Zero;

        public static Point MousePosition => Mouse.GetState().Position;
        public static bool IsMouseInWindow
        {
            get
            {
                if (_graphicsDevice == null)
                {
                    // Default to true if there is no graphics device provided
                    return true;
                }
                {
                    return _graphicsDevice.Viewport.Bounds.Contains(MousePosition);

                }
            }
        }
        #endregion

        #region Mouse Clicks
        public delegate void MouseClickHandler();
        public static MouseClickHandler OnMouseClickLeft;
        public static MouseClickHandler OnMouseClickRight;
        public static MouseClickHandler OnMouseClickMiddle;

        public delegate void MouseHoldHandler();
        public static MouseHoldHandler OnMouseHoldLeft;
        public static MouseHoldHandler OnMouseHoldRight;
        public static MouseHoldHandler OnMouseHoldMiddle;

        private static ButtonState _lastMouseLeftState;
        private static ButtonState _lastMouseRightState;
        private static ButtonState _lastMouseMiddleState;

        private static ButtonState _currentMouseLeftState;
        private static ButtonState _currentMouseRightState;
        private static ButtonState _currentMouseMiddleState;

        public static bool MouseLeftPressed => Mouse.GetState().LeftButton == ButtonState.Pressed;
        public static bool MouseRightPressed => Mouse.GetState().RightButton == ButtonState.Pressed;
        public static bool MouseMiddlePressed => Mouse.GetState().MiddleButton == ButtonState.Pressed;
        #endregion

        #region Checks
        private static void CheckMouseScroll()
        {
            _lastScrollWheelValue = _currentScrollWheelValue;
            _currentScrollWheelValue = Mouse.GetState().ScrollWheelValue;

            if (_currentScrollWheelValue < _lastScrollWheelValue) OnMouseScroll?.Invoke(-1);        // Scroll Down
            else if (_currentScrollWheelValue > _lastScrollWheelValue) OnMouseScroll?.Invoke(1);    // Scroll Up
        }

        private static void CheckMouseMove()
        {
            _lastMousePosition = _currentMousePosition;
            _currentMousePosition.X = Mouse.GetState().X;
            _currentMousePosition.Y = Mouse.GetState().Y;

            Vector2 difference = _currentMousePosition - _lastMousePosition;

            OnMouseMove?.Invoke(difference);
        }

        private static void CheckMouseButtons()
        {
            // Update mouse states
            _lastMouseLeftState = _currentMouseLeftState;
            _currentMouseLeftState = Mouse.GetState().LeftButton;

            _lastMouseRightState = _currentMouseRightState;
            _currentMouseRightState = Mouse.GetState().RightButton;

            _lastMouseMiddleState = _currentMouseMiddleState;
            _currentMouseMiddleState = Mouse.GetState().MiddleButton;

            // Holds
            if (_currentMouseLeftState == ButtonState.Pressed) OnMouseHoldLeft?.Invoke();
            if (_currentMouseRightState == ButtonState.Pressed) OnMouseHoldRight?.Invoke();
            if (_currentMouseMiddleState == ButtonState.Pressed) OnMouseHoldMiddle?.Invoke();

            // Clicks
            if (_currentMouseLeftState == ButtonState.Pressed && _lastMouseLeftState == ButtonState.Released) OnMouseClickLeft?.Invoke();
            if (_currentMouseRightState == ButtonState.Pressed && _lastMouseRightState == ButtonState.Released) OnMouseClickRight?.Invoke();
            if (_currentMouseMiddleState == ButtonState.Pressed && _lastMouseMiddleState == ButtonState.Released) OnMouseClickMiddle?.Invoke();
        }
        #endregion

        #endregion

        public static void CheckInputs()
        {
            CheckMouseScroll();
            CheckMouseMove();
            CheckMouseButtons();
            CheckKeyboard();
        }
    }
}
