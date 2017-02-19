using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace Snowfall.Input
{
    public class Keyboard
    {
        private bool[] keyStates; //Stores states of keys
        public Keyboard(GameWindow window) //Initializes handlers and buffers
        {
            keyStates = new bool[(int)Key.LastKey];
            window.KeyDown += Window_KeyDown;
            window.KeyUp += Window_KeyUp;
        }

        private void Window_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
            if (keyStates[(int)e.Key]) KeyChanged(this, e.Key, false); 
            keyStates[(int)e.Key] = false;
        }

        private void Window_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (!keyStates[(int)e.Key]) KeyChanged(this, e.Key, true);
            keyStates[(int)e.Key] = true;
        }

        public bool IsKeyDown(Key k) //Checks if a key is down
        {
            return keyStates[(int)k];
        }

        public event EventHandler<Key, bool> KeyChanged = (o, s, t) => { }; //Senses change in a key
    }
}
