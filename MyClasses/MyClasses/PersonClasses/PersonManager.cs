using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses.PersonClasses
{
    public class PersonManager
    {

        public Person CreatePerson(string first, string last, bool isSupervisor)
        {
            Person person = null;

            if (!string.IsNullOrEmpty(first))
            {
                if (isSupervisor)
                {
                    person = new Supervisor();
                }
                else
                {
                    person = new Employee();
                }


                //assign variables
                person.FirstName = first;
                person.LastName = last;

            }

            return person;
        }


    }
}
