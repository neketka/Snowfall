using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace Snowfall.Input
{
    public delegate void EventHandler<T, T1>(object sender, T arg1, T1 arg2);
    public class Mouse
    {
        private bool[] buttonStates; //Stores mouse button states
        public Mouse(GameWindow window) //Initializes handlers and buffers
        {
            buttonStates = new bool[(int)MouseButton.LastButton];
            window.MouseDown += Window_MouseDown;
            window.MouseUp += Window_MouseUp;
            window.MouseWheel += Window_MouseWheel;
            window.MouseMove += Window_MouseMove;
        }

        private void Window_MouseMove(object sender, MouseMoveEventArgs e)
        {
            MouseX = e.X; //Set the mouse position
            MouseY = e.Y;
            MouseMoved(this, e.XDelta, e.YDelta); //Mouse motion
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollChanged(this, e.Delta); //Scroll motion
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (buttonStates[(int)e.Button]) ButtonChanged(this, e.Button, false);
            buttonStates[(int)e.Button] = false;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!buttonStates[(int)e.Button]) ButtonChanged(this, e.Button, true);
            buttonStates[(int)e.Button] = true;
        }

        public bool IsButtonDown(MouseButton button) //Checks if mouse button is down
        {
            return buttonStates[(int)button];
        }

        public void SetMousePos(int x, int y)
        {
            System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y); //Change mouse position
        }

        public int MouseX { get; private set; }
        public int MouseY { get; private set; }

        public event EventHandler<MouseButton, bool> ButtonChanged = (o, s, t) => { }; //Called on mouse button change
        public event EventHandler<int, int> MouseMoved = (o, s, t) => { }; //Called on mouse motion
        public event EventHandler<int> ScrollChanged = (o, s) => {}; //Called on scroll wheel change
    }
}
