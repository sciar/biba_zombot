using BibaFramework.BibaMenu;

namespace BibaFramework.BibaGame
{
    public class IntroMediator : BaseBibaMediator
	{
        [Inject]
        public IntroView IntroView { get; set; }

        public override BaseBibaView View { get { return IntroView; } }

        public override void SetupMenu (BibaMenuState menuState)
        {
          //  throw new System.NotImplementedException ();
        }
   	}
}