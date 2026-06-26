using System;
using System.Media;

namespace POE_Check
{
    public class voice_greeting
    {

        //void method to play the sound named greet
        public void greet()
        { //star of greet method

            //replace the \bin\Debug\ from the path with greeting.wav

            string auto_path = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", @"\POE_voice.wav");

            //create an instance for the soundPlayer class
            SoundPlayer greetMe = new SoundPlayer(auto_path);
            //then greet
            greetMe.Play();


        }//end of greet method

    }
}