using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class UserInput
    {
        public DateTime Date {  get; set; } // User inputted date and time combined
        public TimeSpan Time { get; set; } // User inputted time

        public UserInput(DateTime date, TimeSpan time)
        {
            Date = date;
            Time = time;
        }
    }
}
