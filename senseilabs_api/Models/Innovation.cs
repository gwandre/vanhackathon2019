using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senseilabs_api.Models
{
    public class Innovation
    {
        public double rate;

        /*
         * Initiate the class with a ZERO value
         */
        public Innovation ()
        {
            this.rate = 0;
        }

        /*
         * Initiate the class with a innovation rate passed by parameter
         */
        public Innovation(Double innovationRate)
        {
            this.rate = innovationRate;
        }

        /*
         * Initiate the class with a innovation rate calculated from a function in Helpers class
         */
        public Innovation(int numberOfInnovations, int numberOfProducts)
        {
            // Calculate innovation Rate based on a Calc Function at the Helpers class
            this.rate = Helpers.innovationRate(numberOfInnovations, numberOfProducts);
        }
    }
}
