using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry
{
    /// <summary>
    /// Pure eye candy at the moment.
    /// </summary>
    public class TitleMenu
    {
        public static void WriteLogo()
        {
            string logo = @"                      __      __       .__                                  __                                 
                     /  \    /  \ ____ |  |   ____  ____   _____   ____   _/  |_  ____                         
                     \   \/\/   // __ \|  | _/ ___\/  _ \ /     \_/ __ \  \   __\/  _ \                        
                      \        /\  ___/|  |_\  \__(  <_> )  Y Y  \  ___/   |  | (  <_> )                       
                       \__/\  /  \___  >____/\___  >____/|__|_|  /\___  >  |__|  \____/                        
                            \/       \/          \/            \/     \/                                       
___________    .__                         __                  ___________              __  .__                
\__    ___/___ |  |   ____   _____   _____/  |________ ___.__. \__    ___/___   _______/  |_|__| ____    ____  
  |    |_/ __ \|  | _/ __ \ /     \_/ __ \   __\_  __ <   |  |   |    |_/ __ \ /  ___/\   __\  |/    \  / ___\ 
  |    |\  ___/|  |_\  ___/|  Y Y  \  ___/|  |  |  | \/\___  |   |    |\  ___/ \___ \  |  | |  |   |  \/ /_/  >
  |____| \___  >____/\___  >__|_|  /\___  >__|  |__|   / ____|   |____| \___  >____  > |__| |__|___|  /\___  / 
             \/          \/      \/     \/             \/                   \/     \/               \//_____/  ";

            Console.WriteLine(logo);

        }
    }
}
// Thank you Michael Winter for inspiring this.
