using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTSProject.Resources.Models
{
    public class UserInput
    {
        public DateTime Date {  get; set; }
        public TimeSpan Time { get; set; }

        public UserInput(DateTime date, TimeSpan time)
        {
            Date = date;
            Time = time;
        }
    }
}
