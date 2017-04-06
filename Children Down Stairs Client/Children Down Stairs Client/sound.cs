using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
namespace sound
{
    class music
    {
        private SoundPlayer hurt_sp;
        private SoundPlayer death_sp;
        private SoundPlayer spring_sp;

        public music() {
            hurt_sp = new SoundPlayer("hurt.wav");
            death_sp = new SoundPlayer("An enemy has been slain.wav");
            spring_sp = new SoundPlayer("spring.wav");
        }
        public void playHurt() {
            hurt_sp.Play();
        }
        public void playDeath() {
            death_sp.Play();
        }
        public void playSpring()
        {
            spring_sp.Play();
        }

    }
}
