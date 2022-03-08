using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testProject
{
    /// <summary>
    /// This is the Admin Class 
    /// Admins Manage the Library and They are the Responsible of users
    /// </summary>
    public class Admin
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Family { get; private set; }


        /// <summary>
        /// This is the Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="family"></param>
        public Admin(string name, string family)
        {
            this.Name = name;
            this.Family = family;
        }

        /// <summary>
        /// This Method Delete Persons
        /// </summary>
        /// <param name="id"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static Person Delete(int id ,string names)
        {
            return Person.Create("", "");
        }
    }
}
